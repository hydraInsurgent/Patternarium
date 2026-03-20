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
