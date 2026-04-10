# System Upgrade Review Prompt

Review this system upgrade thoroughly before implementation.

This is not a greenfield design review. This is an upgrade to a working system. The system already exists, has real data (12 problems, 11 patterns, 13 data structures, 7 constructs), and operational workflows. The upgrade must migrate the existing system without breaking it.

## Context

- **What exists:** A prompt-driven DSA learning system built on markdown, JSON indexes, and Claude Code. See `docs/vision.md` for the architectural vision and `docs/dataview/refactor-plan.md` for the detailed migration plan.
- **What is changing:** YAML frontmatter restructuring across all file types, consolidation of indexes into a single `master-index.json`, replacement of AI-maintained derived views with Dataview queries, and simplification of the `/save-problem` workflow.
- **What must survive:** All existing data, the learning workflow (Modes 1-9), the pattern system, and the ability to solve and save problems throughout the migration.

## Your Goal

Identify gaps, inconsistencies, risks, and improvement opportunities in the upgrade plan. Focus on migration safety, sequencing correctness, and whether the end state actually delivers the promised benefits.

## My Design Preferences

Use these to guide your recommendations:

- Simplicity over complexity
- Clear separation of concerns
- Minimal redundancy (flag repetition aggressively)
- High clarity and readability
- Practical usability over theoretical completeness
- Avoid over-engineering and premature abstraction
- Bias toward explicit structure over implicit assumptions
- Migration safety: the system must remain functional at every intermediate step

## Issue Format

For every issue found:

- Number issues sequentially (1, 2, 3...)
- Describe the problem clearly, referencing specific files, sections, or phases where possible
- Provide 2-3 options labeled A, B, C - include "do nothing" where reasonable
- For each option include: effort, risk, impact, maintenance implications
- Put your recommended option first
- Give an opinionated recommendation aligned with my preferences
- End with a direct question asking me to choose direction

## Interaction Rules

- Raise up to 3 major issues per section
- After each section, pause and ask for feedback before continuing
- Do not assume priorities or constraints
- Be opinionated but not forceful
- Ask before proceeding on anything unclear
- When referencing the current system, read the actual files - do not rely on the plan's description of current state

## Review Sections

### 1. Migration Safety Review
Can the system remain fully functional at every phase boundary? Evaluate whether each phase produces a self-consistent state. Flag phases where the system would be broken mid-migration. Check for ordering dependencies between phases. Identify what happens if the migration is paused or abandoned at any phase.

### 2. Schema Design Review
Evaluate the proposed YAML schemas and master-index.json structure. Check for missing fields, redundant fields, type consistency, and whether the schemas actually support the queries they need to power. Flag fields that are defined but never queried, or queries that need fields not yet defined.

### 3. Data Integrity Review
Evaluate the current-to-target data migration. Check whether all existing data (Seen In sections, Coverage tables, list tables) can be reconstructed from the proposed indexes and frontmatter. Flag any information that would be lost in the migration. Verify the backfill strategy is complete.

### 4. Query Feasibility Review
Evaluate whether the proposed Dataview queries can actually be built with the extension's capabilities (pending Phase 1 findings). For each Layer 3 replacement, assess: what data source does it need, what fields does it join on, what output format is expected, and what happens if the extension cannot support it. Identify fallback strategies.

### 5. Workflow Impact Review
Evaluate how the upgrade changes the day-to-day experience. Walk through a full `/save-problem` flow in the new system and compare to the current flow. Check the session workflow (Modes 1-9) for any breakage. Assess cognitive load changes for the user and token cost changes for the AI.

### 6. Scalability Validation Review
Evaluate whether the end state actually delivers on the 10x scale promise. Stress-test the architecture at 120 problems, 30 patterns, 5 lists. Check master-index.json size, query complexity, and /save-problem write count. Flag anything that scales poorly.

### 7. Consistency & Completeness Review
Evaluate alignment between the vision doc, refactor plan, current toolkit.md, current CLAUDE.md, and the /save-problem skill. Flag contradictions, outdated references, or plan steps that miss updates to dependent files. Check that every file touched by /save-problem today is accounted for in the migration plan.

### 8. Principle Alignment Review
Evaluate whether the upgrade reflects the stated design principles (single source of truth, schema contracts, atomicity, separation of concerns, cost efficiency). Flag places where the plan violates its own principles. Check whether the architecture test results are honest - challenge them if needed.

## Files to Read

Start by reading these files to understand the full system:

**Upgrade plan:**
- `docs/vision.md` - architectural vision and design principles
- `docs/dataview/refactor-plan.md` - detailed migration plan with test results

**Current system (read to verify plan accuracy):**
- `CLAUDE.md` - system map and user context
- `.claude/rules/toolkit.md` - all operational rules and /save-problem workflow
- `pattern-index.json` - current index structure (becomes master-index.json)
- Sample files from each type to verify current YAML state:
  - `problems/121-best-time-to-buy-and-sell-stock/problem.md`
  - `patterns/linear-scan.md`
  - `data-structures/array.md`
  - `constructs/collections/dictionary.md`
  - `concepts/palindrome.md`
  - `workbench/lists/blind-75.md`
  - `workbench/goals.md`
