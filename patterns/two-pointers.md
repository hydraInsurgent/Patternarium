---
name: two-pointers
display_name: Two Pointers
category: pattern
variations:
  - name: Sorted Pair
    ds: [array]
  - name: Symmetry Check
    ds: [string]
  - name: Expand Around Center
    ds: [string]
  - name: Converging/Diverging
    ds: [array]
  - name: Parallel Merge
    ds: [linked-list, array]
ds-primary: [array, string, linked-list]
---

# Two Pointers Pattern

## Core Idea

Place two pointers at different positions in a sequence and move them toward each other (or outward) based on a condition. Each move eliminates part of the search space. One scan covers what would otherwise require nested loops.

The pointers define their own boundary. The outer `while (left < right)` is the single source of termination - inner logic should be bounded relative to the pointers, not the array edges.

## Variation: Sorted Pair

**When to reach for this:**
- Sorted array with pair relationship (sum, difference, product)
- Range shrinking problems
- Problems where moving left increases value and moving right decreases it
- Finding closest pair to a target
- Value or validity depends only on the two endpoint positions, not on elements between them

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
- **Reaching for sliding window when only endpoints matter** - sliding window requires a validity condition on window contents. If the problem only cares about the two boundary positions (not elements between them), there is no contraction rule and two pointers applies instead (Container With Most Water)

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

## Variation: Converging/Diverging

**When to reach for this:**
- You need to build information from both ends simultaneously
- The answer at each position combines a left component and a right component
- You want O(1) extra space by using the output array as working storage

**Mental Trigger:**
> "Can I deposit partial results from both ends and complete them after the pointers cross?"
> "Do I need to change direction mid-algorithm?"

**Template:**
```csharp
int left = 0, right = n - 1;
int leftProduct = 1, rightProduct = 1;

// Phase 1: Converge - deposit one half at each position
while (left < right)
{
    output[left] = leftProduct;
    output[right] = rightProduct;
    leftProduct *= nums[left];
    rightProduct *= nums[right];
    left++;
    right--;
}

// Phase 2: Diverge - complete each position with the other half
// (pointers continue moving in the same direction, visiting slots
// that already have one half stored)
```

**Tradeoffs:**

| | Value |
|--|--|
| Time | O(n) |
| Space | O(1) extra - output array is the only allocation |
| vs Two-Pass | Same complexity, more complex logic, fewer loops |
| vs Separate Arrays | Saves O(n) space but adds center-element handling for odd-length inputs |

**Common Mistakes:**
- **Odd-length center element unhandled** - when converging, the center element is never visited if `left < right` exits before they meet. Need a special case when `left == right` to assign both halves directly
- **Even-length crossing** - pointers cross without meeting, which is fine as long as Phase 2 handles all remaining positions

**Solved Problems:**
- **Product of Array Except Self** (problems/238-product-of-array-except-self/solutions/two-pointer-converging.cs) - converge depositing partial products, diverge completing each position

---

## Variation: Parallel Merge

**When to reach for this:**
- Two sorted sequences need to merge into one sorted output
- One pointer per sequence, advanced by comparison rather than by position
- Linked lists where the merge can splice existing nodes without allocation
- Arrays where the merge step of merge sort applies
- Generally: any problem of the form "produce sorted output from two pre-sorted inputs"

**Mental Trigger:**
> "Two sorted sequences in, one sorted sequence out - which head is smaller right now?"
> "Can I walk both with one pointer each and pick the smaller current value at every step?"
> "Do I need to allocate new storage, or can I reroute existing pointers in place?"

**Template (linked list, iterative):**
```csharp
// Seed the head from whichever input has the smaller first node
ListNode mergedList = null;
if (list1 == null) mergedList = list2;
else if (list2 == null) mergedList = list1;
else if (list1.val <= list2.val) { mergedList = list1; list1 = list1.next; }
else { mergedList = list2; list2 = list2.next; }

ListNode tail = mergedList;

// Advance whichever pointer has the smaller head; splice that node onto tail
while (list1 != null && list2 != null)
{
    if (list1.val <= list2.val) { tail.next = list1; list1 = list1.next; }
    else                         { tail.next = list2; list2 = list2.next; }
    tail = tail.next;
}

// Append whatever remains (one of the two will already be null)
tail.next = (list1 != null) ? list1 : list2;
return mergedList;
```

