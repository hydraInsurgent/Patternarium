---
name: "QuickSort"
slug: quicksort
category: algorithm
tags: [sorting, divide-and-conquer, in-place, unstable]
time: "O(n log n) average, O(n^2) worst"
space: "O(log n)"
related: [mergesort, insertion-sort]
---

# QuickSort

## What It Is
A divide-and-conquer sorting algorithm that picks a pivot, partitions elements around it, and recursively sorts each partition.

## How It Works
1. Pick a pivot element (commonly last, first, or median)
2. Partition: move all elements smaller than pivot to its left, larger to its right
3. The pivot is now in its final sorted position
4. Recursively apply to the left and right partitions

## Pseudocode
```
quicksort(arr, low, high):
    if low < high:
        pivot_index = partition(arr, low, high)
        quicksort(arr, low, pivot_index - 1)
        quicksort(arr, pivot_index + 1, high)

partition(arr, low, high):
    pivot = arr[high]
    i = low - 1
    for j from low to high - 1:
        if arr[j] <= pivot:
            i++
            swap(arr[i], arr[j])
    swap(arr[i+1], arr[high])
    return i + 1
```

## Complexity
- Time: O(n log n) average, O(n^2) worst case (already sorted array with bad pivot choice)
- Space: O(log n) - recursive call stack

## When to Use It
- General-purpose in-memory sorting
- When average-case performance matters more than worst-case guarantee
- Note: in practice, use `Array.Sort` - do not implement QuickSort manually unless required

## vs Alternatives
- vs MergeSort: QuickSort is faster in practice (cache-friendly), but MergeSort guarantees O(n log n) worst case
- vs HeapSort: HeapSort guarantees O(n log n) worst case but is slower in practice due to cache misses

## Gotchas
- Worst case O(n^2) on sorted or reverse-sorted input with naive pivot selection
- Not stable - equal elements may change relative order

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN algorithms AS algo
WHERE algo = "quicksort"
SORT number asc
```
