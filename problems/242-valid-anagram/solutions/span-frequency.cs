// Approach: Span + stackalloc - Frequency Count
// Time:  O(n)
// Space: O(1)
// Key Idea: Same as int[26] but stack-allocated via Span<int> + stackalloc - no heap allocation, no GC pressure, lowercase ASCII only

public class Solution
{
    public bool IsAnagram(string s, string t)
    {
        // Anagrams must have the same number of characters - early exit
        if (s.Length != t.Length)
            return false;

        // stackalloc allocates 26 ints on the stack, not the heap.
        // Span<int> provides safe, bounds-checked access to that memory.
        // No GC involvement - memory is reclaimed when the method returns.
        Span<int> charCounts = stackalloc int[26];

        // Count character frequencies in s
        for (int i = 0; i < s.Length; i++)
        {
            int index = s[i] - 'a';
            charCounts[index]++;
        }

        // Cancel out frequencies using t
        for (int i = 0; i < t.Length; i++)
        {
            int index = t[i] - 'a';
            if (charCounts[index] != 0)
                charCounts[index]--;
            else
                return false;
        }

        // Validate all counts are zero
        for (int i = 0; i < charCounts.Length; i++)
        {
            if (charCounts[i] != 0)
                return false;
        }

        return true;
    }
}

// Why Span + stackalloc instead of int[]:
// int[] allocates on the heap - GC must track and eventually collect it.
// stackalloc puts the buffer on the stack - no GC pressure, no heap allocation at all.
// For a short-lived 26-slot buffer, this is measurably faster in hot loops.
// Tradeoff: Span<int> is a ref struct - cannot be stored in class fields or used in async methods.
//
// Note: Only works for lowercase English letters. Does not support Unicode.
