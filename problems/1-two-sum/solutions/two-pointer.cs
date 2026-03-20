// Approach: Sorting + Two Pointers (with Index Preservation)
// Time:  O(n log n) - dominated by the sort
// Space: O(n) - tuple array to preserve original indices
// Key Idea: Sort by value but carry original index along as metadata
//           Then use two pointers from both ends to find the pair

// Note: This approach is NOT faster than HashMap for unsorted input.
//       Its value is learning the Two Pointer pattern and the "carry metadata" technique.
//       Use HashMap for the general unsorted case.

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        int n = nums.Length;

        // Step 1: Attach original index to each value before sorting
        // If we sort nums[] directly, we lose the original index positions
        (int value, int index)[] arr = new (int, int)[n];
        for (int i = 0; i < n; i++)
        {
            arr[i] = (nums[i], i);
        }

        // Step 2: Sort by value - the index travels with it
        Array.Sort(arr, (a, b) => a.value.CompareTo(b.value));

        // Step 3: Two pointer from both ends
        int left = 0, right = n - 1;

        while (left < right)
        {
            int sum = arr[left].value + arr[right].value;

            if (sum == target)
            {
                // Return original indices, not sorted positions
                return new int[] { arr[left].index, arr[right].index };
            }
            else if (sum < target)
            {
                left++;   // sum too small, move left pointer right for bigger value
            }
            else
            {
                right--;  // sum too big, move right pointer left for smaller value
            }
        }

        return new int[0]; // guaranteed not to reach here
    }
}

// Why the Two Pointer works on sorted arrays:
// - Left side = smallest values, right side = largest values
// - If sum > target: the right value is too large, move right pointer left
// - If sum < target: the left value is too small, move left pointer right
// - Each pointer only moves in one direction -> O(n) total moves
//
// Why we need the (value, index) tuple:
// - Sorting rearranges positions, destroying original index info
// - By pairing (value, original index) before sorting, the index travels with the value
// - This is the "carry identity with data" pattern - used in merge sort variants,
//   greedy algorithms, and custom sorting problems
//
// Why this is O(n log n) not O(n):
// - The sort takes O(n log n)
// - The two pointer scan takes O(n)
// - Total is dominated by O(n log n)
// -> HashMap is better for the general unsorted case (O(n))
