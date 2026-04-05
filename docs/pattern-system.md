# Pattern System

## What Is a Pattern

A pattern is a reusable way of thinking that applies to a family of problems. It is not a solution. It is the reasoning strategy behind a class of solutions.

Example: "HashMap = store previous elements for O(1) lookup" is a pattern. The specific Two Sum code is a solution that uses that pattern.

## Why Patterns Matter

If you learn 100 problems, you know 100 solutions.
If you learn 20 patterns, you can approach hundreds of problems.

The pattern system is the core asset of this repo. Every solved problem feeds into it.

## Pattern File Format

Every pattern lives in `patterns/<pattern-name>.md`. The filename is a short kebab-case identifier. The file defines a `display_name` (the base name) and contains one or more named variations as headings. Each variation is a distinct use of the same core idea - different trigger, different template, different tradeoffs.

```markdown
# Pattern Name

**display_name:** Pattern Name

## Core Idea
What this pattern does in general, independent of any specific variation.

## Variation: [Variation Name]

**When to reach for this:**
- Signal 1 (e.g., "pair sum problems")
- Signal 2 (e.g., "complement lookup")

**Mental Trigger:**
> The question you should ask yourself when you see this type of problem.

**Template:**
Pseudocode or skeleton showing the variation's structure.

**Tradeoffs:**
- Time complexity
- Space complexity
- When it beats alternatives

**Solved Problems:**
- Problem 1 (approach used)

---

## Variation: [Variation Name 2]
[same structure as above]

---

## Try Next
- Problem 2 (suggested, not yet solved) - note which variation applies

## Common Mistakes
- Mistake 1 and why it happens (Variation Name if variation-specific)
- Mistake 2 and why it happens
```

### Pattern Naming Convention

- **Filename:** short kebab-case (e.g., `linear-scan.md`, `hashmap.md`)
- **display_name:** base name only, no variation suffix (e.g., `"HashMap"`, `"Two Pointers"`)
- **Variation names:** descriptive labels used as headings within the file (e.g., `"Complement Lookup"`, `"Sorted Pair"`)
- **pattern-index.json:** uses short names matching the filename (e.g., `"Linear Scan"`, `"HashMap"`)
- **solutions.md links:** combine display_name and variation name as link text, with a heading anchor pointing to the variation

**Link format:** `[display_name - Variation Name](../../patterns/<file>.md#variation-<anchor>)`

Example: `[HashMap - Complement Lookup](../../patterns/hashmap.md#variation-complement-lookup)`

The anchor is derived from the heading: `## Variation: Complement Lookup` becomes `#variation-complement-lookup` (lowercase, spaces to hyphens, special characters dropped).

When referencing a pattern, always look up both the `display_name` and the variation heading from the pattern file to ensure consistency across sessions.

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

- [HashMap - Complement Lookup](../../patterns/hashmap.md#variation-complement-lookup) (Approach 2) - description
- [Two Pointers - Sorted Pair](../../patterns/two-pointers.md#variation-sorted-pair) (Approach 3) - description

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

| Pattern | display_name | Variations |
|---------|-------------|------------|
| HashMap | HashMap | Complement Lookup, Last Seen Index, HashSet Existence Lookup |
| Two Pointers | Two Pointers | Sorted Pair, Symmetry Check |
| Sliding Window | Sliding Window | Shrink-Based, Index Jump |
| Presence Array | Presence Array | Boolean Presence, Integer Slot |
| Linear Scan | Linear Scan | Neighbor Comparison |
| Preprocessing | Preprocessing | Normalize Before Compute |
| Chunked Iteration | Chunked Iteration | Variable Step |
| Prefix Sum | Prefix Sum | (no variations yet) |

## How Patterns Grow

Every new problem solved:
1. Tags to existing patterns if applicable
2. Adds the problem to the Solved Problems list under the relevant variation in the pattern file
3. If a new variation of an existing pattern is discovered, add a `## Variation:` section to the existing pattern file
4. If a genuinely new thinking strategy emerges (distinct mental trigger, distinct template), create a new `patterns/<new-pattern>.md`

## Variation vs New Pattern

New ideas start as variations inside a parent pattern. They earn their own file when:
- The idea appears in 2+ problems and feels distinct from all existing patterns
- It has a clearly different mental trigger (you recognize the problem differently)
- It has a meaningfully different code template

When promoting: extract the variation into `patterns/<name>.md`, add it to pattern-index.json, and note the relationship in the original pattern file.

Variations stay inside the parent when the core structure is the same and the difference is in application context (sorted vs unsorted, boolean vs integer slot, etc.).

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

---

## Concepts System

Concepts are foundational algorithmic and mathematical ideas that problems are built on top of. They are not patterns (thinking strategies) and not constructs (language tools). They are the vocabulary of problem solving.

Examples: palindrome, prime number, GCD, modular arithmetic, anagram, permutation, factorial.

### Where Concepts Live

Every concept lives in `concepts/<name>.md`. Filename is kebab-case.

### Concept File Format

```markdown
# Concept Name

## What It Is
Plain definition in one or two sentences.

## How to Verify
The core check - how do you determine if something fits this concept?

## Approaches
Different ways to verify or compute it, each with a brief pseudocode or description.

## Examples
- Example 1 (fits the concept)
- Non-example 2 (does not fit - and why)

## Practice Problems
- [Problem name] - [what it tests about this concept]

## Seen In
- Problem 1 (what role the concept played)
```

### How Concepts Are Tracked

- When a problem is pasted, required concepts are silently identified and logged to `## Concepts` in `active-problem.md` with status `pending`
- When a concept is confirmed (reviewed or explored), its status updates to `confirmed`
- On `/save-problem`, all `confirmed` concepts are persisted: create or update `concepts/<name>.md` and append to `## Seen In`
- See the Concept Check Workflow in `toolkit.md` for the full trigger and discovery flow
