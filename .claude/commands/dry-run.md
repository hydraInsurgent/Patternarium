# /dry-run

Step-by-step dry run for the current solution. Two modes depending on whether code exists.

---

## Mode 1 - Conversational Dry Run (no implementation yet)

**Trigger:** `active-solution.cs` has only blank template blocks (no actual implementation), OR user asks to dry run an approach they have described in words or pseudocode.

**What AI does:**

1. Ask the user for a small concrete input (or pick one if obvious from the problem)
2. Simulate execution step by step in the conversation, narrating each decision in plain language
3. After each meaningful step or decision point, pause and ask: "What do you think happens next?"
4. Wait for the user's prediction before continuing
5. If the prediction is wrong, stop and ask: "Why did you expect that?" - do not just continue narrating
6. If the prediction is right, confirm briefly and move to the next step
7. At the end, ask: "Did the logic behave the way you expected overall?"

**Narrative style:**
Write each step as a sentence describing what the code is deciding, not just what the variables are.

Good: "I'm at index 2, character 'b'. I check if 'b' is already in the set. It is - so I have a collision."
Bad: "i=2, s[i]=b, set={a,b}, action=collision"

The goal is to make the execution feel like a story the user can follow and predict.

---

## Mode 2 - Terminal Dry Run (implementation exists)

**Trigger:** `active-solution.cs` has a real implementation in the last `// ==== Approach N ====` block.

### What AI does

AI generates `Runner.cs` only. Two responsibilities:

1. **Input section** - read the function signature, call the right `InputParser` method for each parameter:
   - `string` -> `InputParser.ReadString("Enter ...")`
   - `int` -> `InputParser.ReadInt("Enter ...")`
   - `int[]` -> `InputParser.ReadIntArray("Enter ...")`
   - `char[]` -> `InputParser.ReadCharArray("Enter ...")`
   - If a type is not covered by InputParser, generate the parsing inline as a fallback

2. **Algorithm section** - exact copy of the solution code, verbatim. AI adds narrative telemetry only (see rules below).

Everything else (parsing infrastructure, logging, file writes, project setup) is handled by the C# app. AI does not touch those files.

### Steps

1. Read `active-solution.cs`. Find the last `// ==== Approach N ====` block with actual implementation code (not a blank template).

2. If the user has shared updated code in the chat that is not yet in the file, say: "Your latest code isn't in active-solution.cs yet. Want me to drop it in first?" Wait for confirmation before proceeding.

3. Generate `Runner.cs` and show it in the chat. Wait for the user to confirm before writing.

4. After confirmation, write `tools/dry-run/Runner.cs` and give the user this command to run themselves:
   ```bash
   cd "tools/dry-run" && dotnet run
   ```
   Do NOT run this command yourself. The terminal must be interactive - the user steps through it with key presses.

5. After the user says they are done, read the `#### Dry Run` section saved to `active-problem.md` and give a one-paragraph interpretation: where the flow diverged from expected behavior and what that points to. Then ask: "Can you see where it goes wrong?"

### Telemetry Rules

**Narrative format - not tabular.** Every `DryRunLogger.Log()` call must describe a decision or phase transition, not dump variable values into a row.

**Phase labels:** Each meaningful stage of execution gets a short bracketed tag.

Common tags (adapt to the algorithm - these are examples, not a fixed list):
- `[ENTER]` - entering a function or loop body
- `[CHECK]` - evaluating a condition
- `[MATCH]` / `[NO-MATCH]` - result of a comparison
- `[EXPAND]` / `[SHRINK]` / `[MOVE]` - pointer or window movement
- `[UPDATE]` - a tracked variable changed (max, count, result)
- `[SKIP]` - a branch was not taken, with the reason why
- `[RETURN]` - what is being returned and why
- `[BASE]` - a base case or early exit fired

**Indentation reflects call depth:**
- Outer function or main loop: no indent - `DryRunLogger.Log("[TAG] ...")`
- Helper function called from outer: 2-space indent - `DryRunLogger.Log("  [TAG] ...")`
- Nested helper or inner block: 4-space indent - `DryRunLogger.Log("    [TAG] ...")`

**Each log line must include the variable values that explain the decision:**

Good: `DryRunLogger.Log($"[CHECK] s[{left}]='{s[left]}' vs s[{right}]='{s[right]}' → match");`
Good: `DryRunLogger.Log($"[SKIP] oddLength={oddLength} did not beat maxLength={maxLength}");`
Bad: `DryRunLogger.Log($"{i,-4} {s[i],-6} {left,-6} {right,-5}");`

**Pause after each meaningful decision**, not after every line:
- Pause after `[MATCH]` / `[NO-MATCH]`
- Pause after `[UPDATE]` or `[SKIP]`
- Pause after `[RETURN]`
- Do not pause after every `[MOVE]` if moves are rapid and mechanical

**Header block** at the top (before input prompt):
```csharp
DryRunLogger.Log("=== Dry Run: [Problem Name] - [Approach Name] ===");
DryRunLogger.Log("Each step shows what the code decided and why.");
DryRunLogger.Log("Press any key to advance.");
DryRunLogger.Log();
```

**Result block** at the end:
```csharp
DryRunLogger.Log();
DryRunLogger.Log($"[RESULT] {result}");
DryRunLogger.Save();
```

### Other Rules

- Never change the algorithm logic - copy it exactly, only add telemetry calls
- If the solution has a bug, instrument it as-is - the dry run exists to reveal bugs, not fix them
- Abbreviate collection contents in logs (`{2:0, 7:1}` not full ToString output)
- After running, tell the user: "Press any key in the terminal to step through. Close when done."
