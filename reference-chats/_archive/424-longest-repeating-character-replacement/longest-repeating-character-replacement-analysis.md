---
problem: 424
title: "Longest Repeating Character Replacement"
slug: 424-longest-repeating-character-replacement
source: LeetCode
difficulty: Medium
import-file: longest repeating character replacement.txt
category: analysis
---

# Analysis: Longest Repeating Character Replacement

**Chat covers:** Both approaches - brute force (first import) and sliding window (second import, fully derived and debugged from first principles)

---

## Thinking Journey

The session spanned two separate chats covering the full arc from misconception to working sliding window implementation.

**First chat (brute force):**

1. **Global character assumption** - thought the answer required finding the globally most frequent character first, then checking substrings against it. Took multiple rounds to correct.
2. **Wrong output type** - thought the goal was to count how many valid substrings exist, not find the max length of one.
3. **Invented a "50% threshold"** - when told maxFreq is local, tried to invent a rule that the dominant character must appear more than 50% of the time to trust the formula. This was unnecessary.
4. **Global vs local maxFreq** - tried to make maxFreq a global variable shared across all substrings. Took effort to understand it must reset per starting index `i`.
5. **maxFreq update method** - thought maxFreq must be recomputed by scanning the entire freq map each time. Realized only the newly added character matters.
6. **Substring counting error** - calculated number of substrings as O(n!) by multiplying group sizes instead of summing them.

The mental model clicked when the user understood: you don't need to know which character is most frequent - only how many times the most frequent character appears.

**Second chat (sliding window):**

7. **Two-pointer starting position** - initially set l=0 and r=n-1 (shrink from outside), had to correct to l=0, r=0 (grow from left).
8. **"Maintain valid window" vs "check substrings"** - took several rounds to shift from the brute force mental model of checking substrings one by one to the idea of a single moving window.
9. **Single shrink vs while loop** - thought one `l++` would always fix an invalid window. Took a concrete counterexample (windowSize=10, maxFreq=5, k=2) to show one move is not always enough.
10. **Moving both pointers together** - proposed `l++ and r++` as a way to "slide" - corrected: this just shifts the same-size window, it doesn't fix invalidity.
11. **Order of operations** - checked validity before adding s[r] to the window. Corrected: add first, then check.
12. **Decreasing maxFreq on shrink** - proposed tracking whether the removed character was the max and decrementing accordingly. Shown to break on ties (e.g., {A:3, B:3} - removing A makes stale max still 3 because B=3, but the proposed logic would set max to 2).
13. **Why stale maxFreq is safe** - this was the hardest concept. The user eventually arrived at: any length accepted with a stale maxFreq was already achievable at an earlier point when maxFreq was truly that high. A larger window is only ever accepted when real frequency increases.
14. **Forgot right++** - first attempt at code had no right++ inside the loop (infinite loop bug).
15. **Not updating freq on shrink** - initial code shrank l without decrementing freq[s[left]], breaking window consistency.
16. **Wrong boundary** - `right < s.Length - 1` missed the last element.
17. **Inverted condition** - accidentally checked `if (valid)` instead of `while (invalid)`, causing indices to drift and eventually an index-out-of-bounds exception.

The final insight that locked sliding window: `l` and `r` each move forward at most `n` times - total moves <= 2n - so the algorithm is O(n) even though locally it feels like expand/shrink alternation.

---

## Core Formula / Key Condition

```
windowSize - maxFreq <= k
```

This is the validity condition. It is character-agnostic - only the count of the most frequent character matters, not which character it is. This formula is the same in both approaches; the difference is in how the window is managed.

**Sliding window invariant:** `maxFreq` is monotonically non-decreasing. It is never decremented when shrinking. This means it may be "stale" (higher than the actual window max), but this only makes the validity check more lenient - and any length accepted with a stale maxFreq was already achievable when maxFreq was genuinely that high.

---

## Approaches

### Approach 1: Brute Force

**Complexity:** O(n²) time, O(1) space (26 uppercase letters max in freq map)

**Structure:**
```
for each starting index i:
    reset freq map
    reset maxFreq = 0

    for each j from i to n:
        add s[j] to freq
        maxFreq = max(maxFreq, freq[s[j]])   // only compare new char, not full map
        length = j - i + 1
        if (length - maxFreq <= k):
            maxLength = max(maxLength, length)

return maxLength
```

**Key optimization within brute force:** freq map resets per `i`, not per `j`. This makes it O(n²) not O(n³).

**Why maxFreq never needs to decrease in brute force:** In this approach we only add characters, never remove. So the maximum frequency of any character can only stay the same or increase.

---

### Approach 2: Sliding Window

**Complexity:** O(n) time, O(1) space (26 uppercase letters max in freq map)

**Structure:**
```
l = 0, r = 0
freq = {}
maxFreq = 0, maxLength = 0

while r < n:
    add s[r] to freq
    maxFreq = max(maxFreq, freq[s[r]])

    while (r - l + 1 - maxFreq > k):   // window invalid
        freq[s[l]]--
        l++

    maxLength = max(maxLength, r - l + 1)
    r++

return maxLength
```

