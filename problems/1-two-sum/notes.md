# Two Sum - Notes

## Mistakes Made

### Approach 2 - HashMap
- Inserted into map before checking - on `[3,3]` with target=6, stored map[3]=0 then checked map[3] which returned [0,0] (same element used twice). Fix: always check first, then store
- Forgot to add `map[nums[i]] = i` inside the loop - dictionary stayed empty forever, nothing was ever found. Fix: after TryGetValue returns false, always store the current number

### Approach 3 - Sorting + Two Pointers
- Assumed sorting would be simpler, forgot that sorting destroys original index positions. Fix: create `(value, index)[]` array before sorting so original index travels with the value

## Key Insights
- Complement transformation: `a + b = target` becomes `b = target - a` - converts "find a pair" into "check if one value exists"
- HashMap as memory of the past: remembers every number seen and where, O(1) lookup
- Check first, then store: prevents using the same element twice
- Carry identity when transforming: attach the original identifier before sorting/transforming

## Mantras
- "HashMap = remember past elements for O(1) lookup"
- "Rewrite pair problems: b = target - a, then check if b exists"
- "If you see nested loops in arrays, ask: what from loop 1 can I store to remove loop 2?"
- "Check first, then store - never the other way with HashMap"
- "If you transform data (sort, slice, map), carry its original identity with it"
- "sorted array + pair relationship = two pointers from both ends"

## Patterns Used
- **HashMap - Complement Lookup** (Approach 2)
- **Two Pointers - Sorted Pair** (Approach 3) - see [two-pointers.md#variation-sorted-pair](../../patterns/two-pointers.md#variation-sorted-pair)
