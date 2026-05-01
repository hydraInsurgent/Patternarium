// Approach: Brute Force - Nested Loops with Reversal
// Time:  O(n^3) - O(n^2) pairs, O(n) per substring reversal
// Space: O(n) - new string allocated per check
// Key Idea: Enumerate all substrings with two nested loops. Reverse each and compare to check if palindrome.

public class Solution
{
    public int CountSubstrings(string s)
    {
        int count = 0;

        for (int start = 0; start < s.Length; start++)
        {
            for (int end = start; end < s.Length; end++)
            {
                // Extract substring and reverse for palindrome check
                string substring = s.Substring(start, end - start + 1);
                string reversedSubstring = new string(substring.Reverse().ToArray());

                if (substring == reversedSubstring)
                    count++;
            }
        }

        return count;
    }
}

// Why end - start + 1 as the Substring length argument?
// string.Substring(startIndex, length) takes a length, not an end index.
// Passing end directly caused ArgumentOutOfRangeException - the bug that required
// a dry run to catch.

// Why new string(substring.Reverse().ToArray())?
// LINQ's .Reverse() returns IEnumerable<char>, not a string. Calling .ToString()
// on it returns the type name, not the reversed characters. ToArray() converts to
// char[], which the string constructor accepts.
