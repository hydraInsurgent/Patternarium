# Plan: Import-Chat Feature Fixes

**Status: COMPLETED** - all fixes implemented and committed.

Source: post-review audit of the `/import-chat` feature.
Scope: all changes needed before the feature is usable end-to-end.
Files touched: `import-chat.md`, `save-problem.md`, `active-problem-spec.md`, `resume-problem.md` (if it exists), both analysis files, both sample files.

**Note on save-problem growth:** this plan adds sub-step 4b, two fallback reading paths in Step 5, sub-step 11b, and a note in Step 12. save-problem is already 12 steps / ~228 lines. These additions are targeted and will not restructure the file, but the natural future split point - when it becomes worth doing - is steps 1-6 (produce the problem folder) into a core save file, and steps 7-12 (index rebuild, DS coverage, list updates, LESSONS, distillation) into a separate post-save enrichment file. Defer until the command is noticeably hard to follow.

---

## Fix order

Fixes are ordered by dependency. Structural changes (Groups 1-2) must land first because later groups reference the new format.

---

## Group 1 - Path and structural alignment in import-chat.md

These are all changes to `.claude/commands/import-chat.md`.

### 1a - Correct the chat file path

**Where:** Step 1 and the `## What the User Provides` section.

**Current:** `reference-chats/<filename>.txt`

**Change to:** `reference-chats/imports/<filename>.txt`

This aligns the command with the actual folder where both existing imports live. Also update the glob in Step 1 (when listing available files for the user) to list `reference-chats/imports/*.txt`.

---

### 1b - Remove Case C from Step 5

**Where:** Step 5 "Format active-problem.md", Case C.

**Current:** Case C handles "already fully written" active files and says skip the step.

**Change:** Remove Case C entirely. An imported session will never arrive with a fully written active-problem.md. Keep only Case A (empty / raw statement) and Case B (partial session data). Relabel them as just the two conditions with no letter designations to reduce clutter.

---

### 1c - Fix approach subsection heading levels in Step 4 template

**Where:** Step 4 "Generate analysis file", the `## Approaches` section of the template.

**Current:** the template shows `### Approach N: [Name]` under `## Approaches` - correct heading level.

**Verify:** the two existing analysis files use top-level `## Approach 1: Brute Force` (wrong) or narrative subsections inside `## Thinking Journey` (no structured Approaches block at all). The template is already correct. This item is about making sure all future imports follow it - no template change needed, but note it in the template as a reminder:

> "Each approach gets its own `### Approach N:` subsection here - do not use top-level H2 headings per approach."

---

### 1d - Add display_name lookup note to Step 5

**Where:** Step 5, in the instruction to write the `## Patterns` section of active-problem.md.

**Add:** "When writing pattern names to `## Patterns`, look up the `display_name` from the pattern file in `patterns/`. If the pattern file does not exist yet, write the name as used in the chat and mark it with a comment: `<!-- no pattern file yet - confirm name at save time -->`."

This prevents informal names from the chat (e.g., "2D DP table") from silently flowing into solutions.md pattern links without a file to back them up.

---

## Group 2 - active-problem.md import extension

This is the architectural change for how imported session data is stored.

**The problem:** constructs identified in the chat, approaches discussed but not explored, and the session checklist all live only in the analysis file. If a session is continued in a new conversation, the AI reads active-problem.md and misses all of this.

**The fix:** import-chat Step 5 writes a new `## Import Notes` section at the bottom of active-problem.md. This section holds everything needed to continue the session that is not already captured in the approach blocks - but synthesized, not copy-pasted.

**Synthesis principle:** the AI reads the analysis file and derives what is actionable and relevant for the active session. Constructs are described in terms of their role in the specific approach, not just listed by name. Remaining approaches are framed as next steps based on what the user already understands. The checklist is filtered - items already addressed by what is in the approach blocks are dropped or pre-marked done.

### 2a - Add `## Import Notes` section to active-problem-spec.md

**Where:** `docs/active-problem-spec.md`, template section.

**Add this block at the bottom of the template** (only present for imported sessions):

