---
name: "dp-tabulation"
display_name: "DP Tabulation"
category: pattern
variations:
  - name: 2D String Palindrome Table
    ds: [string, array]
ds-primary: [string, array]
---

# DP Tabulation

## Core Idea
When the answer to a larger subproblem depends on the already-computed answer to a smaller subproblem, store results in a table and look them up in O(1). Fill the table bottom-up (smallest subproblems first) so inner results are always ready when outer ones are computed.

## Variation: 2D String Palindrome Table

**When to reach for this:**
- Problem requires checking or counting properties of all substrings
- Checking substring s[i..j] requires knowing whether s[i+1..j-1] is valid (inner substring)
- The result for a range is built from the result of a strictly shorter range

**Mental Trigger:**
> "Is s[0..3] a palindrome? I need s[1..2]. If I already stored that result, I save re-checking it every time."

**Template:**
```
n = s.length
dp = bool[n][n]
for gap in 0..n-1:             // fill by increasing substring length
    start = 0
    end = start + gap
    while end < n:
        if gap <= 2:           // base cases: length 1 and 2
            dp[start][end] = s[start] == s[end]
        else:
            dp[start][end] = s[start] == s[end] && dp[start+1][end-1]
        if dp[start][end]: count++
        start++, end++
```

**Tradeoffs:**
- Time: O(n²) - each cell computed once at O(1)
- Space: O(n²) - full n×n table
- Same time as center expansion but O(n²) vs O(1) space
- Table is reusable: same dp answers any substring palindrome query in O(1)

---

## Try Next
- LeetCode 5 - Longest Palindromic Substring (same dp table, track max length instead of count)
- LeetCode 1143 - Longest Common Subsequence (different state definition, same tabulation idea)

## Common Mistakes
- Wrong array size: `new bool[n-1, n-1]` instead of `new bool[n, n]`. Array constructor takes element count, not last index. Causes index out of range at position n-1. (2D String Palindrome Table)
- Base case only covering length 1 (`start == end`). For length 2, `dp[start+1][end-1]` accesses `dp[end][start]` (never filled, defaults to false), making all 2-character palindromes fail. Base case must cover lengths 1 AND 2. Fix: `end - start <= 2`. (2D String Palindrome Table)
- Filling by row (outer loop over `i`, inner over `j`) instead of by gap. `dp[i][j]` depends on `dp[i+1][j-1]` which has a larger `i`. Filling row by row leaves `dp[i+1][j-1]` unfilled when `dp[i][j]` is computed. Use gap-based fill order instead. (2D String Palindrome Table)

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "DP Tabulation"
SORT number asc
```
