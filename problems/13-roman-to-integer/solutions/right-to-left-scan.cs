// Approach: Right-to-left scan with subtraction rule
// Time:  O(n)
// Space: O(1)
// Key Idea: Smaller number after a greater number is subtracted

public class Solution
{
    public int RomanToInt(string s)
    {
        var romanValues = new Dictionary<char, int>()
        {
            {'M', 1000}, {'D', 500}, {'C', 100},
            {'L', 50},   {'X', 10},  {'V', 5}, {'I', 1}
        };

        int result = 0;
        int previousValue = 0;

        // Walk right to left: if current < previous, subtract (e.g., I before V = 4)
        for (int i = s.Length - 1; i >= 0; i--)
        {
            int currentValue = romanValues[s[i]];

            if (previousValue > currentValue)
                result -= currentValue;
            else
                result += currentValue;

            previousValue = currentValue;
        }

        return result;
    }
}

// Why romanValues[s[i]] and not just s[i]?
// - s[i] returns a char, and assigning it to int gives the ASCII code (e.g., 'I' = 73)
// - Must look up the mapped value in the dictionary to get the actual roman numeral value
//
// Why i >= 0 and not i > 0?
// - i > 0 skips the first character (index 0) entirely
// - The first character must also be processed
//
// Why previousValue starts at 0?
// - The rightmost character has nothing to its right, so it is always added
// - Starting at 0 ensures the first comparison (previousValue > currentValue) is false, so it adds
