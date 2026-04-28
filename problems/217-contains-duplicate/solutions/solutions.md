---
problem: 217
problem-title: Contains Duplicate
difficulty: Easy
category: solutions
patterns: [HashMap, Preprocessing]
constructs: [hashset, array-sort]
ds-used: [array, hashset]
algorithms: []
concepts: []
approaches:
  - name: HashSet Lookup
    file: hashset-seen.cs
    patterns: [HashMap]
    variation: HashSet Existence Lookup
    constructs: [hashset]
    ds-used: [array, hashset]
    ds-notes:
      hashset: "O(1) existence check; check before adding to catch duplicates"
    time: "O(n)"
    space: "O(n)"
  - name: Sort + Adjacent Check
    file: sort-adjacent-check.cs
    patterns: [Preprocessing]
    variation: Sort Adjacent Check
    constructs: [array-sort]
    ds-used: [array]
    ds-notes:
      array: "sorted array; duplicates become adjacent neighbors"
    time: "O(n log n)"
    space: "O(1)"
---

# Contains Duplicate - Solutions

## Approaches

### Approach 1: HashSet Lookup
**Code:** [hashset-seen.cs](hashset-seen.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** For each number, check if it's already in a HashSet of previously seen values. If found, return true. If not, add it and move on. Return false after the loop. HashSet is the right tool here - only need to know if something exists, not how many times or where.

---

### Approach 2: Sort + Adjacent Check
**Code:** [sort-adjacent-check.cs](sort-adjacent-check.cs)
**Time:** O(n log n) | **Space:** O(1)

**Thinking:** Sort the array in place so duplicates become adjacent. Then scan once checking if any element equals its right neighbor. Trades time for space - no extra structure needed. Mutates the original array, which matters if the caller needs the original order.

**Mistakes:**
- **Off-by-one at loop boundary** - loop must be `i < nums.Length - 1`, not `i < nums.Length`. Accessing `nums[i+1]` when `i` is the last index throws IndexOutOfRangeException. The last element has no right neighbor.

---

## Patterns

- HashMap - HashSet Existence Lookup (Approach 1) - store each number in a HashSet; if it's already there on arrival, a duplicate exists
- Preprocessing - Sort to Expose Structure (Approach 2) - sort so duplicates land adjacent, then scan for equal neighbors

## Reflection

- **Key insight:** Sorting doesn't create duplicates - it makes them visible by bringing them adjacent. Transform the input to expose what's hidden.
- **Future strategy:** Reach for HashSet when you only need existence. Reach for sort when the relationship you're looking for becomes obvious in sorted order.
- **HashMap vs HashSet:** Use HashSet when you only need to know if something exists. Use HashMap when you need to store something alongside the key - an index, a count, or any associated value.
- **Trade-off:** HashSet is O(n) time but O(n) space. Sorting is O(n log n) time but O(1) space. Neither dominates - choose based on constraints.
- **Notes Insights:**
  - Sorting exposes structure that was already there but not visible. Duplicates exist in the unsorted array - sorting just brings them together so a single scan can find them.
  - HashSet vs HashMap: if you only need to know whether something exists, HashSet is enough. HashMap is for when you need to carry extra data alongside the key.
- **Mantras:**
  - "Use a HashSet to remember what you've seen. If you see it again, you have your answer."
  - "Sort the input when the relationship you're looking for becomes obvious once elements are in order."
  - "HashSet = existence only. HashMap = existence plus data."
