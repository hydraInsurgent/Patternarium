---
name: "Linked List"
slug: linked-list
status: stub
progress: 0
---

# Linked List

## What It Is
A sequence of nodes where each node holds a value and a pointer to the next node. Unlike arrays, nodes are not stored contiguously in memory - they are connected by pointers.

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Access by index | Walk from head to index | O(n) |
| Search | Walk until found | O(n) |
| Insert at head | Rewire one pointer | O(1) |
| Insert at tail | Walk to end, rewire | O(n) - or O(1) with tail pointer |
| Insert at middle | Walk to position, rewire | O(n) |
| Delete at head | Move head pointer forward | O(1) |
| Delete at middle | Walk to predecessor, rewire | O(n) |

## Space Complexity
O(n) - one node per element, plus pointer overhead.

## Mental Model
A chain of boxes, each box holding a value and a string tied to the next box. You cannot jump to box 5 directly - you must follow the chain from box 1. But inserting a new box anywhere only requires retying two strings, not moving everything.

## When to Reach For It
- You need frequent insertions or deletions at the head
- You do not need random index access
- You are implementing a Stack or Queue from scratch
- The problem explicitly involves pointer manipulation

## When NOT to Use It
- You need index-based access - use an array
- You need to search frequently - linked list search is O(n)
- Memory overhead of pointers is a concern

## Variants
- **Singly Linked List**: each node points to next only
- **Doubly Linked List**: each node points to both next and previous - enables O(1) delete if you have the node reference

## vs Alternatives
- vs Array: Array is O(1) access but O(n) insert/delete at middle; Linked List is the reverse
- vs Stack/Queue: Stack and Queue are often implemented using a Linked List underneath

## C# Implementation
- Built-in: `LinkedList<T>` (doubly linked)
- Manual: define a `ListNode` class with `val` and `next` fields (common in interview problems)

## Coverage

**Progress: 0 / 8 techniques explored (0%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| Linear Traversal | not started | - (iterate, search, count nodes) |
| Pointer Manipulation | not started | - (insert, delete, rewire nodes) |
| Two Pointers - Fast and Slow | not started | - (cycle detection, find middle) |
| Two Pointers - n-th From End | not started | - (remove nth node from end) |
| In-place Reversal | not started | - (reverse full list or subrange) |
| Merge | not started | - (merge two sorted lists) |
| Dummy Head | not started | - (simplify edge cases at head) |
| Recursion | not started | - (reverse recursively, merge recursively) |

---

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty, patterns
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "linked-list"
SORT problem asc
```
