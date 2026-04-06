# Dry Run

A dry run is a step-by-step simulation of code or an approach using a concrete input. It is the most reliable way to catch bugs in your logic before or after writing code.

## Two Modes

### Mode 1 - Conversational (before or without code)

Use this when you have an approach in mind but have not written code yet, or when you want to test your thinking on pseudocode.

1. Pick a small concrete input
2. Narrate what your approach would do at each step, in plain language
3. After each step, verify: did it do what you expected?
4. If not - that is where your logic breaks

AI will simulate this with you in conversation, pausing after each step to ask what you predict happens next.

### Mode 2 - Terminal (code exists)

Use this when you have a real implementation and want to trace its actual execution.

AI instruments your code with narrative log statements and runs it via the `tools/dry-run` console app. You step through execution one decision at a time in the terminal.

---

## Narrative Style

Both modes use the same format: describe decisions and phase transitions, not just variable values.

**Good:** "I'm at index 2. I check if 'b' is already in the set. It is - collision. I move start forward."
**Bad:** "i=2, s[i]=b, set={a,b,c}, action=collision"

The goal is to make the execution readable as a story. When something goes wrong, you can point to the exact sentence where the story stopped making sense.

### Phase Labels (for terminal mode)

Each `DryRunLogger.Log()` call uses a short bracketed tag to mark what is happening:

| Tag | Meaning |
|-----|---------|
| `[ENTER]` | Entering a function or loop body |
| `[CHECK]` | Evaluating a condition |
| `[MATCH]` / `[NO-MATCH]` | Result of a comparison |
| `[EXPAND]` / `[SHRINK]` / `[MOVE]` | Pointer or window movement |
| `[UPDATE]` | A tracked variable changed |
| `[SKIP]` | A branch was not taken, and why |
| `[RETURN]` | What is being returned and why |
| `[BASE]` | A base case or early exit fired |

Indentation reflects call depth: outer function has no indent, helper functions indent by 2 spaces.

---

## Example: Expand Around Center (Longest Palindromic Substring)

Input: `"babad"`

```
=== Dry Run: Longest Palindromic Substring - Expand Around Center ===

[BASE] Length > 1, continuing.

[ENTER] i=0, center='b', starting odd expansion from (0,0)
  [ENTER] left=0, right=0
  [CHECK] left=0 is at boundary - cannot expand further
  [RETURN] palindrome (0,0) = "b", length=1
[SKIP] oddLength=1 did not beat maxLength=1

[ENTER] i=1, center='a', starting odd expansion from (1,1)
  [ENTER] left=1, right=1
  [CHECK] s[0]='b' vs s[2]='b' → match
  [EXPAND] left=0, right=2
  [CHECK] left=0 is at boundary - cannot expand further
  [RETURN] palindrome (0,2) = "bab", length=3
[UPDATE] new max: length=3, start=0

[ENTER] i=1, starting even expansion from (1,2)
  [ENTER] left=1, right=2
  [CHECK] s[1]='a' vs s[2]='b' → no match
  [RETURN] palindrome (1,2) but mismatch - correcting to (2,1)
[SKIP] evenLength=0 did not beat maxLength=3

...

[RESULT] "bab" (start=0, length=3)
```

---

## When to Dry Run

- Before writing code: verify your approach handles the example correctly
- When code produces wrong output: find exactly where the behavior diverges from expected
- When you are unsure about an edge case: trace it explicitly

## Tips

- Use the smallest input that exercises the interesting logic
- Pick an input where you already know the expected output
- If the bug is not visible on a normal input, try an edge case (empty, single element, all same characters)
