# Patternarium Roadmap

This is the single source of truth for all planned and in-progress system work.

**Scope check rule:**
When a new request comes in, check Active first.
- If there is active work: anything outside that scope goes to backlog, not into the current session.
- If nothing is active: new items go directly to the appropriate section.

---

## Active

What is currently being worked on:

| Item | Status |
|------|--------|
| Vault revision - technique tagging + pattern restructuring | Layer 1 done; full plan in `docs/vault-revision-plan.md` |
| Folder note migration - consolidate problem folders into named hub files | Phase 1 ready to start; full plan in `docs/folder-note-plan.md` |

---

## On Hold

Started or partially working - not urgent, resume when time allows.

| # | Title | Notes |
|---|-------|-------|
| 3 | Dry run improvements | Currently works (comment table + user fills values), but not ideal. Debug logging toggle not yet built. AI sometimes sets up dry run without being asked. |

---

## Improvement Backlog

System improvements - not yet started.

| # | Title | Priority | Notes |
|---|-------|----------|-------|
| 1 | Frontmatter description field on pattern and construct files | medium | One-liner description field distinct from display_name. Helps when pattern names sound similar. Example: `description: Two-pointer for longest/shortest subarray with constraint.` |
| 2 | Pattern menu file | low | Single `pattern-menu.md` with name + core idea + mental trigger per pattern. Avoids opening individual files during sessions. Trigger: when library hits 15+ patterns. Same idea for constructs. |
| 3 | (moved to On Hold) | - | - |
| 4 | YAML frontmatter across all file types | high | Do before the library grows large. Goal: AI reads frontmatter first to decide relevance, loads full file only when needed - reduces token usage per session. Constructs are done (name, category, tags, language, related). Remaining: **patterns** (name, display_name, tags, related-patterns), **concepts** (name, tags, related), **algorithms** (name, tags, complexity, related), **solutions.md** (title, approaches, patterns-used, status). Define schema per type, confirm with user, then retrofit. Do one type at a time. |

---

## Someday / Maybe

Untracked ideas - not prioritized, not committed to. Just worth remembering.

- `/create-issue` style commands to manage this backlog from the CLI instead of editing manually
- **YAML as a scan layer for token reduction** - AI reads YAML frontmatter first to decide relevance, loads full file only when needed. The existing master-index.json is the same idea at the problem level; YAML extends it down to the file level.
- Pattern promotion automation - when a sub-pattern has appeared in 2+ problems, flag it for promotion to its own file
- Review session scoring - track how many hints were needed during `/review` and log it to the problem's notes.md over time
- **Semantic search layer** - vector DB (MemPalace or similar) lets Claude query the vault by meaning instead of reading every file. Makes Claude a better knowledge manager as the vault grows. Real payoff at ~50+ patterns / 150+ problems. Can be added at any time - just runs against existing markdown files.

---

## Closed

Recently completed system work:

| # | Title | Closed |
|---|-------|--------|
| - | Timing system (Time Started / Time Taken per approach and problem) | 2026-04-03 |
| - | Sliding Window pattern file created | 2026-04-03 |
| - | Dictionary, Span constructs created | 2026-04-03 |
| - | Backlog created | 2026-04-03 |
| - | Dry-run tool - conversational and terminal modes | 2026-04-06 |
| - | Concepts system - concept files, encounter workflow, palindrome.md | 2026-04-06 |
| - | Workbench system - library, backlog, goals, lists, sessions | 2026-04-06 |
| - | /pause-problem and /resume-problem skills | 2026-04-06 |
| - | Blind 75 list with progress tracking | 2026-04-06 |
| - | lists: frontmatter field + auto-update on /save-problem | 2026-04-06 |
| - | Dataview query layer - Seen In and Solved Problems auto-rendered from problem frontmatter; master-index.json promoted from pattern-index.json with enriched schema; YAML frontmatter added to all file types; algorithm system fully integrated | 2026-04-11 |
| - | Techniques system - techniques/ folder, file format, toolkit.md identification behavior, by-technique index in Rebuild-Index.cs, techniques field per approach in solutions.md, CLAUDE.md and active-problem-spec.md updated | 2026-04-27 |
