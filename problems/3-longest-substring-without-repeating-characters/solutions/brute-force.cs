// Approach: Brute Force with HashSet
// Time:  O(n^2)
// Space: O(n)
// Key Idea: For each starting index, scan forward adding chars to a fresh HashSet
//           until a repeat is found. Track the maximum window size seen.

public class Solution1
{
    public int LengthOfLongestSubstring(string s)
    {
        if (s.Length == 0) return 0;
        if (s.Length == 1) return 1;

        int maxCount = 1;

        // Try every possible starting position
        for (int start = 0; start < s.Length - 1; start++)
        {
            // Fresh window for each starting position
            HashSet<char> window = new HashSet<char>();
            window.Add(s[start]);

            // Extend the window until a repeat is found
            for (int end = start + 1; end < s.Length; end++)
            {
                if (!window.Contains(s[end]))
                {
                    window.Add(s[end]);
                }
                else
                {
                    break; // Repeat found - this window is done
                }
            }

            if (window.Count > maxCount)
            {
                maxCount = window.Count;
            }
        }

        return maxCount;
    }
}
