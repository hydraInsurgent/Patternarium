// Approach: Single-Pass Dual Write
// Time:  O(n)
// Space: O(n)
// Key Idea: One pass; each input index writes to two output positions (i and n+i).

public class Solution
{
    public int[] GetConcatenation(int[] nums)
    {
        int n = nums.Length;
        int[] output = new int[2 * n];

        // Each input index writes to two output positions: i and n+i
        for (int i = 0; i < n; i++)
        {
            output[i] = nums[i];
            output[n + i] = nums[i];
        }

        return output;
    }
}
