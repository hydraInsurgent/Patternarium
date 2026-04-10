# Dataview Refactor Plan

## Problem

The Patternarium system does not scale. Two root causes:

1. **Fan-out writes:** `/save-problem` touches 12-17 files per problem. About half are derived views that duplicate facts already stored elsewhere (Seen In, Coverage tables, list status rows, goals percentages). This is expensive in tokens and error-prone.

2. **Inconsistent YAML frontmatter:** Files across the repo have incomplete or inconsistent frontmatter. Some file types lack it entirely. This makes it impossible to query across files programmatically.

This refactor addresses both: restructure every file type with proper YAML frontmatter, build centralized indexes, and replace derived views with Dataview queries where feasible. The result is a system where AI only writes source-of-truth data, and everything else is either rendered live or updated with simple link-based matching.

For the broader vision behind this refactor - why scalability matters, the design principles, and how this maps to a future product architecture - see [docs/vision.md](../vision.md).

### Current /save-problem file operations

| Step | Files written | What it writes | Source of truth? |
|------|--------------|----------------|-----------------|
| 3 - problem.md | 1 | Statement, frontmatter | Yes - problem metadata |
| 4 - solution .cs files | 1-3 | Prettified code | Yes - solution code |
| 5 - solutions.md | 1 | Approaches, patterns, reflection | Yes - approach narrative |
| 6 - notes.md | 1 | Bugs, insights, mantras | Yes - learning notes |
| 7 - pattern-index.json | 1 | Problem -> pattern mapping | Yes - cross-reference index |
| 8 - pattern files | 1-2 | Append to Solved Problems, Common Mistakes | **Derived** - from pattern-index.json |
| 9 - list files | 1-2 | Update row status | Partially derived - status is derived, roster is curated |
| 10 - LESSONS.md | 0-1 | New lesson entries | Yes - learning log |
| DS coverage tables | 1-3 | Update coverage rows, recalc % | Partially derived - rows are curated, status is derived |
| goals.md | 1 | Update DS coverage % | **Derived** - from data-structures coverage |
| construct files | 1-2 | Append to Seen In | **Derived** - from active-problem.md constructs |
| concept files | 0-1 | Append to Seen In | **Derived** - from session |

**Total: ~15 file operations. About 5-6 are fully derived views that repeat facts already stored elsewhere.**

---

## Scope

This is a full system migration. The work splits into two independent tracks:

**Track A - Data migration** (independent of Dataview extension capabilities):
1. Promote pattern-index.json to master-index.json with enriched schema
2. Backfill YAML frontmatter across all file types
3. Establish cache rules and ownership boundaries
4. Simplify coverage table matching to link-based

**Track B - Query replacement** (depends on Phase 1 Dataview findings):
1. Design and test Dataview queries for each derived view
2. Replace Seen In sections with queries
3. Replace pattern Solved Problems with queries
4. Replace goals.md coverage table with a query

Track A can begin immediately. Track B waits for Phase 1 to confirm extension capabilities.

### File types to migrate (in order)

| File type | Count | Has YAML? | Needs migration? |
|-----------|-------|-----------|-----------------|
| `master-index.json` | 1 | N/A | Promote from pattern-index.json - add constructs, ds-used, ds-notes, lists, slug, reverse lookups |
| `problems/*/problem.md` | 12 | Yes - but inconsistent fields | Yes - add slug, ds-used, patterns, constructs, tags |
| `patterns/*.md` | 11 | **No** - uses inline `display_name:` | Yes - add full YAML frontmatter with variation-level DS |
| `data-structures/*.md` | 13 | Yes - name, status, progress | Yes - extend; coverage table stays manual (Layer 2) |
| `constructs/**/*.md` | 7 | Yes - name, category, tags, language, related | Minimal - already well-structured |
| `concepts/*.md` | 2 | **No** | Yes - add YAML frontmatter |
| `workbench/lists/*.md` | 2+ | **No** | Roster stays manual (Layer 2); `/save-problem` still updates status |
| `workbench/goals.md` | 1 | **No** | Coverage % table becomes Dataview query |

---

## Solution: Three-Layer Architecture

### Layer 1 - AI writes (source of truth)

These are the canonical data stores. AI writes them during /save-problem.

