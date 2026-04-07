# 242. Valid Anagram - Solutions

## Approaches

### Approach 1: Sort and Compare
**Code:** [sort-compare.cs](solutions/sort-compare.cs)
**Time:** O(n log n) | **Space:** O(n)
**Thinking:** Sort both strings - anagrams rearrange the same letters, so sorted versions will be identical. Compare character by character after sorting. Length difference is an immediate early exit.

---

### Approach 2: HashMap - Frequency Count
**Code:** [hashmap-frequency.cs](solutions/hashmap-frequency.cs)
**Time:** O(n) | **Space:** O(1) for lowercase only; O(n) for Unicode
**Thinking:** Count character frequencies in s using a Dictionary. Walk t and decrement each count. If t has a character not in the map, return false immediately. At the end, any non-zero value means mismatch - positive means extra in s, negative means extra in t. Used TryGetValue instead of ContainsKey + indexer to avoid double lookup.

---

### Approach 3: Integer Array - Frequency Count
**Code:** [array-frequency.cs](solutions/array-frequency.cs)
**Time:** O(n) | **Space:** O(1)
**Thinking:** Replace Dictionary with int[26]. Map each character to an index using `c - 'a'`. Same three-phase logic as Approach 2 but no hashing overhead and no need for ContainsKey - all slots start at 0. Constrained to lowercase English letters only; does not support Unicode.

---

### Approach 4: Span + stackalloc - Frequency Count
**Code:** [span-frequency.cs](solutions/span-frequency.cs)
**Time:** O(n) | **Space:** O(1)
**Thinking:** Identical to Approach 3 but uses `Span<int>` with `stackalloc int[26]` instead of a heap-allocated array. The buffer lives on the stack - no GC pressure, no heap allocation. Faster in practice for short-lived fixed-size buffers. Same constraints as Approach 3 - lowercase ASCII only.

---

## Patterns

- [Preprocessing - Sort to Expose Structure](../../patterns/preprocessing.md#variation-sort-to-expose-structure) (Approach 1) - sort both strings so anagrams become identical, then compare index by index
- [HashMap - Frequency Count](../../patterns/hashmap.md#variation-frequency-count) (Approaches 2, 3, 4) - count occurrences in s, cancel with t, verify all counts reach zero

## Reflection

- **Key insight:** Sorting and HashMap came naturally for anagram detection. The int[26] and Span optimizations were reached by thinking about character set constraints after the core solution was working - not required upfront.
- **Future strategy:** For "are these the same?" problems, reach for frequency count first - O(n) time, O(1) space for bounded sets. Sort-and-compare is simpler to reason about but costs O(n log n). Under constraints, frequency count wins.
- **Most natural approach:** Both sorting and HashMap were intuitive from the start. The optimization path (Dictionary -> int[] -> Span) followed naturally from understanding the character set constraints.
