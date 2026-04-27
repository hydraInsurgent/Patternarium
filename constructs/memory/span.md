---
name: "Span / stackalloc"
slug: span
category: memory
tags: [span, stackalloc, stack-allocation, ascii, fixed-size-array, performance]
language: csharp
related: []
---

# Span / stackalloc

## What It Is
`Span<T>` is a stack-safe view over a contiguous block of memory. Combined with `stackalloc`, it allocates a fixed-size array on the call stack instead of the heap - zero GC pressure, bounds-checked, no `unsafe` keyword needed.

## C# Syntax
```csharp
// Stack-allocate an array of 128 ints - all default to 0
Span<int> lastSeen = stackalloc int[128];

// Index like a normal array
lastSeen[65] = 5;       // write
int val = lastSeen[65]; // read

// Char implicitly casts to int (its ASCII value) - use directly as index
Span<int> seen = stackalloc int[128];
char c = 'A';
seen[c] = 1;            // 'A' = 65, so seen[65] = 1
```

## When to Reach For It
- You need a fixed-size array of known, small size (e.g., 26 letters, 128 ASCII chars)
- Performance matters and you want to avoid heap allocation
- You are replacing a Dictionary<char, int> where keys are bounded ASCII characters

## vs Alternatives
- vs Dictionary<char, int>: Span+stackalloc is O(1) space (fixed size), no hashing overhead, no heap allocation. Dictionary is O(n) space and involves heap allocation.
- vs `new int[128]`: heap-allocated array. Functionally the same but puts pressure on the GC. Use stackalloc when the array is small and short-lived.
- vs bool[]: use int[] when you need to store a value per slot (e.g., last seen index). Use bool[] when you only need presence.

## Gotchas
- `stackalloc` only works inside a method (not as a field). Span cannot escape the current stack frame.
- All slots initialize to 0. If 0 is a valid stored value in your domain, use a sentinel like storing `index + 1` to make 0 mean "never seen."
- Size must be a compile-time or runtime constant - but must be known before allocation. Cannot resize.
- Only covers chars in the ASCII range (0-127). For Unicode input, fall back to Dictionary.

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "span"
SORT problem asc
```
