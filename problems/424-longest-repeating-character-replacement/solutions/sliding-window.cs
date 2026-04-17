// Approach: Sliding Window
// Time:  O(n) - both left and right only move forward; total moves <= 2n
// Space: O(1) - frequency map bounded at 26 uppercase letters
// Key Idea: Maintain one window; expand right each step; shrink from left while invalid; never decrease maxFreq

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
            // Include s[right] in window BEFORE checking validity - window must be fully formed first
            if (!freqMap.ContainsKey(s[right]))
                freqMap.Add(s[right], 1);
            else
                freqMap[s[right]]++;

            // maxFreq only increases - never decremented when shrinking (see Why below)
            maxFreq = Math.Max(maxFreq, freqMap[s[right]]);

            // Shrink until valid - one expansion can require multiple contractions
            while (right - left + 1 - maxFreq > k)
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

// Why never decrement maxFreq when shrinking?
// - Decrementing breaks on ties: if {A:3, B:3} and A exits the window, the actual
//   max is still 3 (B), but a naive "decrement if this was the max" rule sets max to 2.
// - The stale maxFreq makes the validity check more lenient - it may let a window pass
//   that is technically invalid. But maxFreq only increases when a real frequency
//   increase happens, so any length we record was already achievable at an earlier
//   point when maxFreq was genuinely that high. The answer is never inflated.

// Why while loop and not if?
// - One expansion (right++) can push the window's replacements-needed count over k
//   by more than 1 if maxFreq has not caught up to window growth. One left++ may not
//   restore validity. Keep shrinking until the condition holds.
