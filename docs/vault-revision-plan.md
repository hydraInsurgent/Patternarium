# Vault Revision Plan

---

## Step 1 - Pattern Library Cleanup (do once, upfront)

Structural fixes to the pattern library before any revision sessions begin. These are system-level changes, not tied to any specific problem.

### Variations to demote to techniques

| Variation | Currently In | New Technique File | Reason |
|---|---|---|---|
| Index Jump | Sliding Window | `techniques/index-jump.md` | Same recognition signal as Shrink-Based. The jump is an optimization on top, not a different approach. |
| Last Seen Index | HashMap | merge into `index-jump.md` | Same technique as Index Jump - appears in two files. One file covers both. |
| Complement Push | Reverse Order Matching | `techniques/complement-push.md` | Pattern is "stack + LIFO matching." Complement Push is how you implement it - push expected closer instead of opener. Implementation trick, not a recognition signal. |
| XOR Cancellation | Odd One Out | `techniques/xor-cancellation.md` | "Pairs cancel under XOR" is a bitwise technique that will appear outside of missing-number problems. Not tied to this problem shape. |
| Normalize Before Compute | Preprocessing | `techniques/normalize-before-compute.md` | Always a means to an end, not a primary strategy. |

### Duplicates to remove

| Variation | Currently In | Already Covered By |
|---|---|---|
| Boolean Presence Check | Odd One Out | Presence Array pattern - add a pointer instead |
| Build Derived Data | Preprocessing | Prefix/Suffix Decomposition |

### Pattern to demote

| Pattern | Action | Reason |
|---|---|---|
| Multi-Pass Construction | Demote to technique `techniques/output-array-as-working-space.md` | Prefix/Suffix Decomposition is the recognition strategy. Multi-Pass Construction is how you implement it without extra arrays. Recognition vs implementation = pattern vs technique. |

### Preprocessing - open question

After removing two of its three variations, Preprocessing becomes a single-variation pattern (Sort to Expose Structure only). Decide at implementation time: keep as standalone pattern, or merge into a broader category.

---

## Step 2 - Build /revise Command

A slash command for cold re-solving a saved problem. This is the mechanism that drives all future vault enrichment - technique tagging, pattern updates, LESSONS.md graduation - problem by problem, as you go.

**Workflow:**
1. Pick a solved problem - load `problem.md` statement only, no solutions or notes
2. Run a normal session (think, code, debug, reflect) - full Modes 1-9
3. Compare phase at the end:
   - Patterns and techniques used this time vs what is saved in `solutions.md`
   - What was recalled correctly, what was missed, what is genuinely new
   - Did anything improve - new approach, cleaner code, fewer hints?
4. Update artifacts from the comparison:
   - New insight → update `notes.md`
   - LESSONS.md mistake not repeated → graduate it
   - Better or different solution → option to add approach and run `/save-problem`
   - Something still forgotten → reinforce in LESSONS.md

**Design decisions to finalize at build time:**
- Does it use `active-problem.md` / `active-solution.cs` during the session, or separate temp files?
- What triggers the compare phase - user says "done", or automatic after reflection?
- Minimal output when the solution is fully reproduced with no new insights?

---

## Step 3 - Revise Problems (ongoing, through /revise)

No batch work. Each `/revise` session handles one problem and naturally updates:
- Technique tagging for that problem's `solutions.md`
- Pattern file enrichment if new signals or mistakes emerge
- LESSONS.md graduation if old mistakes weren't repeated

The vault gets richer as you go.

---

## Initial Analysis Notes

*Written before implementation. Reference only - some details may shift.*

Reviewed all 11 pattern files and 26 variations. Core finding: before techniques existed as a category, variations were the only place to capture "same pattern, different implementation." Now that techniques exist, some variations are redundant - they are implementation moves, not different problem recognition signals.

Test used: **does this variation have a genuinely different mental trigger?** If the user reaches for it in response to the same problem signal as another variation, but implements it differently, it's a technique.

Patterns that are clean with no changes needed: Two Pointers (all 4 variations have distinct triggers), HashMap (Complement Lookup, HashSet Existence Lookup, Frequency Count), Presence Array, Linear Scan, Sliding Window Shrink-Based, Odd One Out (Gauss Sum), Reverse Order Matching core, Chunked Iteration.

Biggest structural insight: Prefix/Suffix Decomposition and Multi-Pass Construction are the same problem seen through two lenses - "how do I think about the answer" vs "how do I build it without extra space." That lens distinction is exactly pattern vs technique.
