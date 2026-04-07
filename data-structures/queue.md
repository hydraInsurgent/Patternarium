---
name: "Queue"
status: stub
progress: 0
---

# Queue

## What It Is
A first-in, first-out (FIFO) collection. The first element added is the first one removed. Think of a queue at a ticket counter - the person who arrived first gets served first.

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Enqueue | Add element to back | O(1) |
| Dequeue | Remove element from front | O(1) |
| Peek | Read front element without removing | O(1) |
| IsEmpty | Check if queue has elements | O(1) |
| Size | Number of elements | O(1) |

## Space Complexity
O(n) - stores n elements.

## Mental Model
A line of people. New people join at the back. The person at the front is served and leaves. The order is always preserved - first come, first served.

## When to Reach For It
- You need to process elements in the order they arrived
- Breadth-first search (BFS) - process level by level
- Sliding window problems where you need to track entry order
- Any problem where "oldest element" needs to be removed first

## When NOT to Use It
- You need the most recently added element - use a Stack
- You need access to any arbitrary element - use a list

## vs Alternatives
- vs Stack: Queue is FIFO (first in, first out); Stack is LIFO (last in, first out)
- vs Deque: Deque is a generalization - supports add/remove at both ends

## Common Patterns Using Queue
- BFS on trees and graphs (level-order traversal)
- Sliding window minimum/maximum (use Deque)

## C# Implementation
- Built-in: `Queue<T>`
- `queue.Enqueue(x)`, `queue.Dequeue()`, `queue.Peek()`, `queue.Count`

## Coverage

**Progress: 0 / 5 techniques explored (0%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| BFS - Level Order Traversal | not started | - (process nodes level by level) |
| BFS - Shortest Path | not started | - (fewest steps in unweighted graph/grid) |
| Multi-Source BFS | not started | - (start from multiple nodes simultaneously) |
| Topological Sort (Kahn's Algorithm) | not started | - (course schedule, dependency ordering) |
| Sliding Window via Queue | not started | - (use with Deque for window min/max) |

---

## Seen In
(not yet encountered in sessions)
