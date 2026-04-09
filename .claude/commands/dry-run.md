# /dry-run

Step-by-step dry run for the current solution. Three modes: one conversational, two code-instrumented.

When `/dry-run` is called and implementation code exists, ask:
> "Console dry run or terminal dry run?"
> - **Console** - adds `Console.WriteLine` logs directly into your code in `active-solution.cs`
> - **Terminal** - generates a separate `Runner.cs` with step-through telemetry

If no implementation exists (only blank templates or pseudocode), go straight to Conversational Dry Run.

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

## Mode 2 - Console Dry Run

**Trigger:** User chooses "console" when asked, OR user says "add logs", "add console logs", etc.

**What AI does:**

Add commented-out `Console.WriteLine` lines directly into the user's code in `active-solution.cs`. The user uncomments whichever logs they want and runs the code themselves.

### Log Style

**Narrative format with phase labels.** Each log describes a decision or state change, not just a variable dump.

Phase tags (adapt to the algorithm - these are examples, not a fixed list):
- `[ENTER]` - entering a function or loop body
- `[CHECK]` - evaluating a condition
- `[MATCH]` / `[NO-MATCH]` - result of a comparison
- `[EXPAND]` / `[SHRINK]` / `[MOVE]` - pointer or window movement
- `[UPDATE]` - a tracked variable changed (max, count, result)
- `[SKIP]` - a branch was not taken, with the reason why
- `[RETURN]` - what is being returned and why
- `[BASE]` - a base case or early exit fired
- `[RESULT]` - final output

**Indentation reflects nesting:**
- Outer loop / main flow: `//Console.WriteLine($"[TAG] ...");`
- Inner block / nested logic: `//Console.WriteLine($"  [TAG] ...");`

**Each log line must include the variable values that explain the decision:**

Good: `//Console.WriteLine($"  [CHECK] Building left[{i}]: taking left[{i-1}]={left[i-1]} and multiplying by nums[{i}]={nums[i]}");`
Bad: `//Console.WriteLine($"i={i} left={left[i-1]} nums={nums[i]}");`

**Show progressive array state** where useful:
`//Console.WriteLine($"  left so far:  [{string.Join(", ", left.Take(i+1))}]");`

### Rules

- **Do not touch the user's code** - no reformatting, no renaming variables, no changing spacing or braces, no extracting into temp variables. The existing code lines must remain byte-identical. Only insert new commented-out log lines between them.
- All logs are commented out by default
- Place logs at every decision point: before/after conditions, after assignments, at loop boundaries, at return
- Never change the algorithm logic - only add log lines
- If the solution has a bug, instrument it as-is - the dry run exists to reveal bugs, not fix them

---

## Mode 3 - Terminal Dry Run

**Trigger:** User chooses "terminal" when asked.

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

2. If the user has shared updated code in the chat that is not yet in the file, copy it into `active-solution.cs` first. Do not ask - just do it.

3. Write `tools/dry-run/Runner.cs` directly. Do not print the file contents in chat - the user can `git diff` to review. Just confirm it was written and give the command:
   ```bash
   cd "tools/dry-run" && dotnet run
   ```
   Do NOT run this command yourself. The terminal must be interactive - the user steps through it with key presses.

4. After the user says they are done, read the `#### Dry Run` section saved to `active-problem.md` and give a one-paragraph interpretation: where the flow diverged from expected behavior and what that points to. Then ask: "Can you see where it goes wrong?"

### Telemetry Rules

**Narrative format - not tabular.** Every `DryRunLogger.Log()` call must describe a decision or phase transition, not dump variable values into a row.

**Phase labels:** Each meaningful stage of execution gets a short bracketed tag (same tags as Console Dry Run above).

**Indentation reflects call depth:**
- Outer function or main loop: no indent - `DryRunLogger.Log("[TAG] ...")`
- Helper function called from outer: 2-space indent - `DryRunLogger.Log("  [TAG] ...")`
- Nested helper or inner block: 4-space indent - `DryRunLogger.Log("    [TAG] ...")`

**Each log line must include the variable values that explain the decision:**

Good: `DryRunLogger.Log($"[CHECK] s[{left}]='{s[left]}' vs s[{right}]='{s[right]}' - match");`
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
