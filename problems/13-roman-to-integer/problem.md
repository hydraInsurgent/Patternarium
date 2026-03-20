---
title: Roman to Integer
category: DSA-Practice
difficulty: Easy
source: LeetCode
status: solved
---

# Roman to Integer

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

## Solutions

- [Solution approaches & learning journey](solutions.md)
- [Mistakes & key insights](notes.md)

tags :: String, HashMap
