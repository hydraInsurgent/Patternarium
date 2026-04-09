// Approach: Two-Pointer Converging/Diverging
// Time:  O(n)
// Space: O(1) extra
// Key Idea: Pointers converge depositing partial products, then diverge completing each position with the other half.

public class Solution3
{
    public int[] ProductExceptSelf(int[] nums)
    {
        int[] output = new int[nums.Length];
        int length = nums.Length;
        int left = 0;
        int right = nums.Length - 1;

        int leftProduct = 1, rightProduct = 1;

        // Phase 1: Converge from edges to center, depositing partial products
        while (left < right)
        {
            if (left == 0)
            {
                leftProduct = 1;
                rightProduct = 1;
            }
            else
            {
                leftProduct *= nums[left - 1];
                rightProduct *= nums[right + 1];
            }
            // Each slot gets one half of its final answer
            output[left] = leftProduct;
            output[right] = rightProduct;
            left++;
            right--;
        }

        // Phase 2: Diverge from center to edges, completing each position
        while (left <= length - 1 && right >= 0)
        {
            leftProduct *= nums[left - 1];
            rightProduct *= nums[right + 1];

            if (left == right)
            {
                // Odd-length center: slot was never visited in Phase 1, needs both halves
                output[left] = leftProduct * rightProduct;
            }
            else
            {
                // Each slot already has one half from Phase 1; multiply in the other half
                output[left] *= leftProduct;
                output[right] *= rightProduct;
            }

            left++;
            right--;
        }

        return output;
    }
}

// Why the left == right special case?
// - For odd-length arrays, the center element is never visited during Phase 1
//   (the while loop exits when left == right). So output[center] is still 0,
//   and multiplying 0 * anything stays 0. The center needs both halves
//   assigned directly, not multiplied against the stored value.
