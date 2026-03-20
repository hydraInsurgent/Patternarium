// Approach: String replacement - eliminate subtraction cases before summing
// Time:  O(n)
// Space: O(n) - new string created by Replace()
// Key Idea: Fixed number of subtraction pairs - replace them with single-char placeholders, then sum

public class Solution3
{
    public int RomanToInt(string s)
    {
        var romanValues = new Dictionary<char, int>()
        {
            // Standard symbols
            {'M', 1000}, {'D', 500}, {'C', 100},
            {'L', 50},   {'X', 10},  {'V', 5}, {'I', 1},
            // Placeholder characters for subtraction pairs
            {'A', 4},    // IV
            {'B', 9},    // IX
            {'Z', 40},   // XL
            {'E', 90},   // XC
            {'F', 400},  // CD
            {'G', 900}   // CM
        };

        // Replace two-char subtraction pairs with single placeholders
        // Note: C# strings are immutable - must reassign the result
        s = s.Replace("IV", "A")
             .Replace("IX", "B")
             .Replace("XL", "Z")
             .Replace("XC", "E")
             .Replace("CD", "F")
             .Replace("CM", "G");

        // Now every character maps to a positive value - just sum them
        int result = 0;
        for (int i = 0; i < s.Length; i++)
        {
            result += romanValues[s[i]];
        }

        return result;
    }
}
