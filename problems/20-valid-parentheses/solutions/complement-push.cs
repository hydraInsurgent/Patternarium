// Approach: Stack with direct complement push
// Time:  O(n)
// Space: O(n)
// Key Idea: Push the expected closer onto stack; pop and compare directly without a map

public class Solution2
{
    public bool IsValid(string s)
    {
        // Manual stack via char array - worst case all openers, so allocate s.Length
        char[] stack = new char[s.Length];
        int top = -1;

        for (int i = 0; i < s.Length; i++)
        {
            char current = s[i];
            bool isOpener = current == '(' || current == '{' || current == '[';

            if (isOpener)
            {
                // Push the complement we EXPECT to see later
                stack[++top] = current switch
                {
                    '(' => ')',
                    '{' => '}',
                    _ => ']'
                };
            }
            else
            {
                // Closing bracket - pop and compare directly (no map needed)
                if (top < 0 || stack[top] != current)
                {
                    return false;
                }

                // No need to clear the slot - it gets overwritten on next push
                top--;
            }
        }

        // Valid only if all openers were matched
        return top == -1;
    }
}
