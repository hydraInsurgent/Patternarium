---
name: preprocessing
display_name: Preprocessing
category: pattern
variations:
  - name: Normalize Before Compute
    ds: [string]
  - name: Sort to Expose Structure
    ds: [array]
  - name: Build Derived Data
    ds: [array]
ds-primary: [string, array]
---

# Preprocessing

## Core Idea

Transform input into a simpler form before applying the main logic. Eliminate special cases upfront so the main loop stays clean.

## Variation: Normalize Before Compute

**When to reach for this:**
- Special cases complicate the main loop
- Multi-character tokens can be reduced to single tokens
- Edge cases are fixed and known upfront

**Mental Trigger:**
> "Can I eliminate the complexity before I even start the main logic?"
> "Are the special cases fixed and enumerable? Can I just replace them all first?"

**Template:**
```
for each known special case:
    replace/transform in input
run simple main logic on cleaned input
```

**Tradeoffs:**
- Time: O(n) - preprocessing pass + main pass
- Space: O(n) - may create new transformed input (e.g., new string)
- Only works when special cases are fixed and enumerable
- Trades space for simpler logic

**Solved Problems:**
- **Roman to Integer** (problems/13-roman-to-integer/solutions/string-replacement.cs) - replace the 6 subtraction pairs with single-character placeholders before summing

---

## Variation: Sort to Expose Structure

**When to reach for this:**
- Duplicates, pairs, or relationships are hard to detect in arbitrary order
- Sorting brings the relevant elements adjacent so a simple scan can finish the job
- You can trade O(n log n) time for O(1) space vs a HashSet approach

**Mental Trigger:**
> "Would this be obvious if the array were sorted?"
> "Can sorting bring the things I want to compare next to each other?"

**Template:**
```csharp
Array.Sort(nums); // in-place, O(n log n)

for (int i = 0; i < nums.Length - 1; i++)
{
    if (nums[i] == nums[i + 1])
        // duplicate found - act here
}
```

**Tradeoffs:**
- Time: O(n log n) - dominated by sort
- Space: O(1) - in-place sort, no extra structure
- Mutates the original array - not safe if caller needs original order preserved

**Solved Problems:**
- **Contains Duplicate** (problems/217-contains-duplicate/solutions/sort-adjacent-check.cs) - sort so duplicates land adjacent, then scan for equal neighbors
- **Longest Consecutive Sequence** (problems/128-longest-consecutive-sequence/solutions/sort-linear-scan.cs) - sort so consecutive numbers land adjacent, then scan for runs (combined with Linear Scan - Neighbor Comparison)
- **Valid Anagram** (problems/242-valid-anagram/solutions/sort-compare.cs) - sort both strings so anagrams become identical, then compare index by index

---

## Variation: Build Derived Data

**When to reach for this:**
- The main logic needs information that is expensive to compute on the fly
- You can precompute auxiliary arrays or structures in O(n) to make the main logic simple
- The derived data is not a transformation of the input but new data computed from it

**Mental Trigger:**
> "What if I precomputed this value for every position before starting the main logic?"
> "Am I recomputing the same thing inside a loop that I could compute once upfront?"

**Template:**
```csharp
// Precompute derived data
int[] derived = new int[n];
for (int i = 0; i < n; i++)
    derived[i] = /* compute from input */;

// Main logic uses derived data directly
for (int i = 0; i < n; i++)
    result[i] = /* combine derived values */;
```

**Tradeoffs:**
- Time: O(n) for precomputation + O(n) for main logic = O(n) total
- Space: O(n) for the derived arrays
- Trades space for avoiding redundant computation inside the main loop

**Solved Problems:**
- **Product of Array Except Self** (problems/238-product-of-array-except-self/solutions/prefix-suffix-arrays.cs) - precompute left and right product arrays, then combine

---

## Try Next

- Calculator problems
- Expression Evaluation
- Decode String

## Common Mistakes

- **Forgetting C# strings are immutable** - `Replace()` returns a new string, does not modify in place
- **Not accounting for space cost** - the transformed input requires O(n) extra space
- **Order of replacements** - replacements can interfere if they overlap; order may matter

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Preprocessing"
SORT number asc
```
- **Including the current element in its own derived value** - when building prefix/suffix arrays, off-by-one in the index formula can include the element at position i in its own precomputed value
