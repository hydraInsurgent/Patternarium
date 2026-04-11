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
---
name: "<pattern-name>"
display_name: "<Pattern Name>"
category: pattern
variations:
  - name: <Variation Name>
    ds: [array]
ds-primary: [array]
---

# <Pattern Name>

## Core Idea
What this pattern does in general, independent of any specific variation.

## Variation: <Variation Name>

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

---

## Variation: <Variation Name 2>
[same structure as above]

---

## Try Next
- Problem 2 (suggested, not yet solved) - note which variation applies

## Common Mistakes
- Mistake 1 and why it happens (Variation Name if variation-specific)
- Mistake 2 and why it happens

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "<Pattern Name>"
SORT number asc
```
```

### Pattern Naming Convention

- **Filename:** short kebab-case (e.g., `linear-scan.md`, `hashmap.md`)
- **display_name:** base name only, no variation suffix (e.g., `"HashMap"`, `"Two Pointers"`)
- **Variation names:** descriptive labels used as headings within the file (e.g., `"Complement Lookup"`, `"Sorted Pair"`)
- **master-index.json:** uses display_name values (e.g., `"Linear Scan"`, `"HashMap"`)
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
number: 1
slug: two-sum
category: DSA-Practice
difficulty: Easy
source: LeetCode
status: solved
lists: [blind-75]
ds-used: [array, hashmap]
patterns: [HashMap, Two Pointers]
constructs: [dictionary]
algorithms: []
tags: [complement-lookup, index-tracking, target-sum]
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
```

- `number` and `slug` are derived from the folder name
- `lists` from what was set at session start; write `lists: []` if none specified
- `ds-used`, `patterns`, `constructs` are written from master-index.json at save time
- `tags` are AI-inferred from the patterns and data structures used during the session

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

`master-index.json` is the central cross-reference. The LeetCode problem number is the stable key. It stores patterns, constructs, DS used, and approach-level details per problem, plus reverse-lookup indexes for fast AI access:

```json
{
  "_saving": null,
  "problems": {
    "1": {
      "title": "Two Sum",
      "slug": "two-sum",
      "difficulty": "Easy",
      "patterns": ["HashMap", "Two Pointers"],
      "constructs": ["dictionary", "array-sort"],
      "algorithms": [],
      "ds-used": ["array", "hashmap"],
      "ds-notes": { "hashmap": "complement lookup: store value -> index" },
      "lists": ["blind-75"],
      "approaches": {
        "hashmap.cs": { "patterns": ["HashMap"], "variation": "Complement Lookup", "ds-used": ["array", "hashmap"] },
        "two-pointer.cs": { "patterns": ["Two Pointers"], "variation": "Sorted Pair", "ds-used": ["array"] }
      }
    }
  },
  "by-pattern": { "HashMap": ["1"] },
  "by-ds": { "array": ["1"], "hashmap": ["1"] },
  "by-construct": { "dictionary": ["1"] },
  "by-algorithm": {}
}
```

- `problems` keyed by problem number (string) - stable, never changes
- `by-pattern`, `by-ds`, `by-construct`, `by-algorithm` - reverse-lookup indexes, maintained by `/save-problem`
- Pattern names match the `display_name` field in the pattern file (e.g., `"HashMap"`, `"Linear Scan"`)
- Construct names match the `slug` field in the construct file (e.g., `"dictionary"`, `"array-sort"`)
- `_saving` - set to problem number during save, `null` otherwise; non-null means interrupted save

This is the AI-facing index. Dataview queries read problem frontmatter instead (see refactor plan).

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
| Linear Scan | Linear Scan | Neighbor Comparison, Running State |
| Preprocessing | Preprocessing | Normalize Before Compute |
| Chunked Iteration | Chunked Iteration | Variable Step |
| Prefix Sum | Prefix Sum | (no variations yet) |
| Reverse Order Matching | Reverse Order Matching | Complement Push |

## How Patterns Grow

Every new problem solved:
1. Tags to existing patterns if applicable
2. The `## Solved Problems` section in the pattern file updates automatically via Dataview query
3. If a new variation of an existing pattern is discovered, add a `## Variation:` section to the existing pattern file
4. If a genuinely new thinking strategy emerges (distinct mental trigger, distinct template), create a new `patterns/<new-pattern>.md`

