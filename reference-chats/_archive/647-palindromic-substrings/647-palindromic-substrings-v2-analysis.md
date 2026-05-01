---
problem: 647
title: "Palindromic Substrings"
slug: 647-palindromic-substrings
source: LeetCode
difficulty: Medium
import-file: palindromic-subsrting-v2
category: analysis
---

# Analysis: Palindromic Substrings (v2)

**Chat covers:** Center Expansion (revision only - already mastered), Brute Force with reversal (new), Brute Force gap-based (new), DP transition revisited and connected to gap fill order. All verbal - no code written yet. User plans to code all four approaches.

**See also:** `palindromic-substring-analysis.md` (v1) for Center Expansion deep-dive and first DP introduction.

---

## Thinking Journey

### Center Expansion (Approach 1 - revision)

User recalled this cleanly: odd center at `(i,i)`, even center at `(i,i+1)`, call expand twice per index, count inside the while loop. One small fix came up: the guard condition for calling the even expansion. User initially said `i < string.length - 2`, then corrected to `i < string.length - 1` (or equivalently `i <= string.length - 2`) after working through a 4-length example. The reasoning was correct - you need `i+1` to be in bounds.

### Brute Force - Nested Loops + Reversal (Approach 2 - new)

User proposed two nested for loops (`i` from 0, `j` from 0) to enumerate all substrings, extract each with `string.Substring`, reverse it, and compare to the original. If equal, increment count. This is the simplest brute force formulation - no special fill order needed.

User correctly identified: time complexity O(n²) for the nested loops, and noted this is what center expansion is optimizing - same big-O but without allocating substrings or reversing.

### Brute Force Gap-Based (Approach 3 - new)

User independently proposed an alternative loop structure: outer for loop over `gap` (0 = single chars, 1 = two chars, etc.), inner while loop sliding both `start` and `end` forward together. This naturally enumerates all substrings in increasing length order.

Key moment: user identified this as a "sliding window of fixed length that grows each outer loop" - that framing is accurate.

Variable naming note from chat: outer variable first called `index`, then `substringLength`, then settled on `gap`. The distinction between `gap` (index difference) and `length` (gap + 1) was clarified: `gap = 0` means one character (`length = 1`).

### DP - Tabulation (Approach 4 - built on v1 foundation)

User arrived at the DP transition correctly and quickly, building on the v1 session:
- `dp[start][end]` = true if `s[start..end]` is a palindrome
- Transition: `dp[start][end] = (s[start] == s[end]) && dp[start+1][end-1]`
- Fill order: smaller substrings before larger - guaranteed by the gap-based outer loop

The key connection user made: **the gap-based brute force loop IS the DP fill order.** Switching from brute force to DP is just replacing the reversal check with a table lookup - the loop structure is identical. User articulated this clearly.

User noted the DP approach is not limited to counting - the same table could be used for longest palindromic substring, suggesting the dp state definition is transferable.

---

## Core Formula / Key Condition

**DP transition (re-confirmed in v2):**
`dp[start][end] = (s[start] == s[end]) && dp[start+1][end-1]`

Base cases:
- `gap == 0` (length 1): always true
- `gap == 1` (length 2): true if `s[start] == s[end]`

**Why gap-based fill order guarantees correctness:**
`dp[start+1][end-1]` has gap = `(end-1) - (start+1) = gap - 2`. It is always a strictly smaller gap than `dp[start][end]`, so it is always filled before the outer loop reaches `gap`. Smaller substrings before larger ones.

---

## Approaches

### Approach 1: Center Expansion (revision)
**Complexity:** O(n²) time, O(1) space
**Status:** Reasoning solid, code written in a prior session on related problem. Needs to be coded fresh for this problem.
**Even-case guard:** `i < s.Length - 1` (need `s[i+1]` to be in bounds)

