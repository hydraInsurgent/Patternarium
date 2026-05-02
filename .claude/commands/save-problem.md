# /save-problem - Save Problem to Repo

Save the current problem session to the repo. Reads from `active-problem.md` (learning journey) and `active-solution.cs` (user's code), decomposes them into the proper persistent structure.

## Architecture

Each problem folder has a single folder note plus a `solutions/` subfolder:
- `<slug>.md` - the **problem folder note** (problem viewer): YAML metadata, problem statement, knowledge links, related problems
- `solutions/solutions.md` - the **solutions folder note** (learning record): YAML for queries, approaches with code/thinking/mistakes, patterns/techniques references (text, not links), reflection
- `solutions/*.cs` - one file per approach, siblings of `solutions.md`

Two-tier folder note pattern: both folders have a folder note named the same as the folder. There is no longer a `problem.md` or `notes.md`. All content lives in the appropriate folder note.

**Pattern/technique references in solutions.md:** the `## Patterns` and `## Techniques` sections in the solutions.md body list pattern/technique names as **plain text** (not markdown links). Reason: pattern/technique links live in the folder note's Knowledge Links callout - duplicating them in solutions.md creates redundant graph edges. The folder note is the single source of truth for cross-vault navigation.

## Behavior

### Step 1 - Read active files

Read both `active-problem.md` and `active-solution.cs` from repo root. If either file does not exist, fall back to reconstructing from conversation history and warn the user: "Active file(s) missing - reconstructing from our conversation. Some details may be incomplete."

### Step 1b - Derive bugs (save-time only)

`#### Bugs` is **not** populated during the session - it is filled here. Read the following sources and identify bugs per approach:
- Conversation history (live discussion of bugs while the user was solving)
- In-file code comments inside `active-solution.cs` (e.g. `// V1 missed null case`, `// fails on []`)
- `reference-chats/analysis/<slug>-analysis.md` `## Mistakes and Lessons` table (if `/import-chat` was used)

For each candidate bug, write the description and root cause as one bullet under `#### Bugs` of the corresponding approach block in `active-problem.md`. Then surface the rollup to the user:

> "Bugs I identified for this session: [list]. Confirm or edit before I save."

Do not invent bugs that have no source. If no bugs surface from any source, leave `#### Bugs` empty for that approach. Do not pad with stylistic critiques.

### Step 1c - Read and route the analysis file (end-to-end)

If `/import-chat` was used, glob `reference-chats/analysis/*.md` for the file whose frontmatter `slug` matches this problem. Read it in full. The analysis file is a first-class input to save - every section has a destination, and each downstream step is responsible for its row:

| Analysis Section | Feeds Into | Used By Step |
|------------------|------------|--------------|
| `## Thinking Journey` | enriches per-approach `**Thinking:**` in solutions.md if active-problem.md thinking is sparse; informs `## Reflection` if reflection has gaps | Step 5 |
| `## Core Formula / Key Condition` | per-approach `**Key Condition:**` field in solutions.md (when an approach has a central formula or invariant) | Step 5 |
| `## Approaches` | cross-reference active-problem.md approach blocks; surface gaps if analysis has richer detail than the live session captured | Step 5 |
| `## Mistakes and Lessons` | candidate bugs (Step 1b); LESSONS.md entries (Step 9) | Steps 1b, 9 |
| `## Key Insights for Pattern Library` | technique / construct / concept candidates worth capturing as files | Step 4b |
| `## Constructs Used` | `constructs:` field in solutions.md frontmatter (fallback when active-problem.md lacks a `## Constructs` subsection per approach) | Step 5 |
| `## Pattern Signals` | pattern file `**When to reach for this:**` enrichment | Step 10 |
| `## Connections to Other Problems` | folder note Related Problems (when `## Connections` in active-problem.md is sparse) | Step 3 |
| `## Session Checklist` | verify completion before save | Step 10b |

After parsing, build a single rollup of NEW candidates (anything in the analysis that is not already reflected in active-problem.md) and surface it together with the Step 1b bug rollup, in one confirmation:

> "Bugs I identified: [list]. Analysis also flagged these candidates that aren't logged yet: [techniques: ..., constructs: ..., concepts: ..., connections: ...]. Confirm before I save."

Apply confirmed additions to active-problem.md (so they flow through Steps 3-10 naturally), then proceed.

If no analysis file exists for this problem, this step is skipped silently.

### Step 2 - Confirm problem slug

Derive the folder name from the source and problem name in kebab-case:
- LeetCode: `<number>-<name>` (e.g., `13-roman-to-integer`)
- HackerRank: `hr-<name>` (e.g., `hr-dynamic-array`)
- Codeforces: `cf-<name>` (e.g., `cf-watermelon`)
- Other: `<source-prefix>-<name>`

Ask user to confirm: "Save to `problems/<slug>/`?"

### Step 3 - Write folder note (`<slug>.md`)

The folder note is the problem viewer. It has YAML metadata, the problem statement (above the `/revise` boundary), and knowledge links (below the boundary, hidden during cold re-solve).

Filename matches the folder name: `problems/<slug>/<slug>.md`.

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
lists: [blind-75]
---

# <Problem Name>

**Difficulty:** Easy | Medium | Hard
**Source:** LeetCode #<number>

## Statement

[Statement as-is from the source]

## Examples

[Examples from the source]

## Constraints

[Constraints from the source]

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

> [!info]- Knowledge Links
>
> ### Patterns
> - [<pattern-slug>](../../patterns/<pattern-slug>.md)
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
> - [<other-folder>](../<other-folder>/<other-folder>.md) - one-sentence connection

## Solutions
[Solutions & Learning Journey](solutions/solutions.md)
```

- `problem` is the number from the folder name
- `slug` matches the folder name
- `lists` from what was set in Mode 1; write `lists: []` if none specified
- `first-solved` set to today's date (YYYY-MM-DD)
- `times-revised: 0` initially - `/revise` increments this
- Knowledge Links callout uses markdown links (works in both Obsidian and VS Code preview), populated from solutions.md frontmatter (patterns, concepts, techniques, ds-used)
- Related Problems comes from `## Connections` in active-problem.md or analysis file's `## Connections to Other Problems`. Write `> _none_` if no connections found.
- If a knowledge layer category has no entries, write `> _none_` for that subsection.
- Statement / Examples / Constraints are above the `/revise boundary` so they show during cold re-solve. Knowledge Links are below so they don't spoil.

### Step 3b - Consolidate snapshots in active-solution.cs

`active-solution.cs` typically has multiple **snapshots** of the same approach (V1, V2, V3...) that accumulated during the session - some working, some broken. **Default rule: the working snapshot is the keeper; broken snapshots are not persisted as code, but they feed into Step 1b (bug derivation) and Step 9 (LESSONS.md).** No questionnaire is needed unless the working version is ambiguous.

For each approach group:
1. Identify the working snapshot. Signals: user's own comments labelling it (`// working`, `// final clean code`), absence of `// broken` / `// missed null case` style notes, code that compiles and matches the problem's I/O. If multiple candidates look working, ask: "Multiple working snapshots for [approach name] - which is the keeper?"
2. Trim the file in place: keep only the working snapshot, remove the others. Broken snapshots have already informed Step 1b; they do not need to remain in the file.
3. Confirm the kept block has filled `// Approach:`, `// Time:`, `// Space:`, `// Key Idea:` headers. If any are still `?`, ask the user to fill them now. Headers are confirmed **only on the keeper** - never spend effort filling metadata for the broken snapshots that are about to be trimmed.

If only one snapshot exists for an approach, no consolidation is needed - just verify the headers.

### Step 4 - Write solution .cs files

Parse `active-solution.cs` for each `// ==== Approach N ====` block:
- Read the user-filled metadata (Approach name, Time, Space, Key Idea) from the comment header
- Derive filename from approach name (e.g., "Subtraction Rule" -> `subtraction-rule.cs`, "Brute Force" -> `brute-force.cs`)
- Cross-reference with `active-problem.md` - only persist approaches with **Status: solved**
- Write `.cs` files into `problems/<slug>/solutions/` subfolder

**Prettification rules:**
- Rename single-letter variables to descriptive names (e.g., `num` -> `result`, `map` -> `romanValues`)
- Add one comment per logical block explaining the *why*, not the *what*
- Add a "Why" block after the code for decisions that came from bugs, mistakes, or discussions during the session. These are learning artifacts - only include "Why" entries for things the user actually struggled with or asked about. Never generate generic "Why" blocks
- Preserve the user's logic exactly - only presentation changes
- **Never change loop structure.** `while` stays `while`, `for` stays `for`. Converting between semantically equivalent loop forms is a structural change and is forbidden.

Approaches with status "in-progress" or "stuck" in `active-problem.md` are skipped.

### Step 4b - Ensure pattern, technique, concept files exist

Sources to scan:
1. `active-problem.md` - patterns/techniques/concepts named directly in the session
2. **`reference-chats/analysis/<slug>-analysis.md` `## Key Insights for Pattern Library`** (if `/import-chat` was used) - technique, construct, or concept candidates surfaced during analysis but not yet logged. Before creating files for these, surface them to the user: "Analysis flagged these as worth capturing: [list]. Add them?"
3. `active-solution.cs` - look at the user's code for techniques implied by the implementation (e.g. `mergedList` + moving `tail` -> head-tail-tracking)

For each accepted pattern, technique, and concept:
- Check if the file exists in `patterns/`, `techniques/`, or `concepts/`. If not, create from the template in `docs/pattern-system.md`.
- For patterns, also confirm the `## Variation:` heading exists. If not, add the variation section.
- Tell the user: "Creating new <type> file: `<path>` - you may want to review it after save."

This step must complete before Step 5.

### Step 5 - Write solutions.md

Create `problems/<slug>/solutions/solutions.md` (inside the `solutions/` folder, sibling to the `.cs` files). YAML frontmatter, then markdown body with mistakes inline per approach.

**Frontmatter:**

```yaml
---
problem: <number>
problem-title: <Problem Name>
difficulty: Easy | Medium | Hard
category: solutions
patterns: [DisplayName1, DisplayName2]
constructs: [slug1, slug2]
ds-used: [ds1, ds2]
algorithms: []
techniques: [technique-slug1]
concepts: [concept-slug1]
approaches:
  - name: Brute Force
    file: brute-force.cs
    patterns: []
    constructs: []
    ds-used: [array]
    techniques: []
    time: "O(n^2)"
    space: "O(1)"
  - name: HashMap - Complement Lookup
    file: hashmap.cs
    patterns: [HashMap]
    variation: Complement Lookup
    constructs: [dictionary]
    ds-used: [array, hashmap]
    techniques: [running-max]
    ds-notes:
      hashmap: "complement lookup: store value -> index"
    time: "O(n)"
    space: "O(n)"
---
```

- Top-level `patterns`, `constructs`, `ds-used`, `algorithms`, `techniques`, `concepts` are flat lists aggregated across all solved approaches - used by Dataview queries and `master-index.json`
- Each entry in `approaches:` maps to one solved approach. Include `variation` only when the approach uses a named pattern variation. Include `ds-notes` only when a DS is used in a non-obvious way.
- Pattern names use `display_name` from the pattern file. Construct, technique, concept, and algorithm names use their `slug` field.
- `category: solutions` enables Dataview queries to filter solution files when querying `FROM "problems"`
- **No `tags:` field** - all metadata lives in structured fields.

**Markdown body:**

```markdown
# [Problem Name] - Solutions

## Approaches

### Approach N: [name]
**Code:** [filename.cs](filename.cs)
**Time:** O(?) | **Space:** O(?)

**Key Condition:** [the specific formula, invariant, or condition - omit if trivial]

**Thinking:** [paraphrase of user's stated approach and key idea]

**Mistakes:**
- [bug description and root cause - only if this approach had bugs]

---

### Approach N+1: ...
[same structure]

---

## Patterns

- Pattern Display Name - Variation Name (Approach N) - one-line description

## Techniques

- Technique Display Name (Approach N) - one-line description

## Reflection

- **Key insight:** [from session]
- **Future strategy:** [from session]
- **Notes Insights:** [problem-level takeaways - bullets]
- **Mantras:** [memorable one-liners - bullets, optional]
- **[session-specific field]:** [any other reflection field that came up]
```

- Mistakes are inline per approach. Skip the `**Mistakes:**` field if the approach had no bugs.
- The `## Patterns` section uses `display_name + Variation` and links to the pattern file with anchor.
- The `## Techniques` section lists each technique used and which approach used it.
- The `## Reflection` section combines reflection answers with insights and mantras (formerly in notes.md).
- Skip empty sections - do not write headings with no content.

**Constructs / techniques fallback for imported sessions:** if approach blocks don't have explicit `## Constructs` or `## Techniques` subsections, infer from the user's code in `active-solution.cs` and from `## Import Notes` -> `### Constructs Identified` if present.

**Remaining approaches source:** check `## Import Notes` -> `### Remaining Approaches` for approaches discussed but not yet explored. Append placeholder blocks:

```markdown
### Approach N: [name] *(not yet explored)*
**Time:** O(?) | **Space:** O(?)
**Idea:** [one-sentence description]
```

### Step 6 - Regenerate master-index.json

Run from repo root:

```
dotnet run scripts/Rebuild-Index.cs
```

This reads the **folder note** YAML (`<slug>.md`) and `solutions.md` frontmatter from every problem folder and regenerates `master-index.json` with the full `problems` map and reverse-lookup indexes (`by-pattern`, `by-ds`, `by-construct`, `by-algorithm`, `by-technique`, `by-concept`).

Do not hand-write master-index.json. The script is the single source of truth. If it fails, report the error before proceeding.

### Step 7 - Update DS coverage tables

For each value in `ds-used` from solutions.md frontmatter:
- Find `data-structures/<slug>.md`
- In the `## Coverage` section, match the pattern row by link target. If found, append this problem to its Problems Solved column. If not found, add a new linked row.
- Recount explored rows, recalculate the percentage, update the `progress` field.
- Update the matching row in `workbench/goals.md` Data Structure Coverage table.

All coverage writes are silent.

### Step 8 - Update workbench lists

Read the `lists` field from the folder note YAML. For each list name:
- Check if `workbench/lists/<list-name>.md` exists
- If it exists: find the row for this problem and update its status to `solved`, add the pattern. Update the link to point to the folder note: `[→](../../problems/<slug>/<slug>.md)`
- If it does not exist: ask the user to confirm before creating.

If `lists: []` is empty, skip this step.

### Step 9 - Update LESSONS.md

If any bugs or reflection mistakes warrant a lesson entry, add them to LESSONS.md (Conceptual Mistakes, Code Mistakes, or Pattern Misidentifications).

If an analysis file exists for this problem: scan its `## Mistakes and Lessons` table. For each row not already in LESSONS.md, add it. Enrich existing entries with `What happened` context if missing.

### Step 10 - Distill analysis file (if present)

Glob `reference-chats/analysis/*.md`. For each file with frontmatter `slug` matching this problem, perform pattern enrichment:

For each pattern used in the session:
- Open the pattern file and find the relevant `## Variation:` section
- Add new signals from the analysis `## Pattern Signals` to `**When to reach for this:**` (skip near-identical wording)
- Add pattern-specific mistakes from the analysis Mistakes table to `## Common Mistakes` (skip near-identical wording, skip mistakes not caused by this pattern's mechanics)

After distillation, move both files to `reference-chats/_archive/<problem-slug>/`:
- `reference-chats/imports/<chat-filename>.txt|.md`
- `reference-chats/analysis/<slug>-analysis.md`

### Step 10b - Verify import checklist (if present)

If `active-problem.md` contains `## Import Notes` -> `### Session Checklist`:
1. Auto-tick items that were addressed during the session.
2. If any items remain unchecked, surface them to the user before save: "These items are still open - address before saving, or save anyway?"

### Step 11 - Confirm and suggest cleanup

Show a summary of what was created and updated:
- Files in `problems/<slug>/`: folder note, solutions.md, .cs files
- master-index.json updated
- DS coverage tables updated
- LESSONS.md entries (if any)
- Analysis distilled and archived (if any)

Then suggest: "Ready to clear the active files for the next problem?"

If yes, clear `active-problem.md` and `active-solution.cs` to empty.

`## Import Notes` in active-problem.md is cleared as part of this.

## Rules

- Ask the user to confirm reflection content before writing - do not invent insights
- If a pattern, technique, concept, construct, or algorithm file does not exist yet, create it from the template in `docs/pattern-system.md`
- If a new variation of an existing pattern was used, add the variation section to the pattern file
- Prettification means: rename variables, one comment per logical block, plus session-derived "Why" blocks
- If `## Reflection` is missing in active-problem.md, ask the user to fill it before saving
- If any `// Time:` or `// Space:` headers in `active-solution.cs` still contain `?`, ask the user to confirm complexity before saving - do not write `?` to solutions.md frontmatter
- Active files are cleared to empty (not deleted) only when the user confirms
- When writing `**Thinking:**` in solutions.md, paraphrase the user's words - do not add insights they did not express
- When referencing patterns, look up the `display_name` from the pattern file
- The folder note ends with a `## Solutions` section linking to `solutions.md` - this is the navigation hop from the problem viewer to the learning record. Knowledge Links callout handles cross-vault navigation.
- **No `problem.md` or `notes.md`** - those files do not exist in this architecture
