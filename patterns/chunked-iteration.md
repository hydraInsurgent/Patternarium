---
name: chunked-iteration
display_name: Chunked Iteration
category: pattern
variations:
  - name: Variable Step
    ds: [string, hashmap]
ds-primary: [string, hashmap]
---

# Chunked Iteration

## Core Idea

Consume one or more elements per step based on a condition, advancing the index by different amounts. Unlike a standard for-loop that moves exactly one step, this pattern processes variable-size units.

## Variation: Variable Step

**When to reach for this:**
- Tokens have variable length
- Pairs or groups need to be consumed as a single unit
- Greedy matching: consume as many elements as possible per step

**Mental Trigger:**
> "Do I sometimes need to process multiple elements as one unit?"
> "Can I consume a group at once instead of one element at a time?"

**Template:**
```
i = 0
while i < length:
    if multi-element condition at i:
        process group, advance by group size
    else:
        process single element, advance by 1
handle any remaining unprocessed element
```

**Tradeoffs:**
- Time: O(n) - each element processed exactly once
- Space: O(1) - just index tracking
- More explicit control than for-loops
- Must handle the "leftover" case after the loop ends

**Solved Problems:**
- **Roman to Integer** (problems/13-roman-to-integer/solutions/two-char-lookahead.cs) - two-character lookahead: consume subtraction pair or single character per step

---

## Try Next

- Decode Ways
- Integer to English Words
- Tokenizer/Parser problems

## Common Mistakes

- **Forgetting the leftover** - the last element may not have been consumed by a multi-element group; always check after the loop
- **Wrong comparison operator** - `>` instead of `>=` for equal values causes them to fall into the wrong branch (e.g., XX should not be a subtraction pair)
- **Loop bounds with lookahead** - when checking `i + 1`, the loop condition must account for it (e.g., `i < length - 1`) to avoid index out of bounds

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Chunked Iteration"
SORT number asc
```
