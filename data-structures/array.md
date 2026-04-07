---
name: "Array"
status: explored
progress: 56
---

# Array

## What It Is
A fixed-size, ordered sequence of elements stored in contiguous memory, accessible by index in O(1).

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Access | Read element at index | O(1) |
| Search | Find element by value | O(n) |
| Insert at end | Append (if space exists) | O(1) |
| Insert at index | Shift elements right | O(n) |
| Delete at index | Shift elements left | O(n) |
| Length | Get size | O(1) |

## Space Complexity
O(n) - one slot per element.

## Mental Model
A row of numbered boxes. You can jump directly to any box if you know its number. Inserting in the middle means physically shifting every box after it.

## When to Reach For It
- You need index-based access
- Order matters
- You know the size ahead of time or are iterating through all elements
- The problem gives you a sequence of numbers or characters

## When NOT to Use It
- You need fast insertion or deletion in the middle - use a linked list
- You need fast existence checks - use a HashSet
- You need key-value lookup - use a HashMap

## vs Alternatives
- vs LinkedList: Array is faster for access (O(1) vs O(n)), slower for mid-insert
- vs HashMap: Array uses index as key (integer only, contiguous), HashMap uses any key

## C# Implementation
- Built-in: `int[] arr = new int[n]`
- Dynamic: [List](../constructs/collections/list.md)
- Sorting: [Array.Sort](../constructs/sorting/array-sort-custom-comparer.md)

## Coverage

**Progress: 5 / 9 techniques explored (56%)**

Linked rows have a pattern file. Plain text rows are placeholders - no pattern file exists yet. Links are added when the pattern file is created.

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| [Linear Scan](../patterns/linear-scan.md) | explored | 1-Two Sum, 217-Contains Duplicate |
| [Preprocessing](../patterns/preprocessing.md) | explored | 217-Contains Duplicate (Sort to Expose Structure variation) |
| [HashMap](../patterns/hashmap.md) | explored | 1-Two Sum, 217-Contains Duplicate, 242-Valid Anagram |
| [Odd One Out](../patterns/odd-one-out.md) | explored | 268-Missing Number (XOR variation) |
| [Two Pointers](../patterns/two-pointers.md) | explored | 1-Two Sum (Sorted Pair variation) |
| [Sliding Window](../patterns/sliding-window.md) | not started | - |
| Prefix Sum | not started | - |
| Binary Search | not started | - |
| Kadane's Algorithm | not started | - |

---

## Seen In
- 1 - Two Sum (brute force: nested loop over array)
- 217 - Contains Duplicate (sort and scan)
- 268 - Missing Number (XOR over array, Gauss sum)
- 242 - Valid Anagram (frequency count over character array)
