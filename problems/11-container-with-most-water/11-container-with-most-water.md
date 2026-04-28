---
title: Container With Most Water
category: problem-hub
problem: 11
slug: container-with-most-water
status: solved
first-solved:
times-revised: 0
last-revised:
lists: []
---

# Container With Most Water

**Difficulty:** Medium
**Source:** LeetCode #11

## Statement

You are given an integer array `height` of length `n`. There are `n` vertical lines drawn such that the two endpoints of the `i`th line are `(i, 0)` and `(i, height[i])`.

Find two lines that together with the x-axis form a container, such that the container contains the most water.

Return the maximum amount of water a container can store.

Notice that you may not slant the container.

## Examples

**Example 1:**
Input: `height = [1,8,6,2,5,4,8,3,7]`
Output: `49`

**Example 2:**
Input: `height = [1,1]`
Output: `1`

## Constraints

- `n == height.length`
- `2 <= n <= 10^5`
- `0 <= height[i] <= 10^4`

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

> [!info]- Knowledge Links
>
> ### Patterns
> - [two-pointers](../../patterns/two-pointers.md)
>
> ### Concepts
> _none_
>
> ### Techniques
> - [running-max](../../techniques/running-max.md)
> - [constraint-ceiling-pruning](../../techniques/constraint-ceiling-pruning.md)
>
> ### Data Structures
> - [array](../../data-structures/array.md)
>
> ### Related Problems
> - [424-longest-repeating-character-replacement](../424-longest-repeating-character-replacement/424-longest-repeating-character-replacement.md) - contrast case: sliding window fits there because validity depends on window contents (character frequency inside the window). Here validity depends only on the two endpoints.
> - [1-two-sum](../1-two-sum/1-two-sum.md) - same converging two-pointer skeleton (start at ends, move one pointer per step based on a condition). Different decision rule: sum vs target vs which side limits height.

## Solutions
[Solutions & Learning Journey](solutions/solutions.md)
