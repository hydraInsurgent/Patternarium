# Linear Scan - Neighbor Comparison

## Core Idea
Process elements by comparing each with its neighbor to decide what operation to apply.

## When to Reach for This
- Value of an element depends on what's next to it
- Directional rules (add vs subtract based on context)
- Sign or operation changes based on adjacent relationship

## Mental Trigger
> "Does the meaning of this element change based on what's next to it?"

## Template
```
for each element:
    compare with neighbor (left or right)
    if condition: apply operation A
    else: apply operation B
    track current for next comparison
```

## Tradeoffs
- Time: O(n) - single pass
- Space: O(1) - only tracking previous/next
- Works in either direction (left-to-right or right-to-left)

## Solved Problems
- Roman to Integer (right-to-left scan, left-to-right scan)

## Try Next
- Best Time to Buy and Sell Stock
- Trapping Rain Water

## Common Mistakes
- Off-by-one in loop bounds when comparing with neighbor (skipping first or last element)
- Forgetting to handle the boundary element that has no neighbor to compare with
- Getting the comparison direction backwards (> vs <) when switching scan direction
