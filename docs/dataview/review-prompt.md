# End-to-End Review Prompt - Dataview Refactor

This prompt is for a fresh review session after the Dataview refactor is complete. The refactor is done and sitting on the `dataview-refactor` branch. The goal is to verify it before merging to main.

Do not trust the plan's description of what was done. Read the actual files. Your job is to find what we could not find from inside the work.

---

## What Was Done

A full system migration of the Patternarium DSA learning system:

1. **Replaced AI-maintained derived views** with Dataview queries: pattern "Solved Problems", data structure "Seen In", construct "Seen In", concept "Seen In", algorithm "Seen In" - all now auto-rendered from problem frontmatter
2. **Promoted pattern-index.json to master-index.json** with enriched schema: constructs, ds-used, algorithms, ds-notes, lists, reverse-lookup indexes (by-pattern, by-ds, by-construct, by-algorithm), `_saving` marker
3. **Added YAML frontmatter** to all file types that lacked it: patterns, concepts, algorithm files
4. **Added `slug` field** to data structures, constructs, concepts, algorithms - canonical ID linking file identity to problem frontmatter arrays and Dataview queries
5. **Extended problem frontmatter** with: number, slug, ds-used, patterns, constructs, algorithms, tags (YAML arrays, not inline `tags ::`)
6. **Simplified /save-problem** from ~15 file operations to ~9-11; removed Seen In appends, Solved Problems appends, goals.md updates
7. **Algorithm system fully integrated** for the first time: YAML frontmatter, Dataview Seen In queries, `algorithms` field in problem frontmatter and master-index.json, `by-algorithm` reverse-lookup
8. **Trimmed toolkit.md** to remove content duplicated in save-problem.md

---

## The Slug Chain - The Single Most Important Thing to Verify

The entire Dataview query system depends on a four-link chain being unbroken for every file type:

```
File's own slug/display_name field
    = value in problem frontmatter array
    = value in master-index.json entry
    = WHERE clause in the file's Seen In / Solved Problems query
```

If any link breaks, the query returns no results silently. No error. Just empty.

| File type | Slug field | Problem frontmatter field | Query WHERE |
|-----------|-----------|--------------------------|-------------|
| Data structure | `slug` (= filename) | `ds-used` | `WHERE ds = "slug"` |
| Construct | `slug` (explicit) | `constructs` | `WHERE construct = "slug"` |
| Concept | `slug` (= primary tag) | `tags` | `WHERE tag = "slug"` |
| Pattern | `display_name` | `patterns` | `WHERE pattern = "display_name"` |
| Algorithm | `slug` (= filename) | `algorithms` | `WHERE algo = "slug"` |

**Verify this chain end-to-end for at least one file of each type.**

---

## Review Sections

Work through each section completely before moving to the next. For each failure found, record it in the Failure Log at the end.

### Section 1 - Slug Chain Integrity

For each file type, pick a real file and trace the full chain:

1. Read the file's frontmatter - note the slug (or display_name for patterns)
2. Find a problem that uses it - read that problem's frontmatter - confirm the field contains the exact same value
3. Check master-index.json - confirm the same value appears in the right field of that problem's entry
4. Read the Seen In / Solved Problems query in the file - confirm the WHERE clause uses the exact same value

**Do this for:** `data-structures/array.md`, `constructs/collections/dictionary.md`, `concepts/palindrome.md`, `patterns/hashmap.md`, `algorithms/quicksort.md`

Then verify two edge cases:
- `constructs/sorting/array-sort-custom-comparer.md` - slug differs from filename (`slug: array-sort`). Does every link in the chain use `array-sort`, not `array-sort-custom-comparer`?
- `concepts/xor.md` - the slug is `xor` but problems also have `xor-cancellation` in tags. Does the query still return results correctly for problems that have both tags?

### Section 2 - master-index.json Integrity

Read `master-index.json` in full.

**Schema completeness:** Every problem entry must have: `title`, `slug`, `difficulty`, `patterns`, `constructs`, `algorithms`, `ds-used`, `ds-notes`, `lists`, `approaches`. Flag any missing fields.

