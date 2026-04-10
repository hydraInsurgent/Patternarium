---
name: linear-scan
display_name: Linear Scan
category: pattern
variations:
  - name: Neighbor Comparison
    ds: [array, string]
  - name: Running State
    ds: [array]
ds-primary: [array, string]
---

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

## Variation: Running State

**When to reach for this:**
- You need the best/worst/optimal result across an array but the answer depends on a relationship between two positions (e.g., buy before sell)
- Past data can be compressed into a running summary (min, max, best-so-far) rather than stored in full
- The problem asks for a single pass over a sequence where at each step you combine "the best so far" with the current element

**Mental Trigger:**
> "Can I summarize everything I've seen so far into one or two variables, and use that to decide at each step?"
> "Am I storing data or just tracking a running condition?"

**Template:**
```
initialize running state (e.g., min = first element)
initialize result (e.g., best = 0)
for each element from second onward:
    update result using current element and running state
    update running state with current element
```

**Tradeoffs:**
- Time: O(n) - single pass
- Space: O(1) - only running variables
- Order of operations matters when the running state and result interact (e.g., compute profit before updating min to ensure buy-before-sell)

**Solved Problems:**
- **Best Time to Buy and Sell Stock** (problems/121-best-time-to-buy-and-sell-stock/solutions/running-state.cs) - track running minimum price, compute max profit at each step

---

## Try Next

- Trapping Rain Water
- Maximum Subarray

## Common Mistakes

- **Off-by-one in loop bounds** - skipping first or last element when comparing with a neighbor
- **Forgetting the boundary element** - the element with no neighbor to compare against needs special handling
- **Getting comparison direction backwards** - `>` vs `<` when switching scan direction

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Linear Scan"
SORT number asc
```
