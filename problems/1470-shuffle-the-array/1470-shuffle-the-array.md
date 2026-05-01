---
title: Shuffle the Array
category: problem-hub
problem: 1470
slug: shuffle-the-array
status: solved
first-solved: 2026-05-01
times-revised: 0
last-revised:
lists: [leetcode-quests]
---

# Shuffle the Array

**Difficulty:** Easy
**Source:** LeetCode #1470

## Statement
Given the array `nums` consisting of `2n` elements in the form `[x1,x2,...,xn,y1,y2,...,yn]`.

Return the array in the form `[x1,y1,x2,y2,...,xn,yn]`.

## Examples

**Example 1:**
Input: `nums = [2,5,1,3,4,7], n = 3`
Output: `[2,3,5,4,1,7]`
Explanation: Since `x1=2, x2=5, x3=1, y1=3, y2=4, y3=7` then the answer is `[2,3,5,4,1,7]`.

**Example 2:**
Input: `nums = [1,2,3,4,4,3,2,1], n = 4`
Output: `[1,4,2,3,3,2,4,1]`

**Example 3:**
Input: `nums = [1,1,2,2], n = 2`
Output: `[1,2,1,2]`

## Constraints
- `1 <= n <= 500`
- `nums.length == 2n`
- `1 <= nums[i] <= 10^3`

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

> [!info]- Knowledge Links
>
> ### Patterns
> _none_
>
> ### Concepts
> _none_
>
> ### Techniques
> - [index-mapping](../../techniques/index-mapping.md)
>
> ### Data Structures
> - [array](../../data-structures/array.md)
>
> ### Related Problems
> - [[1929-concatenation-of-array|Concatenation of Array]] - same Index Mapping technique, simpler (i, n+i) instead of (2i, 2i+1)

## Solutions
[Solutions & Learning Journey](solutions/solutions.md)
