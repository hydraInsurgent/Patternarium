// Approach: Brute Force (Try All Substrings)
// Time:  O(n^2)
// Space: O(1) - frequency map bounded at 26 uppercase letters
// Key Idea: Try every substring [startIndex...endIndex]; track most-frequent character count incrementally; check windowSize - maxFreq <= k

public class Solution
{
    public int CharacterReplacement(string s, int k)
    {
        int maxLength = 0;

        for (int startIndex = 0; startIndex < s.Length; startIndex++)
        {
            // Reset per outer loop - frequency and maxFreq are local to each starting position
            Dictionary<char, int> freqMap = new Dictionary<char, int>();
            int maxFreq = 0;

            for (int endIndex = startIndex; endIndex < s.Length; endIndex++)
            {
                // Extend window one character to the right and update its frequency
                if (!freqMap.ContainsKey(s[endIndex]))
                    freqMap.Add(s[endIndex], 1);
                else
                    freqMap[s[endIndex]]++;

                // Only the newly added character can become the new max - no full map scan needed
                maxFreq = Math.Max(maxFreq, freqMap[s[endIndex]]);

                int windowSize = endIndex - startIndex + 1;
                if (windowSize - maxFreq <= k)
                    maxLength = Math.Max(maxLength, windowSize);
            }
        }

        return maxLength;
    }
}

// Why maxFreq resets per outer loop?
// - First instinct was to make maxFreq global across all substrings. That causes
//   windowSize - globalMax to go negative, making every window look valid. Each
//   window is independent - maxFreq must be local to each starting index.

// Why maxFreq = max(maxFreq, freq[s[endIndex]]) and not a full map scan?
// - Only one character's count changes per inner step. That character can only
//   stay the same or become the new max. Scanning the full map is O(26) wasted work.
