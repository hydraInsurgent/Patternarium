# /save-problem - Save Problem to Repo

Save the current problem session to the repo. Reads from `active-problem.md`, decomposes it into the proper persistent structure, then deletes the active file.

## Behavior

### Step 1 - Read active-problem.md

Read `active-problem.md` from repo root. If the file does not exist, fall back to reconstructing from conversation history and warn the user: "No active problem file found - reconstructing from our conversation. Some details may be incomplete."

### Step 2 - Confirm problem slug

Ask: "What should we call this problem folder? (e.g., two-sum, valid-anagram)"

### Step 3 - Write problem.md

Parse `## Problem` and `## Statement` from the active file. Write to `problems/<slug>/problem.md` with:
- Problem title and difficulty (from `**Name:**` and `**Difficulty:**`)
- Tags (from `**Tags:**`)
- Source (from `**Source:**` if present)
- Problem statement, examples, constraints (from `## Statement`)

### Step 4 - Write solution files

For each `### Approach N: [name]` block with **Status: solved**:
- Derive filename from approach name (e.g., "HashMap" -> `hashmap.cs`, "Brute Force" -> `brute-force.cs`)
- Extract code from `#### Solution` code block
- Extract time/space complexity and key idea
- Write `.cs` file with comment block header: approach name, complexity, key idea

Approaches with status "in-progress" or "stuck" are skipped.

### Step 5 - Write notes.md

Combine from the active file:
- All `#### Bugs` entries from all approaches -> "Mistakes Made" section
- `## Reflection` answers -> "Key Insights" and "Mantras" sections
- `## Patterns` entries -> "Patterns Used" section

### Step 6 - Update pattern-index.json

Build the entry from `## Patterns` and the approach-to-pattern mapping:
```json
"ProblemName": {
  "patterns": ["PatternName1", "PatternName2"],
  "approaches": {
    "approach-file.cs": ["PatternName1"],
    "other-approach.cs": ["PatternName2"]
  }
}
```

### Step 7 - Update pattern files

For each pattern in `## Patterns`:
- Add this problem to the "Solved Problems" section of `patterns/<pattern>.md`
- Add any new bugs from this session to the "Common Mistakes" section if applicable

### Step 8 - Update LESSONS.md

If any bugs or reflection mistakes warrant a lesson entry, add them to the appropriate section in LESSONS.md (Conceptual Mistakes, Code Mistakes, or Pattern Misidentifications).

### Step 9 - Delete active-problem.md

Remove the active file from repo root. The session is now fully persisted.

### Step 10 - Confirm

Show a summary of what was created and updated:
- Files created in `problems/<slug>/`
- Patterns updated
- LESSONS.md entries added (if any)

## Rules

- Ask the user to confirm the notes content before writing - do not invent reflection notes
- If a pattern file does not exist yet, create it using the template in `docs/pattern-system.md`
- Keep solution files clean and well-commented - this is a reference library
- If the active file is incomplete (e.g., no Reflection section), ask the user to fill in the gaps before saving
