// Approach: Sorting + Two Pointers (with Index Preservation)
// Time:  O(n log n) - dominated by the sort
// Space: O(n) - tuple array to preserve original indices
// Key Idea: Sort by value but carry original index along as metadata

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        int n = nums.Length;

        // Attach original index before sorting so identity travels with data
        (int value, int index)[] arr = new (int, int)[n];
        for (int i = 0; i < n; i++)
        {
            arr[i] = (nums[i], i);
        }

        Array.Sort(arr, (a, b) => a.value.CompareTo(b.value));

        // Two pointers from both ends
        int left = 0, right = n - 1;

        while (left < right)
        {
            int sum = arr[left].value + arr[right].value;

            if (sum == target)
            {
                return new int[] { arr[left].index, arr[right].index };
            }
            else if (sum < target)
            {
                left++;   // need bigger sum
            }
            else
            {
                right--;  // need smaller sum
            }
        }

        return new int[0]; // guaranteed not to reach here
    }
}

// Why we need the (value, index) tuple:
// - Sorting rearranges positions, destroying original index info
// - By pairing (value, original index) before sorting, the index travels with the value
//
// Why this is O(n log n) not O(n):
// - The sort takes O(n log n), the two pointer scan takes O(n)
// - Total is dominated by O(n log n) - HashMap is better for unsorted input
