#:package YamlDotNet@16.3.0

// Rebuild-Index.cs
// Regenerates master-index.json from YAML frontmatter in problem.md and solutions.md.
// Usage: dotnet run scripts/Rebuild-Index.cs
//        dotnet run scripts/Rebuild-Index.cs -- --verbose

using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;
using YamlDotNet.Serialization;

// ---------------------------------------------------------------------------
// Locate repo root
// ---------------------------------------------------------------------------

var repoDir = new DirectoryInfo(Directory.GetCurrentDirectory());
while (repoDir != null && !File.Exists(Path.Combine(repoDir.FullName, "master-index.json")))
    repoDir = repoDir.Parent;

if (repoDir == null)
{
    Console.Error.WriteLine("Error: cannot find repo root (no master-index.json). Run from inside the repo.");
    return 1;
}

var repoRoot   = repoDir.FullName;
var indexPath  = Path.Combine(repoRoot, "master-index.json");
var problemsRoot = Path.Combine(repoRoot, "problems");
bool verbose   = args.Contains("--verbose") || args.Contains("-v");

// ---------------------------------------------------------------------------
// Helpers
// ---------------------------------------------------------------------------

var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();

string ExtractFrontmatter(string path)
{
    if (!File.Exists(path)) return "";
    var lines = File.ReadAllLines(path);
    if (lines.Length == 0 || lines[0].Trim() != "---") return "";
    int end = Array.FindIndex(lines, 1, l => l.Trim() == "---");
    return end < 0 ? "" : string.Join("\n", lines[1..end]);
}

Dictionary<string, object?> ParseFrontmatter(string path)
{
    var yaml = ExtractFrontmatter(path);
    if (string.IsNullOrWhiteSpace(yaml)) return [];
    return deserializer.Deserialize<Dictionary<string, object?>>(yaml) ?? [];
}

// Converts any YAML value (scalar, sequence, null) to a string array.
string[] ToArr(object? val)
{
    if (val is null) return [];
    if (val is string s) return s.Length > 0 ? [s] : [];
    if (val is IEnumerable seq)
        return [..seq.Cast<object?>().Select(x => x?.ToString() ?? "").Where(s => s.Length > 0)];
    return [];
}

// Looks up a key in an IDictionary whose keys may be string or object.
object? DictGet(IDictionary dict, string key)
{
    if (dict.Contains(key)) return dict[key];
    foreach (var k in dict.Keys) if (k?.ToString() == key) return dict[k];
    return null;
}

JsonArray ToJsonArray(string[] items)
    => new([..items.Select(s => (JsonNode?)JsonValue.Create(s))]);

// ---------------------------------------------------------------------------
// Main build
// ---------------------------------------------------------------------------

var problems     = new JsonObject();
var byPattern   = new Dictionary<string, SortedSet<int>>();
var byDs        = new Dictionary<string, SortedSet<int>>();
var byConstruct = new Dictionary<string, SortedSet<int>>();
var byAlgorithm = new Dictionary<string, SortedSet<int>>();
var byTechnique = new Dictionary<string, SortedSet<int>>();
var byConcept   = new Dictionary<string, SortedSet<int>>();

void AddToIndex(Dictionary<string, SortedSet<int>> index, string key, int num)
{
    if (!index.TryGetValue(key, out var set)) index[key] = set = [];
    set.Add(num);
}

