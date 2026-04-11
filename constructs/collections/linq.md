---
name: "LINQ"
slug: linq
category: collections
tags: [linq, query, sort, filter, aggregate, enumerable]
language: csharp
related: [dictionary, hashset]
---

# LINQ

## What It Is
Language Integrated Query - a set of extension methods on `IEnumerable<T>` for filtering, sorting, transforming, and aggregating collections with a fluent, declarative syntax.

## C# Syntax
```csharp
// Sort and convert to array
char[] sorted = s.OrderBy(c => c).ToArray();

// Filter
var alphanumeric = s.Where(c => char.IsLetterOrDigit(c));

// Convert to new collection
string cleaned = new string(s.Where(char.IsLetterOrDigit).ToArray());

// Aggregate
int sum = nums.Sum();
int max = nums.Max();

// Group by
var groups = nums.GroupBy(n => n);

// All / Any
bool allPositive = nums.All(n => n > 0);
bool hasNegative = nums.Any(n => n < 0);

// Distinct
var unique = nums.Distinct();
```

## When to Reach For It
- Sorting a string or char array for comparison (e.g., anagram check via sort)
- Filtering characters to build a cleaned version of a string (e.g., palindrome preprocessing)
- Expressing a transformation in one readable line when performance is not critical

## vs Alternatives
- vs manual loop: LINQ is more readable but allocates more. For interview problems where space complexity matters, a manual loop with explicit allocation is more transparent.
- vs `Array.Sort`: `OrderBy().ToArray()` allocates a new array; `Array.Sort` sorts in-place. Prefer `Array.Sort` when you already have an array and want O(1) extra space.
- vs `Span<T>`: LINQ does not work on `Span<T>`. Use manual loops or `MemoryExtensions.Sort` for stack-allocated arrays.

## Gotchas
- LINQ is lazy: `Where`, `Select`, `OrderBy` return `IEnumerable<T>` - they do not execute until enumerated. Call `.ToArray()` or `.ToList()` to materialize the result.
- `OrderBy` is stable (preserves original order for equal elements). `Array.Sort` is not guaranteed stable.
- Each LINQ call can allocate an enumerator and intermediate objects. In tight loops or when space complexity matters, prefer explicit loops.

## See Also
- [dictionary.md](dictionary.md) - key-value store; LINQ's `GroupBy` produces similar groupings
- [hashset.md](hashset.md) - LINQ's `Distinct()` deduplicates; `HashSet<T>` is faster for explicit membership checks

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "linq"
SORT number asc
```
