# Active Problem File Spec

During a problem session, two files live at repo root:
- `active-problem.md` - tracks the learning journey (thinking, hints, bugs, patterns, reflection). AI-managed.
- `active-solution.cs` - the user's coding workspace. AI only appends blank template blocks; the user writes all code and metadata.

- One active problem at a time
- Created at Phase 1 (user pastes a problem)
- Updated silently at each phase transition
- Both files deleted by `/save-problem` after decomposition into permanent storage

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
**Time:** O(?) | **Space:** O(?)
**Key Idea:** [One sentence]
(Solution code lives in `active-solution.cs`, not here)

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
| A solution is reached or revealed | Write `#### Solution` (complexity + key idea, no code) + set `**Status:** solved` |
| User says "start coding" or approach is clear | Append blank template block to `active-solution.cs` |
| A new alternative approach begins | Start new `### Approach N` block in `active-problem.md`; append new template block to `active-solution.cs` when user is ready to code |
| Patterns are identified | Write `## Patterns` |
| User answers reflection questions | Write `## Reflection` |

All writes are automatic. The user is never asked about this file.

## How /save-problem Decomposes This File

| Section | Becomes |
|---------|---------|
| `## Problem` + `## Statement` | `problems/<slug>/problem.md` |
| Each solved approach block from `active-solution.cs` | One `.cs` file per approach |
| `#### Bugs` + `## Reflection` | `problems/<slug>/notes.md` |
| `## Patterns` + approach names | `pattern-index.json` entry |
| `## Patterns` | Updates to `patterns/*.md` Solved Problems |
| `#### Bugs` (new categories) | Updates to `patterns/*.md` Common Mistakes |

## Interrupted Session Handling

If `active-problem.md` or `active-solution.cs` already exists when a new problem is pasted (Phase 1):
1. Read the existing file, extract problem name
2. Ask: "There is an unfinished session for [Problem Name]. (a) Save it with /save-problem, (b) Discard and start fresh, (c) Resume it"
3. Act on user's choice (discard removes both files)

## Active Solution File Format

`active-solution.cs` supports multiple approaches stacked in one file:

```csharp
// ==== Approach 1 ====
// Approach: ?
// Time:  ?
// Space: ?
// Key Idea: ?

public class Solution1
{
    public <return-type> <MethodName>(<params>)
    {
        // Your implementation here
    }
}

// ==== Approach 2 ====
// Approach: ?
// Time:  ?
// Space: ?
// Key Idea: ?

public class Solution2
{
    public <return-type> <MethodName>(<params>)
    {
        // Your implementation here
    }
}
```

**AI writes:** Only the blank template block (separator, empty metadata fields, class with method signature)
**User writes:** Everything else (approach name, complexity, key idea, implementation)

## Rules

- The last `### Approach` block is always the current one - always append to it
- `/review` sessions do not create active problem or solution files
- If `/save-problem` is run without an active file, fall back to reconstructing from conversation history
- AI never modifies user code in `active-solution.cs` - it can only read it for debugging and append new blank template blocks
