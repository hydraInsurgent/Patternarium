# Two Sum - Session Notes

## Mistakes Made

**Mistake 1: Inserting into map before checking**
I wrote the loop so that I stored the current number in the map first, then checked for the complement. This fails on `[3,3]` with target=6 because at i=0 we store map[3]=0, then check map[3] which exists - returning [0,0] (same element used twice).

Fix: Always check first, then store. The map starts empty so the first element's check always fails safely.

**Mistake 2: Forgetting to add map[nums[i]] = i inside the loop**
My first implementation checked for the complement and returned, but had no else branch to store the current number. The dictionary stayed empty forever, so nothing was ever found.

Fix: After the TryGetValue check (when it returns false), always store the current number.

**Mistake 3: Thinking sort + two pointer was simpler**
I assumed sorting would make it easier, but forgot that sorting destroys the original index positions. Returning `[left, right]` after sorting gives sorted positions, not original indices.

Fix: Before sorting, create a `(value, index)[]` array so the original index travels with the value through the sort.

---

## Key Insights

**Complement transformation:**
`a + b = target` becomes `b = target - a`. This converts "find a pair" into "check if one specific value exists." This rewrite is used in many problems.

**HashMap as memory of the past:**
As we scan left to right, the HashMap remembers every number we have seen and where we saw it. For each new number, we check if its complement is in that memory. O(1) check, O(n) space.

**Check first, then store - always:**
This rule prevents using the same element twice. The map only contains elements we have already passed, so any match found is always a different index.

**Carry identity when transforming:**
If you sort or transform data, attach the original identifier (index) before the transformation. The identity must travel with the data. Pattern: `(value, originalIndex)[]` before sort.

---

## Mantras

- "HashMap = remember past elements for O(1) lookup"
- "Rewrite pair problems: b = target - a, then check if b exists"
- "If you see nested loops in arrays, ask: what from loop 1 can I store to remove loop 2?"
- "Check first, then store - never the other way with HashMap"
- "If you transform data (sort, slice, map), carry its original identity with it"
- "sorted array + pair relationship = two pointers from both ends"

---

## Patterns Used

- **HashMap - Complement Lookup** (hashmap.cs)
- **Two Pointers - Sorted Pair** (two-pointer.cs)

---

## Complexity Summary

| Approach | Time | Space | When to Use |
|----------|------|-------|-------------|
| Brute Force | O(n^2) | O(1) | Never in practice, good for establishing baseline |
| HashMap | O(n) | O(n) | General case, unsorted input |
| Sorting + Two Pointers | O(n log n) | O(n) | Good for learning Two Pointer pattern; HashMap is faster here |
