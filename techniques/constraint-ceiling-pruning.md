---
name: constraint-ceiling-pruning
slug: constraint-ceiling-pruning
display_name: Constraint-Ceiling Pruning
category: technique
tags: [constraint-ceiling-pruning, early-exit, optimization, constraints]
---

# Constraint-Ceiling Pruning

## What It Is
Use a problem constraint as a theoretical ceiling - compute the maximum possible value remaining, and exit early when that ceiling can no longer beat your current best.

## Core Reasoning
If even the best case from this point forward cannot improve the answer, every remaining iteration is wasted work. Problem constraints give you the ceiling for free: the maximum any single element can contribute is bounded by the constraint.

The argument: `remaining_capacity * MAX_ELEMENT_VALUE` is the theoretical best case from here. If this is already below your current best, no future state can help you.

## When to Apply
- You have a running best value
- Remaining capacity decreases monotonically as you progress (width shrinks, array length shrinks, etc.)
- The problem constraints explicitly bound the maximum value of any single element
- You expect inputs where a good answer is found early (pays off on lucky inputs; no-op on unlucky ones)

## Template
```csharp
int MAX_CONSTRAINT = 10000; // from problem constraints

while (left < right && (right - left) * MAX_CONSTRAINT > best)
{
    best = Math.Max(best, computeValue(left, right));
    // move pointers
}
```

## Tradeoffs
- Worst-case time complexity is unchanged - the condition may never trigger
- Best case: exits O(n) iterations early when a large answer is found with wide capacity
- Most effective on long inputs where the best answer appears near the start
- Adds one multiply + compare per iteration - negligible cost

## Example
Container With Most Water (n = 10000, max height = 10000):
- Find `res = 90,000,000` at width 9999
- At width 9000: `9000 * 10000 = 90,000,000` - not greater than res, exit
- Skipped ~9000 iterations

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN techniques AS technique
WHERE technique = "constraint-ceiling-pruning"
SORT number asc
```
