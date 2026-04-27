---
name: presence-array
display_name: Presence Array
category: pattern
variations:
  - name: Boolean Presence
    ds: [array]
  - name: Integer Slot
    ds: [string, array]
ds-primary: [array, string]
---

# Presence Array

## Core Idea

Map values to indices in an array using a known offset. Direct index access replaces hash lookup with true O(1) array access. Consecutive indices in the array correspond directly to consecutive values.

The key question: are my values integers (or chars) in a known, bounded range? If so, use the value itself as an index.

## When to Reach for This

- Values are integers (or chars) in a known, bounded range
- Deduplication or consecutive run detection on dense integer input
- When HashSet or Dictionary overhead matters and range is manageable (rule of thumb: range < 1,000,000)

**Mental Trigger:**
> "Are my values integers or chars in a small range? Can I use the value itself as an index?"
> "Would an array indexed by value be cheaper than a HashSet or Dictionary here?"
> "Do I need presence only (bool) or a stored value per slot (int)?"

## Variation: Boolean Presence

**When to reach for this:**
- Only need to know if a value exists - true/false per slot
- Consecutive run detection (scan for longest true run)
- Deduplication of dense integer input

**Template:**
```csharp
int min = nums.Min();
int max = nums.Max();
bool[] present = new bool[max - min + 1];

foreach (int n in nums)
    present[n - min] = true;  // offset by min to normalize index

// Scan for longest consecutive true run
int count = 0;
foreach (bool slot in present)
{
    if (slot) count++;       // check the slot itself, not its neighbor
    else count = 0;          // reset to 0, not 1 - next true does count++ to reach 1
    // update max
}
```

**Solved Problems:**
- **Longest Consecutive Sequence** (problems/128-longest-consecutive-sequence/solutions/presence-array.cs) - mark presence, scan for longest true run, with HashSet fallback for large ranges
- **Missing Number** (problems/268-missing-number/solutions/boolean-flag-array.cs) - mark each value as seen, find the one slot that stays false

---

## Variation: Integer Slot

**When to reach for this:**
- Need to store a value per slot (last seen index, count, frequency)
- ASCII character problems - char casts directly to index 0-127, no offset needed
- When distinguishing "never seen" (default 0) from "seen at index 0" requires the index+1 trick

**Template:**
```csharp
// For ASCII chars - no offset needed, char casts directly to index
int[] lastSeen = new int[128]; // default 0 = "never seen"

// Store index+1 to distinguish "never seen" (0) from "seen at index 0" (1)
lastSeen[s[i]] = i + 1;

// To read: lastSeen[c] - 1 gives the actual index (check > 0 first)
if (lastSeen[s[end]] > 0)
{
    int actualIndex = lastSeen[s[end]] - 1;
    // use actualIndex
}
```

**Solved Problems:**
- **Longest Substring Without Repeating Characters** (problems/3-longest-substring-without-repeating-characters/solutions/sliding-window-span.cs) - int[128] indexed by ASCII char value; stores end+1 to distinguish "never seen" from "seen at 0"

---

## Tradeoffs

| | Value |
|--|--|
| Time | O(n) if array is only used for lookups within a single pass. O(n + r) if you fill then scan the full range (r = max - min + 1) |
| Space | O(r) - array of size range |
| vs HashSet | Faster access, but fails when range is huge (e.g. -10^9 to 10^9) |
| Use when | Range r is small and known upfront |
| Avoid when | Range is unbounded or very large |

## Try Next

- Contains Duplicate II - store last seen index per value, check distance (Integer Slot)
- First Missing Positive - index marking with negation trick (Boolean Presence variant)

## Common Mistakes

- **Checking neighbor instead of slot** - `if(current == last)` catches false==false as consecutive. Check `if(current)` directly (Boolean Presence)
- **Resetting count to 1 instead of 0** - after a gap, the next true slot does count++, so reset must be 0 or the first element after a gap counts as 2 (Boolean Presence)
- **Forgetting the offset** - using `present[n]` instead of `present[n - min]` causes index out of bounds
- **Not using index+1 trick** - default value 0 is indistinguishable from "seen at index 0" without the +1 shift (Integer Slot)

## Solved Problems

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Presence Array"
SORT problem asc
```
