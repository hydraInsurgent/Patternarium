# /save-problem - Save Problem to Repo

Save the current problem session to the repo. Reads from `active-problem.md` (learning journey) and `active-solution.cs` (user's code), decomposes them into the proper persistent structure.

## Behavior

### Step 1 - Read active files

Read both `active-problem.md` and `active-solution.cs` from repo root. If either file does not exist, fall back to reconstructing from conversation history and warn the user: "Active file(s) missing - reconstructing from our conversation. Some details may be incomplete."

### Step 2 - Confirm problem slug

Derive the folder name from the source and problem name in kebab-case:
- LeetCode: `<number>-<name>` (e.g., `13-roman-to-integer`)
- HackerRank: `hr-<name>` (e.g., `hr-dynamic-array`)
- Codeforces: `cf-<name>` (e.g., `cf-watermelon`)
- Other: `<source-prefix>-<name>`

Ask user to confirm: "Save to `problems/<slug>/`?"

### Step 3 - Write problem.md

Parse `## Problem` and `## Statement` from the active file. Write to `problems/<slug>/problem.md` with:
- YAML frontmatter with all fields:
  ```yaml
  title: Two Sum
  number: 1
  slug: two-sum
  category: DSA-Practice
  difficulty: Easy
  source: LeetCode
  status: solved
  lists: [blind-75]
  tags: [complement-lookup, index-tracking, target-sum]
  ```
  - `number` and `slug` are derived from the folder name
  - `lists` from what was set in Mode 1; write `lists: []` if none specified
  - `tags` are AI-inferred from the patterns and data structures used during the session
- Problem title as `# Heading`
- `## Statement`, `## Examples`, `## Constraints` as separate sections. Keep the problem statement as-is from the source - do not restructure
- `## Solutions` section at bottom with links to `solutions.md` and `notes.md`

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

Approaches with status "in-progress" or "stuck" in `active-problem.md` are skipped.

### Step 4b - Ensure pattern files exist

For each pattern named in `active-problem.md` `## Patterns`:
- Check if `patterns/<file>.md` exists. If it does, read its `display_name` and confirm the `## Variation:` heading that will be referenced exists in the file.
- If the pattern file does not exist, create it now from the template in `docs/pattern-system.md`. Derive `display_name` from the pattern name used in the session. Tell the user: "Creating new pattern file: `patterns/<file>.md` - you may want to review it after save."
- If the file exists but the specific variation does not, add the variation section now before proceeding.

This step must complete before Step 5, which uses `display_name` to build links in solutions.md.

### Step 5 - Write solutions.md

Create `problems/<slug>/solutions.md`. Write YAML frontmatter first, then the markdown body.

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
approaches:
  - name: Brute Force
    file: solutions/brute-force.cs
    patterns: []
    constructs: []
    ds-used: [array]
    time: "O(n^2)"
    space: "O(1)"
  - name: HashMap - Complement Lookup
    file: solutions/hashmap.cs
    patterns: [HashMap]
    variation: Complement Lookup
    constructs: [dictionary]
    ds-used: [array, hashmap]
    ds-notes:
      hashmap: "complement lookup: store value -> index"
    time: "O(n)"
    space: "O(n)"
---
```

- `patterns`, `constructs`, `ds-used`, `algorithms` at the top level are flat lists aggregated across all solved approaches - used by Dataview queries
- Each entry in `approaches:` maps to one solved approach. Include `variation` only when the approach uses a named pattern variation. Include `ds-notes` only when a DS is used in a non-obvious way. `file` is the relative path within the problem folder (e.g., `solutions/hashmap.cs`)
- Pattern names use `display_name` from the pattern file. Construct and algorithm names use their `slug` field
- `category: solutions` enables Dataview queries to filter solutions.md files from problem.md files when querying `FROM "problems"`
- **Constructs fallback for imported sessions:** if approach blocks in active-problem.md do not have `## Constructs` subsections, check `## Import Notes` -> `### Constructs Identified` in active-problem.md. Map each construct name to its slug using the construct files in `constructs/`. If no construct file exists yet, use the name as a provisional slug and create the file (per the Rules section).
- **Remaining approaches source:** check `## Import Notes` -> `### Remaining Approaches` in active-problem.md for approaches discussed but not yet explored. If that section exists, use it as the source for not-yet-explored placeholder blocks. If it does not exist, infer from conversation history.

**Markdown body:**
- `# [Problem Name] - Solutions` heading
- `## Approaches` section: for each solved approach, write:
  - `### Approach N: [name]`
  - `**Code:**` link to `.cs` file in `solutions/` subfolder
  - `**Time:** / **Space:**` from active-problem.md or active-solution.cs metadata
  - `**Key Condition:**` - the specific formula, invariant, or condition that defines this approach. Source in priority order: (1) `#### Solution` block's `**Key Condition:**` field in active-problem.md, (2) the analysis file's `## Core Formula / Key Condition` section if one exists for this problem, (3) derive from the user's stated approach in `#### Thinking`. Omit entirely for trivial brute force where Thinking already covers it fully. Never fabricate - if no clear condition is identifiable, omit.
  - `**Thinking:**` - paraphrase what the user said about their approach and key idea. Do not add insights the user did not express
  - `---` separator between approaches
- After solved approaches, check if any alternative approaches were discussed during the session but not explored. For each discussed-but-not-explored approach, append a placeholder block:
  ```
  ### Approach N: [name] *(not yet explored)*
  **Time:** O(?) | **Space:** O(?)
  **Idea:** [one-sentence description of what was discussed]
  ```
  Use `---` separator before each placeholder block, same as solved approaches.
- `## Patterns` section: for each pattern, link using `display_name` and variation name with a heading anchor: `[display_name - Variation Name](../../patterns/<file>.md#variation-<anchor>) (Approach N) - description`
- `## Reflection` section with required fields: `**Key insight:**`, `**Future strategy:**`, plus any session-specific fields that capture what the user actually said

### Step 6 - Write notes.md

Combine from the active file:
- All `#### Bugs` entries -> `## Mistakes Made` section, grouped by `### Approach N - [name]` subheadings (no links). Skip approaches that had no bugs - do not create empty subheadings
- `## Reflection` answers -> `## Key Insights` and `## Mantras` sections. Mantras can also come from bug lessons, not just reflection
- `## Patterns` entries -> `## Patterns Used` section, listed by approach name with bold pattern names (no links - links live in solutions.md)
- Source for `## Connected Problems` in notes.md: (1) if an analysis file exists, read its `## Connections to Other Problems` section; (2) otherwise, check `## Connections` in active-problem.md. Use whichever source has content. If both exist, merge them (deduplicate by problem number). Format: `**[Number] - [Problem Name]** - one-sentence connection`. Skip the section entirely if neither source has content.

In saved files, skip empty sections entirely. Do not write section headings with no content.

### Step 7 - Regenerate master-index.json

Run from repo root:

```
dotnet run scripts/Rebuild-Index.cs
```

This reads the `solutions.md` YAML frontmatter from every problem folder and regenerates `master-index.json` with the full `problems` map and all four reverse-lookup indexes (`by-pattern`, `by-ds`, `by-construct`, `by-algorithm`).

Do not hand-write master-index.json entries. The script is the single source of truth for this file. If the script fails, report the error to the user before proceeding.

### Step 8 - Update DS coverage tables

For each value in `ds-used` from the problem's `solutions.md` frontmatter:
- Find `data-structures/<slug>.md` (the `slug` field in DS frontmatter matches the ds-used value)
- In the `## Coverage` section, match by link target: search for a row containing `[display_name](../patterns/file.md)` for each pattern used
- If the linked row exists: append this problem to its Problems Solved column
- If no linked row exists: add a new row using the exact format `[display_name](../patterns/file.md)`
- Plain text placeholders: if a plain text row closely matches the pattern's display_name and the pattern file exists, convert it to a link first, then update

After updating rows:
- Recount explored rows, recalculate the percentage, update the `progress` field in DS frontmatter
- Update the matching row in `workbench/goals.md` Data Structure Coverage table

All coverage writes are silent.

### Step 9 - Update workbench lists

Read the `lists` field from the problem's YAML frontmatter. For each list name:
- Check if `workbench/lists/<list-name>.md` exists
- If it exists: find the row for this problem and update its status to `solved`, add the pattern link and problem link
- If it does not exist: ask the user "List '<list-name>' not found - is this a typo? Known lists are: [list existing files in workbench/lists/]. Confirm the correct name before creating a new list file."

List file format:
```markdown
# <List Name>

| # | Problem | Difficulty | Status | Pattern | Link |
|---|---------|-----------|--------|---------|------|
| 5 | Longest Palindromic Substring | Medium | solved | Two Pointers - Expand Around Center | [→](../../problems/5-longest-palindromic-substring/problem.md) |
```

If no `lists` field exists in the frontmatter, skip this step.

### Step 10 - Update LESSONS.md

If any bugs or reflection mistakes warrant a lesson entry, add them to the appropriate section in LESSONS.md (Conceptual Mistakes, Code Mistakes, or Pattern Misidentifications).

If an analysis file exists for this problem: scan its `## Mistakes and Lessons` table. For each row, check if the mistake is already in LESSONS.md. If not, add it. If it is, enrich the existing entry with the `What happened` context from the table if the current entry lacks that detail.

### Step 11 - Distill analysis file (if present)

Glob `reference-chats/analysis/*.md`. For each file found, read its YAML frontmatter and check if `slug` matches the current problem's slug (e.g., `647-palindromic-substrings`). If a match is found, that is the analysis file for this problem. If no match is found, skip this step entirely.

If found, perform these distillations:

**Pattern file enrichment:**
For each pattern used in the session:
- Read the analysis `## Pattern Signals` and `## Key Insights for Pattern Library` sections
- Open the pattern file and find the relevant `## Variation:` section
- Add any signals not already present to `**When to reach for this:**`. Deduplication rule: skip a bullet only if it uses identical or near-identical wording to an existing bullet. If the meaning overlaps but the phrasing is distinct, add it - the pattern file is a reference, not a summary.
- Add any pattern-specific mistakes (filtered from the analysis Mistakes table) to `## Common Mistakes`. Filter rule: only add mistakes that are caused by misapplying this pattern's specific mechanics. Do not add general learning behavior mistakes, DP concept confusions unrelated to the pattern, or problem comprehension errors - those belong in LESSONS.md only. Deduplication rule: skip a mistake only if it uses identical or near-identical wording to an existing entry.

**Archive:**
After all distillation is complete, move both files to `reference-chats/_archive/<problem-slug>/`:
- `reference-chats/imports/<chat-filename>.txt` (if present)
- `reference-chats/analysis/<slug>-analysis.md`

Create the `_archive/<problem-slug>/` folder if it does not exist. If either source file is not found, skip moving it silently.

### Step 11b - Verify import checklist (if present)

If `active-problem.md` contains `## Import Notes` -> `### Session Checklist`:
1. Cross-check each unchecked item (`- [ ]`) against what was actually done during the session (approach blocks solved, complexity confirmed, code written, reflection answered). If the work is done but the item was never ticked, mark it `[x]` silently.
2. After auto-updating, if any items remain unchecked, surface them to the user:
   > "These checklist items are still open - address them before saving, or save anyway?"
   > [list remaining unchecked items]
   The user decides whether to proceed. Do not block the save.

### Step 12 - Confirm and suggest cleanup

Show a summary of what was created and updated:
- Files created in `problems/<slug>/`
- master-index.json updated
- DS coverage tables updated
- LESSONS.md entries added or enriched (if any)
- Analysis distilled and archived (if analysis file was present)

Then suggest: "Ready to clear the active files for the next problem?"

If the user confirms, clear both `active-problem.md` and `active-solution.cs` to empty files. If not, leave them for review.

`## Import Notes` in active-problem.md is cleared as part of this - it does not need separate handling.

## Rules

- Ask the user to confirm the notes content before writing - do not invent reflection notes
- If a pattern file does not exist yet, create it using the template in `docs/pattern-system.md`
- If an algorithm file does not exist yet, create it using the template in `docs/pattern-system.md`
- If a construct file does not exist yet, create it using the construct template in `docs/pattern-system.md`. Choose the category from the taxonomy in that file. Create the file at `constructs/<category>/<slug>.md`
- If a concept file does not exist yet, create it using the concept template in `docs/pattern-system.md`. Create the file at `concepts/<slug>.md`
- If a new variation of an existing pattern was used, check whether the variation name (from the `variation` field in Step 7 approaches) already exists as a `## Variation: [name]` heading in the pattern file. If not, add the section: populate **When to reach for this** from the session, **Template** from the code structure used, **Common Mistakes** from any bugs logged during the session. The `## Seen In` block within the variation is a Dataview query - do not write it manually
- Prettification means: rename variables, one comment per logical block, plus session-derived "Why" blocks for decisions the user struggled with
- If the active file is incomplete (e.g., no Reflection section), ask the user to fill in the gaps before saving
- If any `// Time:` or `// Space:` headers in `active-solution.cs` still contain `?`, treat this as incomplete. Ask the user to confirm complexity for each unconfirmed approach before saving. Do not write `?` into solutions.md frontmatter
- Active files are cleared to empty (not deleted) only when the user confirms
- When writing the Thinking field in solutions.md: paraphrase the user's stated approach and key idea. Do not add insights the user did not express
- When referencing patterns in solutions.md, look up the `display_name` from the pattern file
- Tags are AI-inferred from patterns and data structures used - do not ask the user for tags
