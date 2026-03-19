# Pattern System

## What Is a Pattern

A pattern is a reusable way of thinking that applies to a family of problems. It is not a solution. It is the reasoning strategy behind a class of solutions.

Example: "HashMap = store previous elements for O(1) lookup" is a pattern. The specific Two Sum code is a solution that uses that pattern.

## Why Patterns Matter

If you learn 100 problems, you know 100 solutions.
If you learn 20 patterns, you can approach hundreds of problems.

The pattern system is the core asset of this repo. Every solved problem feeds into it.

## Pattern File Format

Every pattern lives in `patterns/<pattern-name>.md` with this structure:

```markdown
# Pattern Name

## Core Idea
One sentence: what this pattern does and why it works.

## When to Reach for This
- Signal 1 (e.g., "pair sum problems")
- Signal 2 (e.g., "complement lookup")
- Signal 3 (e.g., "frequency counting")

## Mental Trigger
> The question you should ask yourself when you see this type of problem.

## Template
Pseudocode or skeleton showing the pattern structure.

## Tradeoffs
- Time complexity
- Space complexity
- When it beats alternatives

## Solved Problems
- Problem 1 (approach used)

## Try Next
- Problem 2 (suggested, not yet solved)

## Common Mistakes
- Mistake 1 and why it happens
- Mistake 2 and why it happens
```

## Problem File Format

Every solved problem lives in `problems/<problem-slug>/` with these files:

```
problems/two-sum/
    problem.md          <- problem statement + constraints
    brute-force.cs      <- O(n squared) approach
    hashmap.cs          <- O(n) HashMap approach
    two-pointer.cs      <- O(n log n) Sorting + Two Pointer approach
    notes.md            <- mistakes made, insights, mantras
```

Every `problem.md` must include metadata at the top:

```markdown
# Problem Name

**Difficulty:** Easy | Medium | Hard
**Tags:** Array, HashMap, Two Pointers
**Source:** LeetCode #1 (optional)
```

This makes problems searchable without needing pattern-index.json.

## Pattern Index

`pattern-index.json` connects problems to patterns and maps each approach to the patterns it uses:

```json
{
  "Two Sum": {
    "patterns": ["HashMap", "Two Pointers"],
    "approaches": {
      "brute-force.cs": [],
      "hashmap.cs": ["HashMap"],
      "two-pointer.cs": ["Two Pointers"]
    }
  }
}
```

- `patterns` lists all patterns used across any approach for the problem
- `approaches` maps each solution file to the specific patterns it uses
- A single approach can use multiple patterns (e.g., Two Pointers + index preservation)

This is the revision layer. When reviewing, you can quiz a specific pattern-approach pair.

## Tagging Rules

1. Every problem must map to at least one pattern
2. Multiple patterns per problem is preferred when meaningful
3. A pattern name must be reusable - it should apply to other problems
4. Do not create pattern names that are too specific to one problem
5. Never store a solution without its pattern context

## Pattern Library (Starter Set)

The patterns discovered so far are:

| Pattern | When to Use |
|---------|-------------|
| HashMap | Pair sum, complement search, frequency counting, "have I seen this before?" |
| Two Pointers | Sorted array, pair relationships, range shrinking |
| Prefix Sum | Subarray sum queries, running totals |
| Sliding Window | Subarray/substring with constraint |
| Sorting + Metadata | Sort without losing original index (carry identity with data) |

## How Patterns Grow

Every new problem solved:
1. Tags to existing patterns if applicable
2. Adds an example to an existing pattern file
3. If a new thinking strategy emerges, add it as a subsection within the closest parent pattern file

## Sub-Pattern Promotion

New ideas start as subsections inside a parent pattern. They earn their own file when:
- The sub-pattern appears in 2+ problems across different parent patterns
- It has a distinct mental trigger (you recognize the problem differently)
- It has a meaningfully different code template

When promoting: extract the subsection into `patterns/<name>.md`, add it to pattern-index.json, and link back from the parent pattern.

Over time, the pattern library becomes a personal DSA knowledge base.
