// Approach: Expand Around Center
// Time:  O(n²)
// Space: O(1)
// Key Idea: For every index, treat it as a palindrome center and expand outward. Run two expansions per index - odd (single center) and even (two adjacent characters). Track the longest result across all centers.

public class Solution1
{
    public string LongestPalindrome(string s)
    {
        // Single character is always a palindrome - nothing to compare
        if (s.Length == 1)
            return s;

        int maxStart = 0;
        int maxLength = 1;

        // Try every index as a potential palindrome center
        for (int i = 0; i < s.Length; i++)
        {
            // Odd expansion: single character center (e.g., "aba" centered at 'b')
            (int l, int r) oddResult = ExpandFromCenter(s, i, i);

            // Even expansion: two adjacent equal characters as center (e.g., "abba" centered between the two 'b's)
            (int l, int r) evenResult = new(0, 0);
            if (i < s.Length - 1)
                evenResult = ExpandFromCenter(s, i, i + 1);

            int oddLength = oddResult.r - oddResult.l + 1;
            int evenLength = evenResult.r - evenResult.l + 1;

            // Take whichever expansion found the longer palindrome
            if (oddLength > evenLength)
            {
                if (oddLength > maxLength)
                {
                    maxStart = oddResult.l;
                    maxLength = oddLength;
                }
            }
            else
            {
                if (evenLength > maxLength)
                {
                    maxStart = evenResult.l;
                    maxLength = evenLength;
                }
            }
        }

        return s.Substring(maxStart, maxLength);
    }

    private (int l, int r) ExpandFromCenter(string s, int left, int right)
    {
        int n = s.Length;

        // Expand outward while characters match and we are within bounds
        while (left > 0 && right < n - 1 && s[left] == s[right])
        {
            left--;
            right++;
        }

        // Mismatch: the last expansion step went one too far - step back to valid pair
        if (s[left] != s[right])
        {
            left++;
            right--;
        }

        // Boundary stop: current positions are still valid - return as-is
        return (left, right);
    }
}

// Why run both odd and even expansions for every index?
// - Palindromes come in two shapes: odd-length (single center) and even-length (two-character center)
// - No upfront check reliably tells you which shape will be longer at each position
// - Running both and comparing is simpler and has the same O(n²) complexity either way

// Why not detect even vs odd with s[i] == s[i+1]?
// - This was tried first and caused two conflicting if/else blocks that overwrote each other
// - The detection logic grew complex and still missed cases like "ccc"
// - Always running both expansions eliminates the detection problem entirely