```markdown
## Import Notes
<!-- Only present when this session was created via /import-chat. Remove section after /save-problem. -->

### Constructs Identified
<!-- AI-synthesized from analysis file. Each line states the construct and its specific role in this session's approach - not a bare name list. -->
- [construct]: [how it was used here, e.g. "Dictionary<char,int> - frequency map reset per starting index to track local window counts"]

### Remaining Approaches
<!-- Synthesized from chat context. Framed as actionable next steps, not raw approach names. Include what the user already understands as context. -->
- [Approach name]: [e.g. "Sliding Window - you understand the brute force structure; next step is shrinking the window instead of resetting. Start by asking: what lets you advance the left pointer?"]

### Session Checklist
<!-- Filtered from analysis file ## Session Checklist. Items already addressed by the approach blocks above are dropped. Remaining items are specific and actionable. -->
#### Overall
- [ ] Problem statement understood
- [ ] Mental model solidified
- [ ] Reflection completed
- [ ] Saved to repo

#### Approach N: [Name]
- [ ] Code written in active-solution.cs
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user
[approach-specific items that remain genuinely open - drop any that the approach blocks already resolve]
```

**Also add** to the `## When Each Section Gets Written` event table:

| `/import-chat` completes | Append `## Import Notes` with constructs, remaining approaches, and checklist if session came from an external chat |

**Also add** to the `## How /save-problem Decomposes These Files` table:

| `## Import Notes` -> `## Constructs` | `solutions.md` frontmatter `constructs:` field (fallback when approach blocks have no `## Constructs` subsection) |
| `## Import Notes` -> `## Remaining Approaches` | `solutions.md` placeholder blocks ("not yet explored") |

---

### 2b - Update import-chat Step 5 to write `## Import Notes`

**Where:** `.claude/commands/import-chat.md`, Step 5, both Case A and Case B.

**Add to Case A (write from scratch):** After writing the approach blocks, write `## Import Notes` at the bottom. Apply the synthesis principle:
- `## Constructs Identified` - read `## Constructs Used` from the analysis file. For each construct, write its name and its specific role in the approach as used in the chat (not a bare name). If a construct appeared in multiple approaches, note which approach used it and how.
- `## Remaining Approaches` - read approaches discussed in the chat but not fully explored (no code, no confirmed solution). Frame each as an actionable next step that builds on what the user already understood - include what clicked and what open question remains.
- `## Session Checklist` - read `## Session Checklist` from the analysis file. Filter out items that are already addressed by the approach blocks written above (e.g., if thinking and status are filled in, drop "approach understood"). Keep only items that represent genuine remaining work.

**Add to Case B (partial file):** Check if `## Import Notes` already exists. If not, append it using the same synthesis process. If it does, only update checklist items that have been completed since the last import run - do not re-synthesize the other sections.

---

### 2c - Update save-problem Step 5 to read constructs from `## Import Notes`

**Where:** `.claude/commands/save-problem.md`, Step 5, the logic that builds `constructs: [...]` in solutions.md frontmatter.

**Add fallback:** "If approach blocks in active-problem.md do not have `## Constructs` subsections, check `## Import Notes` -> `## Constructs Identified` in active-problem.md as a fallback source. Map the construct names to their slugs using the construct files in `constructs/`."

---

### 2d - Update save-problem Step 5 to read remaining approaches from `## Import Notes`

**Where:** `.claude/commands/save-problem.md`, Step 5, the placeholder block logic.

**Change:** "check if any alternative approaches were discussed during the session but not explored" - replace the vague "check from conversation" source with: "read `## Import Notes` -> `## Remaining Approaches` from active-problem.md if present; otherwise infer from conversation history."

---

### 2e - Remove `## Import Notes` after save

**Where:** `.claude/commands/save-problem.md`, Step 12.

**Add:** "If `## Import Notes` exists in active-problem.md, it is consumed by the save - no need to preserve it. It will be cleared with the rest of the active files when the user confirms cleanup."

---

## Group 3 - save-problem guards

### 3a - Add complexity `?` guard

**Where:** `.claude/commands/save-problem.md`, Rules section.

