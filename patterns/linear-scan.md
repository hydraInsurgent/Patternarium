# Linear Scan

**display_name:** Linear Scan

## Core Idea

Process elements one by one in a single pass, using local context (current element, neighbor, or accumulated state) to decide what operation to apply. No backtracking, no restarting.

## Variation: Neighbor Comparison

**When to reach for this:**
- Value or operation of an element depends on what is next to it
- Directional rules (add vs subtract based on adjacent relationship)
- Sign or operation changes based on left or right neighbor

**Mental Trigger:**
> "Does the meaning of this element change based on what's next to it?"
> "Do I need to look left or right to decide what to do with the current element?"

**Template:**
```
for each element:
    compare with neighbor (left or right)
    if condition: apply operation A
    else: apply operation B
    track current for next comparison
```

**Tradeoffs:**
- Time: O(n) - single pass
- Space: O(1) - only tracking previous/next
- Works in either direction (left-to-right or right-to-left)

**Solved Problems:**
- **Roman to Integer** (problems/13-roman-to-integer/solutions/right-to-left-scan.cs, left-to-right-scan.cs) - add or subtract based on comparison with adjacent value
- **Longest Consecutive Sequence** (problems/128-longest-consecutive-sequence/solutions/sort-linear-scan.cs) - sort first, then scan comparing each element to previous to detect consecutive runs

---

## Try Next

- Best Time to Buy and Sell Stock
- Trapping Rain Water

## Common Mistakes

- **Off-by-one in loop bounds** - skipping first or last element when comparing with a neighbor
- **Forgetting the boundary element** - the element with no neighbor to compare against needs special handling
- **Getting comparison direction backwards** - `>` vs `<` when switching scan direction
