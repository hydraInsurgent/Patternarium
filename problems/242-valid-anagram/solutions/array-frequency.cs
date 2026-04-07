// Approach: Integer Array - Frequency Count
// Time:  O(n)
// Space: O(1)
// Key Idea: Use int[26] indexed by c - 'a' instead of a Dictionary - same logic, no hashing overhead, lowercase ASCII only

public class Solution
{
    public bool IsAnagram(string s, string t)
    {
        // Anagrams must have the same number of characters - early exit
        if (s.Length != t.Length)
            return false;

        // Fixed array of 26 slots - one per lowercase letter. Index 0 = 'a', index 25 = 'z'.
        // All slots initialize to 0, so no ContainsKey check needed.
        int[] charCounts = new int[26];

        // Count character frequencies in s using ASCII offset mapping
        for (int i = 0; i < s.Length; i++)
        {
            int index = s[i] - 'a'; // 'a' -> 0, 'b' -> 1, ..., 'z' -> 25
            charCounts[index]++;
        }

        // Cancel out frequencies using t - if a count hits 0 and t still has that char, not an anagram
        for (int i = 0; i < t.Length; i++)
        {
            int index = t[i] - 'a';
            if (charCounts[index] != 0)
                charCounts[index]--;
            else
                return false;
        }

        // Any remaining non-zero count means s had extra characters t did not
        for (int i = 0; i < charCounts.Length; i++)
        {
            if (charCounts[i] != 0)
                return false;
        }

        return true;
    }
}

// Note: This only works for lowercase English letters (a-z).
// For full ASCII, use int[128]. For Unicode, use a Dictionary instead.
