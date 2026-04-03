// Approach: Sliding Window with ASCII Array (Span<int>)
// Time:  O(n)
// Space: O(1) - fixed 128-int array, size independent of input
// Key Idea: Replace Dictionary<char,int> with a stack-allocated int[128] indexed
//           by ASCII char value. Store end+1 so that 0 safely means "never seen."
// Source:   LeetCode community (reference solution - not user's own code)

public class Solution21
{
    public int LengthOfLongestSubstring(string s)
    {
        // Stack-allocate 128 ints covering the full ASCII range.
        // Span<int> + stackalloc avoids heap allocation entirely.
        // Size is always 128 regardless of input - true O(1) space.
        // All slots initialize to 0, which we treat as "never seen."
        Span<int> lastSeen = stackalloc int[128];

        int start = 0;
        int end = 0;
        int maxLength = 0;

        while (end < s.Length)
        {
            // s[end] implicitly casts char to int (its ASCII value), used as array index.
            // lastSeen[s[end]] holds the previous (end+1) for this char, or 0 if never seen.
            // Only jump start forward if the previous occurrence is inside the current window.
            // Equivalent to: start = Math.Max(start, lastSeen[s[end]])
            if (lastSeen[s[end]] > start)
            {
                start = lastSeen[s[end]];
            }

            maxLength = Math.Max(maxLength, end - start + 1);

            // Store end+1, not end, so that 0 unambiguously means "never seen."
            // Storing end directly would make index 0 indistinguishable from "not seen."
            lastSeen[s[end]] = end + 1;

            end++;
        }

        return maxLength;
    }
}

// Why int[] instead of bool[]?
// - The user's original instinct was a boolean array (mark presence only).
//   This solution extends that idea: store the last seen position instead of true/false,
//   which gives you the index to jump to - not just whether the char exists.

// Why end+1 instead of end?
// - Default int value is 0. If we stored end directly, a char seen at index 0 would
//   store 0 - indistinguishable from "never seen." Storing end+1 shifts all values
//   up by 1, making 0 a safe sentinel for "this slot has never been written."

// Why Span<int> + stackalloc?
// - stackalloc allocates on the call stack instead of the heap. No GC pressure.
// - Span<int> is a safe wrapper around the stack memory (bounds-checked, no unsafe keyword needed).
// - For a fixed 128-slot array this is a meaningful micro-optimization at scale.
