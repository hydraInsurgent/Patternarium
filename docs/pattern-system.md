# Pattern System

## What Is a Pattern

A pattern is a reusable way of thinking that applies to a family of problems. It is not a solution. It is the reasoning strategy behind a class of solutions.

Example: "HashMap = store previous elements for O(1) lookup" is a pattern. The specific Two Sum code is a solution that uses that pattern.

## Why Patterns Matter

If you learn 100 problems, you know 100 solutions.
If you learn 20 patterns, you can approach hundreds of problems.

The pattern system is the core asset of this repo. Every solved problem feeds into it.

## Pattern File Format

Every pattern lives in `patterns/<pattern-name>.md`. The filename is a short kebab-case identifier. The file defines a `display_name` that is used as the human-readable label in links and references.

```markdown
# Pattern Name

**display_name:** [Full Descriptive Name]

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

### Pattern Naming Convention

- **Filename:** short kebab-case (e.g., `linear-scan.md`, `hashmap.md`)
- **display_name:** descriptive label (e.g., "Linear Scan - Neighbor Comparison", "HashMap - Complement Lookup")
- **pattern-index.json:** uses short names matching the filename (e.g., `"Linear Scan"`, `"HashMap"`)
- **solutions.md links:** uses display_name as link text, filename for the URL

When referencing a pattern, always look up the `display_name` from the pattern file to ensure consistency across sessions.

## Problem File Format

Every solved problem lives in `problems/<number>-<name>/` using the LeetCode number prefix in kebab-case. Solution code lives in a `solutions/` subfolder to keep the top level clean and spoiler-free.

For non-LeetCode sources, use a source prefix: `hr-<name>` (HackerRank), `cf-<name>` (Codeforces), etc.

```
problems/1-two-sum/
    problem.md          <- problem statement + constraints (spoiler-free)
    solutions.md        <- learning journey: approaches, patterns, reflection
    notes.md            <- mistakes made, insights, mantras
    solutions/
        brute-force.cs
        hashmap.cs
        two-pointer.cs
```

### problem.md

YAML frontmatter for queryable metadata. Tags are inline at the bottom to avoid spoiling the solution approach.

```markdown
---
title: Two Sum
category: DSA-Practice
difficulty: Easy
source: LeetCode
status: solved
---

# Two Sum

## Statement
[Problem statement as-is from source]

## Examples
[Examples from the problem]

## Constraints
[Constraints from the problem]

---

## Solutions

- [Solution approaches & learning journey](solutions.md)
- [Mistakes & key insights](notes.md)

tags :: Array, HashMap, Two Pointers
```

Tags are AI-inferred from the patterns and data structures used during the session.

### solutions.md

The learning journey file. Links to code files in `solutions/` subfolder and to pattern files using display names.

```markdown
# Two Sum - Solutions

## Approaches

### Approach 1: Brute Force
**Code:** [brute-force.cs](solutions/brute-force.cs)
**Time:** O(n^2) | **Space:** O(1)

