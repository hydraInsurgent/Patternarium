// Approach: Presence Array (small range only, HashSet fallback for large range)
// Time:  O(n + r) for small range, O(n) for large range, where r = max - min + 1
// Space: O(r) for small range, O(n) for large range
// Key Idea: For small ranges, map values to a boolean array using (num - min) as offset.
//           Consecutive true values = consecutive numbers. Falls back to HashSet for large ranges.

public class Solution3
{
    public int LongestConsecutive(int[] nums)
    {
        if (nums.Length == 0)
        {
            return 0;
        }

        int max = nums.Max();
        int min = nums.Min();
        int size = max - min + 1;

        // Choose approach based on range size to avoid allocating billions of slots
        return size < 1_000_000 ? SmallRange(size, nums, min) : LargeRange(nums);
    }

    private int SmallRange(int size, int[] nums, int min)
    {
        // Mark which values exist using index offset (num - min)
        bool[] present = new bool[size];
        for (int i = 0; i < nums.Length; i++)
        {
            present[nums[i] - min] = true;
        }

        // Scan for longest consecutive run of true slots
        int maxCount = 1;
        int count = 1;

        for (int i = 1; i < present.Length; i++)
        {
            if (present[i])
            {
                // This index has a number - extend the run
                count++;
            }
            else
            {
                // Gap - reset to 0 so the next true starts fresh at 1
                count = 0;
            }

            if (count > maxCount)
            {
                maxCount = count;
            }
        }

        return maxCount;
    }

    private int LargeRange(int[] nums)
    {
        // Standard HashSet approach when range is too large for a presence array
        HashSet<int> set = new HashSet<int>(nums);
        int maxCount = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            int current = nums[i];

            if (!set.Contains(current - 1))
            {
                int count = 0;
                while (set.Contains(current))
                {
                    current++;
                    count++;
                }
                if (count > maxCount) maxCount = count;
            }
        }

        return maxCount;
    }
}

// Why check if(present[i]) instead of if(present[i] == last)?
// - The boolean array is already ordered by construction - index 0 = min, index 1 = min+1.
//   Consecutive indices are consecutive numbers. There is no need to compare neighbors.
//   Checking current == last caught false==false as "consecutive", giving wrong counts.

// Why reset count to 0 (not 1) on a gap?
// - After a gap, the next true slot does count++, making it 1. If reset to 1, the first
//   element after a gap counts as 2. Caught via [1, 100] returning 2 instead of 1.

// Why 1,000,000 as the threshold?
// - Range can span up to 2 billion (constraints: -10^9 to 10^9). Allocating a 2-billion
//   slot array is not viable. The threshold limits presence array to manageable sizes.
