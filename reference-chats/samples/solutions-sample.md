# Sample: solutions.md
# Based on: 647 - Palindromic Substrings (hypothetical completed session)
# Purpose: Review proposed additions before wiring into save-problem

---

```markdown
---
problem: 647
problem-title: Palindromic Substrings
difficulty: Medium
category: solutions
patterns: [Center Expansion, Dynamic Programming]
constructs: []
ds-used: [array, string]
algorithms: []
approaches:
  - name: Center Expansion
    file: solutions/center-expansion.cs
    patterns: [Center Expansion]
    variation: Count All
    constructs: []
    ds-used: [string]
    time: "O(n^2)"
    space: "O(1)"
  - name: Dynamic Programming
    file: solutions/dynamic-programming.cs
    patterns: [Dynamic Programming]
    variation: 2D Palindrome Table
    constructs: []
    ds-used: [string, array]
    time: "O(n^2)"
    space: "O(n^2)"
---

# Palindromic Substrings - Solutions

## Approaches

### Approach 1: Center Expansion

**Code:** [center-expansion.cs](solutions/center-expansion.cs)
**Time:** O(n²) | **Space:** O(1)

**Key Condition:** `while left >= 0 AND right < n AND s[left] == s[right]` - every iteration of this loop is one valid palindrome. Count inside, not after.

**Thinking:** For each index, treat it as the center of a palindrome. Expand outward as long as characters match. Run two expansions per index - one for odd-length (start at `(i, i)`) and one for even-length (start at `(i, i+1)`). No pre-check needed before starting even expansion - the while condition handles mismatches naturally.

---

### Approach 2: Dynamic Programming

**Code:** [dynamic-programming.cs](solutions/dynamic-programming.cs)
**Time:** O(n²) | **Space:** O(n²)

**Key Condition:** `dp[i][j] = (s[i] == s[j]) AND dp[i+1][j-1]` with base cases: `i == j` always true, `j == i+1` check only `s[i] == s[j]`. Fill in order of increasing substring length so inner subproblems are always ready.

**Thinking:** Define dp[i][j] as whether s[i..j] is a palindrome. A longer substring is a palindrome if its inner substring is already one and its outer characters match. Fill by length so dp[i+1][j-1] is always computed before dp[i][j]. Count as you fill - every time dp[i][j] = true, increment the total.

---

## Patterns

- [Center Expansion - Count All](../../patterns/center-expansion.md#variation-count-all) (Approach 1) - expand from each center, count every successful step
- [Dynamic Programming - 2D Palindrome Table](../../patterns/dynamic-programming.md#variation-2d-palindrome-table) (Approach 2) - bottom-up table, fill by substring length

## Reflection

- **Key insight:** The center expansion approach counts, not tracks. Each expansion step is a palindrome - no boundary adjustment needed, unlike when finding the longest.
- **Future strategy:** When a problem asks to count palindromic substrings, start with center expansion. Add DP if space is not a constraint and you want to reuse palindrome checks across queries.
- **Biggest shift:** Carrying over the "fix overshoot" logic from Longest Palindromic Substring - had to consciously unlearn it.
```

---

## What Is New Here vs. Current Format

**`**Key Condition:**` field (proposed addition)**
- Location: per-approach block in solutions.md, between Time/Space and Thinking
- What it captures: the specific formula, invariant, or condition that defines the approach - more concrete than Thinking, less code-heavy than the .cs file
- Currently missing: only exists as `// Key Idea:` one-liner in .cs, which is not visible in solutions.md
- Required or optional: required for approaches that have a clear core condition. Can be omitted for trivial brute force where Thinking covers it fully.

**Pseudocode (not shown - deliberately omitted)**
- Considered but kept out for now. The Key Condition covers the critical invariant. Full pseudocode would duplicate what is already in the .cs file's comment header.
- Revisit if a problem comes up where neither Key Condition nor Thinking adequately bridges thinking to code.
