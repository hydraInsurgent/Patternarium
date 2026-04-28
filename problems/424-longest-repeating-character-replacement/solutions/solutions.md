---
problem: 424
problem-title: Longest Repeating Character Replacement
difficulty: Medium
category: solutions
patterns: [Sliding Window]
constructs: [dictionary, math-functions]
ds-used: [string, hashmap]
algorithms: []
concepts: []
approaches:
  - name: Brute Force
    file: brute-force.cs
    patterns: []
    constructs: [dictionary, math-functions]
    ds-used: [string, hashmap]
    time: "O(n^2)"
    space: "O(1)"
  - name: Sliding Window
    file: sliding-window.cs
    patterns: [Sliding Window]
    variation: Shrink-Based
    constructs: [dictionary, math-functions]
    ds-used: [string, hashmap]
    ds-notes:
      hashmap: "frequency map for current window - decremented on left exit; maxFreq monotonically non-decreasing"
    time: "O(n)"
    space: "O(1)"
  - name: Sliding Window - Monotonic
    file: sliding-window-monotonic.cs
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
**Code:** [brute-force.cs](brute-force.cs)
**Time:** O(n^2) | **Space:** O(1)

**Thinking:** For any substring, only the count of the most frequent character matters - not which character it is. For each starting index, reset the freq map and maxFreq. For each ending index, extend by one character and update maxFreq by comparing only the newly added character. Check `windowSize - maxFreq <= k`.

**Mistakes:**
- **Global vs local maxFreq** - maxFreq must reset per outer loop (per starting index). A global max causes `windowSize - globalMax` to go negative, making every window look valid.
- **Full map scan for maxFreq update** - only one character changes per step. Only compare `max(maxFreq, freq[s[j]])` - no need to scan the full map.

---

### Approach 2: Sliding Window
**Code:** [sliding-window.cs](sliding-window.cs)
**Time:** O(n) | **Space:** O(1)

**Key Condition:** `windowSize - maxFreq <= k`

**Thinking:** Instead of checking all substrings, maintain a single valid window. Start l=0, r=0. For each r, include s[r] first (update freq, update maxFreq), then check validity. If invalid, shrink from left with a `while` loop until valid. maxFreq is never decremented on shrink - stale maxFreq is safe because any length accepted was already achievable when maxFreq was genuinely that high. Both pointers only move forward, so total movements are at most 2n.

**Mistakes:**
- **Starting at l=0, r=n-1** - tried to start with the full string and shrink inward. Must start at l=0, r=0 and grow outward.
- **Checking validity before adding s[r]** - the window must be fully formed (s[r] added to freq) before validity is checked.
- **Proposed decrementing maxFreq on shrink** - breaks on ties: if {A:3, B:3} and A exits, actual max is still 3 (B). Fix: never decrement maxFreq.
- **Single if instead of while for shrinking** - one expansion can require multiple contractions. Must shrink until the condition holds.
- **Moving l++ and r++ together when invalid** - just shifts the same-size invalid window; does not fix invalidity.
- **Forgot right++** - infinite loop; the right pointer must advance each outer loop iteration.
- **Did not decrement freq[s[left]] before left++** - broke window consistency; map no longer reflected the actual window contents.
- **Wrong boundary `right < s.Length - 1`** - off-by-one, missed the last element.
- **Inverted condition** - accidentally shrunk when valid instead of when invalid; caused index drift and index-out-of-bounds crash.

---

### Approach 3: Sliding Window - Monotonic
**Code:** [sliding-window-monotonic.cs](sliding-window-monotonic.cs)
**Time:** O(n) | **Space:** O(1)

**Key Condition:** `windowSize - maxFreq <= k`

**Thinking:** Same logic as Approach 2 but the window size never shrinks. Use `if` instead of `while` for shrinking. When invalid, one `left++` shrinks the window back to the previous max size and `right++` slides it forward. Not restoring validity - sliding at the same size until a genuinely better window appears. Only works for max-length problems.

---

## Patterns

- Sliding Window - Shrink-Based (Approaches 2 and 3) - variable window; validity condition `windowSize - maxFreq <= k`; maxFreq monotonically non-decreasing and never decremented on shrink

## Reflection

- **Key insight:** The window grows right and contracts left - never the reverse. The stale maxFreq is the hardest part to trust: it makes validity more lenient, but any accepted length was achievable when maxFreq was genuinely that high.
- **Future strategy:** "Longest valid substring" + "validity is a condition you can check and maintain incrementally as you add/remove elements" = sliding window.
- **Stale maxFreq:** This problem's specific invariant - maxFreq only increases. Never decrement it on shrink. The validity check absorbs the staleness without corrupting the answer.
- **Notes Insights:**
  - `windowSize - maxFreq <= k` is character-agnostic - only the count of the most frequent character matters, not which character it is.
  - maxFreq is monotonically non-decreasing. Stale is safe: any length we accept was achievable when maxFreq was genuinely that high.
  - Sliding window is O(n) because both pointers only move forward. Locally it feels like expand-shrink-expand, but globally each pointer moves at most n times.
  - A window of size 1 always satisfies `1 - 1 = 0 <= k`, so left can catch up to right but never cross it.
- **Mantras:**
  - "Add first, check second." - window state updates before validity check.
  - "Grow right, shrink left, never reverse."
  - "Stale maxFreq is safe - it only lets through sizes already achievable."