**Reverse-lookup index accuracy:** The `by-pattern`, `by-ds`, `by-construct`, `by-algorithm` indexes must be derivable from the problem entries. Spot-check 3 entries:
- Pick a pattern (e.g., "HashMap") - list all problem numbers where `patterns` includes "HashMap" - compare to `by-pattern["HashMap"]`
- Pick a construct (e.g., "dictionary") - same check
- Pick a DS (e.g., "array") - same check

**`_saving` field:** Must be `null`. If it is non-null, a previous save was interrupted.

**Approach-level data:** Each approach in `approaches` should have `patterns` and `ds-used`. Most should have `variation`. Flag any approach that has `patterns: []` but is not a brute-force approach.

### Section 3 - Problem Frontmatter Completeness

Read the frontmatter of all 12 problem files. Every file must have these fields in order: `title`, `number`, `slug`, `category`, `difficulty`, `source`, `status`, `lists`, `ds-used`, `patterns`, `constructs`, `algorithms`, `tags`.

Check:
- No file has a `tags ::` line remaining at the bottom (old inline format)
- `algorithms` field exists on every file (even if `[]`)
- `tags` is a YAML array, not a comma-separated string
- `slug` matches the folder name (e.g., folder `1-two-sum` → `slug: two-sum`)
- `number` matches the folder number

### Section 4 - Dataview Query Correctness

Read every file that contains a Dataview query (DS files, construct files, concept files, pattern files, algorithm files). For each query verify:

- `FROM "problems"` - correct source
- FLATTEN variable matches the field name (`ds-used AS ds`, `patterns AS pattern`, `constructs AS construct`, `tags AS tag`, `algorithms AS algo`)
- WHERE variable matches the FLATTEN alias (e.g., `WHERE ds =` not `WHERE ds-used =`)
- WHERE value is a string literal matching the file's own slug or display_name exactly
- SORT is `number asc`
- No query still says `WHERE pattern = "HashMap"` in a file that isn't the HashMap pattern

**Specific checks:**
- DS files: query uses `FLATTEN ds-used AS ds` and `WHERE ds = "<slug>"`; TABLE includes `patterns` column
- Construct files: query uses `FLATTEN constructs AS construct` and `WHERE construct = "<slug>"`; TABLE does NOT include `patterns` column
- Concept files: query uses `FLATTEN tags AS tag` and `WHERE tag = "<slug>"`
- Pattern files: query uses `FLATTEN patterns AS pattern` and `WHERE pattern = "<display_name>"`
- Algorithm files: query uses `FLATTEN algorithms AS algo` and `WHERE algo = "<slug>"`

### Section 5 - Pattern File Structure

Read 3 pattern files: `patterns/hashmap.md`, `patterns/two-pointers.md`, `patterns/odd-one-out.md`.

Each file must have:
- YAML frontmatter with: `name`, `display_name`, `category`, `variations` (list with name + ds), `ds-primary`
- H1 heading matching `display_name`
- No `**display_name:**` bold line in the body (transition scaffolding should be gone)
- `## Core Idea` section
- At least one `## Variation:` section with When/Mental Trigger/Template/Tradeoffs
- `## Common Mistakes` section
- `## Solved Problems` section with Dataview query at the bottom

The `display_name` in frontmatter must exactly match the `WHERE pattern = "..."` value in the Solved Problems query.

### Section 6 - save-problem.md Completeness

Read `.claude/commands/save-problem.md` in full.

Walk through a hypothetical save of a problem that uses: one pattern (with a new variation), one construct, one algorithm, two data structures, one list.

**Verify each step produces complete output:**
- Step 3: frontmatter includes all 13 fields (including `algorithms`)
- Step 7: master-index.json entry includes `algorithms` field; reverse-lookup update mentions `by-algorithm`
- Step 8: DS coverage table update - what happens if the pattern link doesn't exist yet?
- Step 9: list file update - what if the `lists` field is `[]`?
- Step 11: summary and cleanup

