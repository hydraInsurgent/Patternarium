# Longest Palindromic Substring - Solutions

## Approaches

### Approach 1: Expand Around Center
**Code:** [expand-around-center.cs](solutions/expand-around-center.cs)
**Time:** O(n²) | **Space:** O(1)

**Thinking:** For each index, treat it as a potential palindrome center and expand outward. Two expansions per index - one for odd-length palindromes (single center character) and one for even-length (two adjacent equal characters). Track the longest result by start index and length across all centers.

---

### Approach 2: Dynamic Programming *(not yet explored)*
**Time:** O(n²) | **Space:** O(n²)
**Idea:** Build a 2D table where `dp[i][j]` = true if `s[i..j]` is a palindrome. Fill from shorter substrings to longer. If `s[i] == s[j]` and `dp[i+1][j-1]` is true, then `s[i..j]` is a palindrome.

---

### Approach 3: Manacher's Algorithm *(not yet explored)*
**Time:** O(n) | **Space:** O(n)
**Idea:** Reuse previous expansion results to avoid redundant work. Maintains a "mirror" relationship between palindromes that have already been found. Requires DP fundamentals first.

---

## Patterns

- [Two Pointers - Expand Around Center](../../patterns/two-pointers.md#variation-expand-around-center) (Approach 1) - fix a center at each index and expand left/right while characters match; run twice per index for odd and even palindromes

## Reflection

- **Key insight:** Fixing a center and expanding outward is more natural for palindromes than checking all substrings. The center is the source of truth.
- **Future strategy:** Always write pseudocode first and trace through all scenarios before coding. Don't try to detect even/odd upfront - just run both expansions and let the results speak.
- **Biggest lesson:** When two possibilities exist, no upfront check replaces exploring both. Keep an open mind and run both paths.
