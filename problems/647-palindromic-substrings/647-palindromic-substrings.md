---
title: Palindromic Substrings
category: problem-hub
problem: 647
slug: 647-palindromic-substrings
status: solved
first-solved: 2026-05-01
times-revised: 0
last-revised:
lists: []
---

# Palindromic Substrings

**Difficulty:** Medium
**Source:** LeetCode #647

## Statement

Given a string s, return the number of palindromic substrings in it.

A string is a palindrome when it reads the same backward as forward.

A substring is a contiguous sequence of characters within the string.

## Examples

Input: s = "abc"
Output: 3
Explanation: Three palindromic strings: "a", "b", "c".

Input: s = "aaa"
Output: 6
Explanation: Six palindromic strings: "a", "a", "a", "aa", "aa", "aaa".

## Constraints

- 1 <= s.length <= 1000
- s consists of lowercase English letters.

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

> [!info]- Knowledge Links
>
> ### Patterns
> - [Center Expansion - Count Palindromic Substrings](../../patterns/center-expansion.md#variation-count-palindromic-substrings)
> - [DP Tabulation - 2D String Palindrome Table](../../patterns/dp-tabulation.md#variation-2d-string-palindrome-table)
>
> ### Concepts
> - [Palindrome](../../concepts/palindrome.md)
>
> ### Techniques
> - [Gap-Based Fill Order](../../techniques/gap-based-fill-order.md)
>
> ### Data Structures
> - [String](../../data-structures/string.md)
>
> ### Related Problems
> - [5-longest-palindromic-substring](../5-longest-palindromic-substring/5-longest-palindromic-substring.md) - same center expansion approach, different tracking goal (max length vs count)

## Solutions
[Solutions & Learning Journey](solutions/solutions.md)
