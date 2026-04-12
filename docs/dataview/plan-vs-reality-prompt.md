# Plan vs Reality Review Prompt

A generic review prompt for checking whether a completed implementation matches its plan. Use this any time a system change has been executed and needs verification before it is considered done.

---

## Setup

Before starting, identify:

1. **The plan document** - the file that described what would be done
2. **The affected files** - everything the plan said it would touch
3. **The branch or commit** - where the changes live

Read the plan document first. Do not trust its summary of what was done - verify against the actual files.

---

## Review Pass 1 - Plan Completeness

Read the plan document and extract every discrete action it committed to. For each action:

- Was it done? (read the actual file to confirm)
- Was it done correctly? (does the result match what the plan described)
- Was it done completely? (no partial edits, no half-migrated files)

Flag anything that is partially done as more dangerous than not done at all - partial states are the hardest to detect and fix later.

---

## Review Pass 2 - Reality Completeness

Read the affected files and look for things that exist in reality but are not accounted for in the plan. Ask:

- Are there files that were modified but not listed in the plan?
- Are there files that should have been modified but weren't?
- Are there side effects of the changes that the plan did not anticipate?
- Does the current state of any file contradict the plan's stated end state?

---

## Review Pass 3 - Cross-File Consistency

For any change that touches multiple files that need to stay in sync, verify the sync is intact. This is where most failures hide.

For each "chain" of dependent values (e.g., a field defined in File A, used in File B, queried in File C):

1. Read File A - note the source value
2. Read File B - confirm it uses the exact same value
3. Read File C - confirm it references the exact same value
4. If any link differs by even one character, flag it as a potential silent failure

Pay extra attention to: renamed fields, slug/key changes, template updates that didn't propagate to existing files.

---

## Review Pass 4 - Instruction Accuracy

Read every file that tells the AI (or a human) how to do something - instructions, templates, rules, commands. For each instruction:

- Is it still accurate given the changes that were made?
- Does it describe the current file format, not the old one?
- Does it reference files, fields, or steps that still exist?
- Would following it produce a correct result today?

Stale instructions are often worse than no instructions - they produce confident wrong behavior.

---

## Review Pass 5 - Regression Check

Identify what existed and worked before the changes. Confirm it still works:

- Existing data: is it still readable and correctly structured?
- Existing workflows: do they still produce the right output?
- Existing references: do links, file paths, and field names still resolve?

A successful migration preserves all prior functionality. Flag anything that worked before and is now broken or changed in an unintended way.

---

## Findings Format

For each issue found:

```
ISSUE #N
Pass: [1-5]
Severity: BREAKING | WRONG | STALE | MISSING
Location: [file path and section]
What: [one sentence describing the problem]
Evidence: [exact quote, field value, or comparison that proves it]
Fix: [what needs to change to resolve it]
```

Severity:
- **BREAKING** - produces wrong output silently, or causes a workflow to fail
- **WRONG** - incorrect content that will cause future mistakes if followed
- **STALE** - refers to something that no longer exists or no longer applies
- **MISSING** - something the plan committed to that isn't there

---

## Verdict

```
Issues found:
- BREAKING: N
- WRONG: N
- STALE: N
- MISSING: N

Plan was followed: FULLY / MOSTLY / PARTIALLY / NOT

Ready to close: YES / NO / YES WITH CONDITIONS

Conditions (if any):
[list]

Biggest risk if closed now:
[one sentence]
```
