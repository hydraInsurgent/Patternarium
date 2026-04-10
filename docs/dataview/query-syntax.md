# Dataview Extension - Query Syntax and Capabilities

**Extension:** `yahsan2.vscode-dataview-preview`
**Source read:** 2026-04-10
**Local copy:** `D:\Personal\Code\Depth Projects\VS Code Extensions\markdown-tooling\vscode-dataview-preview`

---

## What It Is

A VS Code extension that renders Dataview Query Language (DQL) blocks in markdown preview. Uses an SQL-like syntax to query workspace markdown files and render results as tables or lists.

---

## Query Syntax

Queries are written in fenced code blocks with the `dataview` language tag:

````
```dataview
TABLE field1, field2 AS "Label"
FROM "folder/path"
WHERE status = "solved"
SORT file.name asc
LIMIT 10
```
````

### Keywords

| Keyword | Purpose | Notes |
|---------|---------|-------|
| `TABLE` | Render results as a table | Columns are field names or expressions |
| `LIST` | Render results as a bulleted list | Uses first field as display text |
| `FROM` | Filter by folder path or tag | One source only - no multi-source |
| `WHERE` | Filter rows by condition | Basic comparisons only |
| `SORT` | Order results | `asc` / `desc` |
| `LIMIT` | Cap row count | Integer |
| `FLATTEN` | Expand an array field into rows | One row per array item |
| `AS` | Rename a column | Works in TABLE and FLATTEN |

### Field access

- Frontmatter fields are accessed by name directly: `status`, `difficulty`, `patterns`
- Built-in file fields use `file.*` prefix: `file.name`, `file.path`, `file.day`
- Dot notation is supported for `file.*` fields; custom nested object access is untested

### WHERE operators

`=`, `!=`, `>`, `<`, `>=`, `<=`

No AND/OR chaining confirmed. No regex. No CONTAINS or IN operator documented.

---

## What It Can Do

### Read YAML frontmatter - YES

Full support. Uses `js-yaml` to parse YAML between `---` delimiters. All frontmatter fields are queryable directly.

```yaml
---
status: solved
difficulty: Easy
patterns: [HashMap, Two Pointers]
ds-used: [array, hashmap]
---
```

```dataview
TABLE difficulty, patterns
FROM "problems"
WHERE status = "solved"
```

### Query arrays via FLATTEN - YES

`FLATTEN` expands an array field into one row per element. This is the mechanism for "show all problems that use pattern X."

```dataview
TABLE file.name, pattern
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "HashMap"
```

### Output as table or list - YES

Both `TABLE` and `LIST` are implemented and render as HTML. `TASK` is parsed but not fully implemented.

### Read JSON files - NO

The indexer scans only `**/*.md` files. No JSON support. `master-index.json` cannot be read by Dataview queries.

### Aggregation (SUM, COUNT, AVG) - NO

No aggregate functions. Cannot count problems, sum progress fields, or compute percentages. GROUP BY is not supported.

### DataviewJS (JavaScript queries) - NO

`dataviewjs` blocks are recognized but deliberately blocked with a warning:
> "DataviewJS (eval) is restricted by VSCode security policies. Please use 'dataview' (DQL) blocks instead."

### Nested YAML object access - UNTESTED

Parsed and stored during indexing, but querying nested objects (e.g., `approaches.variation`) via dot notation is not confirmed to work. Do not rely on it.

### Multi-source FROM - NO

`FROM` accepts one folder path or one tag. Cannot combine sources in a single query.

---

## Confirmed Working Queries (for this project)

### Pattern "Solved Problems" replacement

```dataview
TABLE file.name AS "Problem", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "HashMap"
SORT file.name asc
```

### DS "Seen In" replacement

```dataview
TABLE file.name AS "Problem"
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "hashmap"
SORT file.name asc
```

### Construct "Seen In" replacement

```dataview
TABLE file.name AS "Problem"
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "dictionary"
SORT file.name asc
```

### Concept "Seen In" replacement

```dataview
TABLE file.name AS "Problem"
FROM "problems"
FLATTEN tags AS tag
WHERE tag = "anagram"
SORT file.name asc
```

---

## What Cannot Be Automated

| Desired view | Reason it cannot be a query |
|--------------|------------------------------|
| Goals coverage % | Requires aggregation (COUNT rows, compute %) - not supported |
| DS frontmatter `progress` field rollup | Same - no SUM/COUNT |
| Variation-level DS queries | Nested YAML access untested; approach data in master-index.json is not readable |

---

## Architectural Implications for the Refactor

1. **All Dataview queries use problem frontmatter as source.** The `master-index.json` fallback described in the refactor plan is now the primary strategy - not a fallback. Dataview never reads JSON.

2. **master-index.json is AI-only.** Its value is for AI fast lookups (reverse indexes, approach-level data), not for rendering. This is unchanged from the plan.

3. **Goals coverage % stays Layer 2.** The plan anticipated this as a possible outcome. The `progress` field in DS frontmatter is still worth maintaining (AI updates it after each save), but `goals.md` cannot query and aggregate it. Goals coverage stays as a manually updated table.

4. **Track B scope is confirmed.** The four Seen In replacements and Pattern Solved Problems replacement are all achievable. Goals coverage is the only Layer 3 item that cannot be automated.

5. **Frontmatter arrays are the key dependency.** Every query above depends on `patterns`, `ds-used`, `constructs`, and `tags` being present as arrays in problem frontmatter. Phase 4 (problem frontmatter migration) is what makes all Track B queries work.

---

## Limitations to Keep in Mind

- No pagination - all results load into memory on each render
- Full workspace rescan on file save - performance scales with problem count
- GitHub renders the raw query syntax, not results - acceptable since work is done in VS Code
- `TASK` output type is not functional - do not use
- Folder matching in `FROM` is path-inclusion, not strict - `FROM "problems"` matches any path containing "problems"
