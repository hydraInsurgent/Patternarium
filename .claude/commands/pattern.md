# /pattern - Show Pattern Used in Current Problem

Extract and display the pattern(s) used in the current problem session.

## Behavior

For each approach explored in this session, output:

```
Pattern: [Pattern Name]
Core Idea: [One sentence describing what this pattern does]
Used when: [2-3 signals that indicate this pattern applies]
This session: [How it was applied to this specific problem]
Also appears in: [2-3 other common problems that use this pattern]
```

## Example Output for Two Sum

```
Approach 1 - HashMap
Pattern: HashMap - Complement Lookup
Core Idea: Store previously seen elements for O(1) lookup to avoid nested loops
Used when: pair sum problems, complement search, "have I seen this before?"
This session: stored number -> index, checked target - current before each insert
Also appears in: Subarray Sum = K, Two Sum II, Group Anagrams

Approach 2 - Two Pointers
Pattern: Two Pointers - Sorted Pair
Core Idea: Use sorted order to shrink search window from both ends
Used when: sorted array, pair relationship (sum/difference), range shrinking
This session: sorted with index preservation, moved left/right based on sum vs target
Also appears in: 3Sum, Container With Most Water, Remove Duplicates
```

## After Output

Ask: "Does this match how you understood the patterns? Do you want to update the pattern files?"

If yes, suggest running `/save-problem` which will also update the pattern files.
