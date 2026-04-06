// Missing Number - Boolean Flag Array
// Time: O(n) | Space: O(n)

public class Solution
{
    public int MissingNumber(int[] nums)
    {
        // Allocate one slot per value in range [0..n]
        bool[] seen = new bool[nums.Length + 1];

        // Mark each value as seen using the value itself as the index
        // WHY: seen[nums[i]], not seen[i] - we are marking the VALUE, not the loop position.
        //      Using i marks slots 0..n-1 in order, regardless of what nums actually contains.
        for (int i = 0; i < nums.Length; i++)
            seen[nums[i]] = true;

        // The first unmarked index is the number that was never seen - the missing one
        for (int i = 0; i < seen.Length; i++)
            if (!seen[i]) return i;

        return 0;
    }
}
