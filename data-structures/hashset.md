---
name: "HashSet"
status: explored
progress: 33
---

# HashSet

## What It Is
An unordered collection of unique values with O(1) average-case insert, delete, and existence check. Like a HashMap with no values - only keys.

## Core Operations

| Operation | Description | Average | Worst |
|-----------|-------------|---------|-------|
| Insert | Add element (no-op if exists) | O(1) | O(n) |
| Exists | Check if element is present | O(1) | O(n) |
| Delete | Remove element | O(1) | O(n) |
| Iterate | Walk all elements | O(n) | O(n) |

## Space Complexity
O(n) - stores n unique elements.

## Mental Model
A bag where duplicates simply don't exist. Toss something in - if it's already there, nothing happens. Ask if something is in the bag - instant answer, no searching.

## When to Reach For It
- You need to check existence fast and don't need a value
- You want to eliminate duplicates
- You need to know if something has been seen before
- The problem asks "does this element appear?"

## When NOT to Use It
- You need to store a value alongside the key - use a HashMap
- You need the count of occurrences - use a HashMap with frequency count
- You need sorted order - use a SortedSet

## vs Alternatives
- vs HashMap: HashSet is simpler when you only care about presence, not associated values
- vs Array: Array is O(n) for existence check; HashSet is O(1)
- vs List: Same as Array - List.Contains() is O(n)

## C# Implementation
- [HashSet](../constructs/collections/hashset.md)

## Coverage

**Progress: 2 / 6 use cases explored (33%)**

HashSet use cases are variations of the [HashMap](../patterns/hashmap.md) pattern (specifically the HashSet Existence Lookup variation). All rows link to that pattern file.

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| [Duplicate Detection](../patterns/hashmap.md) | explored | 217-Contains Duplicate |
| [Existence / Membership Check](../patterns/hashmap.md) | explored | 128-Longest Consecutive Sequence |
| Complement Existence | not started | - (is the needed value present without needing index) |
| Visited Tracking | not started | - (graph traversal, cycle detection) |
| Set Intersection / Union / Difference | not started | - |
| Deduplication | not started | - (reduce list to unique elements) |

---

## Seen In
- 217 - Contains Duplicate (track seen elements, check before inserting)
- 128 - Longest Consecutive Sequence (existence lookup to find sequence starts)
