# Presence Array

**display_name:** Presence Array - Bucket Marking

## Core Idea
Map values to indices in an array using a known offset. Direct index access replaces hash lookup with true O(1) array access. The array slot can hold a boolean (presence only) or an integer (last seen index, count, or any associated value). Consecutive indices in the array correspond directly to consecutive values.

## When to Reach for This
- Values are integers (or chars) in a known, bounded range
- You need to answer "does this value exist?" - use bool[]
- You need to track position, count, or any value per slot - use int[]
- Deduplication or consecutive run detection on dense integer input
- When HashSet or Dictionary overhead matters and range is manageable (rule of thumb: range < 1,000,000)

## Mental Trigger
> "Are my values integers or chars in a small range? Can I use the value itself as an index?"
> "Would an array indexed by value be cheaper than a HashSet or Dictionary here?"
> "Do I need presence only (bool) or a stored value per slot (int)?"

## Template

**Boolean variant (presence only):**
```csharp
int min = nums.Min();
bool[] present = new bool[max - min + 1];
foreach (int n in nums)
    present[n - min] = true;  // offset by min to normalize index
```

**Integer variant (store a value per slot):**
```csharp
// For ASCII chars - no offset needed, char casts directly to index
int[] lastSeen = new int[128]; // default 0 = "never seen"
// Store index+1 to distinguish "never seen" (0) from "seen at index 0" (1)
lastSeen[c] = index + 1;

// To read: lastSeen[c] - 1 gives the actual index (or check > 0 first)
```

## Tradeoffs
| | Value |
|--|--|
| Time | O(n) if array is only used for lookups within a single pass. O(n + r) if you fill then scan the full range (r = max - min + 1). |
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
- **Longest Substring Without Repeating Characters** (Approach 2.1) - int[128] indexed by ASCII char value; extended from bool to int to store last seen index+1
