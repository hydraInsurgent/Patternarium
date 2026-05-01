---
name: trailing-pointer
slug: trailing-pointer
display_name: Trailing Pointer
category: technique
tags: [linked-list, pointer, in-place, traversal]
---

# Trailing Pointer

## What It Is
Maintain a pointer one step behind the current node during a forward traversal, so that the previous node is always reachable without scanning backward.

## Core Reasoning
A singly linked list only has forward pointers. To reference the predecessor of the current node (for reversal, deletion, or insertion), you must carry it yourself. Initialize `prev = null` before traversal and update it at each step before advancing `curr`.

## When to Apply
- In-place linked list reversal - need to know where to point `curr.next` backward
- Deleting a node - need the predecessor to rewire its `next`
- Any problem where a singly-linked traversal needs access to the previous node

## Template
```
prev = null
curr = head
while curr != null:
    next = curr.next     // save lookahead
    // ... work using prev and curr ...
    prev = curr          // advance trailing pointer
    curr = next          // advance current
```

## Tradeoffs
- Adds one pointer variable - O(1) space overhead
- Works in a single pass - no need to traverse backward or build a reverse index
- The order matters: always advance `prev` before `curr`

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN techniques AS technique
WHERE technique = "trailing-pointer"
SORT number asc
```
