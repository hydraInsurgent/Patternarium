---
problem: 3
problem-title: Longest Substring Without Repeating Characters
difficulty: Medium
category: solutions
patterns:
  - Sliding Window
  - HashMap
  - Presence Array
constructs:
  - hashset
  - dictionary
  - span
ds-used:
  - string
  - hashmap
  - hashset
  - array
algorithms: []
concepts: []
approaches:
  - name: Brute Force with HashSet
    file: brute-force.cs
    patterns:
      - Sliding Window
    variation: Brute Force
    constructs:
      - hashset
    ds-used:
      - string
      - hashset
    ds-notes:
      hashset: presence check in brute force window
    time: O(n^2)
    space: O(n)
  - name: Sliding Window with HashMap
    file: sliding-window-hashmap.cs
    patterns:
      - Sliding Window
      - HashMap
    variation: Last Seen Index
    constructs:
      - dictionary
    ds-used:
      - string
      - hashmap
    ds-notes:
      hashmap: char -> last seen index for window boundary jump
    time: O(n)
    space: O(n)
  - name: Sliding Window with ASCII Array (Span<int>)
    file: sliding-window-span.cs
    patterns:
      - Sliding Window
      - Presence Array
    variation: ASCII Span Array
    constructs:
      - span
    ds-used:
      - string
      - array
    ds-notes:
      array: Span<int> stack-allocated ASCII index array; fixed size independent of input
    time: O(n)
    space: O(1)
---

# Longest Substring Without Repeating Characters - Solutions

## Approaches

### Approach 1: Brute Force with HashSet
**Code:** [brute-force.cs](brute-force.cs)
**Time:** O(n^2) | **Space:** O(n)

**Thinking:** For each starting position, seed a fresh HashSet with s[i], then scan forward adding characters until a repeat is found. Record the max window size. Restarts completely from i+1 each time.

---

### Approach 2: Sliding Window with HashMap
**Code:** [sliding-window-hashmap.cs](sliding-window-hashmap.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** One pass with two pointers (start, end) and a Dictionary storing each char's last seen index. On repeat, jump start directly to lastSeenIndex+1 - skipping the stale portion of the window in one move. Guard condition ensures start never moves left: only update start when the stored index is >= current start.

**Mistakes:**
- **start++ instead of index jump** - incremented start by 1 on each repeat instead of jumping to lastSeenIndex+1. The window slid one step at a time, missing the optimization entirely.
- **start moving left** - when a char's previous occurrence was before the current window start, updating start to lastSeenIndex+1 would move it backwards, corrupting the window. Fixed by only updating start when lastSeen[char] >= start.

---

### Approach 2.1: Sliding Window with ASCII Array (Span<int>)
**Code:** [sliding-window-span.cs](sliding-window-span.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Reference solution from LeetCode community. Same sliding window logic as Approach 2, but replaces Dictionary<char,int> with a stack-allocated int[128] indexed by ASCII char value. Stores end+1 so that the default value 0 safely means "never seen." This is the original intuition about using a pattern/boolean array - realized as integers to track position.

---

## Patterns

- Sliding Window - Shrink-Based (Approach 1) - restart-based window; establishes the concept before optimization
- Sliding Window - Index Jump (Approaches 2 & 2.1) - find longest window with no repeated chars by jumping start directly on violation
- HashMap - Last Seen Index (Approach 2) - store char -> last seen index for O(1) jump to window start
- Presence Array - Integer Slot (Approach 2.1) - int[128] indexed by ASCII value replaces Dictionary; stores end+1 to distinguish "never seen" from "seen at 0"

## Reflection

- **Key insight:** One pass works when you can skip the stale left portion in one jump. A HashMap (or ASCII array) gives you the exact index to jump to - no crawling needed.
- **Trickiest bugs:** (1) start++ instead of start = lastSeen+1 - window slid one step at a time instead of jumping. (2) start moving left when a char's previous occurrence was before the current window - fixed with a guard condition.
- **When to use sliding window:** When the problem asks for a longest/shortest contiguous chunk where validity depends on what is inside the range, and you can grow or shrink the window without restarting. Key signal: "substring" or "subarray" with a constraint.
- **Notes Insights:**
  - The key to O(n) sliding window is that start only ever moves right. Once a position is left behind, it is gone forever - you never need to revisit it.
  - Storing index+1 in an int array (vs storing index) is a common trick to avoid ambiguity with the default value 0. Always ask: "is 0 a valid value in my domain? If so, what does the default mean?"
- **Mantras:**
  - "start never moves left - only jump forward"
  - "the window only grows or slides, it never shrinks from the right"