| File | What it stores | When written |
|------|---------------|-------------|
| `master-index.json` | All cross-references per problem + reverse-lookup indexes | Every /save-problem |
| Problem YAML frontmatter | difficulty, lists, status, slug, ds-used, patterns, constructs, tags | Every /save-problem |
| Pattern/construct/DS YAML frontmatter | stable metadata (display_name, category, variations, etc.) | On file creation or new variation |

#### Ownership rules (cache policy)

Cross-reference fields are duplicated between master-index.json and problem frontmatter for different consumers (AI reads the index, Dataview reads frontmatter). Ownership determines which copy is authoritative if they ever drift:

| Field | Authority | Also appears in | Consumer |
|-------|-----------|----------------|----------|
| `title` | problem frontmatter | master-index.json | AI quick-access |
| `difficulty` | problem frontmatter | master-index.json | AI quick-access |
| `status` | problem frontmatter | - | Dataview queries |
| `lists` | problem frontmatter | master-index.json | Both |
| `slug` | problem frontmatter | master-index.json | Both |
| `tags` | problem frontmatter | - | Dataview queries |
| `patterns` | master-index.json | problem frontmatter | Both |
| `ds-used` | master-index.json | problem frontmatter | Both |
| `constructs` | master-index.json | problem frontmatter | Both |
| `approaches` | master-index.json | - | AI only |
| `ds-notes` | master-index.json | - | AI only |

**Rule:** `/save-problem` writes both locations in the same pass, so drift should not happen. If manual correction is needed, edit the authority and re-sync the copy.

#### master-index.json (replaces pattern-index.json)

One index to rule them all. Consolidates pattern-index.json and construct cross-references into a single file.

```json
{
  "_saving": null,
  "problems": {
    "121": {
      "title": "Best Time to Buy and Sell Stock",
      "slug": "best-time-to-buy-and-sell-stock",
      "difficulty": "Easy",
      "patterns": ["Linear Scan"],
      "constructs": ["math-functions"],
      "ds-used": ["array"],
      "ds-notes": {
        "array": "brute force nested loop, running state single pass"
      },
      "lists": ["blind-75", "phased-75"],
      "approaches": {
        "brute-force.cs": {
          "patterns": [],
          "ds-used": ["array"]
        },
        "running-state.cs": {
          "patterns": ["Linear Scan"],
          "variation": "Running State",
          "ds-used": ["array"]
        }
      }
    }
  },
  "by-pattern": {
    "Linear Scan": ["13", "121", "128"],
    "HashMap": ["1", "3", "128", "217", "242"]
  },
  "by-ds": {
    "array": ["1", "121", "238", "268"],
    "hashmap": ["1", "3", "128", "217", "242"]
  },
  "by-construct": {
    "dictionary": ["1", "3", "242", "20"],
    "hashset": ["128", "3", "217"]
  }
}
```

**Structure:**
- `_saving` - set to the problem number when `/save-problem` is in progress, null otherwise. If non-null at session start, the previous save was interrupted - check which steps completed and resume.
- `problems` - keyed by problem number. Full cross-reference data per problem.
- `by-pattern`, `by-ds`, `by-construct` - reverse-lookup indexes. AI reads only the section it needs instead of scanning all problems. Maintained automatically by `/save-problem`.
- `ds-notes` - optional usage context per DS (e.g., "complement lookup: store value -> index"). Kept separate from `ds-used` array so queries filter on the simple array and notes are display-only. Revisit after Phase 1.
- Approach-level `variation` and `ds-used` - enables AI to answer "which variation of pattern X was used on DS Y in this problem?" This data is primarily AI-facing. Whether Dataview can query it depends on Phase 1.

### Layer 2 - AI writes (rich content + curated views)

These files contain human-authored narrative content and curated editorial views. Written at save time. Some sections are updated by later saves (coverage rows, list status).

| File | Content | Updated by later saves? |
|------|---------|------------------------|
| `problems/<slug>/problem.md` | Statement, examples, constraints | No |
| `problems/<slug>/solutions.md` | Approach narratives, pattern links, reflection | No |
| `problems/<slug>/notes.md` | Bugs, insights, mantras | No |
| `problems/<slug>/solutions/*.cs` | Prettified solution code | No |
| `patterns/<name>.md` | Core idea, template, tradeoffs | Yes - new variations, Common Mistakes |
| `data-structures/<name>.md` | What it is, operations, complexity, **coverage table** | Yes - coverage row updates |
| `constructs/<cat>/<name>.md` | Syntax, gotchas, examples | No |
| `concepts/<name>.md` | Definition, approaches, practice problems | No |
| `workbench/lists/*.md` | **Full problem roster** with status column | Yes - status/pattern/link updates |
| `LESSONS.md` | Learning log | Yes - new entries |

