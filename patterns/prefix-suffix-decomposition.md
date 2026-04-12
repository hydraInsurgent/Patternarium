---
name: prefix-suffix-decomposition
display_name: Prefix/Suffix Decomposition
category: pattern
variations:
  - name: Prefix/Suffix Product
    ds: [array]
ds-primary: [array]
---

# Prefix/Suffix Decomposition

## Core Idea

Break a problem into two directional subproblems: what comes before each position (prefix) and what comes after (suffix). Solve each direction independently, then combine the results. This transforms "compute something about everything else" into two simpler passes.

## Variation: Prefix/Suffix Product

**When to reach for this:**
- The answer at each position depends on all other elements
- Division is not allowed or not safe (zeros, modular arithmetic)
- You can express the answer as a combination of "left side" and "right side"

**Mental Trigger:**
> "What is this element's answer actually made of?"
> "Can I split 'everything except self' into 'everything before' and 'everything after'?"

**Template:**
```csharp
// Build prefix products
int[] prefix = new int[n];
prefix[0] = 1;
for (int i = 1; i < n; i++)
    prefix[i] = prefix[i - 1] * nums[i - 1];

// Build suffix products
int[] suffix = new int[n];
suffix[n - 1] = 1;
for (int i = n - 2; i >= 0; i--)
    suffix[i] = suffix[i + 1] * nums[i + 1];

// Combine
for (int i = 0; i < n; i++)
    output[i] = prefix[i] * suffix[i];
```

**Tradeoffs:**
- Time: O(n) - two or three linear passes
- Space: O(n) for separate arrays, O(1) extra if output array is used as working space
- Avoids division entirely - safe with zeros and edge cases

**Solved Problems:**
- **Product of Array Except Self** (problems/238-product-of-array-except-self/solutions/prefix-suffix-arrays.cs) - precompute left and right product arrays, combine with complementary indices
- **Product of Array Except Self** (problems/238-product-of-array-except-self/solutions/two-pointer-converging.cs) - same decomposition but deposited via converging/diverging pointers
- **Product of Array Except Self** (problems/238-product-of-array-except-self/solutions/two-pass-sequential.cs) - same decomposition with two sequential passes into the output array

---

## Try Next

- Trapping Rain Water - left max and right max at each position
- Prefix Sum problems - running sum from the left
- Range Sum Query - precompute prefix sums for O(1) range queries

## Common Mistakes

- **Including the current element in its own prefix/suffix** - `prefix[i] = prefix[i-1] * nums[i]` includes nums[i] in its own left product. Must use `nums[i-1]` to exclude self
- **Using same-direction indices when combining** - if suffix is built in a different direction, you may need complementary indices to pair correctly

## Solved Problems

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Prefix/Suffix Decomposition"
SORT problem asc
```
