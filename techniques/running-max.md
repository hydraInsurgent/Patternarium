---
name: running-max
slug: running-max
display_name: Running Max
category: technique
tags: [running-max, running-extremum, linear-scan, optimization]
---

# Running Max

## What It Is
Carry the best value seen so far as you scan, updating it at each step instead of re-scanning.

## Core Reasoning
At any index, the best value across all previous elements is already known - it was computed one step ago. You only need to check if the current element beats it. One comparison per step, zero re-scanning.

The same reasoning applies to running min - just flip the comparison.

## When to Apply
- You need the best value seen so far at any point in a linear scan
- The answer at each position depends on all previous elements
- Re-scanning from the start each time would push you to O(n^2)

## Template
```csharp
int best = initialValue; // e.g. 0, int.MinValue, int.MaxValue

for (int i = 0; i < n; i++)
{
    // use best here if needed (e.g. compute profit = current - best)
    best = Math.Max(best, current);  // or Math.Min for running min
}
```

## Tradeoffs
- Reduces re-scan cost from O(n) per step to O(1) per step
- Only valid when past best is monotonically preserved - if the window shrinks and old values leave scope, running max may become stale (see: Monotonic Running Max)

## Related
- [Monotonic Running Max](monotonic-running-max.md) - intentionally stale running max when only improvement matters
- [Running Max with Reset](running-max-with-reset.md) - reset to zero on a break condition; used for streak/run-length problems

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN techniques AS technique
WHERE technique = "running-max"
SORT number asc
```
