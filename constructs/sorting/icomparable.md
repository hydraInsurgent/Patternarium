---
name: "IComparable<T> / CompareTo"
slug: icomparable
category: sorting
tags: [icomparable, compareto, comparison, ordering, interface, custom-class]
language: csharp
related: [array-sort-custom-comparer]
---

# IComparable<T> / CompareTo

## What It Is
A contract that any type can implement to define what "less than, equal to, greater than" means for itself. Once implemented, the type works automatically with all comparison-based operations - sorting, binary search, sorted collections, and more.

## The Contract
`CompareTo` must return:
```
this < other  →  negative
this == other →  0
this > other  →  positive
```

The exact values do not matter (-1 or -500 are both "negative"). Only the sign matters.

## Layer 1: Built-in - Primitive and Common Types
`int`, `string`, `decimal`, `DateTime`, `char`, and most built-in types already implement `IComparable<T>`. You get `CompareTo` for free:

```csharp
3.CompareTo(5)               // negative  (3 < 5)
5.CompareTo(5)               // 0         (equal)
7.CompareTo(5)               // positive  (7 > 5)

"apple".CompareTo("banana")  // negative  (alphabetical)
"zebra".CompareTo("apple")   // positive
```

This is why `a.Price.CompareTo(b.Price)` works inside a sort lambda when `Price` is an `int` or `decimal` - you are just calling `CompareTo` on a type that already has it.

## Layer 2: Custom Class - Implement IComparable<T>
If you have your own class and want a default sort order, implement the interface:

```csharp
public class Item : IComparable<Item>
{
    public string Category;
    public decimal Price;

    public int CompareTo(Item other)
    {
        // Define what "less than / equal to / greater than" means for Item
        // Here: sort by Category first, then by Price
        int cat = this.Category.CompareTo(other.Category);
        if (cat != 0) return cat;
        return this.Price.CompareTo(other.Price);
    }
}
```

Once implemented, `Array.Sort(items)` with no extra arguments uses your `CompareTo` automatically:

```csharp
Array.Sort(items);  // uses Item.CompareTo - no lambda needed
```

## Layer 3: Overriding an Existing CompareTo
Even if a type already has `CompareTo`, you can ignore it and define your own comparison logic by passing a comparer to the sort call. The built-in one is just the default - it is not locked in:

```csharp
// string already has CompareTo (alphabetical by default)
// Override it: sort by string length instead
Array.Sort(words, (a, b) => a.Length.CompareTo(b.Length));
```

The same applies to your own class - even if `Item` implements `IComparable<T>`, you can pass a different lambda or `IComparer<T>` when you need a different order for a specific sort.

## When Each Layer Applies

| Situation | What to use |
|-----------|-------------|
| Sorting by a primitive field (Price, Name) | Call `.CompareTo()` on the field - it's already there |
| Your class needs a permanent default sort order | Implement `IComparable<T>` on the class |
| One-off sort with different logic, without changing the class | Pass a lambda or `IComparer<T>` to `Array.Sort` |
| Override an existing default for one specific sort | Same as above - pass a comparer, it wins over `IComparable<T>` |

## Where It Shows Up
`CompareTo` / `IComparable<T>` is used under the hood by:
- `Array.Sort` and `List<T>.Sort` (when no comparer is passed)
- `SortedSet<T>` and `SortedDictionary<TKey, TValue>`
- Binary search (`Array.BinarySearch`)
- Any algorithm that needs to compare two elements

## Gotchas
- If your class does not implement `IComparable<T>` and you call `Array.Sort` with no comparer, it throws at runtime - no compile error
- `CompareTo` on `string` is case-sensitive and culture-aware by default - `"A".CompareTo("a")` is not zero
- Returning inconsistent results (e.g., `a.CompareTo(b)` is negative but `b.CompareTo(a)` is also negative) causes undefined sort behavior

## See Also
- [array-sort-custom-comparer.md](../sorting/array-sort-custom-comparer.md) - how to pass custom comparison logic to `Array.Sort`

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "icomparable"
SORT problem asc
```