**Coverage tables** stay as Layer 2 because they are curated roadmaps, not derived views. The denominator (total techniques per DS) includes placeholder rows for patterns that don't have files yet - a query can't know about planned-but-unstarted techniques. Matching rules are simplified to link-based (see Phase 8).

**List files** stay as Layer 2 because the full roster (including unsolved problems) is editorial content. A Dataview query on problem frontmatter would only show problems that have files - missing the 65 unsolved entries in blind-75.

### Layer 3 - Dataview queries (replaces AI writes)

These sections currently require AI to append/update during /save-problem. They will be replaced with Dataview queries that read from Layer 1 indexes and YAML frontmatter at render time.

| Current manual section | Location | Replaced by |
|-----------------------|----------|-------------|
| "Solved Problems" list | `patterns/*.md` | Dataview query on master-index.json (or frontmatter fallback) |
| "Seen In" section | `data-structures/*.md` | Dataview query on master-index.json (or frontmatter fallback) |
| "Seen In" section | `constructs/**/*.md` | Dataview query on master-index.json (or frontmatter fallback) |
| "Seen In" section | `concepts/*.md` | Dataview query on problem frontmatter tags |
| Coverage % in goals | `workbench/goals.md` | Dataview query on DS frontmatter `progress` field |

**Not replaced (stays Layer 2):**

| Manual section | Location | Why it stays |
|----------------|----------|-------------|
| Coverage table | `data-structures/*.md` | Curated roadmap with placeholder rows for unstarted patterns |
| List roster table | `workbench/lists/*.md` | Full roster includes unsolved problems with no files |

**Fallback strategy:** If Phase 1 reveals the Dataview extension cannot read JSON files, all queries fall back to scanning problem frontmatter. This works because the Issue 4 decision to duplicate cross-reference fields in frontmatter accidentally created a safety net. No extra migration needed for the fallback.

---

## Expected Impact

- /save-problem drops from ~15 file ops to ~9-11
- Token cost per save reduced ~30-35%
- Coverage table matching simplified from 4-rule fuzzy cascade to link-based lookup
- Pattern Solved Problems, all Seen In sections, and goals coverage % become automatic
- No more name-matching bugs for Seen In sections
- Reverse-lookup indexes keep AI read costs flat as problem count grows

---

## Implementation Steps

**Migration runs on a dedicated git branch.** Work on `dataview-refactor`, merge to main only when fully complete. If new problems are solved on main during migration, they'll need to be reconciled before merge.

### Phase 1 - Understand the Dataview extension

**Track:** B (query replacement) | **Safe to pause after:** Yes | **Status: COMPLETE (2026-04-10)**

**Goal:** Learn what queries the VS Code extension supports before designing any queries.

- Extension: `yahsan2.vscode-dataview-preview` (VS Code Marketplace)
- Local copy: `D:\Personal\Code\Depth Projects\VS Code Extensions\markdown-tooling\vscode-dataview-preview`
- Findings documented in `docs/dataview/query-syntax.md`

**Findings summary:**

| Question | Answer |
|----------|--------|
| Can it read JSON files? | No - indexes `*.md` only |
| Can it read YAML frontmatter? | Yes - full support via js-yaml |
| Query language | DQL: TABLE/LIST + FROM/WHERE/SORT/LIMIT/FLATTEN/AS |
| Aggregation (COUNT, SUM, AVG) | No - not implemented |
| Structured/nested YAML | Arrays via FLATTEN - yes. Nested object dot access - untested |
| DataviewJS | No - blocked by VS Code security sandbox |
| Multi-source FROM | No - one source per query |

**Architectural decisions from Phase 1:**

1. **All Dataview queries use problem frontmatter as source.** JSON fallback in the plan is now the primary strategy - Dataview never reads `master-index.json`. That file remains AI-only (fast lookups, reverse indexes).

2. **Goals coverage % stays Layer 2.** No aggregation means `goals.md` percentage rollup cannot be automated. AI continues to update the `progress` field in DS frontmatter after each save. This was the anticipated fallback.

3. **Track B scope confirmed.** All four Seen In replacements and Pattern Solved Problems are achievable via FLATTEN + WHERE on frontmatter arrays. Goals coverage is the only Layer 3 item that cannot be automated with this extension.