**Check the Rules section:** Are there rules covering: new pattern file creation, new variation in existing pattern, new algorithm file creation, new construct file creation? Are any of these missing or contradictory?

### Section 7 - toolkit.md / save-problem.md Overlap Audit

Read the Pattern Library Integration section of `.claude/rules/toolkit.md`.

List everything in toolkit.md that is also in save-problem.md. List everything in toolkit.md that is NOT in save-problem.md (these are session-time behaviors - they should stay). List anything in save-problem.md that should be in toolkit.md instead.

The split should be clean: toolkit.md = session behaviors, save-problem.md = save-time procedure.

**Specific checks:**
- Is algorithm session logging in toolkit.md (it should be)?
- Is "new variation in existing pattern file" covered somewhere (it should be in save-problem.md Rules)?
- Is construct/concept/algorithm file creation at save time covered somewhere (toolkit.md, save-problem.md, or both)?

### Section 8 - Template Accuracy in pattern-system.md

Read `docs/pattern-system.md`. For each of the 5 file-type templates (pattern, problem, data structure, construct, concept) and the algorithm template:

Compare the template against a real live file of that type. List every discrepancy:
- Fields in template but not in real file
- Fields in real file but not in template
- Structural differences (section order, heading levels)
- Query format differences

The templates are the source of truth for future file creation. If they don't match reality, new files will be created wrong.

### Section 9 - Stale Content Sweep

Run these checks and report results:

1. `grep -r "pattern-index.json"` across all `.md` files - expect exactly 2 results: `docs/vision.md` and `docs/dataview/refactor-plan.md`
2. `grep -r "tags ::"` across `problems/` - expect zero results
3. `grep -r "append to.*Seen In"` across all `.md` files - expect zero results (all should say "Dataview query")
4. `grep -r "algorithms-index.json"` - expect zero results
5. `grep -r "\*\*display_name:\*\*"` in `patterns/` - expect zero results (transition scaffolding gone)
6. Check all 13 data structure files have `slug` in frontmatter
7. Check all 7 construct files have `slug` in frontmatter
8. Check both concept files have full YAML frontmatter (name, slug, display_name, category, tags)
9. Check all 3 algorithm files have full YAML frontmatter (name, slug, category, tags, time, space, related)

### Section 10 - End State Validation

Answer these questions directly, reading files as needed:

1. **If I solve a new problem tomorrow using the HashMap pattern with a new variation "Last Occurrence Index" and implement QuickSort from scratch** - walk through exactly what happens at /save-problem time. Which files get written? What goes in each? Does anything get missed?

2. **If I open `patterns/hashmap.md` in VS Code with the Dataview extension** - what do I see in the Solved Problems section? Name the exact problems that should appear.

3. **If I open `data-structures/array.md`** - what do I see in the Seen In section? Name the exact problems that should appear.

4. **What happens when a problem uses a construct that has no file yet?** Walk through the exact steps.

5. **What is the total number of file operations in /save-problem now vs before?** Confirm the claimed reduction.

---

## Failure Log

Record every failure found using this format:

```
FAILURE #N
Section: [which section]
Severity: BREAKING | WRONG | STALE | MISSING
File: [file path]
What: [one sentence]
Evidence: [exact quote or field value that proves it]
Fix: [what needs to change]
```

Severity definitions:
- **BREAKING** - system fails silently or produces wrong data (e.g., query returns empty because slug mismatch)
- **WRONG** - incorrect content that will mislead future behavior (e.g., template doesn't match real format)
- **STALE** - outdated reference or instruction that no longer applies
- **MISSING** - something that should exist but doesn't

---

## Confidence Verdict

After completing all sections, give a verdict in this format:

```
CONFIDENCE: HIGH / MEDIUM / LOW

Failures by severity:
- BREAKING: N
- WRONG: N
- STALE: N
- MISSING: N

Ready to merge: YES / NO / YES WITH CONDITIONS

Conditions (if any):
[list]

Biggest risk remaining:
[one sentence]
```