**Key insight:** `maxFreq` is never decremented when shrinking. Any window size accepted with a stale maxFreq was already achievable earlier when maxFreq was truly that high. We only need a larger window when a genuinely higher frequency appears, and that is exactly when maxFreq gets updated correctly.

**Why O(n):** Both `l` and `r` only move forward. Total moves across the whole algorithm <= 2n.

**Key invariant:** A window of size 1 always satisfies `1 - 1 = 0 <= k`, so `l` can catch up to `r` but never cross it.

---

## Mistakes and Lessons

| Mistake | Category | What happened |
|---------|----------|---------------|
| Assumed globally most frequent char determines answer | Problem comprehension | Missed that each substring is evaluated independently |
| Counted substrings as factorial (multiplied group sizes) | Math error | Was grouping correctly, aggregated with multiply instead of add |
| Made maxFreq global | Variable lifecycle | Thought tracking the global max would help, but it makes replacements negative |
| Tried to add 50% threshold rule | Unnecessary complexity | Didn't trust the simple formula, invented extra conditions |
| Thought maxFreq requires full map scan | Logic error | Didn't realize only one character changes per step |
| Confused "count valid substrings" with "max length" | Problem comprehension | Drifted toward counting instead of maximizing |
| Set l=0, r=n-1 for sliding window | Mental model carryover | Carried shrink-from-outside thinking from a different approach |
| One `l++` is always enough to fix invalid window | Logic error | Counterexample: windowSize=10, maxFreq=5, k=2 - one shrink is still invalid |
| Proposed moving l++ and r++ together when invalid | Logic error | Just shifts the window, does not fix invalidity |
| Checked validity before adding s[r] | Variable lifecycle | Window not yet fully formed when check runs |
| Proposed decrementing maxFreq when removing a char | Logic error | Breaks on ties: if {A:3, B:3} and remove A, actual max is still 3 (B) |
| Forgot right++ (infinite loop) | Logic error | Window never advanced |
| Did not update freq map when shrinking | Variable lifecycle | Broke window consistency, map no longer reflected actual window |
| Used `right < s.Length - 1` | Logic error | Off-by-one: missed the last element |
| Inverted the while condition (checked valid instead of invalid) | Logic error | Caused index drift, eventually index-out-of-bounds exception |

---

## Key Insights for Pattern Library

**The identity insight:** We don't track which character is most frequent - only its count. The condition `windowSize - maxFreq <= k` is character-agnostic.

**Stale maxFreq is intentional, not a bug:** maxFreq never decreases. It may overestimate the actual window max after shrinking. This makes the validity check more lenient, but it is safe: any length we record was already achievable at an earlier point when maxFreq was genuinely that high.

**Why sliding window is O(n):** Even though locally it feels like "expand, shrink, expand, shrink", globally both pointers only move forward. Total pointer movements <= 2n.

**The size-1 window invariant:** A window containing one character has `windowSize - maxFreq = 0`, which is always <= k. This is why `l` can never cross `r` - the inner while loop always stops at `left == right`.

**Expand first, then check:** Add s[r] to the window before running the validity check. The window must be fully formed before it can be evaluated.

---

## Constructs Used

- `Dictionary<char, int>` - frequency tracking for current window
- `Math.Max` - maxFreq and maxLength tracking

---

## Pattern Signals

This problem signals sliding window because:
- We want the longest valid substring
- Validity depends on a simple condition about window contents (`windowSize - maxFreq <= k`)
- The condition involves a "count" that can be maintained incrementally
- We want to avoid recomputing from scratch for each starting index

The shift from brute force to sliding window is enabled by realizing: we don't need to re-examine every substring. We can maintain a single window and adjust it dynamically.

---

## Connections to Other Problems

- Longest Substring Without Repeating Characters - same sliding window structure, different validity condition
- Minimum Window Substring - variable window, find smallest valid instead of largest

---

## Session Checklist

### Overall
- [x] Problem statement understood
- [x] Core formula derived: `windowSize - maxFreq <= k`
- [ ] Reflection completed
- [ ] Saved to repo (`/save-problem`)

### Approach 1: Brute Force
- [x] Validity condition understood
- [x] freq map placement understood (reset per `i`, not per `j`)
- [x] Incremental maxFreq update understood (`max(maxFreq, freq[newChar])`)
- [x] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user (expected: O(n²))
- [ ] Space complexity confirmed by user (expected: O(1) / 26 chars)
- [ ] Dry run on a concrete example

### Approach 2: Sliding Window
- [x] Mental model: maintain valid window, not check all substrings
- [x] Start condition: l=0, r=0 (not l=0, r=n-1)
- [x] Expand first (add s[r]), then check validity
- [x] Inner while loop for shrinking (not single if)
- [x] maxFreq is monotonically non-decreasing (never decremented)
- [x] Stale maxFreq is safe - any accepted length was achievable when maxFreq was genuinely that high
- [x] O(n) because both pointers only move forward (total moves <= 2n)
- [x] l can never cross r (size-1 window always valid)
- [x] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user (expected: O(n))
- [ ] Space complexity confirmed by user (expected: O(1) / 26 chars)
- [ ] Dry run on a concrete example (e.g., "AABABBA", k=1)
