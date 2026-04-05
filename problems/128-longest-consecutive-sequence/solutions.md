# Longest Consecutive Sequence - Solutions

## Approaches

### Approach 1: Sort + Linear Scan
**Code:** [sort-linear-scan.cs](solutions/sort-linear-scan.cs)
**Time:** O(n log n) | **Space:** O(1)

**Thinking:** Sort the array first to bring consecutive numbers adjacent. Then scan once: if the current element equals previous + 1, extend the run; if equal to previous, it is a duplicate - skip without resetting; otherwise reset count. Track the running max.

---

### Approach 2: HashSet - Sequence Start Detection
**Code:** [hashset-sequence-start.cs](solutions/hashset-sequence-start.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** Load all numbers into a HashSet for O(1) lookup. A number is a sequence start only if n-1 is absent from the set - that absence is the signal. Count forward from each start using set.Contains. Every other number is skipped, so each element is visited at most twice total.

---

### Approach 3: Presence Array (small range optimization)
**Code:** [presence-array.cs](solutions/presence-array.cs)
**Time:** O(n + r) | **Space:** O(r) where r = max - min + 1

**Thinking:** Not a fully independent approach - uses Approach 2 as a fallback for large ranges. For small ranges: find min and max, allocate a boolean array of size (max - min + 1), mark index (num - min) = true for each number. Scan the boolean array for the longest consecutive true run. The array structure guarantees consecutive indices are consecutive numbers, so no neighbor comparison is needed.

---

## Patterns

- [Linear Scan - Neighbor Comparison](../../patterns/linear-scan.md#variation-neighbor-comparison) (Approach 1) - after sorting, scan comparing each element to the previous to detect consecutive runs and duplicates
- [HashMap - HashSet Existence Lookup](../../patterns/hashmap.md#variation-hashset-existence-lookup) (Approach 2) - use n-1 absence as the sequence start signal, extend runs with O(1) contains checks
- [Presence Array - Boolean Presence](../../patterns/presence-array.md#variation-boolean-presence) (Approach 3) - map values to boolean array indices using (num - min) offset, scan for longest consecutive true run

## Reflection

- **Key insight:** A sequence start has no predecessor in the set. Checking if n-1 is absent is the signal that unlocks the O(n) solution.
- **Future strategy:** When the problem says O(n) and involves existence checks, reach for HashSet. When numbers span a small range, presence array is a viable optimization.
- **Presence array vs HashSet:** The deciding factor is range (max - min), not array size. Ten numbers spanning billions still needs a 2-billion slot array.
