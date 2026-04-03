## Problem
- **Name:** Longest Substring Without Repeating Characters
- **Number:** 3
- **Difficulty:** Medium
- **Tags:** (to be inferred)
- **Time Started:** 2026-04-03 19:03
- **Time Taken:** 3 hours

## Statement
Given a string s, find the length of the longest substring without duplicate characters.

**Examples:**
- Input: s = "abcabcbb" -> Output: 3 ("abc")
- Input: s = "bbbbb" -> Output: 1 ("b")
- Input: s = "pwwkew" -> Output: 3 ("wke")

**Constraints:**
- 0 <= s.length <= 5 * 10^4
- s consists of English letters, digits, symbols and spaces.

---

### Approach 1: Brute Force with HashSet
- **Status:** solved
- **Time Started:** 2026-04-03 19:03
- **Time Taken:** 44 minutes

#### Thinking
User initially thought to collect all unique chars in a HashSet and count them. Realized the flaw: that ignores the contiguous constraint. Corrected themselves.

Described brute force correctly: for each start index, scan forward until repeat found, record length, move to i+1.

Then independently discovered the sliding window optimization: instead of clearing and restarting, keep the same set, remove the character at the old start when sliding forward. Two pointers (left/right), HashSet tracks the current window.

#### Hints Given
(none)

#### Solution
**Time:** O(n^2) | **Space:** O(n)
**Key Idea:** For each starting index, seed a fresh HashSet with s[i], then scan forward adding characters until a repeat is found (break), recording the max window size seen.

#### Constructs
- HashSet - track characters in the current window (O(1) lookup and insert)

---

### Approach 2: Sliding Window with HashMap
- **Status:** solved
- **Time Started:** 2026-04-03 19:47
- **Time Taken:** 2 hours 16 minutes

#### Thinking
Started with HashSet + start++. Independently identified that start needs to jump to matchedIndex+1, not increment by 1. Switched to Dictionary<char,int> to store char -> last seen index. Also independently caught the edge case: start should never move left, so only update start when map[s[i]] >= start. Implemented with an explicit if condition instead of Math.Max - more expressive, same correctness.

#### Hints Given
(none)

#### Bugs
- start was incrementing by 1 instead of jumping to old index + 1 - window was sliding one step at a time instead of skipping past the stale duplicate
- start could move left when a character's previous occurrence was before the current window - fixed by only updating start when map[s[i]] >= start

#### Solution
**Time:** O(n) | **Space:** O(n)
**Key Idea:** One pass with two pointers (start, i) and a HashMap storing each char's last seen index. On repeat, jump start to max(start, map[char]+1) to skip past the stale portion of the window.

#### Constructs
- Dictionary<char,int> - store char -> last seen index for O(1) lookup and jump

---

### Approach 2.1: Sliding Window with ASCII Array (Span<int>)
- **Status:** solved
- **Time Started:** 2026-04-03 22:03
- **Time Taken:** (reference - not timed)

#### Thinking
Reference solution from LeetCode community. Same sliding window logic as Approach 2, but replaces Dictionary<char,int> with a stack-allocated array of 128 ints (Span<int> stackalloc), indexed by ASCII char value. Stores end+1 instead of end so that the default value 0 safely means "never seen." This is the user's original intuition about a pattern/boolean array - realised as integers to track position instead of booleans. Uses Math.Max variant instead of explicit if condition.

#### Hints Given
(none)

#### Solution
**Time:** O(n) | **Space:** O(1) - fixed 128-int array, independent of input size
**Key Idea:** Stack-allocated int[128] indexed by ASCII char value replaces Dictionary. Storing end+1 lets 0 mean "never seen." Window logic is identical to Approach 2.

#### Constructs
- Span<int> / stackalloc - stack-allocated fixed-size array, avoids heap allocation. O(1) space since size is constant (128).

## Reflection
- **Pattern signal:** Recognized that one pass works when you can remove stale elements from the left - HashMap lets you jump to the old index directly instead of crawling
- **Trickiest bugs:** (1) start needed to jump to oldIndex+1, not increment by 1 - window was not skipping past the duplicate. (2) start should never move left - only update when the stored index is >= current start
- **When to use sliding window:** When the problem asks for a longest/shortest contiguous chunk where validity depends on what is inside the range, and you can grow or shrink the window without restarting. Key signal: the word "substring" or "subarray" with a constraint.
