# Patternarium

A living, growing ecosystem for learning Data Structures and Algorithms - powered by [Claude Code](https://docs.anthropic.com/en/docs/claude-code).

Like an aquarium or terrarium, a Patternarium is a place where things grow. Except here, what grows is your ability to recognize patterns. Every problem you solve feeds the system - new patterns emerge, existing ones deepen, mistakes become lessons, and connections form across problems you have already solved.

It is not a solver. It is not a hint machine. It is a thinking companion that walks alongside you, and a personal pattern library that evolves with you.

> Don't collect solutions. Collect ways of thinking.

## Why This Exists

Most DSA learners solve problems and forget them. They revisit the same problem months later and have no memory of the solution. This happens because they memorized the answer without understanding the reasoning behind it.

Patternarium changes this by:

- Never giving the solution first - asking for your approach instead
- Guiding you through hints that escalate gradually, not jumping to answers
- Exploring multiple approaches per problem so each one teaches a different idea
- Extracting reusable patterns that apply across families of problems
- Tracking mistakes and breakthroughs so lessons compound over time

## How It Works

Patternarium runs as a set of Claude Code project instructions (CLAUDE.md + toolkit rules) that turn your AI assistant into a structured DSA tutor. You paste a problem, think through it together, and the system records everything into a growing personal knowledge base.

### The Session Flow

1. **Paste a problem** - AI restates it simply, highlights constraints, and asks: "How would you approach this?"
2. **Think out loud** - share your approach. AI validates what is correct and asks questions about gaps
3. **Get guided hints** - if stuck, hints escalate through 5 levels (conceptual nudge, directional hint, pattern hint, near-solution, guided dry run) before any solution is revealed
4. **Write code** - implement in C# with syntax help and debugging guidance. AI reads your code but never rewrites it
5. **Debug with dry runs** - step through your code mentally with concrete inputs to find bugs
6. **Explore alternatives** - after solving one way, explore at least one alternative approach that teaches a different idea
7. **Extract patterns** - name the patterns used and connect them to other problems
8. **Reflect** - answer targeted questions based on how the session went
9. **Save** - persist everything into the repo's structured problem library

### Slash Commands

| Command | What It Does |
|---------|-------------|
| `/hint` | Escalate to next hint level without revealing the solution |
| `/solution` | Reveal the full solution with explanation |
| `/next-approach` | Introduce an alternative approach |
| `/reflect` | Trigger reflection questions |
| `/pattern` | Show pattern(s) used in the current problem |
| `/save-problem` | Save the session to the repo |
| `/review` | Pick a past problem and test pattern recall from memory |

## Project Structure

```
Patternarium/
    CLAUDE.md                  # Project instructions for Claude Code
    LESSONS.md                 # Tracked mistakes and breakthroughs
    active-problem.md          # Current session state (temporary)
    active-solution.cs         # Current coding workspace (temporary)
    master-index.json          # Cross-reference index (patterns, constructs, DS, algorithms)

    .claude/
        rules/
            toolkit.md         # All operational rules, phases, hints, commands
        commands/
            save-problem.md    # /save-problem command definition

    docs/
        product-description.md # What this system is and why
        learning-philosophy.md # Core learning principles
        pattern-system.md      # File formats, templates, tagging rules
        active-problem-spec.md # Active problem file lifecycle
        dry-run-template.md    # Dry run format for mental simulation

    data-structures/           # Language-agnostic DS mental models (13 files)
        array.md
        hashmap.md
        string.md
        ...

    algorithms/                # Named algorithm procedures (QuickSort, MergeSort, etc.)
        quicksort.md
        mergesort.md
        insertion-sort.md

    constructs/                # C#-specific language features and APIs
        collections/
            dictionary.md
            hashset.md
            linq.md
        sorting/
            array-sort-custom-comparer.md
        memory/
            span.md
        strings/
            char-methods.md
            string-replace.md

    concepts/                  # Foundational mathematical/algorithmic ideas
        palindrome.md
        xor.md

    patterns/                  # Reusable thinking strategies (11 files)
        hashmap.md
        two-pointers.md
        linear-scan.md
        sliding-window.md
        ...

    problems/                  # Solved problems archive
        1-two-sum/
            problem.md         # Problem statement + YAML metadata
            solutions.md       # Approaches, patterns, reflection
            notes.md           # Mistakes and key insights
            solutions/
                brute-force.cs
                hashmap.cs
                two-pointer.cs
        13-roman-to-integer/
            ...
```

## The Pattern Library

The living core of the Patternarium. A pattern is not a solution - it is a reusable way of thinking that applies to a family of problems. The library starts small and grows with every problem you solve.

| Pattern | When to Reach for It |
|---------|---------------------|
| [HashMap](patterns/hashmap.md) | Pair sum, complement search, frequency counting, "have I seen this before?" |
| [Two Pointers](patterns/two-pointers.md) | Sorted array, pair relationships, range shrinking |
| [Linear Scan](patterns/linear-scan.md) | Value depends on adjacent element, directional rules |
| [Preprocessing](patterns/preprocessing.md) | Fixed special cases that can be eliminated before main logic |
| [Chunked Iteration](patterns/chunked-iteration.md) | Variable-length tokens, consume 1 or more elements per step |
| Prefix Sum | Subarray sum queries, running totals |
| Sliding Window | Subarray/substring with constraint |
| Sorting + Metadata | Sort without losing original index |

Patterns grow organically. Every solved problem adds examples and common mistakes to existing patterns. New patterns are created when a genuinely new thinking strategy emerges. Sub-patterns start as notes inside a parent and earn their own file when they appear across multiple problems.

## How the System Grows

The Patternarium is not a static tool you use and put down. It is a living system with three layers that feed each other:

```
Problems solved  -->  Patterns extracted  -->  Connections formed
      |                     |                        |
      v                     v                        v
  mistakes logged     examples added         review questions generated
  lessons tracked     triggers refined       cross-problem links built
```

- **After every problem:** patterns are tagged, examples are added, mistake traps are documented
- **After recurring mistakes:** lessons are logged and tracked until mastered, then graduated
- **During review:** the system quizzes you on past patterns without showing notes - testing recall, not recognition
- **Over time:** the pattern library becomes a personal DSA knowledge base that reflects how *you* think

## Key Design Principles

- **Patterns over solutions** - a solution answers one problem; a pattern answers a family of problems
- **Guided struggle** - productive difficulty is part of learning; hints escalate, they don't shortcut
- **Multi-approach learning** - every problem gets at least two approaches, each teaching a different idea
- **Earn the optimization** - alternatives are introduced only after you feel the limitation of the current approach
- **Reflection loop** - reflection converts short-term problem memory into long-term pattern memory
- **Cumulative growth** - every session makes the system smarter; nothing is throwaway

See [Learning Philosophy](docs/learning-philosophy.md) for the full rationale.

## Tech Stack

- **C#** - primary language for all solutions
- **Markdown** - docs, notes, pattern files
- **JSON** - master-index.json (cross-reference index)
- **Claude Code** - AI engine (runs via CLAUDE.md project instructions)

## Getting Started

### Prerequisites

- [Claude Code](https://docs.anthropic.com/en/docs/claude-code) installed and configured
- A terminal or IDE with Claude Code integration (VS Code recommended)

### Usage

1. Clone this repo
2. Open the project in your terminal or IDE with Claude Code
3. Paste a LeetCode problem and start solving

The system activates automatically through the project instructions in [CLAUDE.md](CLAUDE.md) and [toolkit.md](.claude/rules/toolkit.md). No additional setup required.

## Internal References

| Document | Purpose |
|----------|---------|
| [CLAUDE.md](CLAUDE.md) | Who the user is and what Claude should read each session |
| [Toolkit Rules](.claude/rules/toolkit.md) | Single source of truth for all operational behavior |
| [Product Description](docs/product-description.md) | What the system is and why it exists |
| [Learning Philosophy](docs/learning-philosophy.md) | Core learning principles |
| [Pattern System](docs/pattern-system.md) | Pattern file format, tagging rules, growth model |
| [Active Problem Spec](docs/active-problem-spec.md) | Active file format and lifecycle |
| [Dry Run Template](docs/dry-run-template.md) | Step-by-step mental simulation format |
| [LESSONS.md](LESSONS.md) | Tracked mistakes and breakthroughs |
| [master-index.json](master-index.json) | Cross-reference index for AI lookups and review |
