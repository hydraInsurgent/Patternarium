# Contains Duplicate - Notes

## Mistakes Made

### Approach 2 - Sort + Adjacent Check
- **Off-by-one at loop boundary** - loop must be `i < nums.Length - 1`, not `i < nums.Length`. Accessing `nums[i+1]` when `i` is the last index throws IndexOutOfRangeException. The last element has no right neighbor.

## Key Insights

- Sorting exposes structure that was already there but not visible. Duplicates exist in the unsorted array - sorting just brings them together so a single scan can find them.
- HashSet vs HashMap: if you only need to know whether something exists, HashSet is enough. HashMap is for when you need to carry extra data alongside the key.

## Mantras

- "Use a HashSet to remember what you've seen. If you see it again, you have your answer."
- "Sort the input when the relationship you're looking for becomes obvious once elements are in order."
- "HashSet = existence only. HashMap = existence plus data."

## Patterns Used

- **Approach 1** - HashMap (HashSet Existence Lookup)
- **Approach 2** - Preprocessing (Sort to Expose Structure)
