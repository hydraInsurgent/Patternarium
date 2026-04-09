// Approach: Prefix/Suffix Product Arrays
// Time:  O(n)
// Space: O(n)
// Key Idea: Precompute prefix products (left) and suffix products (right) in separate passes, then multiply complementary indices.

public class Solution2
{
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] leftProducts = new int[nums.Length];
        int[] rightProducts = new int[nums.Length];
        int[] output = new int[nums.Length];

        for (int i = 0; i < nums.Length; i++)
        {
            int leftProduct = 1;
            int rightProduct = 1;

            if (i == 0)
            {
                // Base case: nothing to the left of index 0, nothing to the right of last index
                leftProduct = 1;
                rightProduct = 1;
            }
            else
            {
                // Left product: previous left product times the element just before current
                leftProduct = leftProducts[i - 1] * nums[i - 1];
                // Right product: previous right product times the element from the opposite end
                rightProduct = rightProducts[i - 1] * nums[nums.Length - i];
            }

            leftProducts[i] = leftProduct;
            rightProducts[i] = rightProduct;
        }

        // Combine: left[i] holds prefix product, right[n-1-i] holds suffix product
        for (int i = 0; i < nums.Length; i++)
        {
            output[i] = leftProducts[i] * rightProducts[nums.Length - 1 - i];
        }

        return output;
    }
}

// Why complementary indices in the output loop?
// - rightProducts is built forwards (index 0 = product from the right end),
//   so right[0] corresponds to the rightmost position. To pair it with
//   leftProducts[i], we need right[n-1-i] to get the suffix product
//   for position i. Using right[i] directly was an early bug - it paired
//   same-direction indices instead of complementary ones.

// Why nums[i-1] instead of nums[i] in the left product formula?
// - left[i] should be the product of everything BEFORE index i.
//   Using nums[i] accidentally included the current element in its own
//   left product, producing wrong results. Same bug mirrored for right.
