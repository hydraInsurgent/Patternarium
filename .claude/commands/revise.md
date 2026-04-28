# /revise - Cold Re-Solve a Saved Problem

Revisit a problem you have already solved. Read the problem fresh, work to a solution without looking at notes, then compare against what was saved and update the vault.

This is not a quiz. It is a full solve session that ends with a comparison and knowledge update.

## Step 1 - Pick a problem

If the user runs `/revise` without arguments:
- Read `master-index.json`, list all solved problems with number, name, difficulty, and patterns
- Ask: "Which problem would you like to revise?"

If the user specifies a problem (e.g., `/revise 121` or `/revise two-pointers`):
- Match by number or pattern name
- If multiple problems match a pattern, list them and ask which one

## Step 2 - Load the problem

- Read the folder note: `problems/<slug>/<slug>.md`
- **Stop at the `<!-- /revise boundary -->` HTML comment** - everything below that line is hidden during cold re-solve (Knowledge Links, etc.)
- Above the boundary you'll find: title, difficulty, source, ## Statement, ## Examples, ## Constraints (and any extra problem-specific sections like Key Observations).
- Do NOT read `solutions.md` or any `.cs` files
- Present the problem statement and constraints cleanly
- Do not mention patterns, techniques, or anything from the saved session
- Ask: "How would you approach this?"

## Step 3 - Run the full session

Follow the normal workflow from Mode 2 onward:
- Explore thinking, guide with hints if needed, support implementation, debug
- Treat this exactly like a new problem session
- Use `active-problem.md` and `active-solution.cs` as the working files

One difference from a new session: **do not run `/save-problem` automatically at the end**. The compare phase decides whether anything needs saving.

## Step 4 - Compare phase

Triggered when the user reaches a working solution and completes reflection.

Read the saved files now:
- `problems/<slug>/solutions.md` - approaches with mistakes inline, patterns, techniques, reflection (insights and mantras are part of Reflection)
- `problems/<slug>/<slug>.md` - the full folder note (you can read everything now, including below the boundary - Knowledge Links and Related Problems)

Show a structured comparison:

```
Revision complete: [Problem Name]

Patterns this time:     [list]
Patterns last time:     [list from solutions.md]
New / missed:           [diff]

Techniques this time:   [list]
Techniques last time:   [list from solutions.md]
New / missed:           [diff]

Approaches:
- Reproduced:  [approaches that match saved ones]
- New:         [approaches not in the saved file]
- Not reached: [saved approaches that were not explored this time]

Hints needed:   [how many, vs first session if known]
Reflection gap: [anything said in reflection this time that differs from saved reflection]
```

Then ask: "What do you want to do with this?"

## Step 5 - Update artifacts

Based on the comparison and user's answer:

**If a new insight emerged:**
- Update `problems/<slug>/solutions.md` `## Reflection` - add to `**Notes Insights:**` or as a session-specific field

**If a LESSONS.md mistake was not repeated:**
- Move that entry to `## Graduated` in `LESSONS.md`
- Note it in the compare output: "Graduated: [mistake name]"

**If a mistake was repeated or a new gap appeared:**
- Add or reinforce in `LESSONS.md`

**If a new or better approach was found:**
- Ask: "Want to save this approach?"
- If yes: run through the normal save flow for that approach only (add to `solutions.md`, write `.cs` file, rebuild index)

**If nothing new emerged:**
- No file changes needed
- Confirm: "Clean revision - nothing new to update."

## Step 6 - Update folder note tracking

After updates are done, update the folder note YAML frontmatter:
- Increment `times-revised` by 1
- Set `last-revised` to today's date (YYYY-MM-DD)
- Update `status` if pattern recall improved significantly (e.g., from `needs-revision` back to `solved`)

## Step 7 - Clear active files

Ask: "Ready to clear the active files?"
If yes, clear both `active-problem.md` and `active-solution.cs` to empty.

## Rules

- Never load saved solutions before the compare phase. The folder note above the `<!-- /revise boundary -->` marker is OK to read - that's the statement. Below the boundary (Knowledge Links, Related Problems) is OFF-LIMITS until compare phase.
- Do not tell the user which pattern to use - let them discover it
- The compare phase is always shown, even if nothing changed
- LESSONS.md graduation requires the mistake to have been genuinely not repeated - not just not triggered by this problem
- If the user says "just show me the solution" mid-session, treat it the same as a normal session (Mode 6 reveal, not a skip to compare)