### Approach 2: Brute Force - Nested Loops + Reversal
**Complexity:** O(n²) time, O(n) space (substring allocation per check)
**Structure:** Two nested for loops over `i` and `j`. Extract `s.Substring(i, j-i+1)`, reverse it, compare to original. Increment count if equal.
**Relationship to center expansion:** Same time complexity, but allocates strings per check. Center expansion avoids this by comparing character-by-character in-place.

### Approach 3: Brute Force Gap-Based
**Complexity:** O(n²) time, O(n) space (substring allocation per check)
**Structure:** Outer for loop over `gap` (0 to n-1). Inner while loop: `start=0`, `end=gap`, slide both forward while `end < n`. Same reversal check as Approach 2.
**Key insight:** Enumerates all substrings in length order (1-char, then 2-char, etc.). Structurally identical to the DP fill loop - the outer variable just controls substring length.

### Approach 4: Dynamic Programming - Tabulation
**Complexity:** O(n²) time, O(n²) space
**Structure:** Same gap-based loop as Approach 3, but instead of reversing the substring, store `dp[start][end] = true/false` and check `dp[start+1][end-1]` for the inner palindrome. Increment count when `dp[start][end] = true`.
**Why it works:** Gap-based fill order ensures inner results are always ready.

---

## Mistakes and Lessons

| Mistake | Category | What happened |
|---------|----------|---------------|
| Even-case guard was `i < s.Length - 2` (off by one) | Logic error | Should be `i < s.Length - 1` (need `i+1` in bounds); caught and self-corrected after tracing through a 4-char example |
| Named outer loop variable `index`, then `substringLength` before settling on `gap` | Variable lifecycle | The concept (difference between indices, not count of characters) was right; name lagged behind the concept |

---

## Key Insights for Pattern Library

- **Gap-based loop as DP scaffolding:** The outer `for gap in 0..n` loop does double duty - it's both a valid brute-force enumeration AND the DP fill order. Recognizing this makes the brute-force-to-DP transition mechanical: just replace the inner reversal check with a table store + lookup.
- **Brute force complexity parity:** Nested-loop brute force and center expansion are both O(n²). The difference is constant-factor overhead from string allocation, not asymptotic. This is a good argument for center expansion as "optimized brute force".
- **`gap` vs `length`:** When the outer loop variable controls the index difference, naming it `gap` is cleaner than `length` because `length = gap + 1`. The semantic matches the code.

---

## Constructs Used

- `string.Substring(start, length)` (Approaches 2, 3): extract substring between two indices
- `string.Reverse()` or manual reversal (Approaches 2, 3): palindrome check via string comparison
- `bool[,] dp` (Approach 4): 2D boolean table, sized `n x n`, filled bottom-up by gap

---

## Pattern Signals

Same as v1 analysis. Nothing new in v2 - user arrived at the same signals for center expansion and DP independently.

---

## Connections to Other Problems

- **Longest Palindromic Substring (#5)** - same DP table; different output (index/length vs count)
- User noted: DP state `dp[i][j]` is reusable for any palindrome substring query, not just counting

---

## Session Checklist

### Overall
- [x] Problem statement understood
- [x] Mental model solidified (all four approaches reasoned through)
- [ ] Reflection completed
- [ ] Saved to repo (`/save-problem`)

### Approach 1: Center Expansion
- [ ] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user
- [x] Even-case guard corrected: `i < s.Length - 1`

### Approach 2: Brute Force - Nested Loops + Reversal
- [ ] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user (note: O(n) per check due to substring allocation)

### Approach 3: Brute Force Gap-Based
- [ ] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user
- [ ] Confirm: `j = i + gap`, inner loop while `j < n`

### Approach 4: Dynamic Programming
- [x] Fill order understood (gap-based = smaller before larger)
- [x] Transition formula: `dp[i][j] = (s[i] == s[j]) && dp[i+1][j-1]`
- [x] Base cases understood (gap 0 = always true, gap 1 = check endpoints)
- [ ] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user
- [ ] Decide: count inline while filling, or second pass?
