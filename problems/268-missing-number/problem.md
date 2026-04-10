---
title: Missing Number
number: 268
slug: missing-number
category: DSA-Practice
difficulty: Easy
source: LeetCode
status: solved
lists: [blind-75, phased-75]
ds-used: [array]
patterns: [Odd One Out, Presence Array]
constructs: []
tags: [missing-number, xor-cancellation, xor, gauss-sum]
---

# Missing Number

## Statement
Given an array `nums` containing `n` distinct numbers in the range `[0, n]`, return the only number in the range that is missing from the array.

## Examples

**Example 1:**
Input: `nums = [3,0,1]`
Output: `2`
Explanation: n = 3, range is [0,3]. 2 is missing.

**Example 2:**
Input: `nums = [0,1]`
Output: `2`
Explanation: n = 2, range is [0,2]. 2 is missing.

**Example 3:**
Input: `nums = [9,6,4,2,3,5,7,0,1]`
Output: `8`
Explanation: n = 9, range is [0,9]. 8 is missing.

## Constraints
- `n == nums.length`
- `1 <= n <= 10^4`
- `0 <= nums[i] <= n`
- All numbers of nums are unique.

## Solutions
- [Solutions overview](solutions.md)
- [Notes and lessons](notes.md)

tags :: [array, math, bit-manipulation]
