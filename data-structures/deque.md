---
name: "Deque"
status: stub
progress: 0
---

# Deque (Double-Ended Queue)

## What It Is
A generalization of Queue and Stack. Supports O(1) add and remove at both the front and the back. Pronounced "deck."

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| AddFront | Add element to front | O(1) |
| AddBack | Add element to back | O(1) |
| RemoveFront | Remove element from front | O(1) |
| RemoveBack | Remove element from back | O(1) |
| PeekFront | Read front without removing | O(1) |
| PeekBack | Read back without removing | O(1) |
| Size | Number of elements | O(1) |

## Space Complexity
O(n) - stores n elements.

## Mental Model
A tunnel open at both ends. You can push or pull from either side. This flexibility makes it the right tool when a Stack or Queue is almost right but you occasionally need to work from the other end.

## When to Reach For It
- Sliding window maximum or minimum (monotonic deque)
- You need both LIFO and FIFO behavior in the same structure
- BFS with priority access at the front

## When NOT to Use It
- Simple LIFO only - use a Stack (simpler)
- Simple FIFO only - use a Queue (simpler)

## vs Alternatives
- vs Stack: Deque can also add/remove from front
- vs Queue: Deque can also add/remove from back
- Both Stack and Queue can be implemented using a Deque

## Common Patterns Using Deque
- Sliding Window Maximum (remove from back when new element is larger, remove from front when outside window)

## C# Implementation
- Built-in: `LinkedList<T>` (used as deque via `AddFirst`, `AddLast`, `RemoveFirst`, `RemoveLast`)
- Also: `Deque` is not a built-in class name in C# - use `LinkedList<T>` or a third-party library

## Coverage

**Progress: 0 / 4 techniques explored (0%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| Monotonic Deque - Sliding Window Max / Min | not started | - (maximum of every window of size k) |
| Monotonic Deque - Previous and Next Extremes | not started | - (combine next greater and previous smaller) |
| BFS with Front Priority | not started | - (0-1 BFS, push to front for zero-cost edges) |
| Palindrome Verification | not started | - (compare front and back simultaneously) |

---

## Seen In
(not yet encountered in sessions)
