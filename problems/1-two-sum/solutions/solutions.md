---
problem: 1
problem-title: Two Sum
difficulty: Easy
category: solutions
patterns: [HashMap, Two Pointers]
constructs: [dictionary, array-sort]
ds-used: [array, hashmap]
algorithms: []
concepts: []
approaches:
  - name: Brute Force
    file: brute-force.cs
    patterns: []
    constructs: []
    ds-used: [array]
    time: "O(n^2)"
    space: "O(1)"
  - name: HashMap - Complement Lookup
    file: hashmap.cs
    patterns: [HashMap]
    variation: Complement Lookup
    constructs: [dictionary]
    ds-used: [array, hashmap]
    ds-notes:
      hashmap: "complement lookup: store value -> index"
    time: "O(n)"
    space: "O(n)"
  - name: Sorting + Two Pointers
    file: two-pointer.cs
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
**Code:** [brute-force.cs](brute-force.cs)
**Time:** O(n^2) | **Space:** O(1)

**Thinking:** For every element, check all other elements to find the complement. Nested loops check every possible pair. Baseline approach - establishes the problem before optimizing.

---

### Approach 2: HashMap - Complement Lookup
**Code:** [hashmap.cs](hashmap.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** Rewrite `a + b = target` as `b = target - a`. For each element, check if its complement was already seen using a HashMap. Critical rule: check first, then store - prevents using the same element twice.

**Mistakes:**
- Inserted into map before checking - on `[3,3]` with target=6, stored map[3]=0 then checked map[3] which returned [0,0] (same element used twice). Fix: always check first, then store
- Forgot to add `map[nums[i]] = i` inside the loop - dictionary stayed empty forever, nothing was ever found. Fix: after TryGetValue returns false, always store the current number

---

### Approach 3: Sorting + Two Pointers
**Code:** [two-pointer.cs](two-pointer.cs)
**Time:** O(n log n) | **Space:** O(n)

**Thinking:** Sort the array but carry original indices as metadata using `(value, index)` tuples. Use two pointers from both ends - if sum is too big, move right pointer left; if too small, move left pointer right. Not faster than HashMap here, but teaches the Two Pointer pattern.

**Mistakes:**
- Assumed sorting would be simpler, forgot that sorting destroys original index positions. Fix: create `(value, index)[]` array before sorting so original index travels with the value

---

## Patterns

- HashMap - Complement Lookup (Approach 2) - store seen elements for O(1) lookup, rewrite pair problems as complement search
- Two Pointers - Sorted Pair (Approach 3) - sorted array + pair relationship, shrink window from both ends

## Reflection

- **Key insight:** `a + b = target` becomes `b = target - a` - converts "find a pair" into "check if one value exists"
- **Future strategy:** Check first, then store with HashMap; carry identity when sorting
- **Critical rule:** Check first, then store - prevents same-element reuse
- **Carry identity:** When transforming data (sorting), attach the original identifier before the transformation
- **Notes Insights:**
  - Complement transformation: `a + b = target` becomes `b = target - a` - converts "find a pair" into "check if one value exists"
  - HashMap as memory of the past: remembers every number seen and where, O(1) lookup
  - Check first, then store: prevents using the same element twice
  - Carry identity when transforming: attach the original identifier before sorting/transforming
- **Mantras:**
  - "HashMap = remember past elements for O(1) lookup"
  - "Rewrite pair problems: b = target - a, then check if b exists"
  - "If you see nested loops in arrays, ask: what from loop 1 can I store to remove loop 2?"
  - "Check first, then store - never the other way with HashMap"
  - "If you transform data (sort, slice, map), carry its original identity with it"
  - "sorted array + pair relationship = two pointers from both ends"
