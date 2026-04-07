---
name: "HashSet"
category: collections
tags: [hashset, set, membership, deduplication, o1-lookup]
language: csharp
related: [dictionary]
---

# HashSet

## What It Is
An unordered collection of unique values that supports O(1) add, remove, and existence checks.

## C# Syntax
```csharp
// Create from array
HashSet<int> set = new HashSet<int>(nums);

// Create empty
HashSet<int> set = new HashSet<int>();

// Add
set.Add(5);

// Check existence - O(1)
set.Contains(5);  // true or false

// Remove
set.Remove(5);

// Size
set.Count;

// Iterate
foreach (int n in set) { ... }
```

## When to Reach For It
- You only need to know if a value exists - no index, no count, no ordering needed
- Deduplication - load an array into a HashSet to eliminate duplicates
- O(1) membership testing inside a loop

## vs Alternatives
- vs Dictionary: use HashSet when you have no value to store, only keys. Dictionary is key-value, HashSet is key-only
- vs List: List.Contains is O(n). HashSet.Contains is O(1)
- vs sorted array: sorted array gives O(log n) lookup via binary search, but requires sorting first (O(n log n)). HashSet gives O(1) with no sorting needed

## Gotchas
- HashSet does not preserve insertion order
- Duplicates are silently ignored on Add - no error thrown
- Cannot access elements by index

## Seen In
- 128 - Longest Consecutive Sequence (existence check to find sequence starts and extend runs)
- 3 - Longest Substring Without Repeating Characters (Approach 1 - track chars in the current brute force window)
- 217 - Contains Duplicate (track seen numbers; if Contains returns true, a duplicate exists)