foreach (var problemDir in Directory.GetDirectories(problemsRoot).Order())
{
    var pFront = ParseFrontmatter(Path.Combine(problemDir, "problem.md"));
    var sFront = ParseFrontmatter(Path.Combine(problemDir, "solutions.md"));

    if (!pFront.TryGetValue("number", out var numObj) || numObj is null) continue;
    if (!int.TryParse(numObj.ToString(), out int number) || number == 0) continue;

    var title      = pFront.GetValueOrDefault("title")?.ToString() ?? "";
    var slug       = pFront.GetValueOrDefault("slug")?.ToString() ?? "";
    var difficulty = pFront.GetValueOrDefault("difficulty")?.ToString() ?? "";
    var lists      = ToArr(pFront.GetValueOrDefault("lists"));

    var patterns   = ToArr(sFront.GetValueOrDefault("patterns"));
    var constructs = ToArr(sFront.GetValueOrDefault("constructs"));
    var dsUsed     = ToArr(sFront.GetValueOrDefault("ds-used"));
    var algorithms = ToArr(sFront.GetValueOrDefault("algorithms"));
    var techniques = ToArr(sFront.GetValueOrDefault("techniques"));
    var concepts   = ToArr(sFront.GetValueOrDefault("concepts"));

    if (verbose)
        Console.WriteLine($"  #{number} {title} - {patterns.Length} patterns, approaches: {(sFront.ContainsKey("approaches") ? "yes" : "none")}");

    // Build approaches block and aggregate ds-notes from all approach entries
    var approachesObj = new JsonObject();
    var dsNotesAgg   = new Dictionary<string, string>();

    if (sFront.TryGetValue("approaches", out var rawAp) && rawAp is IEnumerable apSeq)
    {
        foreach (var apObj in apSeq.Cast<object?>().OfType<IDictionary>())
        {
            var file = DictGet(apObj, "file")?.ToString() ?? "";
            if (string.IsNullOrEmpty(file)) continue;
            var fileName = Path.GetFileName(file);

            var apPatterns   = ToArr(DictGet(apObj, "patterns"));
            var apDs         = ToArr(DictGet(apObj, "ds-used"));
            var apTechniques = ToArr(DictGet(apObj, "techniques"));
            var variation    = DictGet(apObj, "variation")?.ToString() ?? "";

            var apEntry = new JsonObject();
            if (apPatterns.Length > 0)   apEntry["patterns"]   = ToJsonArray(apPatterns);
            if (!string.IsNullOrEmpty(variation)) apEntry["variation"] = variation;
            if (apDs.Length > 0)         apEntry["ds-used"]    = ToJsonArray(apDs);
            if (apTechniques.Length > 0) apEntry["techniques"] = ToJsonArray(apTechniques);

            // Aggregate ds-notes (first note per DS wins)
            if (DictGet(apObj, "ds-notes") is IDictionary notes)
                foreach (DictionaryEntry kv in notes)
                {
                    var k = kv.Key?.ToString() ?? "";
                    if (k.Length > 0 && !dsNotesAgg.ContainsKey(k))
                        dsNotesAgg[k] = kv.Value?.ToString() ?? "";
                }

            approachesObj[fileName] = apEntry;
        }
    }

    // Assemble problem entry
    var entry = new JsonObject
    {
        ["title"]      = title,
        ["slug"]       = slug,
        ["difficulty"] = difficulty,
        ["patterns"]   = ToJsonArray(patterns),
        ["constructs"] = ToJsonArray(constructs),
        ["algorithms"] = ToJsonArray(algorithms),
        ["ds-used"]    = ToJsonArray(dsUsed),
        ["techniques"] = ToJsonArray(techniques),
        ["concepts"]   = ToJsonArray(concepts),
        ["lists"]      = ToJsonArray(lists),
    };

    if (dsNotesAgg.Count > 0)
    {
        var dsNotesNode = new JsonObject();
        foreach (var kv in dsNotesAgg) dsNotesNode[kv.Key] = kv.Value;
        entry["ds-notes"] = dsNotesNode;
    }
    if (approachesObj.Count > 0) entry["approaches"] = approachesObj;

    problems[number.ToString()] = entry;

    foreach (var p  in patterns)   AddToIndex(byPattern,   p,  number);
    foreach (var ds in dsUsed)     AddToIndex(byDs,        ds, number);
    foreach (var c  in constructs) AddToIndex(byConstruct, c,  number);
    foreach (var a  in algorithms) AddToIndex(byAlgorithm, a,  number);
    foreach (var t  in techniques) AddToIndex(byTechnique, t,  number);
    foreach (var c  in concepts)   AddToIndex(byConcept,   c,  number);
}

// ---------------------------------------------------------------------------
// Assemble and write
// ---------------------------------------------------------------------------

JsonObject BuildReverseIndex(Dictionary<string, SortedSet<int>> index)
{
    var obj = new JsonObject();
    foreach (var kv in index)
        obj[kv.Key] = new JsonArray([..kv.Value.Select(n => (JsonNode?)JsonValue.Create(n.ToString()))]);
    return obj;
}

var root = new JsonObject
{
    ["_saving"]      = JsonValue.Create<string?>(null),
    ["problems"]     = problems,
    ["by-pattern"]   = BuildReverseIndex(byPattern),
    ["by-ds"]        = BuildReverseIndex(byDs),
    ["by-construct"] = BuildReverseIndex(byConstruct),
    ["by-algorithm"] = BuildReverseIndex(byAlgorithm),
    ["by-technique"] = BuildReverseIndex(byTechnique),
    ["by-concept"]   = BuildReverseIndex(byConcept),
};

File.WriteAllText(indexPath, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

Console.WriteLine($"master-index.json rebuilt: {problems.Count} problems, {byPattern.Count} patterns, {byDs.Count} DS, {byConstruct.Count} constructs, {byTechnique.Count} techniques, {byConcept.Count} concepts");
return 0;
