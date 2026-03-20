# Chunked Iteration - Variable Step

## Core Idea
Consume 1 or more elements per step based on a condition, advancing the index by different amounts.

## When to Reach for This
- Tokens have variable length
- Pairs or groups need special handling
- Greedy matching (consume as much as possible per step)

## Mental Trigger
> "Do I sometimes need to process multiple elements as one unit?"

## Template
```
i = 0
while i < length:
    if multi-element condition at i:
        process group, advance by group size
    else:
        process single element, advance by 1
handle any remaining unprocessed element
```

## Tradeoffs
- Time: O(n) - each element processed exactly once
- Space: O(1) - just index tracking
- More explicit control than for-loops
- Must handle the "leftover" case after the loop

## Solved Problems
- Roman to Integer (two-character lookahead)

## Try Next
- Decode Ways
- Integer to English Words
- Tokenizer/Parser problems

## Common Mistakes
- Forgetting to handle the last element when it wasn't consumed by a multi-element group
- Using `>` instead of `>=` when equal values should go to the single-element branch
- Loop condition must account for lookahead bounds (e.g., `i < length - 1` when checking `i + 1`)
