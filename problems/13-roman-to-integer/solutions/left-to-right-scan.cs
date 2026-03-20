// Approach: Left-to-right scan with subtraction rule
// Time:  O(n)
// Space: O(1)
// Key Idea: If current value is smaller than the next, subtract it; otherwise add it

public class Solution2
{
    public int RomanToInt(string s)
    {
        var romanValues = new Dictionary<char, int>()
        {
            {'M', 1000}, {'D', 500}, {'C', 100},
            {'L', 50},   {'X', 10},  {'V', 5}, {'I', 1}
        };

        int result = 0;
        int currentValue = romanValues[s[0]];

        // Compare each value with the next one to decide add or subtract
        for (int i = 1; i < s.Length; i++)
        {
            int nextValue = romanValues[s[i]];

            if (nextValue > currentValue)
                result -= currentValue;  // Subtraction case: e.g., C before M = 900
            else
                result += currentValue;

            currentValue = nextValue;
        }

        // Last character is always added (no next value to compare against)
        result += currentValue;

        return result;
    }
}
