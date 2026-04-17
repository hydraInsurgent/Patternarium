## Problem

**Name:** 424. Longest Repeating Character Replacement
**Difficulty:** Medium
**Source:** LeetCode
**Tags:** sliding-window, hashmap, string
**Lists:** <!-- ask user -->
**Time Started:** <!-- imported from external chat - original session date unknown -->
**Time Taken:** <!-- fill on close -->

---

## Statement

You are given a string `s` and an integer `k`. You can choose any character of the string and change it to any other uppercase English character. You can perform this operation at most `k` times.

Return the length of the longest substring containing the same letter you can get after performing the above operations.

**Example 1:**
Input: s = "ABAB", k = 2
Output: 4
Explanation: Replace the two 'A's with two 'B's or vice versa.

**Example 2:**
Input: s = "AABABBA", k = 1
Output: 4
Explanation: Replace the one 'A' in the middle with 'B' and form "AABBBBA".

**Constraints:**
- 1 <= s.length <= 10^5
- s consists of only uppercase English letters.
- 0 <= k <= s.length

---

### Approach 1: Brute Force (Try All Substrings)

**Status:** solved
**Time Started:** <!-- imported -->
**Time Taken:** <!-- imported -->

#### Thinking

The key insight: for any substring, you don't need to know which character is most frequent - only how many times the most frequent character appears. The number of replacements needed is `windowSize - maxFreq`. If that is <= k, the substring is valid.

Tried all substrings [i...j]. For each starting index i, reset the freq map and maxFreq to zero. For each j, add s[j] to freq, update maxFreq by comparing only the newly added character (not scanning the full map), then check the validity condition.

#### Bugs

- **Global vs local maxFreq** - initially made maxFreq a global variable shared across all i values. Fix: reset maxFreq = 0 per i.
- **Full map scan for maxFreq** - tried recomputing maxFreq by scanning the entire map each step. Fix: only compare `max(maxFreq, freq[s[j]])` since only one character was added.

#### Constructs

- `Dictionary<char, int>` - frequency tracking per starting index

#### Solution

**Time:** O(n^2)
**Space:** O(1) - at most 26 keys in freq map
**Key Condition:** `windowSize - maxFreq <= k`
**Key Idea:** Try all substrings; for each, track the frequency of the most common character incrementally and check the validity condition.

---

### Approach 2: Sliding Window

**Status:** solved
**Time Started:** <!-- imported -->
**Time Taken:** <!-- imported -->

#### Thinking

After brute force hit TLE, the key goal shift was: instead of checking all substrings explicitly, maintain a single valid window and adjust it dynamically.

Start with l=0, r=0. For each r, add s[r] to the freq map and update maxFreq (only ever increases). Then check validity: if `(r - l + 1) - maxFreq > k`, the window is invalid - shrink by incrementing l (and decrementing freq[s[l]]), but do NOT decrease maxFreq. Repeat until valid. Then update maxLength and advance r.

The hardest part to trust: maxFreq is never decremented when shrinking. It may be stale (higher than the actual window max), but this is safe. Any window size accepted with a stale maxFreq was already achievable when maxFreq was genuinely that high. A larger valid window only forms when a real frequency increase occurs, and that is exactly when maxFreq is updated correctly.

The O(n) argument: both l and r only move forward. Total pointer movements across the whole algorithm are at most 2n.

Why l can never cross r: a window of size 1 satisfies `1 - 1 = 0 <= k` always, so the while(invalid) loop always stops before left exceeds right.

#### Bugs

- **l=0, r=n-1 starting condition** - initial instinct was to start with the full string and shrink. Fix: start l=0, r=0 and grow.
- **Single l++ (if) instead of while loop** - thought one shrink was always enough. Fix: while(invalid) loop, because one expansion may require multiple contractions.
- **Proposing l++ and r++ together** - thought this would "slide" the window. Fix: this just shifts the same-size invalid window, not fixes it.
- **Checked validity before adding s[r]** - the window was not fully formed when the check ran. Fix: add s[r] to freq and update maxFreq first, then check.
- **Proposed decrementing maxFreq on shrink** - breaks on ties (e.g., {A:3, B:3} - removing A should keep max at 3 because B=3, but proposed logic sets max to 2). Fix: never decrement maxFreq.
- **Forgot right++** - infinite loop. Fix: r++ at end of outer loop.
- **Did not update freq on shrink** - map[s[left]]-- was missing, breaking window consistency and causing index-out-of-bounds. Fix: always decrement freq[s[left]] before l++.
- **Wrong boundary: `right < s.Length - 1`** - missed the last element. Fix: `right < s.Length`.
- **Inverted condition** - accidentally wrote `if (valid)` to trigger shrink instead of `while (invalid)`. Caused indices to drift and crash. Fix: `while (!(r - l + 1 - maxFreq <= k))`.

#### Constructs

- `Dictionary<char, int>` - frequency tracking for current window
- `Math.Max` - maxFreq and maxLength tracking

#### Solution

**Time:** O(n)
**Space:** O(1) - at most 26 keys in freq map
**Key Condition:** `windowSize - maxFreq <= k` (same condition, maintained dynamically)
**Key Idea:** Maintain a single window - expand r each step, shrink from l when invalid, never decrease maxFreq.

---

## Patterns

- **Sliding Window - Variable Window**: expand r unconditionally each step, shrink from l when the validity condition breaks. Condition is `windowSize - maxFreq <= k`. The stale-maxFreq optimization is specific to this problem and is what makes the approach O(n).

---

## Reflection

**Where thinking diverged:** The mental picture was wrong - kept imagining a window shrinking from both edges, or some center-out approach. The correct picture is a window that only ever opens to the right and contracts from the left. Once that picture was clear, the pointer behavior followed naturally.

**Stale maxFreq:** It makes the validity check more lenient - overestimating maxFreq means windowSize - maxFreq is smaller, so more windows pass. But maxFreq only increases when a real frequency increase happens, so any length we accept was already genuinely achievable at an earlier point. The answer is never inflated.

**Sliding window signal:** "Longest valid substring" + "validity is a condition you can check and maintain incrementally as you add/remove elements." Continuous elements in a subarray/substring that you want to maximize or minimize.

---

## Import Notes

### Constructs Identified

- **`Dictionary<char, int>`** - used in both approaches as a frequency map. In brute force: resets per starting index i. In sliding window: maintained as a live window map - decremented when a character exits the left side.
- **`Math.Max`** - used in both approaches: for maxFreq (only increments, never decrements) and for maxLength.

### Remaining Approaches

No additional approaches were discussed. Both brute force and sliding window are complete.

### Session Checklist

#### Overall
- [x] Problem statement understood
- [x] Core formula derived: `windowSize - maxFreq <= k`
- [ ] Reflection completed
- [ ] Saved to repo (`/save-problem`)

#### Approach 1: Brute Force
- [x] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user
- [ ] Dry run on a concrete example

#### Approach 2: Sliding Window
- [x] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user
- [ ] Dry run on a concrete example (e.g., "AABABBA", k=1)
- [ ] Confirm: stale maxFreq invariant understood (any accepted length was achievable when maxFreq was genuinely that high)
