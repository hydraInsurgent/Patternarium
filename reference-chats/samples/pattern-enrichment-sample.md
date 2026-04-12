# Sample: Pattern File Enrichment
# Based on: what gets added to a Center Expansion pattern file after saving Palindromic Substrings
# Purpose: Review how pattern files grow from imported chat sessions

---

## What the pattern file looks like BEFORE this problem

```markdown
## Variation: Count All

**When to reach for this:**
- [empty - pattern file not yet created for this variation]

**Mental Trigger:**
> [not yet written]

**Template:**
[not yet written]

**Tradeoffs:**
- Time: O(n²)
- Space: O(1)

## Common Mistakes
[empty]
```

---

## What gets ADDED from the Palindromic Substrings session

**From "Pattern Signals" in the analysis:**
Added to `**When to reach for this:**`
```
- Problem asks to count (not find) palindromic substrings
- You need to check every possible palindrome center
- The problem involves symmetry that grows outward from a center point
```

**From "Key Insights for Pattern Library" in the analysis:**
Added to `**Mental Trigger:**`
```
> "Is there a center point I can expand from, and does the result at each step answer my question directly?"
```

**From "Mistakes and Lessons" table - filtered to pattern-related only:**
Added to `## Common Mistakes`
```
- Carrying over boundary-fix logic from "find longest" problems into "count" problems (Center Expansion - Count All). When counting, you increment inside the while condition. There is no overshoot to correct because invalid states are never counted.
- Adding a pre-check before even-length expansion (e.g., `if s[i] == s[i+1]`). The while condition handles mismatches - no guard needed at entry.
```

NOT added to pattern file (code-specific, goes to LESSONS.md instead):
- "Confused list-repetition with computational repetition" - this is a DP concept mistake, not a Center Expansion mistake
- "Mental model carryover from LPS problem" - goes to LESSONS.md as a general learning behavior mistake

---

## What the pattern file looks like AFTER this problem

```markdown
## Variation: Count All

**When to reach for this:**
- Problem asks to count (not find) palindromic substrings
- You need to check every possible palindrome center
- The problem involves symmetry that grows outward from a center point

**Mental Trigger:**
> "Is there a center point I can expand from, and does the result at each step answer my question directly?"

**Template:**
```csharp
int count = 0;
for (int i = 0; i < n; i++)
{
    count += Expand(s, i, i);       // odd-length
    count += Expand(s, i, i + 1);   // even-length
}

int Expand(string s, int left, int right)
{
    int count = 0;
    while (left >= 0 && right < s.Length && s[left] == s[right])
    {
        count++;
        left--;
        right++;
    }
    return count;
}
```

**Tradeoffs:**
- Time: O(n²)
- Space: O(1)
- Beats DP when space matters; same time complexity

## Common Mistakes
- Carrying over boundary-fix logic from "find longest" problems. When counting, increment inside the while condition - there is no overshoot to correct (Center Expansion - Count All).
- Adding an unnecessary pre-check before even-length expansion. The while condition already handles a starting mismatch.
```

---

## Filter Rule Applied

Mistakes that go to the pattern file:
- Caused by misapplying this pattern's specific mechanics (the counting loop, the two-call structure)
- Would recur specifically when someone uses Center Expansion - Count All

Mistakes that go to LESSONS.md instead:
- General learning behavior (carrying over mental models from other problems)
- DP-specific conceptual confusion (not about center expansion at all)
- Problem comprehension errors (not pattern-specific)
