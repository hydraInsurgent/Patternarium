---
name: running-max-with-reset
slug: running-max-with-reset
display_name: Running Max with Reset
category: technique
tags: [running-max-with-reset, running-max, linear-scan, streak]
---

# Running Max with Reset

## What It Is
Walk a sequence carrying a running counter. On a "break" condition, settle the counter into a running max and reset it to zero. After the loop, settle once more so a streak that runs to the end isn't lost.

## Core Reasoning
This is Running Max applied to *streaks* rather than values. The current streak count is only meaningful within an unbroken run. When a break occurs, the streak ends and its value should compete for the maximum once - then the counter starts fresh for the next streak. The post-loop settle catches the case where the array ends mid-streak (no break happened to trigger an in-loop settle).

## When to Apply
- You are counting consecutive runs that match a condition (consecutive 1s, consecutive non-zeros, longest streak under a constraint)
- Each "break" element ends the current run and starts a new one
- You only care about the longest run, not its position

## Template
```csharp
int maxStreak = 0;
int streak = 0;

for (int i = 0; i < n; i++)
{
    if (isBreak(arr[i]))
    {
        maxStreak = Math.Max(maxStreak, streak);
        streak = 0;
    }
    else
    {
        streak++;
    }
}
maxStreak = Math.Max(maxStreak, streak); // catches a streak that runs to the end

return maxStreak;
```

## Tradeoffs
- O(n) time, O(1) space - no auxiliary structures
- Distinct from plain Running Max: reset semantics matter. Forgetting either the in-loop settle or the post-loop settle is the classic bug
- For "longest streak with at most k flips" style problems this technique is the foundation, but Sliding Window extends it

## Related
- [Running Max](running-max.md) - the parent technique without reset semantics

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN techniques AS technique
WHERE technique = "running-max-with-reset"
SORT problem asc
```
