# Insertion Sort

## What It Is
A simple sorting algorithm that builds the sorted array one element at a time by inserting each new element into its correct position among the already-sorted elements.

## How It Works
1. Start from index 1 (first element is trivially sorted)
2. Pick the current element (the "key")
3. Shift all sorted elements that are greater than the key one position right
4. Insert the key into the gap left behind
5. Repeat until all elements are sorted

## Pseudocode
```
insertionsort(arr):
    for i from 1 to len(arr) - 1:
        key = arr[i]
        j = i - 1
        while j >= 0 and arr[j] > key:
            arr[j + 1] = arr[j]
            j--
        arr[j + 1] = key
```

## Complexity
- Time: O(n^2) average and worst case, O(n) best case (already sorted)
- Space: O(1) - in-place

## When to Use It
- Small arrays (fast in practice due to low overhead)
- Nearly sorted data (approaches O(n) performance)
- Used internally by IntroSort (which powers C#'s `Array.Sort`) for small subarrays

## vs Alternatives
- vs BubbleSort: both are O(n^2) but InsertionSort is faster in practice - fewer swaps
- vs QuickSort/MergeSort: much slower on large arrays, but simpler and faster on small ones

## Gotchas
- Do not use on large unsorted arrays - O(n^2) becomes very slow

## Seen In
- (not yet linked to any solved problem)
