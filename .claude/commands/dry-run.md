# /dry-run

Generate a step-by-step dry run for the current solution and run it in the terminal.

## What AI does

AI generates `Runner.cs` - nothing else. Two responsibilities only:

1. **Input section** - read the function signature, call the right `InputParser` method for each parameter:
   - `string` -> `InputParser.ReadString("Enter ...")`
   - `int` -> `InputParser.ReadInt("Enter ...")`
   - `int[]` -> `InputParser.ReadIntArray("Enter ...")`
   - `char[]` -> `InputParser.ReadCharArray("Enter ...")`
   - If a type is not covered by InputParser, generate the parsing inline as a fallback

2. **Algorithm section** - exact copy of the solution code, verbatim. AI adds telemetry only:
   - `DryRunLogger.Log(header)` before the loop (problem name, approach, column headers)
   - `DryRunLogger.Log(row)` after each meaningful step (end of loop body or decision point)
   - `DryRunLogger.Pause()` after each row
   - `DryRunLogger.Log(result)` + `DryRunLogger.Save()` as the very last lines

Everything else (parsing infrastructure, logging, file writes, project setup) is handled by the C# app. AI does not touch those files.

## Steps

1. Read `active-solution.cs`. Find the last `// ==== Approach N ====` block with actual implementation code (not a blank template).

2. If the user has shared updated code in the chat that is not yet in the file, say: "Your latest code isn't in active-solution.cs yet. Want me to drop it in first?" Wait for confirmation before proceeding.

3. Generate `Runner.cs` and show it in the chat. Wait for the user to confirm before writing.

4. After confirmation, write `tools/dry-run/Runner.cs` and run:
   ```bash
   cd "tools/dry-run" && dotnet run
   ```

## Rules

- Never change the algorithm logic - copy it exactly, only add telemetry calls
- Column headers are AI-decided based on what variables actually matter for this algorithm
- If the solution has a bug, instrument it as-is - the dry run exists to reveal bugs, not fix them
- Keep table rows short - abbreviate collection contents (e.g. `{2:0, 7:1}` not full ToString)
- After running, tell the user: "Press any key in the terminal to step through. Close when done."
