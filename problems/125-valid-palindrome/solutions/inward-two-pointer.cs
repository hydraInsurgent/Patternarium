// ==== Approach 1: Inward Two Pointer ====
// Time:  O(n)
// Space: O(n)
// Key Idea: Clean the string first (remove non-alphanumeric, lowercase), then walk pointers inward from both ends

public class Solution
{
    public bool IsPalindrome(string s)
    {
        // Normalize: strip non-alphanumeric characters and convert to lowercase
        string cleaned = new string(s.Where(c => char.IsLetterOrDigit(c)).ToArray()).ToLower();

        int left = 0;
        int right = cleaned.Length - 1;

        // Walk inward - if any pair mismatches, it's not a palindrome
        while (left < right)
        {
            if (cleaned[left] != cleaned[right])
                return false;

            left++;
            right--;
        }

        return true;
    }
}
