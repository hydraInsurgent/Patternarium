# Project Instructions for Claude

<!-- PROJECT-CODE: DSA -->

## About This Project

Patternarium is a living, growing ecosystem for learning DSA (Data Structures and Algorithms). Like an aquarium is a place where aquatic life grows, the Patternarium is a place where algorithmic thinking grows. It is a thinking companion, not a solver. Every problem solved feeds the system - patterns emerge, deepen, and connect across problems over time. The goal is to build pattern recognition through guided problem solving, multi-approach exploration, and reflection.

**Tech Stack:**
- C# (primary language for all solutions)
- Markdown (docs, notes, pattern files)
- JSON (master-index.json)

## Who I Am

A learner who is fresh into DSA. I understand basic programming but have not yet built pattern recognition instincts. I want to understand how to think about problems, not just memorize solutions. I have solved Two Sum multiple times before but kept forgetting it because I always just got the answer - I never understood the reasoning.

## My Preferences

- Guide me, do not solve for me
- Ask questions before giving answers
- When I get something right, tell me why it is right
- When I get something wrong, tell me what is wrong and why - do not just rewrite it
- No em dashes or en dashes - use regular hyphens or rewrite the sentence
- Keep explanations simple and conversational
- Use dry run examples to make abstract ideas concrete

## Key Documents

**Read every session:**
- `.claude/rules/toolkit.md` - all operational rules, phases, hints, commands (auto-loaded)
- `LESSONS.md` - mistakes made so far, patterns to reinforce
- `active-problem.md` - current problem session state (if it exists)
- `active-solution.cs` - user's coding workspace for current problem (if it exists, read-only for AI)

**Design context (read once for background, not every session):**
- `docs/product-description.md` - what this system is and why it exists
- `docs/learning-philosophy.md` - core learning principles
- `docs/pattern-system.md` - how patterns, constructs, algorithms, and concepts are defined and stored
- `docs/active-problem-spec.md` - active problem file format and lifecycle
- `docs/dry-run-template.md` - dry run format for mental simulation
- `workbench/README.md` - workbench structure (library, backlog, goals, lists, sessions)

## System Map

Every folder in this repo has a distinct purpose. This is the complete inventory.

| Folder / File | What It Contains | When to Write Here |
|---------------|-----------------|-------------------|
| `data-structures/` | Language-agnostic mental models for each data structure - what it is, operations, complexity, when to use | When a new DS is encountered or explored for the first time |
| `algorithms/` | Named algorithms (Merge Sort, BFS, DFS, etc.) - how they work, pseudocode, complexity | When an algorithm is implemented from scratch in a solution |
| `patterns/` | Problem-solving templates - the thinking strategy behind a family of problems | After every solved problem; grows through variations |
| `constructs/` | C#-specific language features and APIs (Dictionary, Stack, Array.Sort, Span, etc.) | When a C# language tool is used in a solution |
| `techniques/` | Reusable optimization and implementation moves (running max, constraint-ceiling pruning, etc.) - the tools patterns are built from | When an optimization or implementation move is identified that applies across problems |
| `concepts/` | Mathematical and logical ideas problems are built on (palindrome, anagram, GCD, etc.) | When a foundational concept is explored during a session |
| `problems/` | One folder per solved problem - folder note (statement + Knowledge Links + Related Problems) and solutions | After every completed problem session via `/save-problem` |
| `workbench/` | Problem lists, backlog, goals, session logs - planning and tracking layer | During session planning; backlog and list management |
| `docs/` | System design documents - file formats, templates, philosophy, specs | When the system structure changes |
| `master-index.json` | Maps problem numbers to patterns, constructs, and DS used - the cross-reference layer | After every solved problem |

**The four layers of the knowledge system:**
1. **Tools** - data structures, algorithms, constructs (the building blocks)
2. **Thinking** - patterns, concepts (the strategies), techniques (the implementation moves)
3. **Evidence** - problems, solutions (the practice record)

Techniques sit between tools and thinking: patterns are strategies assembled from techniques; techniques are the atomic optimization moves that recur across many patterns.

## How We Work Together

All operational rules, workflow phases, hint escalation, slash commands, and AI behaviors are defined in `.claude/rules/toolkit.md`. That file is the single source of truth for how the system operates.

CLAUDE.md defines **who the user is** and **what to read**. toolkit.md defines **how to behave**.
