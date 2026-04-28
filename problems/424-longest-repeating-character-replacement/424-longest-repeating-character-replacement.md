---
title: Longest Repeating Character Replacement
category: problem-hub
problem: 424
slug: 424-longest-repeating-character-replacement
status: solved
first-solved:
times-revised: 0
last-revised:
lists: [blind-75, phased-75]
---

# Longest Repeating Character Replacement

**Difficulty:** Medium
**Source:** LeetCode #424

## Statement

You are given a string `s` and an integer `k`. You can choose any character of the string and change it to any other uppercase English character. You can perform this operation at most `k` times.

Return the length of the longest substring containing the same letter you can get after performing the above operations.

## Examples

**Example 1:**
- Input: `s = "ABAB"`, `k = 2`
- Output: `4`
- Explanation: Replace the two 'A's with two 'B's or vice versa.

**Example 2:**
- Input: `s = "AABABBA"`, `k = 1`
- Output: `4`
- Explanation: Replace the one 'A' in the middle with 'B' and form "AABBBBA". There may exist other ways to achieve this answer too.

## Constraints

- `1 <= s.length <= 10^5`
- `s` consists of only uppercase English letters.
- `0 <= k <= s.length`

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

> [!info]- Knowledge Links
>
> ### Patterns
> - [sliding-window](../../patterns/sliding-window.md)
>
> ### Concepts
> _none_
>
> ### Techniques
> _none_
>
> ### Data Structures
> - [string](../../data-structures/string.md)
> - [hashmap](../../data-structures/hashmap.md)
>
> ### Related Problems
> - [3-longest-substring-without-repeating-characters](../3-longest-substring-without-repeating-characters/3-longest-substring-without-repeating-characters.md) - same sliding window structure, different validity condition (`no repeats` vs `windowSize - maxFreq <= k`)
> - [76-minimum-window-substring](../76-minimum-window-substring/76-minimum-window-substring.md) - sliding window with more complex validity condition; find smallest valid window instead of largest

## Solutions
[Solutions & Learning Journey](solutions/solutions.md)
