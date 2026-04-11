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
  ds-used: [array, hashmap]
  patterns: [HashMap, Two Pointers]
  constructs: [dictionary]
  algorithms: []
  tags: [complement-lookup, index-tracking, target-sum]
  ```
  - `number` and `slug` are derived from the folder name
  - `lists` from what was set in Mode 1; write `lists: []` if none specified
  - `ds-used`, `patterns`, `constructs`, `algorithms` are derived from the master-index.json entry written in Step 7 (or from active-problem.md if saving out of order)
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

### Step 5 - Write solutions.md

Create `problems/<slug>/solutions.md` with:
- `# [Problem Name] - Solutions` heading
- `## Approaches` section: for each solved approach, write:
  - `### Approach N: [name]`
  - `**Code:**` link to `.cs` file in `solutions/` subfolder
  - `**Time:** / **Space:**` from active-problem.md or active-solution.cs metadata
  - `**Thinking:**` - paraphrase what the user said about their approach and key idea. Do not add insights the user did not express
  - `---` separator between approaches
- After solved approaches, check if any alternative approaches were discussed during the session but not explored. For each discussed-but-not-explored approach, append a placeholder block:
  ```
  ### Approach N: [name] *(not yet explored)*
  **Time:** O(?) | **Space:** O(?)
  **Idea:** [one-sentence description of what was discussed]
  ```
  Use `---` separator before each placeholder block, same as solved approaches.
- `## Patterns` section: for each pattern, look up its `display_name` from the pattern `.md` file. Link using that display name: `[Display Name](../../patterns/<filename>.md) (Approach N) - description`
- `## Reflection` section with required fields: `**Key insight:**`, `**Future strategy:**`, plus any session-specific fields that capture what the user actually said

### Step 6 - Write notes.md

Combine from the active file:
- All `#### Bugs` entries -> `## Mistakes Made` section, grouped by `### Approach N - [name]` subheadings (no links). Skip approaches that had no bugs - do not create empty subheadings
- `## Reflection` answers -> `## Key Insights` and `## Mantras` sections. Mantras can also come from bug lessons, not just reflection
- `## Patterns` entries -> `## Patterns Used` section, listed by approach name with bold pattern names (no links - links live in solutions.md)

In saved files, skip empty sections entirely. Do not write section headings with no content.

### Step 7 - Write master-index.json

Set `_saving` to the problem number at the start of this step. Clear it back to `null` at the end.

Add or update the entry under `"problems"` keyed by problem number (string). Full entry format:
```json
"13": {
  "title": "Roman to Integer",
  "slug": "roman-to-integer",
  "difficulty": "Easy",
  "patterns": ["Linear Scan", "Preprocessing", "Chunked Iteration"],
  "constructs": ["dictionary", "string-replace"],
  "algorithms": [],
  "ds-used": ["string", "hashmap"],
  "ds-notes": {
    "hashmap": "char -> int lookup table for roman numeral values",
    "string": "string.Replace used to encode subtractive pairs before scanning"
  },
  "lists": [],
  "approaches": {
    "right-to-left-scan.cs": {
      "patterns": ["Linear Scan"],
      "variation": "Right to Left",
      "ds-used": ["string", "hashmap"]
    },
    "string-replacement.cs": {
      "patterns": ["Preprocessing"],
      "variation": "String Replacement",
      "ds-used": ["string", "hashmap"]
    }
  }
}
```

After writing the entry, update all four reverse-lookup indexes:
- `by-pattern`: for each pattern, add this problem number to its array (if not already present)
- `by-ds`: for each ds-used value, add this problem number
- `by-construct`: for each construct, add this problem number
- `by-algorithm`: for each algorithm, add this problem number (skip if `algorithms` is empty)

Pattern names use `display_name` from the pattern file. Construct names use the `slug` field from the construct file. Algorithm names use the `slug` field from the algorithm file.

### Step 8 - Update DS coverage tables

For each value in `ds-used` from the Step 7 entry:
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

### Step 11 - Confirm and suggest cleanup

Show a summary of what was created and updated:
- Files created in `problems/<slug>/`
- master-index.json updated
- DS coverage tables updated
- LESSONS.md entries added (if any)

Then suggest: "Ready to clear the active files for the next problem?"

If the user confirms, clear both `active-problem.md` and `active-solution.cs` to empty files. If not, leave them for review.

## Rules

- Ask the user to confirm the notes content before writing - do not invent reflection notes
- If a pattern file does not exist yet, create it using the template in `docs/pattern-system.md`
- If an algorithm file does not exist yet, create it using the template in `docs/pattern-system.md`
- If a construct file does not exist yet, create it using the construct template in `docs/pattern-system.md`. Choose the category from the taxonomy in that file. Create the file at `constructs/<category>/<slug>.md`
- If a concept file does not exist yet, create it using the concept template in `docs/pattern-system.md`. Create the file at `concepts/<slug>.md`
- If a new variation of an existing pattern was used, check whether the variation name (from the `variation` field in Step 7 approaches) already exists as a `## Variation: [name]` heading in the pattern file. If not, add the section: populate **When to reach for this** from the session, **Template** from the code structure used, **Common Mistakes** from any bugs logged during the session. The `## Seen In` block within the variation is a Dataview query - do not write it manually
- Prettification means: rename variables, one comment per logical block, plus session-derived "Why" blocks for decisions the user struggled with
- If the active file is incomplete (e.g., no Reflection section), ask the user to fill in the gaps before saving
- Active files are cleared to empty (not deleted) only when the user confirms
- When writing the Thinking field in solutions.md: paraphrase the user's stated approach and key idea. Do not add insights the user did not express
- When referencing patterns in solutions.md, look up the `display_name` from the pattern file
- Tags are AI-inferred from patterns and data structures used - do not ask the user for tags
- `_saving` in master-index.json must be cleared to `null` at the end of Step 7. If it is non-null at session start, a previous save was interrupted - check which steps completed and resume from the next incomplete step
