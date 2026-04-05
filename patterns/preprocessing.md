# Preprocessing

**display_name:** Preprocessing

## Core Idea

Transform input into a simpler form before applying the main logic. Eliminate special cases upfront so the main loop stays clean.

## Variation: Normalize Before Compute

**When to reach for this:**
- Special cases complicate the main loop
- Multi-character tokens can be reduced to single tokens
- Edge cases are fixed and known upfront

**Mental Trigger:**
> "Can I eliminate the complexity before I even start the main logic?"
> "Are the special cases fixed and enumerable? Can I just replace them all first?"

**Template:**
```
for each known special case:
    replace/transform in input
run simple main logic on cleaned input
```

**Tradeoffs:**
- Time: O(n) - preprocessing pass + main pass
- Space: O(n) - may create new transformed input (e.g., new string)
- Only works when special cases are fixed and enumerable
- Trades space for simpler logic

**Solved Problems:**
- **Roman to Integer** (problems/13-roman-to-integer/solutions/string-replacement.cs) - replace the 6 subtraction pairs with single-character placeholders before summing

---

## Try Next

- Calculator problems
- Expression Evaluation
- Decode String

## Common Mistakes

- **Forgetting C# strings are immutable** - `Replace()` returns a new string, does not modify in place
- **Not accounting for space cost** - the transformed input requires O(n) extra space
- **Order of replacements** - replacements can interfere if they overlap; order may matter
