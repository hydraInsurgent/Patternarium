---
title: Merge Two Sorted Lists
category: problem-hub
problem: 21
slug: 21-merge-two-sorted-lists
status: solved
first-solved: 2026-05-03
times-revised: 0
last-revised:
lists: [blind-75, phased-75]
---

# Merge Two Sorted Lists

**Difficulty:** Easy
**Source:** LeetCode #21

## Statement

You are given the heads of two sorted linked lists `list1` and `list2`.

Merge the two lists into one sorted list. The list should be made by splicing together the nodes of the first two lists.

Return the head of the merged linked list.

## Examples

Input: list1 = [1,2,4], list2 = [1,3,4]
Output: [1,1,2,3,4,4]

Input: list1 = [], list2 = []
Output: []

Input: list1 = [], list2 = [0]
Output: [0]

## Constraints

- The number of nodes in both lists is in the range [0, 50].
- -100 <= Node.val <= 100
- Both list1 and list2 are sorted in non-decreasing order.

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

> [!info]- Knowledge Links
>
> ### Patterns
> - [Two Pointers - Parallel Merge](../../patterns/two-pointers.md#variation-parallel-merge)
>
> ### Concepts
> _none_
>
> ### Techniques
> - [Head-Tail Tracking](../../techniques/head-tail-tracking.md)
>
> ### Data Structures
> - [Linked List](../../data-structures/linked-list.md)
>
> ### Related Problems
> - [206-reverse-linked-list](../206-reverse-linked-list/206-reverse-linked-list.md) - shares the in-place pointer-rewiring frame: mutate `.next` without losing forward access. Different goal (reverse vs merge), same constraint.
> - Merge Sort (algorithm) - the merge step uses identical compare-and-pick logic on two sorted sequences, just on array indices instead of pointers. Same pattern, different data structure.
> - LeetCode 23 - Merge k Sorted Lists - generalization to k lists; typical solution uses a min-heap over the heads. Drops the "two-pointer" framing once you scale beyond two.

## Solutions
[Solutions & Learning Journey](solutions/solutions.md)
