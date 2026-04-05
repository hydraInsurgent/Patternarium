// ==== Approach 2: Two Pointer Without Extra Space ====
// Time:  O(n)
// Space: O(1)
// Key Idea: Skip non-alphanumeric on the fly using inner while loops bounded by left < right

public class Solution
{
    public bool IsPalindrome(string s)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            // Skip non-alphanumeric - bounded by left < right, not array edges
            while (!char.IsLetterOrDigit(s[left]) && left < right)
                left++;

            while (!char.IsLetterOrDigit(s[right]) && right > left)
                right--;

            // Both pointers now sit on valid characters - compare case-insensitively
            if (char.ToLower(s[left]) != char.ToLower(s[right]))
                return false;

            // Advance unconditionally - outer while handles termination
            left++;
            right--;
        }

        return true;
    }
}

// Why: Inner while loops must be bounded by left < right (relative), not left < s.Length-1 (absolute).
// Using array bounds caused the need for an extra IsLetterOrDigit guard before comparison and
// conditional advancement at the end. Once the boundary was corrected to be relative, both extras
// became dead code and the pointers could advance unconditionally after each comparison.
