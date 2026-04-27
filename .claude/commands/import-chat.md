# /import-chat - Import External Chat as Problem Session

Use this command when a problem was solved in an external chat (ChatGPT, etc.) and you want to bring that session into Patternarium. This command reads the chat, builds a structured analysis, and sets up the active files so the regular workflow can continue.

## When to Use

- User solved a problem in ChatGPT or another tool
- User provides a chat file (in `reference-chats/`) plus optional active files
- The goal is to reconstruct the learning journey from the external chat, not re-solve the problem

## What the User Provides

1. **Chat file** - path to the exported chat in `reference-chats/imports/<filename>.txt` or `.md`
2. **Active problem** - `active-problem.md` at repo root (may contain just the raw problem statement, or may be empty)
3. **Active solution** - `active-solution.cs` at repo root (may contain raw labeled code, or may be empty)

Any of the three may be missing or minimal. The chat is the primary source of truth.

## Procedure

### Step 1 - Identify the chat file

If the user ran `/import-chat` without arguments, ask:

> "Which reference chat should I import? Available files in `reference-chats/imports/`:"
> [list .txt and .md files in `reference-chats/imports/`]

Accept a filename or full path.

### Step 2 - Read all inputs

Read in parallel:
- The chat file (full read - do not skim)
- `active-problem.md` (for problem statement if present)
- `active-solution.cs` (for any code already written)

### Step 3 - Extract from the chat

Read the full chat with these extraction targets:

**Problem understanding**
- How was the problem initially misunderstood or framed incorrectly?
- What questions revealed gaps in thinking?
- What was the moment the problem clicked?

**Approaches explored**
- Every approach that was reasoned through (even if not coded)
- What drove transitions between approaches
- Approaches that were started and abandoned, and why

**Bugs and gotchas**
- Logic errors caught during discussion (pre-coding)
- Any "I thought X but actually Y" moments
- Off-by-one, wrong scope, wrong variable placement

**Key insights**
- Non-obvious realizations worth capturing in a pattern
- The exact moment a formula or invariant clicked

**Mistakes and lessons**
- Patterns of confusion that could repeat on future problems
- Anything worth adding to LESSONS.md

**Pattern signals**
- What properties of the problem pointed to which pattern
- Constructs mentioned or used

**Session completeness**
- What was fully resolved vs. what was left open
- Whether code was written, tested, or only reasoned about

### Step 4 - Generate analysis file

Write `reference-chats/analysis/<problem-slug>-analysis.md` with this structure:

```markdown
---
problem: <number>
title: "<Problem Name>"
slug: <problem-slug>
source: LeetCode | HackerRank | Codeforces | Other
difficulty: Easy | Medium | Hard
import-file: <chat-filename>.txt | <chat-filename>.md
category: analysis
---

# Analysis: [Problem Name]

**Chat covers:** [one-line summary of what was and was not covered]

---

## Thinking Journey

[Narrative of how the user's understanding evolved. Misunderstandings first, in the order they appeared. What corrected them. What the final mental model was.]

---

## Core Formula / Key Condition

[The central insight - the formula, condition, or invariant that makes the solution work. Why it is non-obvious.]

---

## Approaches

[One subsection per approach. Use `### Approach N:` (H3 under this H2) - do not use top-level H2 headings per approach.]
### Approach N: [Name]
**Complexity:** O(?) time, O(?) space
**Structure:** [pseudocode or 3-5 line description]
**Key optimization / insight:** [what made this approach work or differ from naive]

---

## Mistakes and Lessons

| Mistake | Category | What happened |
|---------|----------|---------------|
| ... | ... | ... |

[Categories: Problem comprehension, Variable lifecycle, Unnecessary complexity, Logic error, Math error, Mental model carryover, DP concept, Other]

---

## Key Insights for Pattern Library

[Bullet points of things worth capturing in pattern/concept files. Be specific - not "sliding window is useful" but the non-obvious part of why it applies here.]

---

## Constructs Used

