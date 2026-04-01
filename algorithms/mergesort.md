# MergeSort

## What It Is
A divide-and-conquer sorting algorithm that splits the array in half, recursively sorts each half, then merges the two sorted halves.

## How It Works
1. If array has 0 or 1 elements, it is already sorted - return
2. Split the array into two halves at the midpoint
3. Recursively sort the left half
4. Recursively sort the right half
5. Merge the two sorted halves into one sorted array

## Pseudocode
```
mergesort(arr):
    if len(arr) <= 1: return arr
    mid = len(arr) / 2
    left = mergesort(arr[0..mid])
    right = mergesort(arr[mid..end])
    return merge(left, right)

merge(left, right):
    result = []
    while left and right are non-empty:
        if left[0] <= right[0]: append left[0] to result, advance left
        else: append right[0] to result, advance right
    append remaining elements
    return result
```

## Complexity
- Time: O(n log n) - best, average, and worst case
- Space: O(n) - needs auxiliary array for merging

## When to Use It
- When you need a guaranteed O(n log n) worst case
- When stability matters (equal elements preserve their original order)
- External sorting (data too large to fit in memory - sort in chunks and merge)

## vs Alternatives
- vs QuickSort: MergeSort guarantees O(n log n) worst case and is stable. QuickSort is faster in practice but degrades to O(n^2) in worst case
- vs HeapSort: both guarantee O(n log n), but MergeSort is stable and easier to understand

## Gotchas
- Requires O(n) extra space - not in-place
- Slower in practice than QuickSort for in-memory sorting due to memory allocation overhead

## Seen In
- (not yet linked to any solved problem)
