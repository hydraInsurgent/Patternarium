# Presence Array

**display_name:** Presence Array - Bucket Marking

## Core Idea
Map values to indices in a boolean array using an offset (value - min). Direct index access replaces O(1) hash lookup with true O(1) array access. Consecutive indices in the array correspond directly to consecutive values.

## When to Reach for This
- Values are integers in a known, bounded range
- You need to answer "does this value exist?" and the range is small enough to allocate
- Deduplication or consecutive run detection on dense integer input
- When HashSet overhead matters and range is manageable (rule of thumb: range < 1,000,000)

## Mental Trigger
> "Are my values integers in a small range? Can I use the value itself as an index?"
> "Would a boolean array indexed by value be cheaper than a HashSet here?"

## Template
```csharp
int min = nums.Min();
int max = nums.Max();
int size = max - min + 1;

bool[] present = new bool[size];
foreach (int n in nums)
    present[n - min] = true;  // offset by min to normalize index

// scan for longest consecutive true run
int maxLen = 0, count = 0;
foreach (bool b in present)
{
    if (b) count++;
    else count = 0;  // reset to 0, not 1
    maxLen = Math.Max(maxLen, count);
}
```

## Tradeoffs
| | Value |
|--|--|
| Time | O(n + r) where r = max - min + 1 |
| Space | O(r) - array of size range |
| vs HashSet | Faster access, but fails when range is huge (e.g. -10^9 to 10^9) |
| Use when | Range r is small and known upfront |
| Avoid when | Range is unbounded or very large |

## Common Mistakes
- **Checking `current == last` instead of `if(current)`** - boolean neighbor comparison catches false==false as consecutive. Only check if the current slot is true.
- **Resetting count to 1 instead of 0** - after a gap, the next true does count++, so reset must be 0 or the first element after a gap counts as 2
- **Forgetting the offset** - using `present[n]` instead of `present[n - min]` causes index out of bounds

## Solved Problems
- **Longest Consecutive Sequence** (Approach 3) - mark presence, scan for longest true run, with HashSet fallback for large ranges
