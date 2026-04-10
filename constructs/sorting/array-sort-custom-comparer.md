---
name: "Array.Sort - Custom Comparer"
slug: array-sort
category: sorting
tags: [sorting, comparer, lambda, icomparer, custom-sort, multi-key]
language: csharp
related: [icomparable]
---

# Array.Sort - Custom Comparer

## What It Is
A way to sort an array using your own comparison logic instead of the default natural order.

## C# Syntax

### 1. Inline lambda - single key
Pass a lambda directly. The lambda receives two elements and returns an int:

```
a.value < b.value  →  return negative  →  a comes first
a.value > b.value  →  return positive  →  b comes first
a.value == b.value →  return 0         →  order unchanged
```

```csharp
// Sort by Price ascending (lower price first)
Array.Sort(items, (a, b) => a.Price.CompareTo(b.Price));
//  a.Price < b.Price  →  negative  →  a (cheaper) comes first

// Sort by Price descending (higher price first)
Array.Sort(items, (a, b) => b.Price.CompareTo(a.Price));
//  flip a and b: now b.Price < a.Price makes b come first
```

### 2. Named method - multi-key sort
Extract to a method when the comparison is complex or reusable. Method signature must match `Comparison<T>`: takes two T, returns int.

```csharp
public int CompareItems(Item a, Item b)
{
    // Sort by Category first, then by Price within each category
    int categoryCompare = a.Category.CompareTo(b.Category);

    if (categoryCompare != 0)
        return categoryCompare;

    return a.Price.CompareTo(b.Price);
}

// Usage
Array.Sort(items, CompareItems);
```

### 3. IComparer<T> class - reusable, injectable
Implement the interface when you want a named, portable comparer you can reuse or swap out.

```csharp
public class ItemComparer : IComparer<Item>
{
    public int Compare(Item a, Item b)
    {
        int categoryCompare = a.Category.CompareTo(b.Category);

        if (categoryCompare != 0)
            return categoryCompare;

        return a.Price.CompareTo(b.Price);
    }
}

// Usage
Array.Sort(items, new ItemComparer());

// Also works with List<T>
List<Item> list = new List<Item>(items);
list.Sort(new ItemComparer());
```

## When to Reach For It
- Sorting objects by a property (price, name, date)
- Sorting by multiple keys with a tiebreaker
- Reversing the default order (descending instead of ascending)
- Sorting by a computed value (e.g., distance from origin, string length)

## vs Alternatives
- vs `Array.Sort(arr)` - no comparer, uses natural order (IComparable on the type). Only works if the type already knows how to compare itself
- vs LINQ `OrderBy` / `ThenBy` - more readable for chained multi-key sorts, but returns a new IEnumerable instead of sorting in place
- vs named method vs IComparer - use a lambda for one-off sorts, a named method for moderate complexity, IComparer when the logic needs to be reused across different call sites or injected

## The Comparison Contract
The function must return:
- **Negative** - `a` comes before `b`
- **Zero** - `a` and `b` are equal in order
- **Positive** - `b` comes before `a`

`CompareTo` already follows this contract, so chaining `.CompareTo` calls is safe.

## Gotchas
- The return value only needs to be negative/zero/positive - the exact magnitude does not matter. But be consistent: if `Compare(a, b)` returns negative, `Compare(b, a)` must return positive
- Inconsistent comparers (ones that violate transitivity) cause undefined sort behavior - no error is thrown, you just get a wrong order
- Lambdas capture variables from the outer scope - if you reference a variable inside the lambda and it changes, the sort may behave unexpectedly
- `Array.Sort` sorts in place. If you need the original order preserved, copy the array first

## See Also
- [icomparable.md](../sorting/icomparable.md) - what `CompareTo` is, how primitives implement it, and how to define it on your own class

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "array-sort"
SORT number asc
```
