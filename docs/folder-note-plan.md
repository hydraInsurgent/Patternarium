# Folder Note Migration Plan

Migrate each problem folder to a single named hub file that acts as the **problem viewer** - statement front and center, knowledge links collapsible, no learning record clutter. **Phases 1-3 are purely additive - no old file is touched, no workflow is changed. Phase 4 is the only destructive step.**

---

## Philosophy

When a user clicks the folder note, they see:
- YAML metadata (problem-related)
- Title, difficulty, source
- A collapsible Knowledge Links callout (hidden by default)
- The problem statement, examples, constraints

That's it. No mistakes, no insights, no mantras in the folder note. The folder note is a problem viewer, not a learning record.

When the user wants to see solutions, they click the link to `solutions.md`. That file holds the full learning record, including mistakes inline per approach.

---

## Target Architecture (end state, after Phase 4)

```
problems/1-two-sum/
    1-two-sum.md        ← folder note: problem viewer (YAML + statement + knowledge links)
    solutions.md        ← full learning record (approaches with inline mistakes, reflection, YAML for queries)
    solutions/
        brute-force.cs
        hashmap.cs
```

`problem.md` and `notes.md` are gone. Their content distributes - statement to folder note, mistakes/insights/connections to solutions.md (or to the folder note as wikilinks).

---

## Decisions Locked Upfront

**Wikilink format:** Full path - `[[patterns/two-pointers]]`. Explicit and unambiguous.

**Tracking fields:** Live in folder note YAML frontmatter (queryable via Dataview).

**solutions.md keeps its markdown body.** It absorbs mistakes content from notes.md (per approach) plus key insights/mantras (in Reflection). The body grows; it does NOT slim to YAML-only.

**Knowledge Links section:** Wrapped in an Obsidian collapsible callout (`> [!info]-`) so it's hidden by default. The user opens it when they want to navigate to patterns/concepts/techniques.

---

## Where notes.md Content Distributes

| Section in notes.md | New home | Why |
|---|---|---|
| `## Mistakes Made` (grouped by approach) | `solutions.md` - inline `**Mistakes:**` field under each approach | Mistakes are tied to specific approaches, no links involved |
| `## Key Insights` | `solutions.md` `## Reflection` - existing `Key insight:` field | Problem-level takeaway, belongs with reflection |
| `## Mantras` | `solutions.md` `## Reflection` - new `Mantras:` field | Same reason; or drop entirely if low-value |
| `## Patterns Used` | DROP | Already in folder note Knowledge Links via wikilinks |
| `## Connected Problems` | Folder note Knowledge Links - `Related Problems` subsection as wikilinks | These ARE links - belong in the link layer |

---

## Phase 1 - Create Folder Notes (additive) ✓ DONE

**Touches existing files:** No.
**Workflow changes:** No.
**Reversible:** Yes.

Folder note structure:

```markdown
---
title: <Problem Name>
category: problem-hub
problem: <number>
slug: <slug>
status: solved | needs-revision
first-solved:
times-revised: 0
last-revised:
---

# <Problem Name>

**Difficulty:** Easy | Medium | Hard
**Source:** LeetCode #N

> [!info]- Knowledge Links
>
> ### Patterns
> - [[patterns/<pattern>]]
>
> ### Concepts
> - [[concepts/<concept>]]
>
> ### Techniques
> - [[techniques/<technique>]]
>
> ### Data Structures
> - [[data-structures/<ds>]]
>
> ### Related Problems
> _(populated in Phase 2 from Connected Problems)_

## Files
- [Statement](problem.md)
- [Solutions & Learning Journey](solutions.md)
- [Mistakes & Insights](notes.md)
```

**Done when:** all 14 folders have a folder note. Graph shows named problem nodes connected to knowledge layer. ✓

---

## Phase 2 - Distribute notes.md Content (additive)

**Touches existing files:** No (notes.md stays - content is COPIED into new locations).
**Workflow changes:** No.
**Reversible:** Yes - revert the targeted appends.

For each problem:

1. Read `notes.md` and parse its sections.
2. **Mistakes Made (per approach):** append a `**Mistakes:**` field under each matching approach in `solutions.md` markdown body. Format:
   ```markdown
   ### Approach 2: HashMap - Complement Lookup
   **Code:** ...
   **Time:** ... | **Space:** ...
   **Thinking:** ...
   **Mistakes:**
   - [bug description and root cause]
   ```
