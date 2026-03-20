// Approach: HashMap - Complement Lookup
// Time:  O(n) - single pass through array
// Space: O(n) - dictionary stores up to n elements
// Key Idea: Store previously seen numbers for O(1) complement lookup

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        var seenNumbers = new Dictionary<int, int>(); // number -> index

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            // Check FIRST, then store - prevents using same element twice
            if (seenNumbers.TryGetValue(complement, out int foundIndex))
            {
                return new int[] { foundIndex, i };
            }

            seenNumbers[nums[i]] = i;
        }

        return new int[0]; // guaranteed not to reach here
    }
}

// Why check first, then store?
// - If we store first: on [3,3] target=6, we store map[3]=0, then check map[3] which returns 0
//   -> we would return [0,0] which uses the same element twice (wrong)
// - If we check first: map is empty at i=0, check fails, store map[3]=0
//   -> at i=1, we check map[3] which returns 0 -> return [0,1] (correct)
//
// Why TryGetValue instead of ContainsKey + indexing?
// - ContainsKey + map[needed] does TWO lookups
// - TryGetValue does ONE lookup and returns the value via out parameter
