# Longest Substring Without Repeating Characters - Notes

## Mistakes Made

### Approach 2 - Sliding Window with HashMap
- **start++ instead of index jump** - incremented start by 1 on each repeat instead of jumping to lastSeenIndex+1. The window slid one step at a time, missing the optimization entirely.
- **start moving left** - when a char's previous occurrence was before the current window start, updating start to lastSeenIndex+1 would move it backwards, corrupting the window. Fixed by only updating start when lastSeen[char] >= start.

## Key Insights

- The key to O(n) sliding window is that start only ever moves right. Once a position is left behind, it is gone forever - you never need to revisit it.
- Storing index+1 in an int array (vs storing index) is a common trick to avoid ambiguity with the default value 0. Always ask: "is 0 a valid value in my domain? If so, what does the default mean?"

## Mantras

- "start never moves left - only jump forward"
- "the window only grows or slides, it never shrinks from the right"

## Patterns Used

- **Sliding Window** (Approach 1, 2, 2.1) - contiguous range tracking
- **HashMap** (Approach 2) - char -> last seen index
- **Presence Array** (Approach 2.1) - ASCII int array as a fixed-size alternative to Dictionary
