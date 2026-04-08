---
name: "Stack"
status: explored
progress: 14
---

# Stack

## What It Is
A last-in, first-out (LIFO) collection. The last element added is the first one removed. Think of a stack of plates - you always take from the top.

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Push | Add element to top | O(1) |
| Pop | Remove element from top | O(1) |
| Peek | Read top element without removing | O(1) |
| IsEmpty | Check if stack has elements | O(1) |
| Size | Number of elements | O(1) |

## Space Complexity
O(n) - stores n elements.

## Mental Model
A stack of plates. You can only add to the top (push) or take from the top (pop). The plate you added last is the first one you remove. There is no access to the middle or bottom.

## When to Reach For It
- You need to reverse something (reverse order = LIFO)
- You are matching pairs: parentheses, brackets, tags
- You need to undo operations in reverse order
- The problem involves "most recent" or "nearest previous" element
- Depth-first search (DFS) implemented iteratively

## When NOT to Use It
- You need access to elements in the middle - use a list
- You need first-in, first-out order - use a Queue

## vs Alternatives
- vs Queue: Stack is LIFO (last in, first out); Queue is FIFO (first in, first out)
- vs Array/List: Stack enforces the LIFO discipline; array/list allow access anywhere

## Common Patterns Using Stack
- Valid Parentheses (push opening, pop on closing)
- Next Greater Element (monotonic stack)
- Min Stack (track minimum alongside values)

## C# Implementation
- [Stack](../constructs/collections/stack.md)
- `stack.Push(x)`, `stack.Pop()`, `stack.Peek()`, `stack.Count`

## Coverage

**Progress: 1 / 7 techniques explored (14%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| [Reverse Order Matching](../patterns/reverse-order-matching.md) | explored | 20-Valid Parentheses (Complement Push variation) |
| Monotonic Stack - Next Greater Element | not started | - (find next larger to the right) |
| Monotonic Stack - Previous Smaller / Greater | not started | - (largest rectangle in histogram) |
| Min / Max Stack | not started | - (track running min alongside values) |
| Expression Evaluation | not started | - (calculator, infix to postfix) |
| Iterative DFS | not started | - (tree or graph DFS without recursion) |
| Undo / History Tracking | not started | - (reverse operations in order) |

---

## Seen In
- 20 - Valid Parentheses (LIFO buffer to track unmatched opening brackets)
