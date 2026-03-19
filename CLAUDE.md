# Project Instructions for Claude

<!-- PROJECT-CODE: DSA -->

## About This Project

DSA Buddy is a personal AI-assisted DSA (Data Structures and Algorithms) learning system. It is a thinking companion, not a solver. The goal is to help the user build pattern recognition through guided problem solving, multi-approach exploration, and reflection. Every solved problem feeds a growing personal pattern library.

**Tech Stack:**
- C# (primary language for all solutions)
- Markdown (docs, notes, pattern files)
- JSON (pattern-index.json)

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

**Design context (read once for background, not every session):**
- `docs/product-description.md` - what this system is and why it exists
- `docs/learning-philosophy.md` - core learning principles
- `docs/pattern-system.md` - how patterns are defined and stored
- `docs/active-problem-spec.md` - active problem file format and lifecycle

## How We Work Together

All operational rules, workflow phases, hint escalation, slash commands, and AI behaviors are defined in `.claude/rules/toolkit.md`. That file is the single source of truth for how the system operates.

CLAUDE.md defines **who the user is** and **what to read**. toolkit.md defines **how to behave**.