## Variation vs New Pattern

New ideas start as variations inside a parent pattern. They earn their own file when:
- The idea appears in 2+ problems and feels distinct from all existing patterns
- It has a clearly different mental trigger (you recognize the problem differently)
- It has a meaningfully different code template

When promoting: extract the variation into `patterns/<name>.md`, update master-index.json, and note the relationship in the original pattern file.

Variations stay inside the parent when the core structure is the same and the difference is in application context (sorted vs unsorted, boolean vs integer slot, etc.).

Over time, the pattern library becomes a personal DSA knowledge base.

---

## Data Structures System

Data structures are abstract, language-agnostic building blocks. They define what a structure is, what operations it supports, and when to reach for it - independent of any programming language. The C# implementation lives in the Constructs system.

### Where Data Structures Live

Every data structure lives in `data-structures/<name>.md`. Filename is kebab-case.

### Status Field

Each file has a `status` in its YAML frontmatter:
- `stub` - pre-created for inventory; not yet encountered through problem solving
- `explored` - actively used in at least one solved problem

### Data Structures Inventory

| File | Name | Status |
|------|------|--------|
| `array.md` | Array | explored |
| `string.md` | String | explored |
| `hashmap.md` | HashMap | explored |
| `hashset.md` | HashSet | explored |
| `linked-list.md` | Linked List | stub |
| `stack.md` | Stack | stub |
| `queue.md` | Queue | stub |
| `deque.md` | Deque | stub |
| `binary-tree.md` | Binary Tree | stub |
| `binary-search-tree.md` | Binary Search Tree | stub |
| `heap.md` | Heap | stub |
| `trie.md` | Trie | stub |
| `graph.md` | Graph | stub |

### Data Structure File Format

```markdown
---
name: "HashMap"
slug: hashmap
status: stub | explored
progress: 0
---

# HashMap

## What It Is
One sentence: the mental model, language-agnostic.

## Core Operations
| Operation | Description | Average | Worst |
|-----------|-------------|---------|-------|

## Space Complexity
O(?)

## Mental Model
How to visualize or reason about this structure.

## When to Reach For It
- Problem signal 1
- Problem signal 2

## When NOT to Use It
- Scenario where another structure is better

## vs Alternatives
- vs [Other DS]: key difference

## C# Implementation
- [ConstructName](../constructs/category/name.md)

## Coverage

**Progress: 0 / 0 techniques explored (0%)**

Linked rows have a pattern file. Plain text rows are placeholders - no pattern file exists yet. Links are added when the pattern file is created.

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty, patterns
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "hashmap"
SORT number asc
```
```

- `slug` must equal the filename without `.md` - this is the value used in `ds-used` arrays in problem frontmatter and in the Seen In query
- `progress` is the percentage of Coverage rows that have at least one problem solved; updated by `/save-problem`

### How Data Structures Link to Constructs

A data structure file describes the abstract concept. The C# construct file describes the syntax and gotchas. They are linked:
- `data-structures/hashmap.md` -> references `constructs/collections/dictionary.md`
- `data-structures/hashset.md` -> references `constructs/collections/hashset.md`

When a new data structure is encountered in a problem session:
- If the `data-structures/` file exists, update its `status` to `explored` (the `## Seen In` section is a Dataview query and updates automatically)
- If no file exists yet (unusual - all core structures are pre-created), create one from the template above

---

## Constructs System

Constructs are the tools that patterns use - language features and data structures with known behaviors and syntax. They are not thinking strategies; they are building blocks.

Examples: HashSet, Dictionary, Array.Sort, Stack, Queue, List.

### Where Constructs Live

Every construct lives in `constructs/<category>/<name>.md`. Filename is kebab-case.

#### Category Taxonomy

Categories are DSA-relevant. When creating a construct, check this list first. If nothing fits, propose a new category.

