# /save-problem - Save Problem to Repo

Save the current problem session to the repo. Creates the problem folder, solution files, notes, and updates the pattern index.

## Behavior

### Step 1 - Confirm problem name

Ask: "What should we call this problem folder? (e.g., two-sum, valid-anagram)"

### Step 2 - Create problem folder structure

Create `problems/<slug>/` with:
- `problem.md` - problem statement, constraints, examples
- One `.cs` file per approach that was explored
- `notes.md` - user's reflection and key insights

### Step 3 - Write problem.md

Include:
- Problem title and difficulty
- Problem statement (what the user pasted)
- Input/output format
- Constraints
- Example test cases

### Step 4 - Write solution files

For each approach explored:
- Use descriptive filename: `brute-force.cs`, `hashmap.cs`, `two-pointer.cs`
- Add a comment block at the top with: approach name, time complexity, space complexity, key idea
- Include clean solution code

### Step 5 - Write notes.md

Include:
- Mistakes made during the session
- Key insight from the session
- Mantras/rules derived from this problem
- Patterns used (with brief explanation)

### Step 6 - Update pattern-index.json

Add or update the entry using the structured format:
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

For each pattern used:
- Add this problem to the "Solved Problems" section of `patterns/<pattern>.md`
- Add any new insight from this session to the "Common Mistakes" section if applicable

## Rules

- Ask the user to confirm the notes content before writing - do not invent reflection notes
- If a pattern file does not exist yet, create it using the template in `docs/pattern-system.md`
- Keep solution files clean and well-commented - this is a reference library