3. **Key Insights:** append to existing `## Reflection` in `solutions.md` if not already there.
4. **Mantras:** append a new `Mantras:` field in `## Reflection` of `solutions.md` if any exist. (Decision: keep mantras for review value.)
5. **Connected Problems:** populate the `### Related Problems` subsection of the folder note's Knowledge Links callout. Convert each entry to a wikilink: `[[problems/424-longest-repeating-character-replacement/424-longest-repeating-character-replacement]]`.
6. **Patterns Used:** drop entirely. Already covered by folder note Knowledge Links.

`notes.md` files stay exactly as-is. `/save-problem` continues to write to `notes.md`.

**Done when:** every problem's mistakes are inline in solutions.md, insights/mantras are in solutions.md Reflection, connected problems are wikilinks in folder note Knowledge Links.

---

## Phase 3 - Copy problem.md Content into Folder Note (additive)

**Touches existing files:** No.
**Workflow changes:** No.
**Reversible:** Yes.

Append the problem statement to the folder note above the Files section, with a clear boundary marker:

```markdown
## Statement

[copied from problem.md]

## Examples
[copied from problem.md]

## Constraints
[copied from problem.md]

---
<!-- /revise boundary - everything below this line is hidden during cold re-solve sessions -->

## Files
- [Statement](problem.md)
- [Solutions & Learning Journey](solutions.md)
- [Mistakes & Insights](notes.md)
```

`problem.md` stays exactly as-is. `/revise` continues to read `problem.md`.

**Done when:** every folder note has the statement copied in, with the boundary marker.

---

## Phase 4 - Consolidate and Clean Up (destructive)

**Touches existing files:** Yes.
**Workflow changes:** Yes.
**Reversible:** Only via git.

Sub-steps for risk management - each should be its own commit:

### 4a - Delete notes.md

After verifying every notes.md section is now in either the folder note (Connected Problems) or solutions.md (Mistakes/Insights/Mantras), delete all 14 `notes.md` files.

### 4b - Delete problem.md

After verifying every problem.md is now in the folder note (Statement/Examples/Constraints), delete all 14 `problem.md` files. Remove the `## Files` section from folder notes (links to deleted files are dead).

### 4c - Update workflows

- `/save-problem`:
  - Writes statement to folder note (between header and `/revise boundary`)
  - Writes mistakes inline per approach in `solutions.md`
  - Writes Key Insights and Mantras to `solutions.md` `## Reflection`
  - Writes Connected Problems as wikilinks to folder note Knowledge Links → Related Problems
  - Updates folder note YAML tracking on each save
  - No more `notes.md` or `problem.md` writes
- `/revise`:
  - Reads folder note up to the `/revise boundary` marker
  - Updates `times-revised`, `last-revised`, `status` in folder note YAML
- `/save-problem.md`, `/revise.md`, and `docs/active-problem-spec.md` all updated to reflect new structure

### 4d - Update workbench list files

`workbench/lists/blind-75.md` and `workbench/lists/phased-75.md` link to `problems/<slug>/problem.md`. Update to `problems/<slug>/<slug>.md`.

### 4e - Verify and rebuild

- Run `dotnet run scripts/Rebuild-Index.cs` - should succeed unchanged
- Open Obsidian graph - should show clean named nodes, no orphaned references
- Run sample Dataview queries from concept/pattern/technique files - should still return results
- Open `/revise` for one problem to confirm boundary marker works

**Done when:** every folder has only `<slug>.md` + `solutions.md` + `solutions/`. Workflows write to the new structure. Lists updated. Index rebuilt clean.

---

## Sequence and Dependencies

| Phase | Depends On | Status | Reversible |
|---|---|---|---|
| 1 | nothing | ✓ done | yes |
| 2 | Phase 1 | pending | yes |
| 3 | Phase 1 | pending | yes |
| 4 | Phases 1, 2, 3 | pending | git only |

Phases 1-3 are checkpoints. After any of them, the vault still works correctly with both old and new structure coexisting.

---

## Impact on /revise

`/revise` does not need any changes during Phases 1-3. It continues reading `problem.md` as designed.

In Phase 4c, `/revise` is updated to:
- Read the folder note instead of `problem.md`
- Stop at the `/revise boundary` marker to keep statement spoiler-free
- Update folder note YAML tracking fields (`times-revised`, `last-revised`, `status`) on each session

---

## Rollback

- After Phase 1: delete `<slug>.md` files. Original structure intact.
- After Phase 2: revert the appended content in folder notes and solutions.md. Original files untouched.
- After Phase 3: revert appended Statement/Examples/Constraints sections in folder notes. Original files untouched.
- During Phase 4: rely on git. Each sub-step (4a-4e) is a separate commit so a single bad sub-step is easy to revert.
