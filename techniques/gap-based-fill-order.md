---
name: gap-based-fill-order
slug: gap-based-fill-order
display_name: Gap-Based Fill Order
category: technique
tags: [dp, string, substring, tabulation, fill-order]
---

# Gap-Based Fill Order

## What It Is
Drive the outer DP loop by gap (index difference between start and end) rather than by start or end directly. This guarantees all shorter substrings are computed before longer ones that depend on them.

## Core Reasoning
`dp[start][end]` depends on `dp[start+1][end-1]`, which has gap = `(end-1) - (start+1) = gap - 2`. Iterating gap from 0 upward means every inner dependency always has a smaller gap and is already filled when needed.

## When to Apply
- Filling a 2D DP table where `dp[i][j]` depends on `dp[i+1][j-1]` (the inner substring)
- Any palindrome DP table
- Any string DP where the answer for a range depends on a strictly shorter range within it

## Template
```
for gap in 0..n-1:
    start = 0
    end = start + gap
    while end < n:
        // fill dp[start][end] - dp[start+1][end-1] is guaranteed to be filled already
        start++
        end++
```

## Tradeoffs
- Guarantees correctness of any DP that relies on inner substring results
- `gap` = index difference, `length` = gap + 1 - name clearly to avoid off-by-one confusion

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN techniques AS technique
WHERE technique = "gap-based-fill-order"
SORT number asc
```
