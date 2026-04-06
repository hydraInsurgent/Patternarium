# Active Problem File Spec

During a problem session, two files live at repo root:
- `active-problem.md` - tracks the learning journey (thinking, hints, bugs, patterns, reflection). AI-managed.
- `active-solution.cs` - the user's coding workspace. AI only appends blank template blocks; the user writes all code and metadata.

- One active problem at a time
- Created at Phase 1 (user pastes a problem)
- Updated silently at each phase transition
- After `/save-problem`, active files are cleared to empty templates (not deleted) when the user is ready to close the session or start a new problem

## Template

```markdown
## Problem
- **Name:** [problem name]
- **Difficulty:** Easy | Medium | Hard
- **Tags:** [comma-separated tags]
- **Source:** [optional, e.g. LeetCode #13]
- **Status:** in-progress
- **Date Started:** [YYYY-MM-DD]
- **Time Started:** [YYYY-MM-DD HH:MM]
- **Time Taken:** [filled when all approaches done and problem closed]

## Statement
[Problem statement as pasted by user - keep as-is from source]

### Approach 1: [name]
**Status:** in-progress | stuck | solved
**Time Started:** [YYYY-MM-DD HH:MM]
**Time Taken:** [filled when approach reaches solved or stuck]

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
**Time Started:** [YYYY-MM-DD HH:MM]
**Time Taken:** [filled when approach reaches solved or stuck]
[same sub-sections as above]

---

## Patterns
- **[Pattern Display Name]**: [one-line description of how it was applied]

## Reflection
Required fields:
- **Key insight:** [User's answer]
- **Future strategy:** [User's answer]

Optional session-specific fields (add as relevant):
- **Trickiest bug:** [if debugging was significant]
- **Most natural approach:** [if multiple approaches explored]
- [any other field that captures what the user actually said]
```

## When Each Section Gets Written

Logging is event-based, not phase-based. Write when the event happens, regardless of which phase you are in.

| Event | Section Updated |
|-------|----------------|
| User pastes a problem | Create file with `## Problem` (including Status: in-progress, Date Started, Time Started) + `## Statement` |
| User shares an approach | Append `### Approach N` with `**Status:** in-progress`, `**Time Started:**` (current time) + `#### Thinking` |
| Approach reaches solved or stuck | Set `**Time Taken:**` on that approach block (compute from Time Started) |
| All approaches done, problem closed | Set `**Time Taken:**` at Problem level (compute from Problem Time Started) |
| A hint is given | Append to `#### Hints Given` in current approach |
| User says "I'm stuck" or solution is revealed for them | Set current approach `**Status:** stuck` |
| A bug is identified | Append to `#### Bugs` in current approach |
| A solution is reached or revealed | Write `#### Solution` (complexity + key idea, no code) + set `**Status:** solved` |
| User says "start coding" or approach is clear | Append blank template block to `active-solution.cs` |
| A new alternative approach begins | Start new `### Approach N` block in `active-problem.md`; append new template block to `active-solution.cs` |
| Patterns are identified | Write `## Patterns` |
| User answers reflection questions | Write `## Reflection` |

All writes are automatic. The user is never asked about this file.

## Approach Status Lifecycle

- `in-progress` - set when the approach block is created
- `stuck` - set when the user says "I'm stuck" or the solution is revealed (Phase 6) for this approach
- `solved` - set when a working solution is confirmed

Only approaches with `**Status:** solved` are persisted by `/save-problem`.

## How /save-problem Decomposes These Files

| Section | Becomes |
|---------|---------|
| `## Problem` + `## Statement` | `problems/<slug>/problem.md` (YAML frontmatter including `lists:` field + inline tags at bottom) |
| Each solved approach block from `active-solution.cs` | `problems/<slug>/solutions/<approach>.cs` (prettified by AI) |
| Approaches + Patterns + Reflection | `problems/<slug>/solutions.md` (with links to .cs files and pattern files) |
| `#### Bugs` + `## Reflection` | `problems/<slug>/notes.md` (grouped by approach name) |
| `lists:` field in frontmatter | Updates `workbench/lists/<name>.md` - marks problem as solved, adds pattern and link |
| `## Patterns` + approach names | `pattern-index.json` entry |
| `## Patterns` | Updates to `patterns/*.md` Solved Problems |
| `#### Bugs` (new categories) | Updates to `patterns/*.md` Common Mistakes |

Problem folder naming:
- LeetCode: `problems/<number>-<name>/` (e.g., `13-roman-to-integer`)
- HackerRank: `problems/hr-<name>/`
- Codeforces: `problems/cf-<name>/`
- Other: `problems/<source-prefix>-<name>/`

## Interrupted Session Handling

If `active-problem.md` or `active-solution.cs` already has content when a new problem is pasted (Phase 1):
1. Read the existing file, extract problem name
2. Ask: "There is an unfinished session for [Problem Name]. (a) Save it with /save-problem, (b) Discard and start fresh, (c) Resume it"
3. Act on user's choice (discard clears both files to empty templates)

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

**AI writes:** Only the blank template block (separator, empty metadata fields, class with method signature). Always append to the bottom - never prepend.
**User writes:** Everything else (approach name, complexity, key idea, implementation)

### Dry Run Template in active-solution.cs

When debugging, AI can suggest the user add a dry run comment block above their approach code. AI creates the empty table structure as comments; the user fills in the values.

```csharp
// ==== Approach N - Dry Run ====
// Input: "[concrete example]" (expected: [value])
// Tracked: [variable names]
//
// | Step | var1 | var2 | ... | Action | Result |
// |------|------|------|-----|--------|--------|
// |      |      |      |     |        |        |
// |      |      |      |     |        |        |
//
// Bug found:
//

// ==== Approach N ====
// [approach code below]
```

AI never fills in the dry run values - only suggests the empty structure with appropriate column headers based on the code's variables and conditions.

## Active File Cleanup

Active files are never deleted. Instead, they are cleared to empty templates:
- When the user confirms they are done reviewing after `/save-problem`
- When the user chooses "Discard and start fresh" during interrupted session handling
- When the user explicitly asks to clean up

AI suggests cleanup after `/save-problem`: "Ready to clear the active files for the next problem?"

Cleared state for `active-problem.md`: empty file
Cleared state for `active-solution.cs`: empty file

## Rules

- The last `### Approach` block is always the current one - always append to it
- `/review` sessions do not create active problem or solution files
- If `/save-problem` is run without an active file, fall back to reconstructing from conversation history
- AI never modifies user code in `active-solution.cs` - it can only read it for debugging and append new blank template blocks
- `/save-problem` does not clear active files immediately - AI suggests cleanup after the user reviews
- Tags in active-problem.md are AI-inferred from the patterns and data structures used in the session
