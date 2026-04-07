// Approach: Sort and Compare
// Time:  O(n log n)
// Space: O(n)
// Key Idea: Sort both strings - anagrams become identical when sorted, so a simple index-by-index comparison works

public class Solution
{
    public bool IsAnagram(string s, string t)
    {
        // Anagrams must have the same number of characters - early exit saves sorting cost
        if (s.Length != t.Length)
            return false;

        // Sort both strings - if they are anagrams, sorted versions will be identical
        string sortedS = new string(s.OrderBy(c => c).ToArray());
        string sortedT = new string(t.OrderBy(c => c).ToArray());

        // Compare character by character - any mismatch means not an anagram
        for (int i = 0; i < sortedS.Length; i++)
        {
            if (sortedS[i] != sortedT[i])
                return false;
        }

        return true;
    }
}
