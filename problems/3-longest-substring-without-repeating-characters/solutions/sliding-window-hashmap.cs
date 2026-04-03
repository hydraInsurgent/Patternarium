// Approach: Sliding Window with HashMap
// Time:  O(n)
// Space: O(n)
// Key Idea: Store each char's last seen index in a Dictionary. On repeat,
//           jump start directly to lastSeenIndex+1 instead of crawling forward.

public class Solution2
{
    public int LengthOfLongestSubstring(string s)
    {
        if (s.Length == 0) return 0;
        if (s.Length == 1) return 1;

        int start = 0;
        int maxLength = 1;

        // Maps each char to the last index it was seen at
        Dictionary<char, int> lastSeen = new Dictionary<char, int>();

        for (int end = 0; end < s.Length; end++)
        {
            if (lastSeen.ContainsKey(s[end]))
            {
                // Only jump start forward - never move it left.
                // If the previous occurrence of s[end] is before the current window,
                // it is irrelevant - moving start left would corrupt the window.
                if (lastSeen[s[end]] >= start)
                {
                    start = lastSeen[s[end]] + 1;
                }
            }

            // Always update to the most recent index seen
            lastSeen[s[end]] = end;

            maxLength = end - start + 1 > maxLength ? end - start + 1 : maxLength;
        }

        return maxLength;
    }
}

// Why the explicit if condition instead of Math.Max?
// - start = Math.Max(start, lastSeen[s[end]] + 1) is equivalent and more compact
// - The explicit if makes the reasoning visible: "only update if the stored index is
//   inside the current window" - which is the key insight from the debugging session