4. **Variation-level DS (Issue 7/12):** Nested YAML access is untested - keep variation-DS data in master-index.json approaches only (Option 2). Do not rely on querying it from pattern frontmatter.

5. **ds-notes (Issue 11):** Not queryable (requires nested access or JSON). Keep in master-index.json as AI-facing context only. Drop from query design.

6. **Fork path available if needed:** The VS Code extension is ~1,100 lines. If goals coverage % automation becomes important, a local fork adding COUNT/SUM is a one-session effort. Obsidian Dataview source is the reference implementation. Not needed now.

### Phase 2 - Design query templates

**Track:** B | **Depends on:** Phase 1 | **Safe to pause after:** Yes | **Status: COMPLETE (2026-04-10)**

**Goal:** Write and test working Dataview queries for each Layer 3 replacement.

All four query types confirmed rendering correctly in VS Code with `yahsan2.vscode-dataview-preview`.

**Results:**

| Query type | File tested | Renders? |
|-----------|-------------|---------|
| Pattern "Solved Problems" | `patterns/hashmap.md` | Yes - 5 problems |
| DS "Seen In" | `data-structures/hashmap.md` | Yes - 5 problems |
| Construct "Seen In" | `constructs/collections/dictionary.md` | Yes - 5 problems |
| Concept "Seen In" | `concepts/palindrome.md` | Yes - 2 problems |
| Goals coverage % | N/A | No - no aggregation, stays Layer 2 |

Full query templates documented in `docs/dataview/query-templates.md`.

**Execution order note:** Phases 3 and 4 were completed before Phase 2 testing to provide real frontmatter data. Actual order run: 1 → 3 → 4 → 2.

### Phase 3 - Promote pattern-index.json to master-index.json

**Track:** A (data migration) | **Safe to pause after:** Yes

**Goal:** Consolidate all cross-references into a single enriched index file.

Current `pattern-index.json` has title, slug, difficulty, patterns, and approaches per problem. This phase extends it to the full schema.

