---
problem: 424
title: "Longest Repeating Character Replacement"
slug: 424-longest-repeating-character-replacement
source: LeetCode
difficulty: Medium
import-file: longest-repeating-character-replacement.txt
category: analysis
---

# Analysis: Longest Repeating Character Replacement

**Chat covers:** Brute force approach only (chat ends when user says "I want to code this myself")

---

## Thinking Journey

The session followed a long arc of misconception correction before the mental model solidified. Misunderstandings appeared in this sequence:

1. **Global character assumption** - thought the answer required finding the globally most frequent character first, then checking substrings against it. Took multiple rounds to correct.
2. **Wrong output type** - thought the goal was to count how many valid substrings exist, not find the max length of one.
3. **Invented a "50% threshold"** - when told maxFreq is local, tried to invent a rule that the dominant character must appear more than 50% of the time to trust the formula. This was unnecessary.
4. **Global vs local maxFreq** - tried to make maxFreq a global variable shared across all substrings. Took effort to understand it must reset per starting index `i`.
5. **maxFreq update method** - thought maxFreq must be recomputed by scanning the entire freq map each time. Realized only the newly added character matters.
6. **Substring counting error** - calculated number of substrings as O(n!) by multiplying group sizes instead of summing them. Was on the right structural track, just aggregated wrong.

The mental model finally clicked when the user understood: **you don't need to know which character is most frequent - only how many times the most frequent character appears.**

---

## Core Formula / Key Condition

```
length - maxFreq <= k
```

This is the validity condition for any substring. It works regardless of which character is dominant. The simplicity of this formula is what made it hard to trust early on.

---

## Approach 1: Brute Force

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

**Why maxFreq never needs to decrease in brute force:** In this approach we only add characters, never remove. So the maximum frequency of any character can only stay the same or increase. Therefore comparing `max(prevMaxFreq, freq[newChar])` is always sufficient - no need to rescan the map.

---

## Mistakes and Lessons

| Mistake | Category | What happened |
|---------|----------|---------------|
| Assumed globally most frequent char determines answer | Problem comprehension | Missed that each substring is evaluated independently |
| Counted substrings as factorial (multiplied group sizes) | Math error | Was grouping correctly, aggregated with multiply instead of add |
| Made maxFreq global | Variable lifecycle | Thought tracking the global max would help, but it makes replacements = negative |
| Tried to add 50% threshold rule | Unnecessary complexity | Didn't trust the simple formula, invented extra conditions |
| Thought maxFreq requires full map scan | Logic error | Didn't realize only one character changes per step |
| Confused "count valid substrings" with "max length" | Problem comprehension | Drifted toward counting instead of maximizing |

---

## Key Insights for Pattern Library

**The identity insight:** We don't track which character is most frequent - only its count. The condition `length - maxFreq <= k` is character-agnostic. This is non-obvious and the most commonly missed thing when first approaching this problem.

**Why incremental maxFreq works:** Only one character's count changes per step. That character can only become the new max or not change the max. So `maxFreq = max(maxFreq, freq[newChar])` is always correct and sufficient.

---

## Constructs Used

- `Dictionary<char, int>` - frequency tracking
- `Math.Max` - maxFreq and maxLength tracking

---

## Pattern Signals

This problem signals sliding window because:
- We want the longest valid substring
- Validity depends on a simple condition about window contents
- The condition involves a "count" that can be maintained incrementally

Chat does NOT cover sliding window - that was planned as a next step.

---

## Session Checklist

### Overall
- [x] Problem statement understood
- [x] Core formula derived: `length - maxFreq <= k`
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
- [ ] Not yet explored (planned next step in chat)
