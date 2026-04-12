# Sample: notes.md
# Based on: 647 - Palindromic Substrings (hypothetical completed session)
# Purpose: Review proposed additions before wiring into save-problem

---

```markdown
# Palindromic Substrings - Notes

## Mistakes Made

### Approach 1 - Center Expansion
- Added an unnecessary `if s[i] == s[i+1]` check before starting even-length expansion. The while condition already handles mismatches - no pre-check needed. Instinct was to guard the entry, but the loop guard is the entry.
- Tried to apply the "fix overshoot" boundary correction from Longest Palindromic Substring. That was needed there because we were tracking boundaries and returning indices. Here we are counting - the loop never counts an invalid state, so there is nothing to correct.

### Approach 2 - Dynamic Programming
- Confused "repetition in the list of substrings" with "repetition in computation." The overlap in DP is not that two substrings look the same - it is that checking (0,3) requires (1,2), and (1,2) is also independently evaluated. Same computation, needed twice.

## Key Insights

- The shift from tracking (find the longest) to counting (count all) changes bookkeeping completely. Same center expansion technique, different mental model for what each expansion step means.
- In a growing-only window (brute force or center expansion), the maximum of any tracked value can only increase - so comparing against the newly added element is always sufficient. No need to rescan.

## Mantras

- "Let the guard do the work." - when your while condition already handles a case, don't add a pre-check.
- "Count inside, not after." - for counting problems, increment inside the valid condition, not after the loop exits.

## Patterns Used

- **Center Expansion** (Approach 1)
- **Dynamic Programming** (Approach 2)

## Connected Problems

- **5 - Longest Palindromic Substring** - same center expansion technique, but returns boundaries instead of counting. The "fix overshoot" logic that was unlearned here comes from that problem. Solve that first if unfamiliar with center expansion.
- **516 - Longest Palindromic Subsequence** - uses DP on palindromes, different state definition but same 2D table structure.
```

---

## What Is New Here vs. Current Format

**`## Connected Problems` section (proposed addition)**
- Location: after Patterns Used, at the bottom of notes.md
- What it captures: problems that are directly related - share a technique, are prerequisites, or were explicitly connected during the session
- Source: "Connections to Other Problems" section of the analysis file
- Format: problem number + name, one-sentence explanation of the connection
- Currently missing: no home for cross-problem connections in any saved file
