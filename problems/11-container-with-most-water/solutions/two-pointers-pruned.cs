// Approach: Two Pointers - Constraint-Ceiling Pruning
// Time:  O(n)
// Space: O(1)
// Key Idea: Same as two pointers but exit early when remaining width * max possible height cannot beat current best

public class Solution
{
    public int MaxArea(int[] height)
    {
        int maxArea = 0;
        int left = 0;
        int right = height.Length - 1;

        // exit early when the theoretical ceiling cannot beat the current best
        while (left < right && (right - left) * 10_000 > maxArea)
        {
            int currentArea = Math.Min(height[left], height[right]) * (right - left);
            maxArea = Math.Max(maxArea, currentArea);

            if (height[right] > height[left])
                left++;
            else
                right--;
        }

        return maxArea;
    }
}

// Why 10_000?
// The constraint states 0 <= height[i] <= 10^4. This is the maximum any line can ever be.
// So (right - left) * 10_000 is the theoretical maximum area achievable with the current
// width - if it cannot beat maxArea, no remaining iteration can improve the answer.
