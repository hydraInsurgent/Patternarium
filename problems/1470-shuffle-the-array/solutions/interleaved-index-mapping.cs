// Approach: Interleaved Index Mapping
// Time:  O(n)
// Space: O(n)
// Key Idea: ans[2*i] takes nums[i] (the x's), ans[2*i+1] takes nums[n+i] (the y's). Arithmetic does the interleave.

// 0  1  2  3  4  5  6  7
// x1 x2 x3 x4 y1 y2 y3 y4
// x1 y1 x2 y2 x3 y3 x4 y4 -- even/odd positions

public class Solution
{
    public int[] Shuffle(int[] nums, int n)
    {
        int[] ans = new int[2 * n];

        // Each iteration places one x and one y at their interleaved positions
        for (int i = 0; i < n; i++)
        {
            ans[2 * i] = nums[i];          // even slot -> x_i
            ans[2 * i + 1] = nums[n + i];  // odd slot  -> y_i
        }

        return ans;
    }
}
