---
name: sliding-window
display_name: Sliding Window
category: pattern
variations:
  - name: Shrink-Based
    ds: [string, hashset]
  - name: Index Jump
    ds: [string, hashmap, array]
ds-primary: [string, hashmap, hashset, array]
---

# Sliding Window

## Core Idea

Maintain a window (a contiguous range of elements) using two pointers - start and end. Instead of restarting from scratch on each failure, slide the window forward by advancing start. Start only ever moves right, never left. Trades the restart cost for a single pass.

The key question: can you grow or shrink the window based on a local condition, without restarting?

## When to Reach for This

- Problem asks for the longest or shortest contiguous substring or subarray
- Validity of the range depends on what is inside it (a constraint on the window contents)
- You can make a local decision at each step to grow or shrink the window
- Key signal: the word "substring" or "subarray" with a constraint like "without repeating" or "sum <= k"

**Mental Trigger:**
> "Am I looking for the longest/shortest contiguous chunk where some condition holds?"
> "Can I extend or shrink this range without restarting from scratch?"
> "Does adding one element to the right tell me whether to shrink from the left?"

## Variation: Shrink-Based

**When to reach for this:**
- General window validity constraint (not character-position based)
- Need to shrink one step at a time when the window becomes invalid
- Use HashSet or a frequency map to track window contents
- Validity depends on a frequency condition like `windowSize - maxFreq <= k` - where the condition involves the count of the most common element in the window

**Template:**
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

**Solved Problems:**
- **Longest Substring Without Repeating Characters** (problems/3-longest-substring-without-repeating-characters/solutions/brute-force.cs) - restart approach; establishes the window concept before optimization
- **Longest Repeating Character Replacement** (problems/424-longest-repeating-character-replacement/solutions/sliding-window.cs) - frequency-based validity condition; stale maxFreq invariant

---

## Variation: Index Jump

**When to reach for this:**
- Window contents are characters or bounded integers - you can store exact positions
- On a violation, you know exactly where the previous occurrence was - jump there directly
- Use a HashMap or ASCII array indexed by value to store last-seen positions

**Template:**
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

**Critical Rule: Start Never Moves Left**

Guard the jump with `>= start` to ensure the stored index is within the current window. A stale stored index (before the window) must not move start backward.

**Solved Problems:**
- **Longest Substring Without Repeating Characters** (problems/3-longest-substring-without-repeating-characters/solutions/sliding-window-hashmap.cs) - HashMap index jump
- **Longest Substring Without Repeating Characters** (problems/3-longest-substring-without-repeating-characters/solutions/sliding-window-span.cs) - ASCII int[128] index jump

---

## Tradeoffs

| | Shrink-Based | Index Jump |
|--|--|--|
| Time | O(n) - each element enters/exits at most once | O(n) - each element visited once |
| Space | O(k) - window state size | O(k) - map/array size |
| Flexibility | Works for any constraint | Requires knowing exact prior position |
| Simplicity | More general | Faster in practice (no inner while loop) |

## Try Next

- Minimum Window Substring - smallest window containing all chars of a target string (Shrink-Based)
- Longest Substring with At Most K Distinct Characters - sliding window with frequency map (Shrink-Based)

## Common Mistakes

- **start++ instead of index jump** - crawling start forward one step when a HashMap lets you jump directly (Index Jump)
- **Starting at l=0, r=n-1** - window shrinks from both edges. Sliding window always starts at l=0, r=0 and grows right (Shrink-Based)
- **Checking validity before adding s[r]** - window state must be updated before the validity check; the element must be in the window to evaluate it (Shrink-Based)
- **Single if instead of while for shrinking** - one expansion can require multiple contractions before the condition is satisfied again (Shrink-Based)
- **Decrementing a monotonic tracking variable on shrink** - when a running max is tracked incrementally (only ever increases), decrementing it on shrink breaks correctness when multiple elements share the max value. Let it stay stale; any window it allows was already achievable (Shrink-Based, frequency problems)

## Solved Problems

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Sliding Window"
SORT problem asc
```
- **start moving left** - not guarding against a stale stored index before the current window. Use `>= start` check (Index Jump)
- **Not updating state on both sides** - window state must be updated when elements enter from the right AND exit from the left (Shrink-Based)
