---
title: Roman to Integer
category: problem-hub
problem: 13
slug: roman-to-integer
status: solved
first-solved:
times-revised: 0
last-revised:
lists: []
---

# Roman to Integer

**Difficulty:** Easy
**Source:** LeetCode #13

## Statement

Given a roman numeral string, convert it to an integer.

Roman numerals use seven symbols: I (1), V (5), X (10), L (50), C (100), D (500), M (1000). Usually written largest to smallest left to right, but six subtraction cases exist:
- I before V (4) or X (9)
- X before L (40) or C (90)
- C before D (400) or M (900)

## Examples

- Input: "III" -> Output: 3
- Input: "LVIII" -> Output: 58 (L=50, V=5, III=3)
- Input: "MCMXCIV" -> Output: 1994 (M=1000, CM=900, XC=90, IV=4)

## Constraints

- 1 <= s.length <= 15
- s contains only valid roman numeral characters ('I', 'V', 'X', 'L', 'C', 'D', 'M')
- s is a valid roman numeral in range [1, 3999]

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

> [!info]- Knowledge Links
>
> ### Patterns
> - [linear-scan](../../patterns/linear-scan.md)
> - [preprocessing](../../patterns/preprocessing.md)
> - [chunked-iteration](../../patterns/chunked-iteration.md)
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
> _none_

## Solutions
[Solutions & Learning Journey](solutions/solutions.md)
