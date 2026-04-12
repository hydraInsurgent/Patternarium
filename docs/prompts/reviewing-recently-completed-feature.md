# Reviewing a Recently Completed Feature

A generic review prompt for auditing a newly implemented feature before it is considered ready to use. This is not a plan-vs-reality check - see `plan-vs-reality-prompt.md` for that. This is an end-to-end correctness audit: does the feature work, are there gaps, will it break anything that existed before?

Use this any time a feature has been implemented across multiple files and you want a fresh read before committing to it.

---

## Setup

Before starting, identify:

1. **The feature files** - every file that defines the feature's behavior (commands, rules, specs, templates)
2. **The data files** - every file the feature reads from or writes to (configs, indexes, generated output)
3. **The existing flow** - what the system did before this feature existed, and which parts of that flow the new feature intersects

Read every file in the surface area in full before forming any opinion. Do not skim. If a file is long, the part you skim is usually where the problem is.

---

## What to Review

Read everything first. Then perform a full audit with these goals:

### 1. End-to-end flow consistency

Trace the complete flow the feature is responsible for, from entry point to final output. For every piece of data that is extracted or generated at the start: verify it has a defined destination. For every destination field that is written at the end: verify it has a defined source. Flag any data that is extracted but never consumed, and any field that is written but has no clear source.

### 2. Field and section name consistency

Every section heading, field name, and key that one file writes must exactly match what another file reads. Check all cross-file references. Any mismatch - even a singular/plural difference, a hyphen vs. space, or a different nesting level - is a silent bug that will produce no error and wrong output.

### 3. Behavioral gaps between new and existing flows

Several behaviors were added specifically to support the new feature. For each one, check whether it also works correctly in a regular session where the new feature is not active. Is the fallback always defined? Is the behavior symmetric? Are there fields that only get populated in one kind of session but are silently missing in the other?

### 4. Assumptions that will break in practice

Look for steps that assume something that may not always be true. A step that assumes a file exists, a section is present, or a prior step always completes. Flag any assumption that has no defined fallback or error path.

### 5. Dead weight and unused pieces

Identify anything that is defined but not actually used anywhere in the active flow. Sections in templates that nothing reads. Fields in specs that no step writes. Rules that have been superseded but not removed. Sample or reference files that show a format no command currently produces. Dead weight is not always a bug today but creates maintenance confusion about what is load-bearing.

### 6. Ordering and dependency issues

Check whether the ordering of steps in the feature is correct. Is there any step that reads something a prior step was supposed to write but might not have? Is there any step that must happen before another but is not sequenced that way? Pay attention to steps that create files or state that later steps depend on.

### 7. Mechanism design

Identify the core mechanisms the feature introduces - trackers, checklists, queues, indexes, or any state the feature maintains over time. For each mechanism: is it specific enough to be actionable? Is there a defined process for keeping it current as the session progresses? Who updates it, and when? If it drifts from reality, what breaks and how visibly?

### 8. Template coverage

If the feature introduces or modifies templates, verify that all existing instances of those templates are aligned with the current definition. Check every section the template defines - does each existing file have it? Check every section an existing file has - is it in the template, or is it extra content that may break downstream processing?

### 9. First-use and continuation experience

Walk through the feature as someone would encounter it for the first time, and again as someone returning to continue work started by it. At each step, ask: what does the system need to know here, and where does it come from? Flag anything that requires context the system cannot derive from the files available - anything the user would have to re-explain, or that the system would silently assume wrong.

### 10. Scope creep and over-specification

Look for rules or steps that are more prescriptive than necessary, that handle edge cases the system will never encounter, or that add complexity without adding reliability. The right amount of specification is exactly what is needed to make behavior deterministic - no more. Flag anything that makes the system harder to maintain without making it more correct.

---

## Output Format

Organize findings by category using the headings above. For each finding:

- State what the issue is in one sentence
- Point to the specific file and section where it lives
- Say what breaks if it is not fixed, or what maintenance risk it creates
- Suggest the minimal fix

At the end, give an overall verdict: is this feature ready to use as-is, or are there blocking issues that should be fixed first? If blocking issues exist, list them in priority order.

Do not restate things that are working correctly. Only report issues, gaps, and risks.
