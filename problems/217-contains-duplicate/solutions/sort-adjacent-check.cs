// Approach: Sort + Adjacent Check
// Time:  O(n log n)
// Space: O(1)
// Key Idea: Sort the array so duplicates land next to each other, then scan for equal neighbors.

public class Solution2
{
    public bool ContainsDuplicate(int[] nums)
    {
        // Bring duplicates adjacent - in-place, no extra array
        Array.Sort(nums);

        // Check each element against its right neighbor
        // Stop at Length-1 to avoid index out of range when accessing nums[i+1]
        for (int i = 0; i < nums.Length - 1; i++)
        {
            if (nums[i] == nums[i + 1])
                return true;
        }

        return false;
    }

    // Why i < nums.Length - 1:
    // We check nums[i+1] inside the loop. If i reached nums.Length-1,
    // nums[i+1] would be out of bounds. The last element has no right neighbor to compare.
}
