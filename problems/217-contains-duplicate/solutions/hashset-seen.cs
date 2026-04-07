// Approach: HashSet Lookup
// Time:  O(n)
// Space: O(n)
// Key Idea: Use a HashSet to track seen numbers. Check before adding - if the number is already there, a duplicate exists.

public class Solution1
{
    public bool ContainsDuplicate(int[] nums)
    {
        // Track every number seen so far - O(1) existence check
        HashSet<int> seen = new HashSet<int>();

        foreach (int num in nums)
        {
            // Check first, then store - if it's already here, we found a duplicate
            if (seen.Contains(num))
                return true;

            seen.Add(num);
        }

        // No number appeared twice
        return false;
    }
}
