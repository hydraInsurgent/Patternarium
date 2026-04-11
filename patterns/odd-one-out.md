---
name: odd-one-out
display_name: Odd One Out
category: pattern
variations:
  - name: Boolean Presence Check
    ds: [array]
  - name: Gauss Sum (Math)
    ds: [array]
  - name: XOR Cancellation
    ds: [array]
ds-primary: [array]
---

# Odd One Out

## Core Idea

A complete set of values is expected, but one (or more) is missing, duplicated, or unpaired. Use a property of the full set - mathematical, bitwise, or structural - to isolate the element that doesn't belong.

The key question: what do I know about the complete set, and how can I use that knowledge to find what's different?

## When to Reach for This

- You have a range or set of values and exactly one is missing or extra
- All other elements are paired or accounted for
- The problem says "find the missing", "find the duplicate", or "find the unpaired"

**Mental Trigger:**
> "I know what the complete set should look like - what's different about what I actually have?"
> "If I had all the expected values, what operation would cancel them out and leave only the answer?"

## Variation: Boolean Presence Check

**When to reach for this:**
- Range is small and bounded (fits in memory as an array)
- You need O(n) time and can afford O(n) space

**Template:**
```csharp
bool[] seen = new bool[n + 1];
foreach (int num in nums)
    seen[num] = true;

for (int i = 0; i <= n; i++)
    if (!seen[i]) return i;
```

**Solved Problems:**
- **Missing Number** (#268) - boolean array of size n+1, mark seen values, find the false slot

---

## Variation: Gauss Sum (Math)

**When to reach for this:**
- Range is [0..n] or [1..n] (a known arithmetic sequence)
- You need O(n) time and O(1) space
- The missing element is a single number

**Template:**
```csharp
int n = nums.Length;
int expected = n * (n + 1) / 2;
int actual = 0;
foreach (int num in nums) actual += num;
return expected - actual;
```

**Solved Problems:**
- **Missing Number** (#268) - expected sum minus actual sum gives the missing number

---

## Variation: XOR Cancellation

**When to reach for this:**
- You need O(n) time and O(1) space
- The missing/unpaired element needs to be isolated without arithmetic
- Works even when the range is not a clean arithmetic sequence

**Template:**
```csharp
int result = 0;
for (int i = 0; i < nums.Length; i++)
    result ^= i ^ nums[i];
result ^= nums.Length; // XOR the last expected index
return result;
```

**Key insight:** XOR every expected value against every actual value. Paired numbers cancel (n XOR n = 0). The survivor is the answer.

**Solved Problems:**
- **Missing Number** (#268) - XOR all indices [0..n] with all array values; the missing number survives

---

## Tradeoffs

| Variation | Time | Space | Use When |
|-----------|------|-------|----------|
| Boolean Presence | O(n) | O(n) | Range is small, space is not a concern |
| Gauss Sum | O(n) | O(1) | Range is a clean arithmetic sequence [0..n] |
| XOR Cancellation | O(n) | O(1) | Need O(1) space, no arithmetic required |

## Common Mistakes

- **Boolean Presence - using loop index instead of value** - `seen[i] = true` marks the position, not the number. Must be `seen[nums[i]] = true`
- **Gauss Sum - integer overflow** - for large n, `n * (n + 1)` can overflow int. Use `long` if needed
- **XOR - forgetting the last index** - the loop runs 0 to n-1, so index n must be XOR'd separately

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Odd One Out"
SORT number asc
```
