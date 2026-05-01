// Approach: Brute Force - Gap-Based
// Time:  O(n^3) - O(n^2) pairs, O(n) per palindrome check
// Space: O(1) - in-place comparison, no string allocation
// Key Idea: Outer loop drives substring length (gap). Inner while slides the window. Two-pointer in-place palindrome check avoids string allocation.

public class Solution
{
    public int CountSubstrings(string s)
    {
        int count = 0;

        // gap = index difference (gap 0 = single chars, gap 1 = two chars, etc.)
        for (int gap = 0; gap < s.Length; gap++)
        {
            int start = 0;
            int end = start + gap;

            // Slide the window of this length across the string
            while (end < s.Length)
            {
                // Two-pointer in-place palindrome check
                int left = start;
                int right = end;
                bool isValid = true;

                while (left <= right)
                {
                    if (s[left] != s[right])
                    {
                        isValid = false;
                        break;
                    }
                    left++;
                    right--;
                }

                if (isValid)
                    count++;

                start++;
                end++;
            }
        }

        return count;
    }
}

// Why separate left/right variables instead of using start/end directly?
// start and end must be preserved for the outer window advancement (start++, end++).
// Incrementing/decrementing start and end inside the inner loop would corrupt the
// window position for the next iteration.

// Why isValid = true (not false)?
// The while loop sets isValid = false and breaks on the first mismatch. If no
// mismatch is found, isValid stays true. Starting from false and setting true on
// match requires checking the first pair twice - redundant.

// Why gap and not length?
// gap = end - start (index difference). length = gap + 1. Naming it gap is precise
// since end = start + gap. Calling it length is off by one in the mental model.
