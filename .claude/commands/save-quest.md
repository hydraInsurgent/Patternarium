# /save-quest - Save Quest Batch to Repo

Save a quest batch session to the repo. A quest batch is a single Level of multiple LeetCode problems solved as practice reps (e.g., LeetCode Quest "Arrays 1" with Q1, Q2, Q3). Each question is a real LeetCode problem and is persisted as its own folder under `problems/`, identical in structure to a `/save-problem` output. The difference is the **session flow** (lighter ceremony) and the **batch nature** (many problems saved together).

## When to Use This Instead of /save-problem

| Use `/save-problem` when... | Use `/save-quest` when... |
|---|---|
| Single problem, deliberate practice | Multiple problems solved as practice reps |
| Multi-approach exploration | Single approach per problem |
| Pattern extraction is the point | Technique surfacing is the point |
| Reflection answers a learning question | Reflection is optional (often skipped) |
| Solo problem from a list (Blind 75) | Problems from a quest series (LeetCode Quest) |

`active-problem.md` format is the differentiator. If it has a `## Quest Batch:` header, run `/save-quest`. Otherwise run `/save-problem`.

## Architecture

Identical to `/save-problem`. Each question becomes its own `problems/<num>-<slug>/` folder with:
- `<slug>.md` - folder note (problem viewer)
- `solutions/solutions.md` - learning record
- `solutions/<approach>.cs` - one solution file (single approach)

The lighter ceremony shows up in the *content* of these files, not their structure:
- No `## Patterns` section in solutions.md unless a pattern genuinely surfaced
- No `## Reflection` section unless the user wrote one
- No `## Mistakes` per approach unless bugs occurred
- Knowledge Links callout writes `> _none_` for empty subsections, same as /save-problem

## Active Problem Quest Batch Format

The expected `active-problem.md` shape:

```markdown
## Quest Batch: <Track Name>
**Lists:** <list-slug> (<Track Name>)
**Time Started:** YYYY-MM-DD HH:MM
**Flow:** quest

---

## Q<N>: <Title>
**Number:** <LC number>
**Difficulty:** Easy | Medium | Hard
**Status:** solved

### Statement
[problem statement as-is]

### Approach: <Approach Name>
**Time:** O(?)
**Space:** O(?)
**Key Idea:** [one sentence]

#### Constructs
- <slug> - one-line role

#### Concepts (optional)
- <slug>

#### Mistakes (optional)
- [bug description and root cause]

---

## Q<N+1>: ...
[same structure]

---

## Techniques Surfaced
- **`technique-slug`** (Q1, Q2) - what the move does in one sentence
- **`other-technique-slug`** (Q3) - description
```

The matching `active-solution.cs` format:

```csharp
// ==== Q<N>: <Title> (LC <num>) ====
// Approach: <Approach Name>
// Time:  O(?)
// Space: O(?)
// Key Idea: <one sentence>

public class Solution
{
    // user's code
}

// ==== Q<N+1>: ...
[same shape]
```

## Behavior

### Step 1 - Read active files

Read `active-problem.md` and `active-solution.cs` from repo root. Validate quest batch format:
- `active-problem.md` must contain `## Quest Batch:` header
- Each `## Q<N>:` block must have `**Status:** solved`, `### Approach:`, `**Time:**`, `**Space:**`, `**Key Idea:**`
- `active-solution.cs` must have a matching `// ==== Q<N>:` block per question

If validation fails, report the missing pieces and stop. Do not write anything.

### Step 2 - Parse batch and confirm

Build a list of items to create:
- Each `## Q<N>:` block -> one problem folder
- Each entry in `## Techniques Surfaced` -> one technique file (create if missing)

Show the user:

```
Quest batch: <Track Name>
Saving N questions:
  - Q1: <Title> -> problems/<num>-<slug>/
  - Q2: <Title> -> problems/<num>-<slug>/
  - Q3: <Title> -> problems/<num>-<slug>/

Techniques to create/update:
  - <technique-slug> (new) - <description>
  - <existing-technique-slug> (update) - <description>

Proceed?
```

Wait for explicit confirmation before writing anything.

### Step 3 - For each question, write the problem folder

For each `## Q<N>:` block where status is `solved`, create `problems/<num>-<slug>/`:

