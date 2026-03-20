// Approach: Two-character lookahead - consume pairs or singles
// Time:  O(n)
// Space: O(1)
// Key Idea: Check if current + next form a subtraction pair; if so consume both, otherwise consume one

public class Solution4
{
    public int RomanToInt(string s)
    {
        var romanValues = new Dictionary<char, int>()
        {
            {'M', 1000}, {'D', 500}, {'C', 100},
            {'L', 50},   {'X', 10},  {'V', 5}, {'I', 1}
        };

        int result = 0;
        int i = 0;

        // Process pairs when current < next (subtraction case), singles otherwise
        while (i < s.Length - 1)
        {
            int current = romanValues[s[i]];
            int next = romanValues[s[i + 1]];

            if (current >= next)
            {
                result += current;
                i++;
            }
            else
            {
                // Subtraction pair: e.g., IV = 5 - 1 = 4
                result += next - current;
                i += 2;
            }
        }

        // If the last character wasn't consumed as part of a pair, add it
        if (i < s.Length)
        {
            result += romanValues[s[s.Length - 1]];
        }

        return result;
    }
}
