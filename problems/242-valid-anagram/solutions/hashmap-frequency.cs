// Approach: HashMap - Frequency Count
// Time:  O(n)
// Space: O(1) for lowercase letters only (max 26 keys); O(n) for Unicode general case
// Key Idea: Count char frequencies in s, cancel out with t, verify all counts are zero at the end

public class Solution
{
    public bool IsAnagram(string s, string t)
    {
        // Anagrams must have the same number of characters - early exit avoids unnecessary work
        if (s.Length != t.Length)
            return false;

        var frequencies = new Dictionary<char, int>();

        // Build frequency map from s - count how many times each character appears
        for (int i = 0; i < s.Length; i++)
        {
            if (frequencies.TryGetValue(s[i], out int count))
                frequencies[s[i]] = count + 1;
            else
                frequencies.Add(s[i], 1);
        }

        // Cancel out frequencies using t - if t has a character not in s, it cannot be an anagram
        for (int i = 0; i < t.Length; i++)
        {
            if (frequencies.TryGetValue(t[i], out int count))
                frequencies[t[i]] = count - 1;
            else
                return false; // character in t that never appeared in s
        }

        // All counts must be zero - positive means extra in s, negative means extra in t
        foreach (var kvp in frequencies)
        {
            if (kvp.Value != 0)
                return false;
        }

        return true;
    }
}

// Why TryGetValue instead of ContainsKey + indexer:
// ContainsKey does one dictionary lookup, then map[key] does a second lookup.
// TryGetValue checks and retrieves in a single pass - half the work.
