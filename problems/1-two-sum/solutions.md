---
problem: 1
problem-title: Two Sum
difficulty: Easy
category: solutions
patterns: [HashMap, Two Pointers]
constructs: [dictionary, array-sort]
ds-used: [array, hashmap]
algorithms: []
tags: [complement-lookup, index-tracking, target-sum]
approaches:
  - name: Brute Force
    file: solutions/brute-force.cs
    patterns: []
    constructs: []
    ds-used: [array]
    time: "O(n^2)"
    space: "O(1)"
  - name: HashMap - Complement Lookup
    file: solutions/hashmap.cs
    patterns: [HashMap]
    variation: Complement Lookup
    constructs: [dictionary]
    ds-used: [array, hashmap]
    ds-notes:
      hashmap: "complement lookup: store value -> index"
    time: "O(n)"
    space: "O(n)"
  - name: Sorting + Two Pointers
    file: solutions/two-pointer.cs
    patterns: [Two Pointers]
    variation: Sorted Pair
    constructs: [array-sort]
    ds-used: [array]
    ds-notes:
      array: "tuple array preserves original indices through sort"
    time: "O(n log n)"
    space: "O(n)"
---

# Two Sum - Solutions

## Approaches

### Approach 1: Brute Force
**Code:** [brute-force.cs](solutions/brute-force.cs)
**Time:** O(n^2) | **Space:** O(1)

**Thinking:** For every element, check all other elements to find the complement. Nested loops check every possible pair. Baseline approach - establishes the problem before optimizing.

---

### Approach 2: HashMap - Complement Lookup
**Code:** [hashmap.cs](solutions/hashmap.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** Rewrite `a + b = target` as `b = target - a`. For each element, check if its complement was already seen using a HashMap. Critical rule: check first, then store - prevents using the same element twice.

---

### Approach 3: Sorting + Two Pointers
**Code:** [two-pointer.cs](solutions/two-pointer.cs)
**Time:** O(n log n) | **Space:** O(n)

**Thinking:** Sort the array but carry original indices as metadata using `(value, index)` tuples. Use two pointers from both ends - if sum is too big, move right pointer left; if too small, move left pointer right. Not faster than HashMap here, but teaches the Two Pointer pattern.

---

## Patterns

- [HashMap - Complement Lookup](../../patterns/hashmap.md) (Approach 2) - store seen elements for O(1) lookup, rewrite pair problems as complement search
- [Two Pointers - Sorted Pair](../../patterns/two-pointers.md#variation-sorted-pair) (Approach 3) - sorted array + pair relationship, shrink window from both ends

## Reflection

- **Key insight:** `a + b = target` becomes `b = target - a` - converts "find a pair" into "check if one value exists"
- **Future strategy:** Check first, then store with HashMap; carry identity when sorting
- **Critical rule:** Check first, then store - prevents same-element reuse
- **Carry identity:** When transforming data (sorting), attach the original identifier before the transformation
