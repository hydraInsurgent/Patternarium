// Approach: Brute Force
// Time:  O(n^2) - nested loops check every pair
// Space: O(1) - no extra data structures
// Key Idea: For every element, check all other elements to find the complement

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        int n = nums.Length;

        for (int i = 0; i < n; i++)
        {
            // Start at i+1 to avoid using the same element twice
            for (int j = i + 1; j < n; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    return new int[] { i, j };
                }
            }
        }

        return new int[0]; // guaranteed not to reach here
    }
}

// Why j = i + 1 and not j = 0?
// - Problem says we cannot use the same element twice
// - j = 0 would eventually check nums[i] + nums[i] which is illegal
// - j = i + 1 also avoids duplicate pairs like (2+7) and (7+2)
