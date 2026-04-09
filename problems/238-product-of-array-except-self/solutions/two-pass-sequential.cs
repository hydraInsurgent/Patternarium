// Approach: Two-Pass Sequential
// Time:  O(n)
// Space: O(1) extra
// Key Idea: First pass deposits prefix products into output, second pass multiplies in suffix products.

public class Solution4
{
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] output = new int[nums.Length];

        // Pass 1: left-to-right, build running prefix product into output
        int leftProduct = 1;
        for (int i = 0; i < nums.Length; i++)
        {
            if (i == 0)
            {
                leftProduct = 1;
            }
            else
            {
                leftProduct *= nums[i - 1];
            }
            output[i] = leftProduct;
        }

        // Pass 2: right-to-left, multiply each slot by running suffix product
        int rightProduct = 1;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            if (i == nums.Length - 1)
            {
                rightProduct = 1;
            }
            else
            {
                rightProduct *= nums[i + 1];
            }
            output[i] *= rightProduct;
        }

        return output;
    }
}
