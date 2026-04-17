// Approach: Sliding Window - Monotonic (If Variant)
// Time:  O(n) - both left and right move forward exactly once
// Space: O(1) - frequency map bounded at 26 uppercase letters
// Key Idea: Window size never shrinks - use if instead of while; slide at current max size until a genuinely larger window is possible

public class Solution
{
    public int CharacterReplacement(string s, int k)
    {
        int left = 0;
        int right = 0;
        Dictionary<char, int> freqMap = new Dictionary<char, int>();
        int maxFreq = 0;
        int maxLength = 0;

        while (right < s.Length)
        {
            if (!freqMap.ContainsKey(s[right]))
                freqMap.Add(s[right], 1);
            else
                freqMap[s[right]]++;

            maxFreq = Math.Max(maxFreq, freqMap[s[right]]);

            // When invalid: slide window forward at the same size rather than shrinking it
            if (right - left + 1 - maxFreq > k)
            {
                freqMap[s[left]]--;
                left++;
            }

            maxLength = Math.Max(maxLength, right - left + 1);
            right++;
        }

        return maxLength;
    }
}

// Why if instead of while?
// - We are only looking for a window LARGER than the best seen so far. When invalid,
//   one left++ reduces window size back to the previous max. Then right++ slides it
//   forward. We are not restoring validity - we are sliding the same-size window,
//   waiting for a position where a genuinely larger window is possible.
// - A larger window is only accepted when a real frequency increase updates maxFreq.
//   Until then, the window slides without growing.
// - Only valid for "max length" problems. Does not work for "find first valid window"
//   or "min length" problems where you may need to shrink below the current max.
