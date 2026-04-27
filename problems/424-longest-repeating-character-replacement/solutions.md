---
problem: 424
problem-title: Longest Repeating Character Replacement
difficulty: Medium
category: solutions
patterns: [Sliding Window]
constructs: [dictionary, math-functions]
ds-used: [string, hashmap]
algorithms: []
tags: [sliding-window, frequency-map, string, variable-window]
approaches:
  - name: Brute Force
    file: solutions/brute-force.cs
    patterns: []
    constructs: [dictionary, math-functions]
    ds-used: [string, hashmap]
    time: "O(n^2)"
    space: "O(1)"
  - name: Sliding Window
    file: solutions/sliding-window.cs
    patterns: [Sliding Window]
    variation: Shrink-Based
    constructs: [dictionary, math-functions]
    ds-used: [string, hashmap]
    ds-notes:
      hashmap: "frequency map for current window - decremented on left exit; maxFreq monotonically non-decreasing"
    time: "O(n)"
    space: "O(1)"
  - name: Sliding Window - Monotonic
    file: solutions/sliding-window-monotonic.cs
    patterns: [Sliding Window]
    variation: Shrink-Based
    constructs: [dictionary, math-functions]
    ds-used: [string, hashmap]
    time: "O(n)"
    space: "O(1)"
---

# Longest Repeating Character Replacement - Solutions

## Approaches

### Approach 1: Brute Force
**Code:** [brute-force.cs](solutions/brute-force.cs)
**Time:** O(n^2) | **Space:** O(1)

**Thinking:** For any substring, only the count of the most frequent character matters - not which character it is. For each starting index, reset the freq map and maxFreq. For each ending index, extend by one character and update maxFreq by comparing only the newly added character. Check `windowSize - maxFreq <= k`.

---

### Approach 2: Sliding Window
**Code:** [sliding-window.cs](solutions/sliding-window.cs)
**Time:** O(n) | **Space:** O(1)

**Key Condition:** `windowSize - maxFreq <= k`

**Thinking:** Instead of checking all substrings, maintain a single valid window. Start l=0, r=0. For each r, include s[r] first (update freq, update maxFreq), then check validity. If invalid, shrink from left with a `while` loop until valid. maxFreq is never decremented on shrink - stale maxFreq is safe because any length accepted was already achievable when maxFreq was genuinely that high. Both pointers only move forward, so total movements are at most 2n.

---

### Approach 3: Sliding Window - Monotonic
**Code:** [sliding-window-monotonic.cs](solutions/sliding-window-monotonic.cs)
**Time:** O(n) | **Space:** O(1)

**Key Condition:** `windowSize - maxFreq <= k`

**Thinking:** Same logic as Approach 2 but the window size never shrinks. Use `if` instead of `while` for shrinking. When invalid, one `left++` shrinks the window back to the previous max size and `right++` slides it forward. Not restoring validity - sliding at the same size until a genuinely better window appears. Only works for max-length problems.

---

## Patterns

- [Sliding Window - Shrink-Based](../../patterns/sliding-window.md#variation-shrink-based) (Approaches 2 and 3) - variable window; validity condition `windowSize - maxFreq <= k`; maxFreq monotonically non-decreasing and never decremented on shrink

## Reflection

- **Key insight:** The window grows right and contracts left - never the reverse. The stale maxFreq is the hardest part to trust: it makes validity more lenient, but any accepted length was achievable when maxFreq was genuinely that high.
- **Future strategy:** "Longest valid substring" + "validity is a condition you can check and maintain incrementally as you add/remove elements" = sliding window.
- **Stale maxFreq:** This problem's specific invariant - maxFreq only increases. Never decrement it on shrink. The validity check absorbs the staleness without corrupting the answer.
