// AI-generated dry run
// Longest Substring Without Repeating Characters - Approach 2: Sliding Window

public static class Runner
{
    public static void Run()
    {
        string s = InputParser.ReadString("Enter string: ");

        DryRunLogger.Log();
        DryRunLogger.Log("Longest Substring Without Repeating Characters - Sliding Window");
        DryRunLogger.Log($"Input: \"{s}\"");
        DryRunLogger.Log();
        DryRunLogger.Log($"{"i",-4} {"s[i]",-6} {"start",-6} {"end",-5} {"set",-20} {"maxLen",-8} {"action"}");
        DryRunLogger.Log(new string('-', 68));

        if (s.Length == 0) { DryRunLogger.Log("Empty -> return 0"); DryRunLogger.Save(); return; }
        else if (s.Length == 1) { DryRunLogger.Log("Single char -> return 1"); DryRunLogger.Save(); return; }

        int start = 0;
        int end = 0;
        int maxLength = 0;
        HashSet<char> set = new HashSet<char>();

        for (int i = 0; i < s.Length; i++)
        {
            string action;
            if (!set.Contains(s[i]))
            {
                set.Add(s[i]);
                action = "add";
            }
            else
            {
                maxLength = end - start + 1 > maxLength ? end - start + 1 : maxLength;
                start++;
                action = $"collision -> start={start}";
            }
            end = i;

            string setStr = "{" + string.Join(",", set) + "}";
            DryRunLogger.Log($"{i,-4} {s[i],-6} {start,-6} {end,-5} {setStr,-20} {maxLength,-8} {action}");
            DryRunLogger.Pause();
        }

        DryRunLogger.Log();
        DryRunLogger.Log($"Result: {maxLength}");
        DryRunLogger.Save();
    }
}
