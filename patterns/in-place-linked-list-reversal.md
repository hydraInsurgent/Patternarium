---
name: "in-place-linked-list-reversal"
display_name: "In-Place Linked List Reversal"
category: pattern
variations:
  - name: Three-Pointer Reversal
    ds: [linked-list]
ds-primary: [linked-list]
---

# In-Place Linked List Reversal

## Core Idea
Reverse the direction of pointers in a linked list by walking it once and rewriting each node's `next` to point backward, using only a constant number of pointer variables.

## Variation: Three-Pointer Reversal

**When to reach for this:**
- Problem asks to reverse a linked list (whole or in part) without allocating new nodes
- Problem involves rewriting pointer direction in a singly linked list
- "In-place" or "O(1) space" reversal is required
- You need to modify the structure of a linked list, not just read its values

**Mental Trigger:**
> "I need to change where each node points. If I overwrite a pointer, I lose forward access. I need three positions: where I came from, where I am, where I'm going."

**Template:**
```
prev = null
curr = head
while curr != null:
    next = curr.next       // save forward link before overwrite
    curr.next = prev       // rewrite pointer backward
    prev = curr            // slide trailing pointer
    curr = next            // advance to saved forward node
return prev
```

**Tradeoffs:**
- Time: O(n) - one pass through the list
- Space: O(1) iterative / O(n) recursive (call stack)
- Beats any approach that allocates a new list or uses a stack (both O(n) space)

---

## Try Next
- LeetCode 92 - Reverse Linked List II (reverse a sublist from position m to n)
- LeetCode 25 - Reverse Nodes in K-Group

## Common Mistakes
- Framing the operation as a "swap" (two-way) instead of a "rewrite" (one-way) - leads to 2 variables + a temp instead of 3 distinct pointers. The correct frame: one field (`curr.next`) is overwritten in one direction. (Three-Pointer Reversal)
- Using `while (next != null)` as the loop condition - next is assigned null inside the loop on the last iteration; the condition check happens before that assignment, so `while (curr != null)` is correct.
- Not initializing `prev = null` before the loop - the original head becomes the new tail and must point to null; initializing prev to null makes the first iteration handle this implicitly.

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "In-Place Linked List Reversal"
SORT number asc
```