**Backfill strategy (combined approach):**
1. Scan all 12 problems' solution `.cs` files for construct usage (Dictionary, HashSet, Stack, Span, Math.Max, Array.Sort, IComparable) - this is ground truth
2. Cross-reference against existing construct file Seen In sections to catch anything the code scan missed
3. Read `ds-used` from solution code (map C# types to DS names using the existing type table in toolkit.md)
4. Read `lists` from problem.md frontmatter (already present)
5. Build reverse-lookup indexes (`by-pattern`, `by-ds`, `by-construct`) from the problem entries

Work:
- Rename `pattern-index.json` to `master-index.json`
- Wrap existing entries under a `"problems"` key
- Add `_saving: null` marker
- Add `constructs` field to each entry (from backfill step 1-2)
- Add `ds-used` field to each entry (from backfill step 3)
- Add `ds-notes` field to each entry (from existing Seen In contextual text where available)
- Add `lists` field to each entry (from backfill step 4)
- Ensure `slug` is present on all entries (already is)
- Restructure `approaches` to include `variation` and `ds-used` per approach
- Add reverse-lookup indexes (from backfill step 5)

**Do not delete any Seen In sections yet.** They stay until Phase 7 replaces them with queries.

### Phase 4 - Migrate problem files

**Track:** A | **Depends on:** Phase 3 (master-index.json provides patterns, ds-used, constructs) | **Safe to pause after:** Yes

**Goal:** Ensure problem YAML frontmatter is complete enough to power all queries.

Current state:
```yaml
title: Two Sum
category: DSA-Practice
difficulty: Easy
source: LeetCode
status: solved
lists: [blind-75]
```

Target state:
```yaml
title: Two Sum
number: 1
slug: two-sum
category: DSA-Practice
difficulty: Easy
source: LeetCode
status: solved
lists: [blind-75]
ds-used: [array, hashmap]
patterns: [HashMap, Two Pointers]
constructs: [dictionary]
tags: [complement, lookup, index-tracking]
```

Work:
- Define the final YAML schema for problems
- Backfill all 12 existing problem files with missing fields
- `patterns`, `ds-used`, and `constructs` are derived from the now-complete master-index.json
- `slug` and `number` derived from the folder name
- `tags` are AI-inferred from patterns and data structures

### Phase 5 - Migrate pattern files

**Track:** A | **Safe to pause after:** Yes

**Goal:** Add YAML frontmatter to all pattern files, including variation-level DS mapping.

Current state: No YAML. Uses inline `**display_name:** Linear Scan` in the body.

Target state:
```yaml
---
name: "linear-scan"
display_name: "Linear Scan"
category: pattern
variations:
  - name: Neighbor Comparison
    ds: [array, string]
  - name: Running State
    ds: [array]
ds-primary: [array, string]
---
```

Note: `ds-primary` is a summary of all DS seen across all variations. The `variations` array carries the precise mapping. Both are maintained to explore which one Dataview can query better (Phase 1 determines this). If Phase 1 shows only one is queryable, the other can be dropped later.

Work:
- Define the YAML schema for patterns
- Add frontmatter to all 11 pattern files
- Build variation list and DS mapping from master-index.json approach data
- Move `display_name` from body text to frontmatter (keep body text for backward compat during transition, remove in Phase 10)

**Do not replace Solved Problems sections yet.** That's Track B, Phase 7.

### Phase 6 - Migrate remaining file types

**Track:** A | **Safe to pause after:** Yes

**Goal:** Complete YAML frontmatter across data structures, concepts, and constructs.

#### Data structure files

Current state:
```yaml
name: "Array"
status: explored
progress: 58
```

Target state:
```yaml
name: "Array"
status: explored
progress: 58
```

No schema change needed. `progress` stays in frontmatter, manually updated by `/save-problem` when coverage rows change. Coverage tables stay as Layer 2 (hand-authored).

Work:
- Verify all 13 DS files have consistent frontmatter
- No Seen In replacement yet (Track B, Phase 7)

#### Concept files

Current state: No YAML at all.

Target state:
```yaml
---
name: "Palindrome"
category: concept
related-patterns: [two-pointers]
related-constructs: [string-reversal]
---
```

Work:
- Define the YAML schema for concepts
- Add frontmatter to both existing concept files
- No Seen In replacement yet (Track B, Phase 7)

#### Construct files

Current state: Already well-structured YAML.

Work:
- Verify consistency across all 7 files
- No changes expected

---

### DATA VERIFICATION CHECKPOINT

**Before proceeding to Track B phases, verify the entire data layer:**

1. Read every problem in master-index.json - confirm patterns, constructs, ds-used, lists match reality
2. Cross-check reverse-lookup indexes against problem entries (should be derivable)
3. Verify every problem.md frontmatter matches its master-index.json entry
4. Verify every pattern file's variation list matches the approaches in the index
5. Run `grep -r "pattern-index.json"` to confirm no references remain (see Phase 9 checklist)

This checkpoint catches drift introduced during the multi-phase data migration before any destructive changes (removing Seen In sections, replacing with queries).

---

### Phase 7 - Replace derived views with Dataview queries

**Track:** B | **Depends on:** Phase 1 (extension capabilities), Phase 2 (query templates), Phases 3-6 (data layer complete), verification checkpoint | **Safe to pause after:** Yes, per sub-step

**Goal:** Replace all Layer 3 sections with tested Dataview queries.

Sub-steps (each independently pauseable):
1. Replace Pattern "Solved Problems" sections with queries
2. Replace DS "Seen In" sections with queries
3. Replace Construct "Seen In" sections with queries
4. Replace Concept "Seen In" sections with queries
5. Replace goals.md coverage % table with a query on DS frontmatter `progress` field

For each replacement:
- Verify the query output matches the current manual section content
- If any information is lost (e.g., contextual notes in Seen In - see Issue 7), confirm the detail is preserved in solutions.md before removing
- Remove the manual section only after the query is verified

### Phase 8 - Update toolkit.md, /save-problem, and all references

**Track:** Both | **Safe to pause after:** No - complete this phase fully

**Goal:** Strip out eliminated steps and rules, update all file references.

#### Changes to `/save-problem` skill (.claude/commands/save-problem.md):
- Fix duplicate Step 10 numbering
- Step 7 becomes "Write master-index.json entry" with enriched schema (patterns, constructs, ds-used, ds-notes, lists, slug, approach-level variations) + update reverse-lookup indexes + set/clear `_saving` marker
- Step 3 adds: write enriched problem frontmatter (slug, ds-used, patterns, constructs, tags)
- Remove Step 8 (update pattern files - Solved Problems) - now a Dataview query
- Simplify Step 9 (update workbench lists) - roster stays, but only update status/pattern/link columns. Remove "create list file if missing" logic
- Remove DS coverage matching rules cascade - replace with link-based matching:
  - Match by link target: search for `[display_name](../patterns/file.md)` in the coverage table
  - If found: update the row's Problems Solved column
  - If not found: add a new linked row
  - Placeholder-to-link conversion happens only when a new pattern file is created (match placeholder text against display_name)
- Remove goals.md updates (now a Dataview query)
- Remove construct/concept Seen In appends (now Dataview queries)
- Add: update `progress` field in DS frontmatter after coverage row changes

#### Changes to toolkit.md:
- Remove the 4-rule Coverage table matching cascade and variation-as-pattern rule
- Replace with link-based matching description (above)
- Remove Seen In append instructions
- Remove "Data Structure Coverage Update" section (replace with simplified coverage row update)
- Update Pattern Library Integration rules
- Update all `pattern-index.json` references to `master-index.json`

#### Changes to CLAUDE.md:
- Update System Map table: `pattern-index.json` -> `master-index.json` with updated description
- Update Tech Stack line: `JSON (pattern-index.json)` -> `JSON (master-index.json)`

#### Changes to `/review` command (.claude/commands/review.md):
- Update to read from `master-index.json` instead of `pattern-index.json`
- Consider using reverse-lookup indexes for pattern-focused review sessions

#### Full reference update checklist (pattern-index.json -> master-index.json):

All 14 files that reference `pattern-index.json`:

| File | Reference type |
|------|---------------|
| `.claude/rules/toolkit.md` | Operational rule |
| `CLAUDE.md` | System map (2 references) |
| `.claude/commands/save-problem.md` | Step 7 instruction |
| `.claude/commands/review.md` | Read instruction (2 references) |
| `docs/active-problem-spec.md` | Data flow mapping |
| `docs/pattern-system.md` | Index format docs (3 references) |
| `docs/roadmap.md` | Feature description |
| `README.md` | Repo structure + reference table |
| `docs/vision.md` | Architecture mapping (already updated) |
| `docs/ideation-brief.md` | File reference |
| `docs/dataview/refactor-plan.md` | Migration steps (this file - self-referential, update last) |
| `docs/dataview/review-prompt.md` | Files to read |

**Verification:** Run `grep -r "pattern-index.json" --include="*.md"` after all updates. Result should be zero matches (except possibly this checklist itself).

### Phase 9 - Verify and clean up

- Run through a full `/save-problem` with the new, slimmed-down flow on the refactor branch
- Confirm all Dataview queries render correctly across every file type
- Confirm no orphaned manual sections remain (Seen In sections should be gone from DS, construct, concept files)
- Remove transition scaffolding (duplicate `display_name` in body + frontmatter for pattern files)
- Run the reference grep one final time
- Update memory files to mark refactor as complete
- Merge `dataview-refactor` branch to main

---

## Post-refactor /save-problem flow

| Step | What | File ops |
|------|------|----------|
| 1 | Read active files | 2 reads |
| 2 | Confirm slug | 0 |
| 3 | Write problem.md (enriched frontmatter) | 1 write |
| 4 | Write solution .cs files | 1-3 writes |
| 5 | Write solutions.md | 1 write |
| 6 | Write notes.md | 1 write |
| 7 | Write master-index.json (entry + reverse lookups + _saving marker) | 1 write |
| 8 | Update DS coverage table rows + progress in frontmatter | 1-2 writes |
| 9 | Update list file status/pattern/link columns | 1-2 writes |
| 10 | Update LESSONS.md | 0-1 write |
| 11 | Confirm + cleanup | 0-2 writes |

**Total: ~9-11 file ops (down from ~15). ~30-35% reduction.**

Eliminated: pattern Solved Problems appends, all Seen In appends, goals.md updates, coverage matching cascade.
Simplified: list updates (status only, no roster management), coverage updates (link-based matching).

---

## Risks and Open Questions

1. **Extension capability (Phase 1 gate for Track B):** We have not confirmed the VS Code Dataview extension can read JSON files or do aggregation. If it cannot, Layer 3 queries fall back to frontmatter-only scanning. The data migration (Track A) is still valuable regardless.

2. **Query performance:** With 75+ problems, queries need to stay fast. Monitor render time as the repo grows. Reverse-lookup indexes in master-index.json keep AI read costs flat, but Dataview query scan cost depends on the extension's implementation.

3. **Offline rendering:** Dataview queries only render in VS Code with the extension. Raw markdown on GitHub will show the query syntax, not the results. Acceptable since the user works in VS Code, but worth noting.

4. **master-index.json backfill:** Existing 12 entries need `constructs`, `ds-used`, `ds-notes`, and `lists` fields added. Phase 3 handles this using a combined approach: scan solution code (ground truth) + cross-reference existing Seen In sections. One-time cost.

5. **Branch divergence:** The migration runs on a git branch. New problems solved on main during migration need reconciliation before merge.

6. **Variation-level DS mapping:** Both pattern frontmatter (Option 1) and master-index.json approaches (Option 2) will carry variation-DS data initially. Phase 1 determines which is Dataview-queryable. The redundant copy may be dropped later.

---

## Architecture Test Results

Tests from [docs/vision.md](../vision.md). Run against this refactor plan on 2026-04-10. Updated after review on 2026-04-10.

### Test 1: Does every fact live in exactly one place?

**Result: Pass (with intentional, ownership-ruled denormalization)**

After the refactor:
- Cross-reference facts (patterns, ds-used, constructs) are **owned by** master-index.json. Copies in problem frontmatter are caches for Dataview queries.
- Problem-local facts (title, difficulty, status, lists, slug, tags) are **owned by** problem frontmatter. Copies in master-index.json are caches for AI quick-access.
- Coverage % is maintained in DS frontmatter `progress`, queried by goals.md. One source.
- Seen In sections are replaced by queries. No stored copies.
- Pattern Solved Problems are replaced by queries. No stored copies.

Every duplicated field has a declared authority (see Ownership Rules table). `/save-problem` writes both in the same pass.

### Test 2: Could each layer be swapped?

**Result: Pass**

- Data layer (JSON + YAML) maps to database tables. Schemas are explicit.
- Content layer (markdown body, .cs files) maps to a document store. Written once, stable.
- View layer (Dataview queries) is tool-specific by design. Swapping VS Code for a web frontend means rewriting queries as frontend components, but the data layer stays untouched.

### Test 3: Would this survive 10x scale?

**Result: Pass**

At 120 problems, 30 patterns, 5 lists:
- master-index.json at 120 entries: the `problems` object grows to ~3,000 lines (~60-75KB). But AI reads reverse-lookup indexes (`by-pattern`, `by-ds`, `by-construct`) which stay small regardless of problem count. Individual problem lookups are O(1) by key.
- /save-problem at 9-11 file ops: scales well. Write count is bounded by approaches per problem (typically 2-4), not total problem count.
- YAML frontmatter on 120 files: independent files, no issue.
- Dataview queries scanning 120+ files: depends on extension implementation. Phase 1 gate.

### Test 4: Is the AI doing work that a query could do?

**Result: Pass (with two justified exceptions)**

All remaining AI work after the refactor is either generative or curated:
- Writing problem statements, solution code, notes (content creation)
- Prettifying code with variable renames and comments (transformation)
- Writing pattern variations (new content)
- Writing master-index.json entries (structured fact creation)
- **Updating coverage table rows** - justified: coverage tables contain placeholder rows for planned techniques that don't have files. A query can't know about these.
- **Updating list file status columns** - justified: list rosters include unsolved problems with no files. A query can't show what doesn't exist.

---

## Review Decision Log

Full review conducted 2026-04-10. 23 issues identified and resolved. Decisions are incorporated into the plan above. The raw review log is preserved in conversation history.

Key architectural decisions from the review:
1. Phase reorder: index promotion (Phase 3) before problem frontmatter (Phase 4)
2. Two independent tracks: data migration proceeds regardless of Dataview capabilities
3. Coverage tables and list rosters stay as Layer 2 (curated content, not derived views)
4. master-index.json gains reverse-lookup indexes, `_saving` marker, `ds-notes`, approach-level variation + DS data
5. Explicit cache ownership rules for all duplicated fields
6. Link-based coverage matching replaces 4-rule fuzzy cascade
7. Data verification checkpoint between data migration and query replacement
8. Migration runs on a dedicated git branch

---

## Tool Reference

- **Extension:** yahsan2.vscode-dataview-preview
- **Marketplace:** VS Code Marketplace
- **Local copy:** `D:\Personal\Code\Depth Projects\VS Code Extensions\markdown-tooling\vscode-dataview-preview`
- **User preference:** Always favor Dataview queries over scripts or manual AI updates for derived views
