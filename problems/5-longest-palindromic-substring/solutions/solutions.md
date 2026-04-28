---
problem: 5
problem-title: Longest Palindromic Substring
difficulty: Medium
category: solutions
patterns: [Two Pointers]
constructs: []
ds-used: [string]
algorithms: []
concepts: [palindrome]
approaches:
  - name: Expand Around Center
    file: expand-around-center.cs
    patterns: [Two Pointers]
    variation: Expand Around Center
    constructs: []
    ds-used: [string]
    ds-notes:
      string: "expand outward from each center index; odd and even expansion per position"
    time: "O(n²)"
    space: "O(1)"
---

# Longest Palindromic Substring - Solutions

## Approaches

### Approach 1: Expand Around Center
**Code:** [expand-around-center.cs](expand-around-center.cs)
**Time:** O(n²) | **Space:** O(1)

**Thinking:** For each index, treat it as a potential palindrome center and expand outward. Two expansions per index - one for odd-length palindromes (single center character) and one for even-length (two adjacent equal characters). Track the longest result by start index and length across all centers.

**Mistakes:**
- **Center from n/2 instead of i** - computed left/right from the string's midpoint instead of the current loop index. Root cause: confused "center of the string" with "current center being tested."
- **Variable scope** - declared left/right inside if/else blocks, not accessible outside. Root cause: C# block scoping - variables declared inside {} don't exist after the closing brace.
- **While condition inverted** - wrote `s[left] != s[right]` instead of `s[left] == s[right]`, so the loop ran only when there was a mismatch.
- **Boundary off-by-one** - used `left > 0` and `right < n-1`, stopping one step short of index 0 and the last index.
- **Two conflicting center selection blocks** - wrote a second if/else block that always ran after the first and overwrote left/right. Fixed by removing detection entirely.
- **Dead branch in exit analysis** - condition `!(left >= 0 || right <= n-1)` can never be true for valid indices. The exit check only needs two branches: mismatch and boundary.

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

- Two Pointers - Expand Around Center (Approach 1) - fix a center at each index and expand left/right while characters match; run twice per index for odd and even palindromes

## Reflection

- **Key insight:** Fixing a center and expanding outward is more natural for palindromes than checking all substrings. The center is the source of truth.
- **Future strategy:** Always write pseudocode first and trace through all scenarios before coding. Don't try to detect even/odd upfront - just run both expansions and let the results speak.
- **Biggest lesson:** When two possibilities exist, no upfront check replaces exploring both. Keep an open mind and run both paths.
- **Notes Insights:**
  - Expand around center is more natural for palindromes than checking all substrings - the center is the source of truth
  - When two possibilities exist (odd/even centers), no upfront detection replaces just running both
  - Pseudocode and tracing all scenarios before coding would have caught most of the bugs above
- **Mantras:**
  - "Run both, compare after - don't detect in advance"
  - "Pseudocode first, code second"
