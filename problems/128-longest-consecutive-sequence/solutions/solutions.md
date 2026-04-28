---
problem: 128
problem-title: Longest Consecutive Sequence
difficulty: Medium
category: solutions
patterns: [Linear Scan, HashMap, Presence Array]
constructs: [hashset, array-sort]
ds-used: [array, hashset]
algorithms: []
concepts: []
approaches:
  - name: Sort + Linear Scan
    file: sort-linear-scan.cs
    patterns: [Linear Scan]
    variation: Sort Linear Scan
    constructs: [array-sort]
    ds-used: [array]
    ds-notes:
      array: "sorted array; skip duplicates, count consecutive runs"
    time: "O(n log n)"
    space: "O(1)"
  - name: HashSet - Sequence Start Detection
    file: hashset-sequence-start.cs
    patterns: [HashMap]
    variation: Sequence Start Detection
    constructs: [hashset]
    ds-used: [array, hashset]
    ds-notes:
      hashset: "O(1) lookup; number is a sequence start only if n-1 is not in the set"
    time: "O(n)"
    space: "O(n)"
  - name: Presence Array (small range optimization)
    file: presence-array.cs
    patterns: [Presence Array, HashMap]
    variation: Presence Array with HashSet Fallback
    constructs: [hashset]
    ds-used: [array, hashset]
    ds-notes:
      array: "bool[] presence array for small value ranges"
      hashset: "fallback for large ranges where presence array would be too big"
    time: "O(n + r) small range, O(n) large range"
    space: "O(r) small range, O(n) large range"
---

# Longest Consecutive Sequence - Solutions

## Approaches

### Approach 1: Sort + Linear Scan
**Code:** [sort-linear-scan.cs](sort-linear-scan.cs)
**Time:** O(n log n) | **Space:** O(1)

**Thinking:** Sort the array first to bring consecutive numbers adjacent. Then scan once: if the current element equals previous + 1, extend the run; if equal to previous, it is a duplicate - skip without resetting; otherwise reset count. Track the running max.

**Mistakes:**
- count initialized to 0 instead of 1 - a single element is already a sequence of length 1
- Empty array check placed after array access - caused index out of bounds crash on empty input
- Duplicates not handled - without checking current == last, a duplicate reset count to 1

---

### Approach 2: HashSet - Sequence Start Detection
**Code:** [hashset-sequence-start.cs](hashset-sequence-start.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** Load all numbers into a HashSet for O(1) lookup. A number is a sequence start only if n-1 is absent from the set - that absence is the signal. Count forward from each start using set.Contains. Every other number is skipped, so each element is visited at most twice total.

---

### Approach 3: Presence Array (small range optimization)
**Code:** [presence-array.cs](presence-array.cs)
**Time:** O(n + r) | **Space:** O(r) where r = max - min + 1

**Thinking:** Not a fully independent approach - uses Approach 2 as a fallback for large ranges. For small ranges: find min and max, allocate a boolean array of size (max - min + 1), mark index (num - min) = true for each number. Scan the boolean array for the longest consecutive true run. The array structure guarantees consecutive indices are consecutive numbers, so no neighbor comparison is needed.

**Mistakes:**
- Checked `current == last` (boolean neighbor comparison) instead of `if(current)` - caught false==false as consecutive
- Reset count to 1 instead of 0 on a gap - caused first element after a gap to count as 2

---

## Patterns

- Preprocessing - Sort to Expose Structure (Approach 1) - sort so consecutive numbers land adjacent, making runs visible to a simple scan
- Linear Scan - Neighbor Comparison (Approach 1) - after sorting, scan comparing each element to the previous to detect consecutive runs and duplicates
- HashMap - HashSet Existence Lookup (Approach 2) - use n-1 absence as the sequence start signal, extend runs with O(1) contains checks
- Presence Array - Boolean Presence (Approach 3) - map values to boolean array indices using (num - min) offset, scan for longest consecutive true run

## Reflection

- **Key insight:** A sequence start has no predecessor in the set. Checking if n-1 is absent is the signal that unlocks the O(n) solution.
- **Future strategy:** When the problem says O(n) and involves existence checks, reach for HashSet. When numbers span a small range, presence array is a viable optimization.
- **Presence array vs HashSet:** The deciding factor is range (max - min), not array size. Ten numbers spanning billions still needs a 2-billion slot array.
- **Notes Insights:**
  - A sequence start is a number with no predecessor in the set - the absence of n-1 is the signal
  - The presence array does not need neighbor comparison because consecutive indices are consecutive numbers by construction
  - Range (max - min), not array size, determines whether a presence array is viable
- **Mantras:**
  - "No predecessor means sequence start"
  - "Range decides the approach, not input size"
