# Longest Consecutive Sequence - Notes

## Mistakes Made

### Approach 1 - Sort + Linear Scan
- count initialized to 0 instead of 1 - a single element is already a sequence of length 1
- Empty array check placed after array access - caused index out of bounds crash on empty input
- Duplicates not handled - without checking current == last, a duplicate reset count to 1

### Approach 3 - Presence Array
- Checked `current == last` (boolean neighbor comparison) instead of `if(current)` - caught false==false as consecutive
- Reset count to 1 instead of 0 on a gap - caused first element after a gap to count as 2

## Key Insights
- A sequence start is a number with no predecessor in the set - the absence of n-1 is the signal
- The presence array does not need neighbor comparison because consecutive indices are consecutive numbers by construction
- Range (max - min), not array size, determines whether a presence array is viable

## Mantras
- "No predecessor means sequence start"
- "Range decides the approach, not input size"

## Patterns Used
- **Linear Scan - Neighbor Comparison** (Approach 1)
- **HashSet - Existence Lookup** (Approach 2)
- **Presence Array - Bucket Marking** (Approach 3)
