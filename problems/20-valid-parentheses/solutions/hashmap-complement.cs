// Approach: Stack with HashMap complement lookup
// Time:  O(n)
// Space: O(n)
// Key Idea: Push openers onto stack; on close, pop and use HashMap to check complement match

public class Solution1
{
    public bool IsValid(string s)
    {
        var bracketStack = new Stack<char>();
        var complementMap = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' }
        };

        for (int i = 0; i < s.Length; i++)
        {
            char current = s[i];

            if (current == '(' || current == '{' || current == '[')
            {
                // Opening bracket - push onto stack
                bracketStack.Push(current);
            }
            else
            {
                // Closing bracket - stack must not be empty
                if (bracketStack.Count == 0)
                {
                    return false;
                }

                // Pop opener and look up its complement via the map
                if (complementMap[bracketStack.Pop()] != current)
                {
                    return false;
                }
            }
        }

        // Valid only if all openers were matched
        return bracketStack.Count == 0;
    }
}

// Why use a HashMap for complement lookup instead of comparing directly?
// - Comparing buffer.Peek() != s[i] compares '(' to ')' which are different chars -
//   this always returns true and breaks the logic. The map translates opener -> closer
//   so the comparison is ')' != ')' which correctly returns false (match found).
