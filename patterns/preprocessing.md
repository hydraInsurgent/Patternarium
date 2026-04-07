# Preprocessing

**display_name:** Preprocessing

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

## Try Next

- Calculator problems
- Expression Evaluation
- Decode String

## Common Mistakes

- **Forgetting C# strings are immutable** - `Replace()` returns a new string, does not modify in place
- **Not accounting for space cost** - the transformed input requires O(n) extra space
- **Order of replacements** - replacements can interfere if they overlap; order may matter
