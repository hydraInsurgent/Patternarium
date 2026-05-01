// Approach: Running Count with Reset on Break
// Time:  O(n)
// Space: O(1)
// Key Idea: Track current streak; on 0, settle max and reset. Settle once more after the loop to catch a tail-streak.

public class Solution
{
    public int FindMaxConsecutiveOnes(int[] nums)
    {
        int maxCount = 0;
        int count = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0)
            {
                // Streak ended - settle into the running max and reset
                maxCount = Math.Max(maxCount, count);
                count = 0;
            }
            else
            {
                count++;
            }
        }

        // Catch a streak that runs to the end of the array
        maxCount = Math.Max(maxCount, count);
        return maxCount;
    }
}