**Thinking:** [Paraphrase of user's stated approach - no AI additions]

---

### Approach 2: HashMap
**Code:** [hashmap.cs](solutions/hashmap.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** [Paraphrase of user's stated approach - no AI additions]

---

## Patterns

- [HashMap - Complement Lookup](../../patterns/hashmap.md) (Approach 2) - description
- [Two Pointers - Sorted Pair](../../patterns/two-pointers.md) (Approach 3) - description

## Reflection

- **Key insight:** [required - from user's session]
- **Future strategy:** [required - from user's session]
- **[session-specific field]:** [optional - captures what user actually said]
```

Use `---` separators between approaches only. Patterns and Reflection flow without separators.

Reflection has two required fields (`Key insight`, `Future strategy`) plus any session-specific fields that capture what the user actually said. Labels should match the session context.

### notes.md

Quick reference for review. Mistakes grouped by approach name, no links. Skip sections and subheadings that have no content.

```markdown
# Two Sum - Notes

## Mistakes Made

### Approach 2 - HashMap
- [bug description and root cause]

## Key Insights
- [insight from session]

## Mantras
- "memorable one-liner"

## Patterns Used
- **Pattern Name** (Approach N)
```

### Solution .cs files

When saving, AI prettifies the user's code:
- Rename single-letter variables to descriptive names
- Add one comment per logical block explaining the *why*
- Add a "Why" block after the code for decisions that came from bugs, mistakes, or discussions during the session. These are learning artifacts - only include entries for things the user actually struggled with or asked about. Never generate generic "Why" blocks
- Preserve the user's logic exactly

Each `.cs` file has a comment header:

```csharp
// Approach: [name]
// Time:  O(?)
// Space: O(?)
// Key Idea: [one sentence]

public class SolutionN
{
    // prettified implementation
}

// Why [decision]?
// - [explanation from session discussion or bug encountered]
```

The "Why" block is optional - only added when the session produced relevant discussion, bugs, or questions about specific design decisions.

## Pattern Index

`pattern-index.json` connects problems to patterns using short pattern names (matching filenames):

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
- Short names in JSON. Display names are looked up from pattern files when rendering links

This is the revision layer. When reviewing, you can quiz a specific pattern-approach pair.

## Tagging Rules

1. Every problem must map to at least one pattern
2. Multiple patterns per problem is preferred when meaningful
3. A pattern name must be reusable - it should apply to other problems
4. Do not create pattern names that are too specific to one problem
5. Never store a solution without its pattern context

## Pattern Library (Starter Set)

The patterns discovered so far are:

| Pattern | display_name | When to Use |
|---------|-------------|-------------|
| HashMap | HashMap - Complement Lookup | Pair sum, complement search, frequency counting, "have I seen this before?" |
| Two Pointers | Two Pointers - Sorted Pair | Sorted array, pair relationships, range shrinking |
| Prefix Sum | Prefix Sum | Subarray sum queries, running totals |
| Sliding Window | Sliding Window | Subarray/substring with constraint |
| Sorting + Metadata | Sorting + Metadata | Sort without losing original index (carry identity with data) |
| Linear Scan | Linear Scan - Neighbor Comparison | Value depends on adjacent element, directional rules |
| Preprocessing | Preprocessing - Normalize Before Compute | Fixed special cases that can be eliminated before main logic |
| Chunked Iteration | Chunked Iteration - Variable Step | Variable-length tokens, consume 1 or more elements per step |

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

---

## Constructs System

Constructs are the tools that patterns use - language features and data structures with known behaviors and syntax. They are not thinking strategies; they are building blocks.

Examples: HashSet, Dictionary, Array.Sort, Stack, Queue, List.

### Where Constructs Live

Every construct lives in `constructs/<name>.md`. Filename is kebab-case.

### Construct File Format

```markdown
# Construct Name

## What It Is
One sentence: what this data structure or feature does.

## C# Syntax
How to declare, add, check, iterate - with code snippets.

## When to Reach For It
- Scenario 1
- Scenario 2

## vs Alternatives
- vs Dictionary: [key difference]
- vs List: [key difference]

## Gotchas
- Known traps or surprising behaviors

## Seen In
- Problem 1 (what it was used for)
```

### How Constructs Are Tracked

During a session, any new construct encountered is noted in `active-problem.md` under a `## Constructs` section in the relevant approach block. This is a lightweight note - just the name and what it was used for.

When `/save-problem` runs, the AI generates or updates the full construct file from the template above.

### Named Algorithms

Named algorithms (Dijkstra, BFS, DFS, KMP, etc.) are a separate category from both patterns and constructs. They are specific procedures, not thinking strategies or language tools. They live in `algorithms/<name>.md`.

---

## Algorithms System

Algorithms are named, well-defined procedures with a specific step-by-step structure. Unlike patterns (which are thinking strategies), algorithms have a canonical form - there is a right way to implement BFS, QuickSort, or Dijkstra.

### Where Algorithms Live

Every algorithm lives in `algorithms/<name>.md`. Filename is kebab-case.

### Algorithm File Format

```markdown
# Algorithm Name

## What It Is
One sentence: what this algorithm does.

## How It Works
Step-by-step description in plain language.

## Pseudocode
```
concise pseudocode
```

## Complexity
- Time: O(?) - best / average / worst if they differ
- Space: O(?)

## When to Use It
- Problem signal 1
- Problem signal 2

## vs Alternatives
- vs [other algorithm]: [key difference and when to prefer each]

## Gotchas
- Known traps or edge cases

## Seen In
- Problem 1 (which approach, what role the algorithm played)
```

### How Algorithms Link to Problems

An algorithm file is linked when you implement the procedure from scratch in a solution - not when a built-in uses it internally (e.g., `Array.Sort` uses IntroSort, but you did not write it, so no algorithm link).

Tracking works identically to constructs:
- During a session, log any algorithm implemented under `#### Algorithms` in the active approach block of `active-problem.md`
- On `/save-problem`, update or create the algorithm file and append to `## Seen In`
- `algorithms-index.json` maps problems to algorithms, same structure as `pattern-index.json`

### algorithms-index.json Format

```json
{
  "Problem Name": {
    "algorithms": ["QuickSort", "BFS"],
    "approaches": {
      "solution1.cs": ["BFS"]
    }
  }
}
```
