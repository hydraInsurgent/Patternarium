# Two Pointers Pattern

**display_name:** Two Pointers

## Core Idea

Place two pointers at different positions in a sequence and move them toward each other (or outward) based on a condition. Each move eliminates part of the search space. One scan covers what would otherwise require nested loops.

The pointers define their own boundary. The outer `while (left < right)` is the single source of termination - inner logic should be bounded relative to the pointers, not the array edges.

## Variation: Sorted Pair

**When to reach for this:**
- Sorted array with pair relationship (sum, difference, product)
- Range shrinking problems
- Problems where moving left increases value and moving right decreases it
- Finding closest pair to a target

**Mental Trigger:**
> "Is the array sorted (or can I sort it)?"
> "Am I looking for a pair where moving one pointer changes the result in a predictable direction?"
> "Can I shrink the search window from both ends?"

**Template:**
```csharp
int left = 0, right = nums.Length - 1;

while (left < right)
{
    int sum = nums[left] + nums[right];

    if (sum == target)
        return new int[] { left, right };
    else if (sum < target)
        left++;   // need bigger values
    else
        right--;  // need smaller values
}
```

**Important: Index Preservation When Sorting**

If the problem returns indices (not values), carry the original index with each element before sorting:

```csharp
(int value, int index)[] arr = new (int, int)[nums.Length];
for (int i = 0; i < nums.Length; i++)
    arr[i] = (nums[i], i);

Array.Sort(arr, (a, b) => a.value.CompareTo(b.value));

return new int[] { arr[left].index, arr[right].index };
```

This is the "carry identity with data" sub-pattern. Sorting changes positions but must not destroy identity.

**Tradeoffs:**

| | Value |
|--|--|
| Time | O(n) for the two pointer scan |
| Space | O(1) if array is already sorted; O(n) if preserving indices via tuple |
| vs HashMap | Requires sorted input; uses less space on sorted input |
| vs Brute Force | O(n) instead of O(n^2) |

**Common Mistakes:**
- **Sorting without preserving indices** - returning `[left, right]` after sort gives sorted positions, not original indices
- **Forgetting the sorted requirement** - two pointers only works correctly when array ordering is meaningful
- **Moving both pointers** - only move one pointer per iteration (the one causing the imbalance)
- **Using `<=` in while condition** - `left < right` is correct; `left <= right` would allow using the same element twice

**Solved Problems:**
- **Two Sum** (problems/1-two-sum/solutions/two-pointer.cs) - sorted + index preservation + two pointers

---

## Variation: Symmetry Check

**When to reach for this:**
- Check if a string or sequence reads the same forwards and backwards
- Sorting not required - structure is the check
- Characters or elements may need to be skipped (non-alphanumeric, whitespace)

**Mental Trigger:**
> "Does it need to match from both ends toward the center?"
> "Am I verifying symmetry, not searching for a target value?"
> "Do I need to skip certain characters while comparing?"

**Template:**
```csharp
int left = 0, right = s.Length - 1;

while (left < right)
{
    // Skip invalid characters - bound by left < right, not array edges
    while (!IsValid(s[left]) && left < right) left++;
    while (!IsValid(s[right]) && right > left) right--;

    if (Normalize(s[left]) != Normalize(s[right]))
        return false;

    // Advance unconditionally - outer while handles termination
    left++;
    right--;
}

return true;
```

**Tradeoffs:**

| | Value |
|--|--|
| Time | O(n) |
| Space | O(1) if skipping in-place; O(n) if cleaning the string first |
| Clean-first vs Skip-in-place | Clean-first is simpler to read; skip-in-place saves space |

**Common Mistakes:**
- **Using array bounds in inner skip loops** - `left < s.Length-1` instead of `left < right` forces extra guard conditions and conditional pointer advancement. Use relative bounds so the outer while handles termination naturally

**Solved Problems:**
- **Valid Palindrome** (problems/125-valid-palindrome/solutions/inward-two-pointer.cs) - inward convergence from both ends to verify string symmetry

---

## Variation: Expand Around Center

**When to reach for this:**
- Finding the longest palindromic substring
- Any problem where you need to discover a structure by growing outward from a known point
- Two shapes possible (odd/even length) that both need to be checked

**Mental Trigger:**
> "Is there a center I can fix and expand from?"
> "Am I looking for a symmetric structure within a string?"
> "Do I need to check two starting configurations per position?"

**Template:**
```csharp
// Run for every index i - once for odd center, once for even center
(int l, int r) odd  = Expand(s, i, i);
(int l, int r) even = Expand(s, i, i + 1);

private (int l, int r) Expand(string s, int left, int right)
{
    while (left > 0 && right < s.Length - 1 && s[left] == s[right])
    {
        left--;
        right++;
    }

    if (s[left] != s[right]) { left++; right--; }

    return (left, right);
}
```

**Key insight:** No upfront detection tells you which center shape will be longer. Always run both and compare after.

**Tradeoffs:**

| | Value |
|--|--|
| Time | O(n²) - n centers, O(n) expansion each |
| Space | O(1) |
| vs Brute Force | O(n²) vs O(n³) - expansion avoids re-checking every substring |
| vs Manacher's | Manacher's achieves O(n) by reusing expansion results |

**Common Mistakes:**
- **Detecting even/odd upfront** - writing complex detection logic to avoid two calls. Detection grows complicated and breaks edge cases. Just run both.
- **Using n/2 as center** - center of the search space, not the current loop index. Each index i is the center being tested.
- **Boundary off-by-one** - `left > 0` stops before checking index 0; `left >= 0` is needed if the loop should include the first character

**Solved Problems:**
- **Longest Palindromic Substring** (problems/5-longest-palindromic-substring/solutions/expand-around-center.cs) - two expansions per index, track longest result

---

## Try Next

- Two Sum II - Input Already Sorted (Sorted Pair)
- 3Sum - sort + two pointers as inner loop (Sorted Pair)
- Container With Most Water - shrink by moving the shorter side (Sorted Pair)
- Trapping Rain Water variant (Sorted Pair)
- Remove Duplicates from Sorted Array (Sorted Pair)
- Valid Palindrome II - can remove one character (Symmetry Check)
- Longest Palindromic Substring - expand outward from center (Expand Around Center) ✓ solved
