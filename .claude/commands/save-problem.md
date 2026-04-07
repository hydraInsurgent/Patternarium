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
- YAML frontmatter: `title`, `category: DSA-Practice`, `difficulty`, `source: LeetCode`, `status: solved`, `lists: [<list-names>]` (from what was set in Mode 1; if no list was specified, write `lists: []`)
- Problem title as `# Heading`
- `## Statement`, `## Examples`, `## Constraints` as separate sections. Keep the problem statement as-is from the source - do not restructure
- `## Solutions` section at bottom with links to `solutions.md` and `notes.md`
- Tags as inline metadata at the very bottom: `tags :: [comma-separated tags]` (placed here to avoid spoiling the approach)
- Tags are AI-inferred from the patterns and data structures used during the session

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

### Step 7 - Update pattern-index.json

Use the LeetCode problem number as the key. Title and metadata live inside the object. Pattern names match the `display_name` field in the pattern file. Example:
```json
"13": {
  "title": "Roman to Integer",
  "slug": "roman-to-integer",
  "difficulty": "Easy",
  "patterns": ["Linear Scan", "Preprocessing", "Chunked Iteration"],
  "approaches": {
    "right-to-left-scan.cs": ["Linear Scan"],
    "string-replacement.cs": ["Preprocessing"]
  }
}
```

Key is always the problem number (string). Display names are looked up from the pattern file when needed.

### Step 8 - Update pattern files

For each pattern in `## Patterns`:
- Add this problem to the "Solved Problems" section of `patterns/<pattern>.md`
- Add any new bugs from this session to the "Common Mistakes" section if applicable

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

### Step 10 - Confirm and suggest cleanup

Show a summary of what was created and updated:
- Files created in `problems/<slug>/`
- Patterns updated
- LESSONS.md entries added (if any)

Then suggest: "Ready to clear the active files for the next problem?"

If the user confirms, clear both `active-problem.md` and `active-solution.cs` to empty files. If not, leave them for review.

## Rules

- Ask the user to confirm the notes content before writing - do not invent reflection notes
- If a pattern file does not exist yet, create it using the template in `docs/pattern-system.md`
- Prettification means: rename variables, one comment per logical block, plus session-derived "Why" blocks for decisions the user struggled with
- If the active file is incomplete (e.g., no Reflection section), ask the user to fill in the gaps before saving
- Active files are cleared to empty (not deleted) only when the user confirms
- When writing the Thinking field in solutions.md: paraphrase the user's stated approach and key idea. Do not add insights the user did not express
- When referencing patterns in solutions.md, look up the `display_name` from the pattern file
- Tags are AI-inferred from patterns and data structures used - do not ask the user for tags
