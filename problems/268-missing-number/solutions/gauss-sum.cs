// Missing Number - Gauss Sum
// Time: O(n) | Space: O(1)

public class Solution
{
    public int MissingNumber(int[] nums)
    {
        int n = nums.Length;

        // The sum of all integers from 0 to n - Gauss formula
        int expectedSum = n * (n + 1) / 2;

        // Sum of what we actually have in the array
        int actualSum = 0;
        for (int i = 0; i < nums.Length; i++)
            actualSum += nums[i];

        // The gap between what should be there and what is there is the missing number
        return expectedSum - actualSum;
    }
}