**Recursive form:** the same compare-and-pick logic, but each recursive frame contributes one node and one `.next` link. The frame that picks the smaller node returns it, and the parent frame wires that returned node into its own `.next`. Recursive form is O(m + n) space due to the call stack.

**Tradeoffs:**

| | Iterative | Recursive |
|--|--|--|
| Time | O(m + n) | O(m + n) |
| Space | O(1) | O(m + n) - call stack |
| When to prefer | Default for production | Cleaner for the merge step of recursive algorithms (e.g. merge sort) |
| Risk | Verbose null handling without a dummy head | Stack overflow on long lists |

**Dummy-head simplification:**
```csharp
ListNode dummy = new ListNode();
ListNode tail = dummy;
while (list1 != null && list2 != null) { ... }
tail.next = list1 ?? list2;
return dummy.next;
```
The dummy node collapses pre-loop initialization into a single line and removes the four null-case branches. The trade-off is one extra allocation of a sentinel node; the chain itself still uses existing nodes.

**Common Mistakes:**
- **Allocating new nodes** - reading "splicing" in the problem statement without registering it. The output reuses existing nodes; only `.next` pointers are rewritten. Allocating new ones is correct but wastes O(m + n) space.
- **Using one variable as both head and cursor** - the cursor advances each iteration, which loses the head reference. Two separate variables required: a frozen head for return, a moving tail.
- **`ref` to a field as a moving cursor (recursive form)** - `ref tail.next` aliases ONE storage slot, not a position that advances through the chain. Every recursive frame writes to the same slot; the last write wins. To build a chain across frames, write into the field via `out tail.next` (so each call writes into the actual previous-tail's next field), and pass a value-typed `tail` local that advances between frames.
- **Pre-loop null deref on inputs** - comparing `list1.val <= list2.val` before checking either is null crashes when one (or both) inputs are empty. Either guard the seeding step explicitly, or use the dummy-head form which sidesteps this.
- **Two-input complexity shorthand** - O(n) is wrong when the two inputs have independent sizes. The correct form is O(m + n).
- **C# parameter passing for the helper** - if a helper picks the smaller head and advances the source, the source pointers need `ref` (read AND write); the picked-node output needs `out` (pure write). Marking everything `out` fails to compile because `out` forbids reading before assigning, and the helper reads the source pointers first thing.

**Solved Problems:**
- **Merge Two Sorted Lists** (problems/21-merge-two-sorted-lists/solutions/iterative-splicing.cs) - iterative two-pointer splicing on linked lists
- **Merge Two Sorted Lists** (problems/21-merge-two-sorted-lists/solutions/recursive-splicing.cs) - same pattern, recursion replaces the loop

---

## Try Next

- Two Sum II - Input Already Sorted (Sorted Pair)
- 3Sum - sort + two pointers as inner loop (Sorted Pair)
- Container With Most Water - shrink by moving the shorter side (Sorted Pair) ✓ solved
- Trapping Rain Water variant (Sorted Pair / Converging/Diverging)
- Remove Duplicates from Sorted Array (Sorted Pair)
- Valid Palindrome II - can remove one character (Symmetry Check)
- Longest Palindromic Substring - expand outward from center (Expand Around Center) ✓ solved
- Merge Two Sorted Lists - one pointer per list, pick smaller (Parallel Merge) ✓ solved
- Merge k Sorted Lists - generalizes Parallel Merge with a min-heap over the heads (Parallel Merge variant)
- Merge Sorted Array (LC 88) - same pattern on arrays instead of linked lists (Parallel Merge)

## Solved Problems

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Two Pointers"
SORT problem asc
```
