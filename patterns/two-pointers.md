# Two Pointers Pattern

**display_name:** Two Pointers - Sorted Pair

## Core Idea

Place two pointers at different positions in a sorted array and move them toward each other based on the sum/comparison result. Each move eliminates part of the search space. One scan covers what would otherwise require nested loops.

## When to Reach for This

- Sorted array with pair relationship (sum, difference, product)
- Range shrinking problems
- Problems where moving left means increasing value, moving right means decreasing
- Removing duplicates while preserving relative order
- Finding closest pair to a target

## Mental Trigger

> "Is the array sorted (or can I sort it)?"
> "Am I looking for a pair where I know moving in one direction increases the value and the other decreases it?"
> "Can I shrink the search window from both ends?"

## Template

```csharp
// Sorted array two pointers
int left = 0, right = nums.Length - 1;

while (left < right)
{
    int sum = nums[left] + nums[right];

    if (sum == target)
    {
        return new int[] { left, right };
    }
    else if (sum < target)
    {
        left++;   // need bigger values, move left pointer right
    }
    else
    {
        right--;  // need smaller values, move right pointer left
    }
}
```

## Tradeoffs

| | Value |
|--|--|
| Time | O(n) for the two pointer scan |
| Space | O(1) if array is already sorted; O(n) if we need to preserve indices via tuple |
| vs HashMap | Requires sorted input; uses less space on sorted input |
| vs Brute Force | O(n) instead of O(n^2) |

## Important: Index Preservation When Sorting

If the problem returns indices (not values), and you sort the array, you must carry the original index with each element:

```csharp
// Attach original index before sorting
(int value, int index)[] arr = new (int, int)[nums.Length];
for (int i = 0; i < nums.Length; i++)
    arr[i] = (nums[i], i);

// Sort by value - original index travels with it
Array.Sort(arr, (a, b) => a.value.CompareTo(b.value));

// Return original indices, not sorted positions
return new int[] { arr[left].index, arr[right].index };
```

This is the "carry identity with data" sub-pattern. Sorting changes positions but must not destroy identity.

## Solved Problems

- **Two Sum** (problems/1-two-sum/solutions/two-pointer.cs) - sorted + index preservation + two pointers

## Try Next

- Two Sum II - Input Already Sorted - same pattern, no index preservation needed
- 3Sum - sort + two pointers as inner loop
- Container With Most Water - shrink by moving the shorter side
- Trapping Rain Water (variant)
- Remove Duplicates from Sorted Array

## Common Mistakes

- **Sorting without preserving indices** - returning `[left, right]` after sort gives sorted positions, not original indices
- **Forgetting the sorted requirement** - two pointers only works correctly when array ordering is meaningful
- **Moving both pointers** - only move one pointer per iteration (the one causing the imbalance)
- **Using `<=` in while condition** - `left < right` is correct; `left <= right` would allow using the same element twice
