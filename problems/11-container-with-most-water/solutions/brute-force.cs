// Approach: Brute Force - Two Nested Loops
// Time:  O(n^2)
// Space: O(1)
// Key Idea: Check every pair of lines and track the maximum area = min(height[i], height[j]) * (j - i)

public class Solution
{
    public int MaxArea(int[] height)
    {
        int maxArea = 0;

        for (int i = 0; i < height.Length - 1; i++)
        {
            for (int j = i + 1; j < height.Length; j++)
            {
                // area is bounded by the shorter line times the distance between them
                int area = Math.Min(height[i], height[j]) * (j - i);
                maxArea = Math.Max(maxArea, area);
            }
        }

        return maxArea;
    }
}