#### Step 3a - Folder note (`<slug>.md`)

```markdown
---
title: <Problem Name>
category: problem-hub
problem: <number>
slug: <slug>
status: solved
first-solved: YYYY-MM-DD
times-revised: 0
last-revised:
lists: [<list-slug>]
---

# <Problem Name>

**Difficulty:** Easy | Medium | Hard
**Source:** LeetCode #<number>

## Statement
[as pasted in active-problem.md]

## Examples
[from statement if present, else omit]

## Constraints
[from statement if present, else omit]

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

> [!info]- Knowledge Links
>
> ### Patterns
> _none_
>
> ### Concepts
> - [<concept-slug>](../../concepts/<concept-slug>.md)
>
> ### Techniques
> - [<technique-slug>](../../techniques/<technique-slug>.md)
>
> ### Data Structures
> - [<ds-slug>](../../data-structures/<ds-slug>.md)
>
> ### Related Problems
> _none_

## Solutions
[Solutions & Learning Journey](solutions/solutions.md)
```

Rules:
- `lists` populated from the `**Lists:**` line of the Quest Batch header. Strip the parenthetical (e.g., `leetcode-quests (Arrays 1)` -> `[leetcode-quests]`)
- `first-solved` = today's date
- Knowledge Links subsections write `> _none_` when empty - same convention as /save-problem
- Most quests will have empty Patterns and Related Problems sections - that's expected

#### Step 3b - Solutions file (`solutions/<approach-slug>.cs`)

Derive `<approach-slug>` from the Approach name (e.g., "Single-Pass Dual Write" -> `single-pass-dual-write.cs`).

Write the prettified `.cs` file with this header:

```csharp
// Approach: <Approach Name>
// Time:  O(?)
// Space: O(?)
// Key Idea: <one sentence>

public class Solution
{
    // prettified user code
}
```

**Prettification rules** (same as /save-problem):
- Rename single-letter variables to descriptive names
- Add one comment per logical block explaining the *why*
- Preserve the user's logic exactly - only presentation changes
- Never change loop structure (while stays while, for stays for)
- Skip the "Why" block - quest sessions rarely produce decision-driving discussion

#### Step 3c - solutions.md (single approach, lighter body)

Frontmatter:

```yaml
---
problem: <number>
problem-title: <Problem Name>
difficulty: Easy | Medium | Hard
category: solutions
flow: quest
patterns: []
constructs: [slug1, slug2]
ds-used: [array]
algorithms: []
techniques: [technique-slug1, technique-slug2]
concepts: []
approaches:
  - name: <Approach Name>
    file: <approach-slug>.cs
    patterns: []
    constructs: [slug1]
    ds-used: [array]
    techniques: [technique-slug1]
    time: "O(?)"
    space: "O(?)"
---
```

- `flow: quest` flags this as a quest-session save - lets future Dataview queries filter quest reps from deliberate practice if needed
- `patterns: []` is expected to be empty for most quests
- `techniques` is the meaningful field for quest sessions

Markdown body (lean - skip empty sections):

```markdown
# <Problem Name> - Solutions

## Approaches

### Approach 1: <Approach Name>
**Code:** [<approach-slug>.cs](<approach-slug>.cs)
**Time:** O(?) | **Space:** O(?)

**Key Idea:** [one sentence from active-problem.md]

**Thinking:** [paraphrase from active-problem.md - what the user did and why]

**Mistakes:**
- [omit this whole field if no Mistakes block in active-problem.md]

---

## Techniques

- <Technique Display Name> (Approach 1) - one-line description matching the technique file
```

Sections to OMIT entirely (do not write empty headings):
- `## Patterns` - if no patterns surfaced
- `## Reflection` - if no reflection block in active-problem.md
- `**Mistakes:**` - if no Mistakes block in the question
- `**Key Condition:**` - quest approaches usually don't have invariant formulas

### Step 4 - Create / update technique files

Read the `## Techniques Surfaced` block in active-problem.md. For each entry:

- Check if `techniques/<slug>.md` exists
- If not, create from the template in `docs/pattern-system.md`:

