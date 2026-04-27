---
name: "Heap"
slug: heap
status: stub
progress: 0
---

# Heap (Priority Queue)

## What It Is
A tree-based data structure that always gives you the minimum (min-heap) or maximum (max-heap) element in O(1). Insert and remove maintain this property in O(log n).

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Insert | Add element, maintain heap property | O(log n) |
| Peek min/max | Read top element without removing | O(1) |
| Extract min/max | Remove and return top element | O(log n) |
| Heapify (build) | Build heap from array | O(n) |
| Size | Number of elements | O(1) |

## Space Complexity
O(n) - stores n elements.

## Mental Model
A partially ordered tree where the top always holds the smallest (min-heap) or largest (max-heap) element. You cannot access elements in arbitrary order - but you always get the extreme value instantly. After removing the top, the heap reorganizes itself to put the new extreme at the top.

## When to Reach For It
- "Find the kth largest/smallest element"
- "Top K elements" problems
- You need repeated access to the minimum or maximum
- Scheduling problems where you always process the highest priority item next
- Merging K sorted lists

## When NOT to Use It
- You need sorted order of all elements - sort instead (O(n log n) is same cost as n heap extractions)
- You need arbitrary access by index - use an array

## vs Alternatives
- vs Sorted Array: Heap insert is O(log n); maintaining a sorted array on insert is O(n)
- vs BST: Heap only guarantees min/max at top; BST gives full sorted order but is more complex

## Variants
- **Min-Heap**: top is always the minimum
- **Max-Heap**: top is always the maximum

## C# Implementation
- Built-in: `PriorityQueue<TElement, TPriority>` (.NET 6+)
- Min-heap behavior: lower priority value = higher priority
- For max-heap: negate the priority value

## Coverage

**Progress: 0 / 7 techniques explored (0%)**

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| Top K Elements | not started | - (k largest, k most frequent) |
| Kth Largest / Smallest | not started | - (maintain heap of size k) |
| Merge K Sorted Lists / Arrays | not started | - (always extract min across k heads) |
| Running Median | not started | - (two heaps: max-heap left, min-heap right) |
| Greedy Scheduling | not started | - (task scheduler, meeting rooms) |
| Dijkstra's Shortest Path | not started | - (min-heap on distance + node) |
| Prim's Minimum Spanning Tree | not started | - (min-heap on edge weight) |

---

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty, patterns
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "heap"
SORT problem asc
```