| Category | What belongs here |
|----------|------------------|
| `collections` | HashSet, Dictionary, List, Stack, Queue, SortedSet, SortedDictionary, PriorityQueue |
| `sorting` | Sorting mechanics, comparers, ordering interfaces (Array.Sort comparer, IComparable, IComparer) |
| `strings` | String manipulation, character operations, StringBuilder |
| `memory` | Span, stackalloc, memory-efficient structures |
| `math` | Bit operations, modular arithmetic, GCD, numeric utilities |
| `search` | Binary search variants, index-based lookup patterns |
| `graph` | Adjacency list, union-find, graph traversal building blocks |

When a construct could fit two categories, choose the one that reflects its primary DSA use case.

### Construct File Format

```markdown
---
name: "Display Name"
slug: construct-slug
category: collections
tags: [tag1, tag2]
language: csharp
related: [other-construct-filename-without-extension]
---

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

## See Also
- [linked-construct.md](../category/linked-construct.md) - one line on why it is related

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "construct-slug"
SORT number asc
```
```

**YAML field rules:**
- `name` - human-readable display name, can include spaces and punctuation
- `slug` - the short public ID used in `constructs` arrays in problem frontmatter and in the Seen In query. Usually equals the filename without `.md`. When the filename is a long descriptive title (e.g., `array-sort-custom-comparer.md`), the slug is the short form (`array-sort`). Must be declared explicitly whenever slug differs from filename
- `category` - must match one of the category taxonomy keys above
- `tags` - lowercase, hyphen-separated, 2-6 tags describing the concept and use cases
- `language` - always `csharp` for now
- `related` - filenames only, no path, no extension (e.g., `icomparable` not `sorting/icomparable.md`)

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
---
name: "Algorithm Name"
slug: algorithm-name
category: algorithm
tags: [tag1, tag2]
time: "O(?)"
space: "O(?)"
related: [other-algorithm]
---

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

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN algorithms AS algo
WHERE algo = "algorithm-name"
SORT number asc
```
```

- `slug` must equal the filename without `.md` - this is the value used in `algorithms` arrays in problem frontmatter and in the Seen In query
- `time` and `space` are flat strings (e.g., `"O(n log n)"`) - kept flat since nested YAML object access is untested with Dataview
- `related` - filenames only, no path, no extension

### How Algorithms Link to Problems

An algorithm file is linked when you implement the procedure from scratch in a solution - not when a built-in uses it internally (e.g., `Array.Sort` uses IntroSort, but you did not write it, so no algorithm link).

Tracking:
- During a session, log any algorithm implemented under `#### Algorithms` in the active approach block of `active-problem.md` (just the name and what it was used for)
- On `/save-problem`, update or create the algorithm file; add the `slug` to the problem's `algorithms` array in frontmatter and to master-index.json (the `## Seen In` section is a Dataview query and updates automatically); update the `by-algorithm` reverse-lookup index in master-index.json

---

## Concepts System

Concepts are foundational algorithmic and mathematical ideas that problems are built on top of. They are not patterns (thinking strategies) and not constructs (language tools). They are the vocabulary of problem solving.

Examples: palindrome, prime number, GCD, modular arithmetic, anagram, permutation, factorial.

### Where Concepts Live

Every concept lives in `concepts/<name>.md`. Filename is kebab-case.

### Concept File Format

```markdown
---
name: concept-name
slug: concept-name
display_name: Concept Name
category: concept
tags: [concept-name, related-tag, related-tag]
---

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

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN tags AS tag
WHERE tag = "concept-name"
SORT number asc
```
```

- `slug` = the primary tag value used in `tags` arrays in problem frontmatter. The Seen In query matches on this exact value. `slug`, `name`, and the first entry in `tags` must all be the same string

### How Concepts Are Tracked

- When a problem is pasted, required concepts are silently identified and logged to `## Concepts` in `active-problem.md` with status `pending`
- When a concept is confirmed (reviewed or explored), its status updates to `confirmed`
- On `/save-problem`, all `confirmed` concepts are persisted: create or update `concepts/<name>.md` (the `## Seen In` section is a Dataview query and updates automatically)
- See the Concept Check Workflow in `toolkit.md` for the full trigger and discovery flow