```markdown
---
name: <slug>
slug: <slug>
display_name: <Display Name>
category: technique
tags: [<slug>, related-tag]
---

# <Display Name>

## What It Is
[one sentence - the optimization or implementation move]

## Core Reasoning
[why it works - the logical argument]

## When to Apply
- Signal 1
- Signal 2

## Template
[pseudocode or skeleton]

## Tradeoffs
- When it helps
- When it does not

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN techniques AS technique
WHERE technique = "<slug>"
SORT number asc
```
```

- The `## Seen In` Dataview query auto-populates from problem frontmatter - do not hand-list problems
- Tell the user: "Created technique file: `techniques/<slug>.md` - review and refine the template once save completes."

If the technique file already exists, leave it alone. The Dataview query in `## Seen In` will pick up the new problems automatically.

### Step 5 - Update workbench list

Read the `lists` field from each new folder note. For each list:

- Open `workbench/lists/<list-slug>.md`
- Find the row matching the LC number under the relevant track section (e.g., `## Arrays 1`)
- **Mandatory updates per row** (do not skip any):
  - `Status` column: `not started` -> `solved`
  - `Pattern / Technique` column: write the pattern display name (or technique display name if no pattern surfaced) used in the approach
  - `Link` column: `-` -> `[→](../../problems/<num>-<slug>/<num>-<slug>.md)`
- Update the track header progress count (e.g., `Progress: 0 / 3 solved` -> `Progress: 3 / 3 solved`)
- If the table for the relevant track is in the older 6-column format (no `Pattern / Technique` column), upgrade it to the 7-column format first: `| Quest # | LC # | Problem | Difficulty | Status | Pattern / Technique | Link |`. Existing not-started rows get `-` in the new column.

If the list file does not exist, ask the user to confirm before creating one.

### Step 6 - Regenerate master-index.json

Run from repo root:

```
dotnet run scripts/Rebuild-Index.cs
```

Same as /save-problem. The script reads folder note YAML and solutions.md frontmatter from every problem folder. It already handles `flow: quest` problems - they appear in the index identically to deliberate-practice problems.

If the script fails, report the error before proceeding.

### Step 7 - Update DS coverage tables (silent)

Same as /save-problem Step 7. For each `ds-used` value:
- Update `data-structures/<slug>.md` `## Coverage` table
- Update `progress` field
- Update `workbench/goals.md` Data Structure Coverage table

All coverage writes are silent.

### Step 8 - Confirm and suggest cleanup

Show a summary:

```
Saved quest batch: <Track Name>
- Created N problem folders:
  - problems/<num>-<slug>/ (Q1)
  - problems/<num>-<slug>/ (Q2)
  - problems/<num>-<slug>/ (Q3)
- Created M technique files:
  - techniques/<slug>.md
- Updated workbench/lists/<list-slug>.md
- Regenerated master-index.json
- Updated DS coverage for: <ds-list>

Ready to clear the active files for the next batch?
```

If yes, clear `active-problem.md` and `active-solution.cs` to empty.

## Rules

- Quest batches must use the format documented above. If the active file is in single-problem format, route to `/save-problem` instead
- Single approach per question - never invent a second approach
- Patterns section is omitted unless a pattern genuinely surfaced. Do not force-fit a pattern label
- Reflection is omitted unless the user wrote one. Do not prompt for reflection - quests are reps, not deliberate practice
- Mistakes are omitted per approach unless bugs were logged
- Technique files are created from the template - the body fields (Core Reasoning, When to Apply, Template, Tradeoffs) are the AI's first draft. Tell the user to review and refine
- Code prettification rules are identical to /save-problem - rename single-letter vars, one comment per logical block, preserve logic exactly, never change loop structure
- The `flow: quest` frontmatter field is mandatory in solutions.md - it differentiates quest reps from deliberate-practice problems for future Dataview filtering
- Active files are cleared to empty (not deleted) only when the user confirms
- If any `// Time:` or `// Space:` headers in `active-solution.cs` still contain `?`, ask the user to confirm complexity before saving - do not write `?` to solutions.md frontmatter
- LESSONS.md is not updated by this command - quest reps don't typically produce lessons. If a notable mistake came up, ask the user whether to log it
- Analysis files (`reference-chats/analysis/`) are not relevant to quest sessions - skip Step 10 from /save-problem entirely
