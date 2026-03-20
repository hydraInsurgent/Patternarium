// Approach: HashMap - Complement Lookup
// Time:  O(n) - single pass through array
// Space: O(n) - dictionary stores up to n elements
// Key Idea: Store previously seen numbers (number -> index) for O(1) lookup
//           For each element, check if its complement (target - current) was already seen

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> map = new Dictionary<int, int>(); // number -> index

        for (int i = 0; i < nums.Length; i++)
        {
            int needed = target - nums[i];

            // Check FIRST, then store - critical order!
            // If we stored first, [3,3] with target=6 would wrongly return [0,0]
            if (map.TryGetValue(needed, out int index))
            {
                return new int[] { index, i };
            }

            map[nums[i]] = i;
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
// - More idiomatic C# and slightly more efficient
//
// Why Dictionary<int, int> with number as key (not index as key)?
// - We want to check: "have I seen this NUMBER before?"
// - The lookup key must be the number, not the index
// - The stored value is the index so we can return it in the answer
