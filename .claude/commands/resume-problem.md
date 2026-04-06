# /resume-problem - Resume a Paused Session

Restore a paused session from `workbench/sessions/` back to the active files at repo root.

## Behavior

### Step 1 - Check for paused sessions

List all subdirectories in `workbench/sessions/` that contain an `active-problem.md` file. If none exist, tell the user: "No paused sessions found." and stop.

### Step 2 - Check active files

If `active-problem.md` at repo root is not empty, tell the user: "You have an active session in progress for [problem name]. Pause it first with `/pause-problem` before resuming another."

### Step 3 - Let user pick

If multiple paused sessions exist, list them by problem name and ask which to resume. If only one exists, confirm: "Resume [problem name]?"

### Step 4 - Restore files

- Copy `workbench/sessions/<slug>/active-problem.md` to repo root `active-problem.md`
- Copy `workbench/sessions/<slug>/active-solution.cs` to repo root `active-solution.cs`
- Delete the `workbench/sessions/<slug>/` folder

### Step 5 - Re-orient

Read the restored `active-problem.md` and give the user a brief re-orientation:
- Problem name and difficulty
- Current approach and its status
- Where the session left off (last hint level, last bug, last action)

Then ask: "Ready to continue?"

## Rules

- Never resume into a non-empty active file - always check first
- Re-orientation should be brief - one short paragraph, not a full replay
- After restoring, the session continues normally from wherever it left off
