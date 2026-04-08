---
name: "HashMap"
status: explored
progress: 43
---

# HashMap

## What It Is
A key-value store that gives O(1) average-case lookup, insert, and delete by hashing keys to internal slots. Keys must be unique.

## Core Operations

| Operation | Description | Average | Worst |
|-----------|-------------|---------|-------|
| Insert | Add or update key-value pair | O(1) | O(n) |
| Lookup | Get value by key | O(1) | O(n) |
| Delete | Remove a key | O(1) | O(n) |
| Exists | Check if key is present | O(1) | O(n) |
| Iterate | Walk all key-value pairs | O(n) | O(n) |

Worst case O(n) happens on hash collision - extremely rare with a good hash function.

## Space Complexity
O(n) - stores n key-value pairs.

## Mental Model
A lookup table. You give it a key, it gives back a value instantly - no scanning needed. Behind the scenes it converts the key to a slot number using a hash function.

## When to Reach For It
- You need to remember something from earlier in the array and look it up fast
- Frequency counting (how many times does each element appear?)
- Complement lookup (have I seen the value I need before?)
- Grouping elements by a property
- Eliminating O(n^2) nested loops by storing prior values

## When NOT to Use It
- You need sorted order - use a sorted map or sort the array
- Keys must be contiguous integers starting at 0 - a plain array is simpler
- Memory is constrained and you expect low collision rate - array may be leaner

## vs Alternatives
- vs Array: HashMap uses any key type and any range; array uses integer indices only
- vs HashSet: HashMap stores key-value pairs; HashSet stores keys only (existence check)
- vs SortedDictionary: HashMap is O(1) lookup but unordered; SortedDictionary is O(log n) but ordered

## C# Implementation
- [Dictionary](../constructs/collections/dictionary.md)

## Coverage

**Progress: 3 / 7 use cases explored (43%)**

All techniques here are use-case scenarios where HashMap is the primary tool. Each corresponds to a variation of the [HashMap](../patterns/hashmap.md) pattern. Linked rows point to that pattern file.

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| [Complement Lookup](../patterns/hashmap.md) | explored | 1-Two Sum |
| [Frequency Count](../patterns/hashmap.md) | explored | 242-Valid Anagram, 217-Contains Duplicate |
| [Last Seen Index](../patterns/hashmap.md) | explored | 3-Longest Substring Without Repeating Characters |
| Grouping / Bucketing | not started | - (Group Anagrams: sort chars as key) |
| Running Prefix Sum | not started | - (Subarray Sum Equals K) |
| Inverse Mapping | not started | - (value -> all positions) |
| Memoization | not started | - (top-down DP cache) |

---

## Seen In
- 1 - Two Sum (complement lookup: store value -> index)
- 242 - Valid Anagram (frequency count: char -> count)
- 128 - Longest Consecutive Sequence (existence lookup via HashSet variant)
- 20 - Valid Parentheses (map opening brackets to closing complements)
