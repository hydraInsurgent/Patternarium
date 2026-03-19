# Active Problem File Spec

During a problem session, `active-problem.md` lives at repo root and tracks the session in progress. The AI creates and updates this file automatically at phase transitions. The user never needs to touch it.

- One active problem at a time
- Created at Phase 1 (user pastes a problem)
- Updated silently at each phase transition
- Deleted by `/save-problem` after decomposition into permanent storage

## Template

```markdown
# Active Problem

## Problem
**Name:** [problem name]
**Difficulty:** Easy | Medium | Hard
**Tags:** [comma-separated tags]
**Source:** [optional, e.g. LeetCode #1]

## Statement
[Problem statement, constraints, examples as pasted by user]

## Journey

### Approach 1: [name]
**Status:** solved | in-progress | stuck

#### Thinking
[User's stated approach]

#### Hints Given
- Level N: "[hint text]"

#### Bugs
- [Bug description and root cause]

#### Solution
[Code block with solution]
**Time:** O(?) | **Space:** O(?)
**Key Idea:** [One sentence]

---

### Approach 2: [name]
**Status:** [status]
[same sub-sections as above]

---

## Patterns
- **[Pattern Name]**: [one-line description of how it was applied]

## Reflection
- **Mistake:** [User's answer]
- **Key Insight:** [User's answer]
- **Pattern Rule:** [User's stated mantra]
- **Still Unclear:** [User's answer or "nothing"]
```

## When Each Section Gets Written

Logging is event-based, not phase-based. Write when the event happens, regardless of which phase you are in.

| Event | Section Updated |
|-------|----------------|
| User pastes a problem | Create file with `## Problem` + `## Statement` |
| User shares an approach | Append `### Approach N` + `#### Thinking` |
| A hint is given | Append to `#### Hints Given` in current approach |
| A bug is identified | Append to `#### Bugs` in current approach |
| A solution is reached or revealed | Write `#### Solution` + set `**Status:** solved` |
| A new alternative approach begins | Start new `### Approach N` block |
| Patterns are identified | Write `## Patterns` |
| User answers reflection questions | Write `## Reflection` |

All writes are automatic. The user is never asked about this file.

## How /save-problem Decomposes This File

| Section | Becomes |
|---------|---------|
| `## Problem` + `## Statement` | `problems/<slug>/problem.md` |
| Each `### Approach` with status "solved" | One `.cs` file per approach |
| `#### Bugs` + `## Reflection` | `problems/<slug>/notes.md` |
| `## Patterns` + approach names | `pattern-index.json` entry |
| `## Patterns` | Updates to `patterns/*.md` Solved Problems |
| `#### Bugs` (new categories) | Updates to `patterns/*.md` Common Mistakes |

## Interrupted Session Handling

If `active-problem.md` already exists when a new problem is pasted (Phase 1):
1. Read the existing file, extract problem name
2. Ask: "There is an unfinished session for [Problem Name]. (a) Save it with /save-problem, (b) Discard and start fresh, (c) Resume it"
3. Act on user's choice

## Rules

- The last `### Approach` block is always the current one - always append to it
- `/review` sessions do not create an active problem file
- If `/save-problem` is run without an active file, fall back to reconstructing from conversation history
