# Sliding Window

**display_name:** Sliding Window - Contiguous Range Tracking

## Core Idea

Maintain a window (a contiguous range of elements) using two pointers - start and end. Instead of restarting from scratch on each failure, slide the window forward by advancing start. Only ever moves right, never left. Trades the restart cost for a single pass.

## When to Reach for This

- Problem asks for the longest or shortest contiguous substring or subarray
- Validity of the range depends on what is inside it (a constraint on the window contents)
- You can make a local decision at each step to grow or shrink the window
- Key signal: the word "substring" or "subarray" with a constraint like "without repeating" or "sum <= k"

## Mental Trigger

> "Am I looking for the longest/shortest contiguous chunk where some condition holds?"
> "Can I extend or shrink this range without restarting from scratch?"
> "Does adding one element to the right tell me whether to shrink from the left?"

## Template

```csharp
int start = 0;
int maxLength = 0;
// window state: HashSet, Dictionary, count, sum - depends on the constraint

for (int end = 0; end < s.Length; end++)
{
    // Add s[end] to the window state

    // Shrink from left while the window violates the constraint
    while (/* window is invalid */)
    {
        // Remove s[start] from window state
        start++;
    }

    // Window is now valid - update answer
    maxLength = Math.Max(maxLength, end - start + 1);
}
```

For problems where you can jump start directly (using a HashMap of last-seen indices):

```csharp
int start = 0;
Dictionary<char, int> lastSeen = new Dictionary<char, int>();

for (int end = 0; end < s.Length; end++)
{
    if (lastSeen.ContainsKey(s[end]) && lastSeen[s[end]] >= start)
    {
        // Jump start past the previous occurrence - never move left
        start = lastSeen[s[end]] + 1;
    }
    lastSeen[s[end]] = end;
    maxLength = Math.Max(maxLength, end - start + 1);
}
```

## Tradeoffs

| | Value |
|--|--|
| Time | O(n) - each element enters and exits the window at most once |
| Space | O(k) where k is the window state size (HashSet, Dictionary, etc.) |
| vs Brute Force | Eliminates the restart cost - O(n) instead of O(n^2) |
| Requires | The constraint must be checkable locally (depends on window contents, not global state) |

## Critical Rule: Start Never Moves Left

When jumping start based on a stored index, always guard against moving backward:
- Wrong: `start = lastSeen[c] + 1` - could move start left if the stored index is before the current window
- Right: `start = Math.Max(start, lastSeen[c] + 1)` or an explicit guard condition

## Solved Problems

- **Longest Substring Without Repeating Characters** (Approach 1, 2, 2.1) - find longest window with no duplicate chars

## Try Next

- Minimum Window Substring - smallest window containing all chars of a target string
- Longest Substring with At Most K Distinct Characters - sliding window with a frequency map

## Common Mistakes

- **start++ instead of index jump** - crawling start forward one at a time when a HashMap lets you jump directly. O(n) loop but with unnecessary iterations.
- **start moving left** - not guarding against the case where a char's previous occurrence is before the current window. Use Math.Max or an explicit if condition.
- **Not updating state on both grow and shrink** - window state (HashSet, Dictionary, sum) must be updated when elements enter from the right AND when they exit from the left.
