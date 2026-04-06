public class Runner
{
    public static void Run()
    {
        DryRunLogger.Log("=== Dry Run: Missing Number - Boolean Flag Array ===");
        DryRunLogger.Log("Each step shows what the code decided and why.");
        DryRunLogger.Log("Press any key to advance.");
        DryRunLogger.Log();

        int[] nums = InputParser.ReadIntArray("Enter nums (e.g. 3 0 1): ");

        // --- Algorithm (verbatim copy with telemetry) ---
        bool[] flags = new bool[nums.Length + 1];

        DryRunLogger.Log($"[ENTER] nums.Length={nums.Length}, flags size={flags.Length} (indices 0..{nums.Length})");
        DryRunLogger.Pause();

        for (int i = 0; i < nums.Length; i++)
        {
            flags[i] = true;
            DryRunLogger.Log($"[UPDATE] i={i}, nums[i]={nums[i]} -> set flags[{i}]=true");
            DryRunLogger.Pause();
        }

        for (int i = 0; i < flags.Length; i++)
        {
            DryRunLogger.Log($"[CHECK] flags[{i}]={flags[i]}");
            if (flags[i] == false)
            {
                DryRunLogger.Log($"[MATCH] flags[{i}] is false -> missing number candidate");
                DryRunLogger.Pause();
                int result = i;
                DryRunLogger.Log();
                DryRunLogger.Log($"[RETURN] {result}");
                DryRunLogger.Save();
                return;
            }
            DryRunLogger.Log($"[SKIP] flags[{i}] is true, moving on");
            DryRunLogger.Pause();
        }

        DryRunLogger.Log("[RETURN] 0 (fallback)");
        DryRunLogger.Save();
    }
}
