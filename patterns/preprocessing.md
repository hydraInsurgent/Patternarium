# Preprocessing - Normalize Before Compute

## Core Idea
Transform input into a simpler form before applying the main logic, eliminating special cases upfront.

## When to Reach for This
- Special cases complicate the main loop
- Multi-character tokens can be reduced to single tokens
- Edge cases are fixed and known upfront

## Mental Trigger
> "Can I eliminate the complexity before I even start the main logic?"

## Template
```
for each known special case:
    replace/transform in input
run simple main logic on cleaned input
```

## Tradeoffs
- Time: O(n) - preprocessing pass + main pass
- Space: O(n) - may create new transformed input (e.g., new string)
- Only works when special cases are fixed and enumerable
- Trades space for simpler logic

## Solved Problems
- Roman to Integer (string replacement of subtraction pairs)

## Try Next
- Calculator problems
- Expression Evaluation
- Decode String

## Common Mistakes
- Forgetting that C# strings are immutable - `Replace()` returns a new string
- Not accounting for the space cost of the transformed input
- Order of replacements can matter if replacements overlap