[List of C# constructs used or mentioned in the chat]

---

## Pattern Signals

[What in the problem pointed to each pattern used]

---

## Connections to Other Problems

[Cross-references to problems in the repo or mentioned in the chat]

---

## Session Checklist

### Overall
- [ ] Problem statement understood  ← mark [x] if the chat shows clear, correct understanding of what was being asked
- [ ] Mental model solidified  ← mark [x] if the user arrived at the core condition or invariant without further confusion
- [ ] Reflection completed
- [ ] Saved to repo (`/save-problem`)

[Then one subsection per approach. Each approach gets a checklist derived from what actually happened in the chat - not a fixed template. Start from the baseline items below, then add approach-specific items based on what the chat revealed: unresolved edge cases, conditions that need dry-run validation, specific invariants the user struggled to trust, open questions that were not answered, lessons that still need to be tested against real code.]

### Approach N: [Name]
- [ ] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user
- [ ] Dry run completed
[Add items here based on the specific chat. Examples of what to add:]
[- [ ] Verify: [specific edge case that confused the user]]
[- [ ] Confirm: [invariant or formula the user didn't fully trust]]
[- [ ] Revisit: [open question left unanswered at end of chat]]
[- [ ] Test: [tricky input case mentioned but not walked through]]
[Each item should be specific enough that the user knows exactly what to do when they see it. Never add generic items. Remove the example lines - replace them with real items from the chat, or omit entirely if there is nothing specific to add.]

[Mark items [x] if they were completed in the chat.]
```

### Step 5 - Write active-problem.md

Two cases based on what is already in `active-problem.md`:

**Empty or only raw problem statement:** Write the full active problem file from scratch using the format from `docs/active-problem-spec.md`. Fill in:
- `## Problem` section with name, difficulty, source, tags (AI-inferred), time started
- `## Statement` with the full problem text
- For each approach explored in the chat: write `### Approach N: [name]` with status `in-progress`, thinking, hints (if any), bugs (if any)
- `## Patterns` section if pattern extraction happened in the chat. When writing pattern names, look up `display_name` from the pattern file in `patterns/`. If the pattern file does not exist yet, write the name as used in the chat and mark it: `<!-- no pattern file yet - confirm name at save time -->`
- Leave `## Reflection` blank - this must come from the user live

**Has partial session data (approach blocks, thinking sections):** Use the existing file as the base. Only fill in sections that are missing or have placeholder content:
- If `## Problem` fields are already populated, leave them - do not overwrite
- If an approach block already exists in the file, check whether the chat adds anything (bugs, hints, resolution) to it - if so, append to that block. Do not overwrite existing thinking
- If the chat covers approaches not yet in the file, append new approach blocks
- If `## Patterns` already exists, leave it. If absent and the chat covers patterns, add it with the same display_name lookup rule above

In both cases: do not fabricate anything not present in the chat. If a section cannot be filled from the chat, leave it with a placeholder comment: `<!-- not covered in imported chat -->`.

After writing the approach blocks, append `## Import Notes` at the bottom of active-problem.md. This section is synthesized from the analysis file - not copy-pasted. Apply these rules:

**`### Constructs Identified`:** Read `## Constructs Used` from the analysis file. For each construct, write its name and its specific role in the approach as used in this session - not a bare name. If a construct appeared across multiple approaches, note which approach used it and how.

**`### Remaining Approaches`:** Identify approaches discussed in the chat but not yet coded or confirmed. For each, write a next-step description that builds on what the user already understood - include what clicked and what open question remains. Do not just list approach names.

**`### Session Checklist`:** Read `## Session Checklist` from the analysis file. Filter: drop items that are already addressed by the approach blocks written above (e.g., if thinking and a bug were captured, the "approach understood" item is done). Mark pre-completed items with `[x]`. Keep only items that represent genuine remaining work. Always include the four Overall items.

If the file already had `## Import Notes` (Case B re-import): only update the checklist section - do not re-synthesize constructs or remaining approaches.

Write silently - do not narrate what you are writing.

### Step 6 - Format active-solution.cs

If `active-solution.cs` has raw unlabeled code, or has labeled blocks (`// Approach 1 - Brute Force`), reformat each block to the standard template:

```csharp
// ==== Approach N ====
// Approach: [name from chat]
// Time:  [leave as ? if not confirmed by user]
// Space: [leave as ? if not confirmed by user]
// Key Idea: [from chat - what the user said was the core idea]

public class SolutionN
{
    public <return-type> <MethodName>(<params>)
    {
        // [user's code, unchanged]
    }
}
```

- Fill `// Approach:`, `// Key Idea:` from what was said in the chat
- Leave `// Time:` and `// Space:` as `?` - these must be confirmed live
- Never modify the user's implementation code

If `active-solution.cs` is empty and no code was written in the chat, leave it empty.

### Step 7 - Report and orient

Show a summary:

```
Import complete: [Problem Name]

Analysis saved: reference-chats/<slug>-analysis.md
active-problem.md: [written / updated / skipped - already filled]
active-solution.cs: [formatted / unchanged / empty - no code in chat]

From the chat:
- Approaches covered: [list]
- Approaches not yet explored: [list]
- Complexity: [confirmed / not confirmed - needs your input]
- Code: [written / not yet written]

Checklist items still open:
[list the unchecked items from ## Import Notes -> ### Session Checklist in active-problem.md]

Ready to continue. What would you like to do next?
```

Do not start asking reflection questions or complexity questions yet. Wait for the user to direct the next step.

## Rules

- The chat is the source of truth. Do not invent insights, approaches, or lessons not present in the chat.
- Never write solution code into `active-solution.cs` - only reformat what the user wrote
- Time and space complexity headers stay as `?` until the user confirms them live
- The analysis file is a reference artifact - it is not part of the regular problem session flow. Do not link it from `active-problem.md`
- If the chat is very long (over 3000 lines), read it in full before generating analysis. Do not skim.
- The session checklist in the analysis file is the tracking layer for the imported session. Keep it updated as the session progresses.
