# 242. Valid Anagram - Notes

## Key Insights

- For "are these the same?" problems, frequency count is almost always better than sorting - O(n) vs O(n log n), and same O(1) space when the character set is bounded.
- The optimization path Dictionary -> int[26] -> Span+stackalloc is driven by character set constraints, not by changing the algorithm. The logic stays identical.
- Positive count at the end = extra in s. Negative count = extra in t. Either way, non-zero means false.

## Patterns Used

- **Approach 1 - Preprocessing (Sort to Expose Structure):** Sort both strings so anagrams become identical, then compare character by character.
- **Approaches 2, 3, 4 - HashMap (Frequency Count):** Count occurrences in s, cancel out with t, verify all counts are zero.
