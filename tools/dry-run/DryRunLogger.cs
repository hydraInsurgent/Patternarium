using System.Text;

public static class DryRunLogger
{
    private static readonly StringBuilder _log = new();

    public static void Log(string line = "")
    {
        Console.WriteLine(line);
        _log.AppendLine(line);
    }

    public static void Pause()
    {
        Console.Write("  [any key] ");
        Console.ReadKey(true);
        Console.WriteLine();
    }

    public static void Save()
    {
        string path = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), "../../active-problem.md"));

        if (!File.Exists(path))
        {
            Console.WriteLine("\n[DryRunLogger] Could not save - active-problem.md not found.");
            return;
        }

        string existing = File.ReadAllText(path);
        string section = $"#### Dry Run\n\n```\n{_log.ToString().TrimEnd()}\n```\n";

        int lastApproach = existing.LastIndexOf("\n### Approach");
        if (lastApproach == -1) lastApproach = 0;

        int dryRunStart = existing.IndexOf("\n#### Dry Run", lastApproach);

        string updated;
        if (dryRunStart != -1)
        {
            // Replace existing dry run section
            int nextHeading = existing.IndexOf("\n####", dryRunStart + 13);
            int sectionEnd = nextHeading != -1 ? nextHeading : existing.Length;
            updated = existing[..(dryRunStart + 1)] + section
                    + existing[sectionEnd..].TrimStart('\n');
        }
        else
        {
            updated = existing.TrimEnd() + "\n\n" + section;
        }

        File.WriteAllText(path, updated);
        _log.Clear();
        Console.WriteLine("\n[Dry run saved to active-problem.md]");
    }
}
