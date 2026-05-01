// Approach: Center Expansion
// Time:  O(n^2)
// Space: O(1)
// Key Idea: At each index, expand from a single center (odd) and adjacent pair (even). Count each valid expansion step directly - every iteration is one confirmed palindrome.

public class Solution
{
    public int CountSubstrings(string s)
    {
        int totalCount = 0;

        for (int i = 0; i < s.Length; i++)
        {
            // Odd-length palindromes: single center at i
            totalCount += PalindromicSubstringCount(i, i, s);

            // Even-length palindromes: center between i and i+1
            if (i + 1 < s.Length)
                totalCount += PalindromicSubstringCount(i, i + 1, s);
        }

        return totalCount;
    }

    private int PalindromicSubstringCount(int left, int right, string s)
    {
        int count = 0;

        // Expand while both ends are in bounds and characters match
        while (left >= 0 && right <= s.Length - 1 && s[left] == s[right])
        {
            count++;
            left--;
            right++;
        }

        return count;
    }
}

// Why count inside the while loop (not after)?
// Every successful iteration confirms one palindrome. The loop exits the moment
// a mismatch or boundary is hit - that state is not a palindrome and should not
// be counted. No boundary correction needed.

// Why no pre-check (s[i] == s[i+1]) before even expansion?
// The while condition handles a starting mismatch by never executing - the helper
// returns 0 immediately. Adding a pre-check is redundant and was a known mistake
// carried over from an earlier session.
