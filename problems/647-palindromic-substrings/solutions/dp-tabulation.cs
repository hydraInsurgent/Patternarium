// Approach: Dynamic Programming - Tabulation
// Time:  O(n^2) - each cell computed once at O(1)
// Space: O(n^2) - n x n boolean table
// Key Idea: dp[start,end] = true if s[start..end] is a palindrome. Fill by increasing gap so inner results are always ready. Base case covers lengths 1-3 with a simple endpoint check.

public class Solution
{
    public int CountSubstrings(string s)
    {
        int count = 0;
        int n = s.Length;
        bool[,] dp = new bool[n, n];

        // Fill by increasing gap (substring length = gap + 1)
        for (int gap = 0; gap < s.Length; gap++)
        {
            int start = 0;
            int end = start + gap;

            while (end < s.Length)
            {
                if (end - start <= 1)
                {
                    // Base case: length 1 (crash without it - negative index) and
                    // length 2 (wrong answer without it - lower triangle defaults to false)
                    dp[start, end] = (s[start] == s[end]);
                }
                else
                {
                    // Transition: endpoints match AND inner substring is a palindrome
                    dp[start, end] = (s[start] == s[end]) && dp[start + 1, end - 1];
                }

                if (dp[start, end])
                    count++;

                start++;
                end++;
            }
        }

        return count;
    }
}

// Why n = s.Length (not s.Length - 1) for the array size?
// Array constructors take element count, not last valid index. new bool[s.Length - 1, ...]
// creates an array with indices 0..s.Length-2, missing the last position and causing
// index out of range at the last character.

// Why end - start <= 1 as the base case (not just start == end)?
// Length 1 (start==end): dp[start+1, end-1] = dp[start+1, start-1] -> negative index -> crash.
// Length 2 (end==start+1): dp[start+1, end-1] = dp[end, start] -> lower triangle, never filled,
//   defaults to false -> all 2-char palindromes silently return 0.
// Length 3 does NOT need the base case: dp[start+1, end-1] = dp[start+1, start+1], a diagonal
//   cell filled in gap=0 (always true). The formula works correctly without special casing.

// Why fill by gap (not by row/column)?
// dp[start][end] depends on dp[start+1][end-1], which has gap-2. Filling by row
// (outer loop over start) would compute dp[start+1] after dp[start], leaving
// dp[start+1][end-1] unfilled when dp[start][end] needs it. Gap-based fill
// guarantees all shorter substrings are ready before longer ones.
