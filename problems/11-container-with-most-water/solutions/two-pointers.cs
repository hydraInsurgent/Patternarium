// Approach: Two Pointers - Opposite Ends
// Time:  O(n)
// Space: O(1)
// Key Idea: Start at opposite ends for maximum width; always move the shorter pointer - it is the only one that can improve the area

public class Solution
{
    public int MaxArea(int[] height)
    {
        int maxArea = 0;
        int left = 0;
        int right = height.Length - 1;

        while (left < right)
        {
            // area is bounded by the shorter line
            int currentArea = Math.Min(height[left], height[right]) * (right - left);
            maxArea = Math.Max(maxArea, currentArea);

            // move the shorter pointer - it is the only move with any upside
            if (height[right] > height[left])
                left++;
            else
                right--;
        }

        return maxArea;
    }
}

// Why move the shorter pointer?
// Moving the taller pointer guarantees no improvement: height is still capped by the shorter
// side, and width shrinks. The shorter pointer is the only one that might find a taller line
// and overcome the width loss.
