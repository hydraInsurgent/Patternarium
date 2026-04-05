// ==== Approach 3: ASCII Two Pointer ====
// Time:  O(n)
// Space: O(1)
// Key Idea: Replace built-in char methods with ASCII range checks and +32 arithmetic

public class Solution
{
    public bool IsPalindrome(string s)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            // Skip non-alphanumeric using raw ASCII range checks
            while (!IsAlphaNumeric(s[left]) && left < right)
                left++;

            while (!IsAlphaNumeric(s[right]) && right > left)
                right--;

            // Normalize case with arithmetic and compare
            if (ToLower(s[left]) != ToLower(s[right]))
                return false;

            left++;
            right--;
        }

        return true;
    }

    // ASCII ranges: 48-57 = digits, 65-90 = uppercase, 97-122 = lowercase
    private static bool IsAlphaNumeric(char c)
    {
        int value = c;
        return (value >= 48 && value <= 57) ||
               (value >= 65 && value <= 90) ||
               (value >= 97 && value <= 122);
    }

    // 'A' = 65, 'a' = 97 - the gap is exactly 32 for every uppercase/lowercase pair
    private static char ToLower(char c)
    {
        if (c >= 'A' && c <= 'Z')
            return (char)(c + 32);
        return c;
    }
}

// Why: This approach is valid specifically because the constraints guarantee "printable ASCII
// characters only." char.IsLetterOrDigit handles the full Unicode range - the ASCII range trick
// does not. Knowing when a shortcut is safe is as important as knowing the shortcut itself.
