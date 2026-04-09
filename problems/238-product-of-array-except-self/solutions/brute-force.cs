// Approach: Brute Force - Nested Loops
// Time:  O(n^2)
// Space: O(n)
// Key Idea: For each index, multiply every other element by skipping the current index.

public class Solution1
{
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] output = new int[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            // Multiply every element except the one at index i
            int currentProduct = 1;
            for (int j = 0; j < nums.Length; j++)
            {
                if (i != j)
                {
                    currentProduct *= nums[j];
                }
            }
            output[i] = currentProduct;
        }

        return output;
    }
}
