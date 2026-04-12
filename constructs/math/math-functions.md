---
name: "Math Functions"
slug: math-functions
category: math
tags: [math, comparison, numeric-utilities, built-in]
language: csharp
related: []
---

# Math Functions

## What It Is
The `System.Math` class provides built-in static methods for common mathematical operations - comparison, rounding, trigonometry, powers, and constants.

## Functions

### Math.Max / Math.Min
Returns the larger or smaller of two values. The simplest way to update running state in a single pass.

```csharp
int bigger = Math.Max(a, b);
int smaller = Math.Min(a, b);

// Common pattern: update running min/max
minSoFar = Math.Min(prices[i], minSoFar);
maxProfit = Math.Max(prices[i] - minSoFar, maxProfit);
```

**When to use:** Tracking a running minimum or maximum across a scan. Comparing two candidate results to keep the best.

**Gotchas:**
- Only compares two values at a time - for arrays, use a loop or LINQ `.Max()` / `.Min()`
- Both arguments must be the same type

## When to Reach For It
- Any numeric comparison, clamping, rounding, or mathematical computation
- Running state problems that track min/max/best-so-far

## Gotchas
- Most methods have overloads for `int`, `double`, `float`, `long` - but arguments must match types
- `Math.Pow` returns `double`, not `int` - cast if needed

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "math-functions"
SORT problem asc
```
