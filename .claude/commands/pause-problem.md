# /pause-problem - Pause Active Session

Stash the current active session into `workbench/sessions/` so a new problem can be started. The session is preserved exactly as-is and can be resumed later.

## Behavior

### Step 1 - Check active files

Read `active-problem.md` from repo root. If it is empty or missing, tell the user: "No active session to pause." and stop.

### Step 2 - Derive session slug

Extract the problem name and number from `## Problem` in `active-problem.md`. Derive a kebab-case slug using the same convention as `/save-problem` (e.g., `5-longest-palindromic-substring`).

### Step 3 - Stash files

- Create `workbench/sessions/<slug>/`
- Copy `active-problem.md` to `workbench/sessions/<slug>/active-problem.md`
- Copy `active-solution.cs` to `workbench/sessions/<slug>/active-solution.cs`

### Step 4 - Clear active files

Clear both `active-problem.md` and `active-solution.cs` at repo root to empty.

### Step 5 - Confirm

Tell the user: "Session paused. Saved to `workbench/sessions/<slug>/`. Active files are clear - ready for a new problem."

## Rules

- Never modify the stashed files - copy exactly as-is, including all in-progress content
- If `workbench/sessions/<slug>/` already exists, ask the user before overwriting: "A paused session for [name] already exists. Overwrite it?"
