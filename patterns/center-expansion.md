---
name: "center-expansion"
display_name: "Center Expansion"
category: pattern
variations:
  - name: Count Palindromic Substrings
    ds: [string]
  - name: Longest Palindromic Substring
    ds: [string]
ds-primary: [string]
---

# Center Expansion

## Core Idea
For palindrome-related string problems, expand outward from each possible center. A palindrome reads the same from both ends, so checking from the center out is natural and complete - every palindrome has exactly one center.

## Variation: Count Palindromic Substrings

**When to reach for this:**
- Count all palindromic substrings in a string
- Output is a single integer (not indices or the substring itself)
- Every expansion step is a valid palindrome - count inside the loop

**Mental Trigger:**
> "Every palindrome has a center. Expanding from every index finds all palindromes without duplicates. Each valid expansion step is one palindrome."

**Template:**
```
count = 0
for each index i in string:
    count += expand(i, i)      // odd-length center
    count += expand(i, i+1)    // even-length center

expand(left, right):
    local_count = 0
    while left >= 0 and right < n and s[left] == s[right]:
        local_count++
        left--
        right++
    return local_count
```

**Tradeoffs:**
- Time: O(n²) - n centers, each expanding up to n/2 steps
- Space: O(1) - no extra memory
- Same time as DP but O(1) vs O(n²) space
- Beats brute force O(n³) by checking in-place without string allocation

---

## Variation: Longest Palindromic Substring
<!-- TODO: #5 LPS was previously filed under Two Pointers "Expand Around Center" in string.md coverage. On next /revise of #5, update solutions.md to reference this pattern instead. -->

**When to reach for this:**
- Find the single longest palindromic substring
- Need to track widest boundaries, not a count
- Return the substring or its start/length

**Mental Trigger:**
> "Expand from every center, track the widest result."

**Template:**
```
maxStart = 0, maxLen = 1
for each index i:
    expand(i, i)       // odd
    expand(i, i+1)     // even

expand(left, right):
    while left >= 0 and right < n and s[left] == s[right]:
        left--, right++
    // loop exits one step past the valid boundary - adjust back
    length = right - left - 1
    if length > maxLen: update maxStart, maxLen
```

**Tradeoffs:**
- Time: O(n²), Space: O(1)
- Boundary adjustment required after loop exits (unlike counting, where you increment inside)

---

## Variation: Unified 2n-1 Centers *(not yet explored)*
**Idea:** Instead of calling expand twice per index (odd + even), iterate `center` from 0 to `2n-2`. Derive `left = center/2`, `right = left + center%2`. Even `center` values give `left == right` (single char center); odd values give `right == left + 1` (adjacent pair). One loop handles all centers.
**Worth exploring when:** You want a tighter loop structure or are studying alternative implementations. No asymptotic difference.

---

## Try Next
- LeetCode 647 - Palindromic Substrings (Count Palindromic Substrings variation)
- LeetCode 5 - Longest Palindromic Substring

## Common Mistakes
- Missing even-length palindromes - only calling expand(i, i). Even-length palindromes need a two-character center: always call expand(i, i+1) for every i. (Both variations)
- Adding an unnecessary even-case pre-check before calling expand - checking s[i] == s[i+1] before the call. The while condition handles a starting mismatch by returning 0 immediately. No pre-check needed.
- Carrying over boundary correction from LPS to counting - in LPS you adjust indices after the loop to get valid boundaries. In counting, you increment inside the loop so every iteration is already valid. No correction needed. (Count Palindromic Substrings)

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Center Expansion"
SORT number asc
```
