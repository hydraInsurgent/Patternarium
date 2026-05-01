---
problem: 647
problem-title: Palindromic Substrings
difficulty: Medium
category: solutions
patterns: [Center Expansion, DP Tabulation]
constructs: [string-substring, string-reverse]
ds-used: [string]
algorithms: []
techniques: [gap-based-fill-order]
concepts: [palindrome]
approaches:
  - name: Center Expansion
    file: solutions/center-expansion.cs
    patterns: [Center Expansion]
    variation: Count Palindromic Substrings
    constructs: []
    ds-used: [string]
    techniques: []
    time: "O(n^2)"
    space: "O(1)"
  - name: Brute Force - Nested Loops with Reversal
    file: solutions/brute-force-reversal.cs
    patterns: []
    constructs: [string-substring, string-reverse]
    ds-used: [string]
    techniques: []
    time: "O(n^3)"
    space: "O(n)"
  - name: Brute Force - Gap-Based
    file: solutions/brute-force-gap-based.cs
    patterns: []
    constructs: []
    ds-used: [string]
    techniques: [gap-based-fill-order]
    time: "O(n^3)"
    space: "O(1)"
  - name: Dynamic Programming - Tabulation
    file: solutions/dp-tabulation.cs
    patterns: [DP Tabulation]
    variation: 2D String Palindrome Table
    constructs: []
    ds-used: [string]
    techniques: [gap-based-fill-order]
    ds-notes:
      string: "2D dp table dp[i,j] stores whether s[i..j] is a palindrome"
    time: "O(n^2)"
    space: "O(n^2)"
---

# Palindromic Substrings - Solutions

## Approaches

### Approach 1: Center Expansion
**Code:** [center-expansion.cs](center-expansion.cs)
**Time:** O(n²) | **Space:** O(1)

**Thinking:** Every palindrome has a center. For each index, call an expand helper twice: once with `(i, i)` for odd-length centers and once with `(i, i+1)` for even-length centers. The helper expands outward while bounds are valid and characters match, incrementing a local count on each valid step. Sum all counts from all centers.

**Mistakes:**
- Added `if s[i] == s[i+1]` pre-check before even expansion (v1 session) - unnecessary, the while guard returns 0 immediately on mismatch
- Even-case guard written as `i < s.Length - 2` instead of `i < s.Length - 1` - off by one on the bound check
- Boundary off-by-one on right side: `right <= s.Length` instead of `right <= s.Length - 1`

---

### Approach 2: Brute Force - Nested Loops with Reversal
**Code:** [brute-force-reversal.cs](brute-force-reversal.cs)
**Time:** O(n³) | **Space:** O(n)

**Thinking:** Two nested for loops enumerate all (start, end) pairs. Extract each substring with `s.Substring(start, end - start + 1)`, reverse it with `new string(substring.Reverse().ToArray())`, and compare. Increment count if equal.

**Mistakes:**
- Called `s.Substring(start, end)` - second argument is length not end index. Caused `ArgumentOutOfRangeException`.
- Tried `substring.Reverse().ToString()` - LINQ Reverse returns `IEnumerable<char>` not a string; must use `new string(...ToArray())`.

---

### Approach 3: Brute Force - Gap-Based
**Code:** [brute-force-gap-based.cs](brute-force-gap-based.cs)
**Time:** O(n³) | **Space:** O(1)

**Thinking:** Outer loop over `gap` (0 = single chars, 1 = two chars, ...). Inner while loop slides start and end forward together. Two-pointer in-place palindrome check using separate `left` and `right` variables (not start/end, which must be preserved for window advancement). Start with `isValid = true`, break on first mismatch.

**Key connection:** This loop structure is identical to the DP fill order - converting to DP is just replacing the O(n) palindrome check with an O(1) table lookup.

---

### Approach 4: Dynamic Programming - Tabulation
**Code:** [dp-tabulation.cs](dp-tabulation.cs)
**Time:** O(n²) | **Space:** O(n²)

**Key Condition:** `dp[start,end] = (s[start] == s[end]) && dp[start+1,end-1]`

**Thinking:** `dp[start,end]` is true if `s[start..end]` is a palindrome. Fill the table by increasing gap so inner subproblems are always computed before outer ones. Base case covers `end - start <= 2` (lengths 1-3) using only endpoint comparison. For larger gaps, use the transition formula. Increment count whenever `dp[start,end]` is true.

**Mistakes:**
- `n = s.Length - 1` for array size - constructor takes count not last index, caused index out of range on last character
- Base case only handled `start == end` (length 1) - for length 2, `dp[start+1, end-1]` = `dp[end, start]` which defaults to false, making all 2-char palindromes fail. Fix: `end - start <= 2`.

---

## Patterns

- Center Expansion - Count Palindromic Substrings (Approach 1) - expand from every center and count each valid step; no boundary correction needed
- DP Tabulation - 2D String Palindrome Table (Approach 4) - precompute palindrome status for all substrings bottom-up; O(1) lookup replaces O(n) check

## Techniques

- Gap-Based Fill Order (Approaches 3, 4) - outer loop over gap ensures all shorter substrings are computed before longer ones that depend on them

## Reflection

- **Key insight:** When a bigger problem's answer depends on a smaller subproblem's answer - precompute and store the smaller results so the bigger ones can look them up in O(1). That's DP Tabulation.
- **DP takeaway:** First real DP implementation. Two concepts clicked: build from smaller to larger (tabulation), and base cases must be handled explicitly for lengths where the transition formula breaks.
- **Pattern mantra:** For count-all palindrome problems: center expansion is optimal (O(n²) time, O(1) space). DP trades O(n²) space for a reusable lookup table. Brute force with reversal is always O(n³) - never the right choice.
- **Future strategy:** When you see "count all substrings with property X" - ask if checking a large substring reuses the result of checking a smaller one. If yes, DP is an option.
- **Confidence:** Knew `i/j` variables were necessary in Approach 3 but backed down when pushed. Hold ground when reasoning is solid.
- **Root mistake:** Trusting that `string.Reverse().ToString()` works - it doesn't. LINQ Reverse returns `IEnumerable<char>`.