**Add** to the "If the active file is incomplete, ask the user to fill in the gaps" rule:

> "Also check: if any `// Time:` or `// Space:` headers in `active-solution.cs` still contain `?`, treat this as incomplete. Ask the user to confirm complexity for each unconfirmed approach before saving. Do not write `?` values into solutions.md frontmatter."

---

### 3b - Add explicit pattern file creation sub-step before Step 5

**Where:** `.claude/commands/save-problem.md`, between Step 4 and Step 5.

**Insert as Step 4b:**

> **Step 4b - Ensure pattern files exist**
> For each pattern named in `active-problem.md` `## Patterns`:
> - Check if `patterns/<file>.md` exists. If it does, read its `display_name` and the `## Variation:` heading that will be referenced.
> - If the file does not exist, create it now from the template in `docs/pattern-system.md`. Populate `display_name` from the pattern name in the session. Tell the user: "Creating new pattern file: `patterns/<file>.md`. You may want to review it after save."
> - If the file exists but the variation does not, add the variation section now.
>
> This step must complete before Step 5, which uses display_name to build links in solutions.md.

---

### 3c - Add checklist verification before Step 12

**Where:** `.claude/commands/save-problem.md`, just before Step 12.

**Insert as Step 11b:**

> **Step 11b - Verify import checklist (if present)**
> If `active-problem.md` contains `## Import Notes` -> `## Session Checklist`, scan for unchecked items (`- [ ]`). If any exist, surface them to the user:
> "The following checklist items are still open. You can continue and save anyway, or address them first:"
> [list unchecked items]
>
> Do not block the save - this is informational only. The user decides whether to proceed.

---

## Group 4 - Resume command awareness

**Where:** `.claude/commands/resume-problem.md` (check if this file exists; if not, the resume behavior is inline in toolkit.md).

When the /resume-problem command is invoked:
- Check whether a matching analysis file exists in `reference-chats/analysis/` for the active problem (match by slug from `## Problem` in active-problem.md)
- If found: read it for session context before resuming. Surface any open checklist items from `## Import Notes` -> `## Session Checklist` as the first thing shown to the user: "Resuming imported session. Open items from last time: [list]"
- If not found: resume normally from active-problem.md content

**Where in toolkit.md:** also add a note to the Active Problem File section:

> "When resuming a session that originated from `/import-chat`: check for a matching analysis file in `reference-chats/analysis/` and read it before continuing. This provides the thinking journey and open questions that are not fully captured in active-problem.md."

---

## Group 5 - Analysis file realignment

These are edits to existing analysis files to match the current template.

### 5a - Add missing sections to palindromic-substring-analysis.md

**Where:** `reference-chats/analysis/palindromic-substring-analysis.md`

**Add `## Core Formula / Key Condition`** after `## Thinking Journey`:
The core condition is not a single formula (unlike LRCR). Write:
> "Center expansion: every successful iteration of `while left >= 0 AND right < len AND s[left] == s[right]` is one valid palindrome. Count inside the loop, not after. DP: `dp[i][j] = (s[i] == s[j]) AND dp[i+1][j-1]` with base cases for length 1 and 2."

**Add `## Constructs Used`** before `## Pattern Signals`:
> - `int count` - simple counter, incremented per valid expansion step
> - `bool[][] dp` - 2D boolean table, `dp[i][j]` = whether s[i..j] is a palindrome
> - Nested loops over substring length (fill order for DP)

**Add `## Pattern Signals`** (currently missing entirely):
> - Problem asks to count, not find - signals a counting loop rather than boundary tracking
> - Output is a single integer (count) - no need to track indices
> - The word "all" in the problem name ("all palindromic substrings") - suggests every center must be evaluated
> - DP signal: "can we reuse the result of a smaller subproblem?" - checking whether a longer substring is a palindrome reuses the check for the inner substring

---

### 5b - Fix approach block heading levels in both analysis files

**Palindromic-substring-analysis.md:** The approach content is inside `## Thinking Journey` as narrative subsections. Add a separate `## Approaches` section after Thinking Journey with the structured format from the template:

```markdown
## Approaches

### Approach 1: Center Expansion
**Complexity:** O(n²) time, O(1) space
**Structure:** For each index i, call expand(i, i) and expand(i, i+1). Each call expands outward while characters match, incrementing count per step.
**Key optimization / insight:** No pre-check before even expansion. No boundary correction after loop. Count inside the while condition.

### Approach 2: Dynamic Programming
**Complexity:** O(n²) time, O(n²) space
**Structure:** Fill dp[i][j] for increasing substring lengths. Base case: length 1 always true, length 2 check s[i]==s[j]. Transition: dp[i][j] = dp[i+1][j-1] AND s[i]==s[j].
**Key optimization / insight:** Fill order (by length) ensures inner subproblems are always ready. j = i + length - 1.
```

**Longest-repeating-character-replacement-analysis.md:** Change `## Approach 1: Brute Force` (top-level H2) to a nested `### Approach 1: Brute Force` under a new `## Approaches` H2 heading.

---

### 5c - Realign sample files to current format

**Scope:** `reference-chats/samples/solutions-sample.md`, `notes-sample.md`, `pattern-enrichment-sample.md`

These will be deleted after the feature merges. Before deletion, verify:
- solutions-sample.md: `**Key Condition:**` field placement and format matches what save-problem Step 5 now produces. The sample shows it between Time/Space and Thinking - confirm this order is still what the command produces.
- notes-sample.md: `## Connected Problems` section format matches the source priority order in save-problem Step 6.
- pattern-enrichment-sample.md: the filter rule matches the updated deduplication rule from Fix 6a below.

No content changes needed unless a format mismatch is found during review.

---

## Group 6 - Interrupted session options alignment

### 6a - Sync active-problem-spec.md interrupted session options with toolkit.md

**Where:** `docs/active-problem-spec.md`, Interrupted Session Handling section.

**Current:** lists three options: `(a) Save it with /save-problem, (b) Discard and start fresh, (c) Resume it`

**toolkit.md Mode 1 has:** `(a) Save it with /save-problem, (b) Pause it with /pause-problem, (c) Discard and start fresh, (d) Resume it`

**Change:** update active-problem-spec.md to match toolkit.md exactly - four options in the same order with the same labels. The Pause option was added to toolkit.md but not back-ported to the spec.

---

## Group 7 - Cleanup and simplification

### 7a - Remove "semantic equivalent" from deduplication rule

**Where:** `.claude/commands/save-problem.md`, Step 11, "Deduplication rule" for `**When to reach for this:**`.

**Current:** "skip a bullet if its core meaning is already expressed by an existing bullet - either exact wording or semantic equivalent"

**Change to:** "skip a bullet only if it uses identical or near-identical wording to an existing bullet. If the meaning overlaps but the phrasing is distinct, add it - the pattern file is a reference, not a summary."

Apply the same change to the `## Common Mistakes` deduplication rule in the same step.

---

## Files changed summary

| File | Change group |
|------|-------------|
| `.claude/commands/import-chat.md` | 1a, 1b, 1c, 1d, 2b |
| `.claude/commands/save-problem.md` | 2c, 2d, 2e, 3a, 3b, 3c, 7a |
| `.claude/commands/resume-problem.md` (or toolkit.md if inline) | 4 |
| `docs/active-problem-spec.md` | 2a, 6a |
| `.claude/rules/toolkit.md` | 4 (Active Problem File section note) |
| `reference-chats/analysis/palindromic-substring-analysis.md` | 5a, 5b |
| `reference-chats/analysis/longest-repeating-character-replacement-analysis.md` | 5b |
| `reference-chats/samples/*.md` | 5c (verify only, no content changes expected) |

---

## Out of scope

- `## Concepts` tracking (defined in pattern-system.md but orphaned) - addressed in a separate cleanup pass, not part of this feature
- Sample file deletion - happens after this plan is executed and the feature is verified
- `## Patterns` / `## Approaches Mentioned But Not Explored` / `## Prior Knowledge Brought In` extra sections in palindromic analysis - leave as-is, they are non-breaking and informative
