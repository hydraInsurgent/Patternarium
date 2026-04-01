// Approach: HashSet - Sequence Start Detection
// Time:  O(n)
// Space: O(n)
// Key Idea: Load all numbers into a HashSet. A number is a sequence start only if n-1 is not in the set. Count forward from each start using O(1) lookups.

public class Solution2
{
    public int LongestConsecutive(int[] nums)
    {
        // Load all numbers for O(1) existence checks. Duplicates are deduplicated automatically.
        HashSet<int> set = new HashSet<int>(nums);

        int maxCount = 0; // 0 handles empty array correctly

        for (int i = 0; i < nums.Length; i++)
        {
            int current = nums[i];

            // Only start counting from sequence starts - skip numbers that have a predecessor
            if (!set.Contains(current - 1))
            {
                int count = 0;

                // Extend the sequence forward as far as it goes
                while (set.Contains(current))
                {
                    current++;
                    count++;
                }

                if (count > maxCount)
                {
                    maxCount = count;
                }
            }
        }

        return maxCount;
    }
}

// Why maxCount starts at 0 (not 1)?
// - Unlike Approach 1, count is computed fresh per sequence start inside the while loop.
//   An empty array never enters the loop, so 0 is the correct base. Starting at 1 would
//   incorrectly return 1 for an empty array.

// Why check !set.Contains(current - 1) before counting?
// - Without this gate, every number in the sequence would trigger its own count, making
//   the algorithm O(n^2) in the worst case. The gate ensures each sequence is counted
//   exactly once, from its true start. This is what keeps the solution O(n).
