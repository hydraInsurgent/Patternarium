// Approach: Sort + Linear Scan
// Time:  O(n log n)
// Space: O(1)
// Key Idea: Sort to bring consecutive numbers adjacent, then scan once tracking run length. Skip duplicates without resetting count.

public class Solution1
{
    public int LongestConsecutive(int[] nums)
    {
        // Guard: empty array has no sequence
        if (nums.Length == 0)
        {
            return 0;
        }

        // Sort brings consecutive numbers next to each other
        Array.Sort(nums);

        int maxCount = 1;
        int count = 1; // single element is already a sequence of length 1
        int last = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            int current = nums[i];

            if (current == last + 1)
            {
                // Consecutive - extend the run
                count++;
            }
            else if (current == last)
            {
                // Duplicate - skip without resetting the run
            }
            else
            {
                // Gap - start a new run
                count = 1;
            }

            if (count > maxCount)
            {
                maxCount = count;
            }

            last = current;
        }

        return maxCount;
    }
}

// Why count starts at 1?
// - A single element is already a sequence of length 1. Starting at 0 undercounts
//   by one. Caught when single-element input returned 0 instead of 1.

// Why handle current == last separately?
// - Without the duplicate check, a repeated number (e.g. [1,1,2]) falls through to
//   the else branch and resets count to 1, breaking the sequence. The duplicate must
//   be skipped silently without resetting or incrementing.
