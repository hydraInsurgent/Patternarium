// Missing Number - XOR Cancellation
// Time: O(n) | Space: O(1)

public class Solution
{
    public int MissingNumber(int[] nums)
    {
        int n = nums.Length;
        int result = 0;

        // XOR all actual values in the array into result
        for (int i = 0; i < nums.Length; i++)
            result ^= nums[i];

        // XOR all expected values [0..n] into result
        // Values that appeared in nums cancel out (n XOR n = 0)
        // The missing number has no pair - it survives as the final result
        for (int i = 0; i <= n; i++)
            result ^= i;

        return result;
    }
}
