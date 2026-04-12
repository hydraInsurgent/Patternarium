---
name: "String"
slug: string
status: explored
progress: 60
---

# String

## What It Is
An ordered sequence of characters. Strings behave like arrays of characters but are typically immutable - you cannot modify a character in place; you build a new string instead.

## Core Operations

| Operation | Description | Time |
|-----------|-------------|------|
| Access character | Read char at index | O(1) |
| Length | Get number of characters | O(1) |
| Concatenation | Join two strings | O(n) - builds new string |
| Substring | Extract a range | O(n) |
| Compare | Check equality | O(n) |
| Search | Find substring or char | O(n) |
| Convert to array | Split into characters | O(n) |

## Space Complexity
O(n) - one slot per character.

## Mental Model
An array of characters with one key difference: immutable. Every "change" creates a new string. This matters for performance when building strings in a loop - use a StringBuilder instead.

## When to Reach For It
- The input is text
- You are checking patterns, characters, or sequences
- The problem involves palindromes, anagrams, substrings, or parsing

## When NOT to Use It
- You are building a string character by character in a loop - use StringBuilder to avoid O(n^2) cost from repeated concatenation

## vs Alternatives
- vs char[]: char array is mutable (can change in place), string is not
- vs StringBuilder: StringBuilder is for building; String is for reading and comparing

## C# Implementation
- Immutable string: `string s = "hello"`
- Convert to array: `s.ToCharArray()`
- Compare: `s == t` or `string.Equals(s, t, StringComparison.OrdinalIgnoreCase)`
- Normalize: `s.ToLower()`, `s.ToUpper()`

## Coverage

**Progress: 6 / 10 techniques explored (60%)**

Linked rows have a pattern file. Plain text rows are placeholders until the pattern file is created.

| Technique | Status | Problems Solved |
|-----------|--------|-----------------|
| [Linear Scan](../patterns/linear-scan.md) | explored | 13-Roman to Integer |
| [Two Pointers](../patterns/two-pointers.md) | explored | 125-Valid Palindrome (Symmetry Check), 5-Longest Palindromic Substring (Expand Around Center) |
| [Sliding Window](../patterns/sliding-window.md) | explored | 3-Longest Substring Without Repeating Characters |
| [Preprocessing](../patterns/preprocessing.md) | explored | 242-Valid Anagram (normalize before compare) |
| [HashMap](../patterns/hashmap.md) | explored | 242-Valid Anagram (frequency count on characters) |
| [Chunked Iteration](../patterns/chunked-iteration.md) | explored | 13-Roman to Integer |
| Prefix Sum | not started | - |
| Pattern Matching (KMP, Z-Algorithm) | not started | - |
| Trie | not started | - |
| Dynamic Programming | not started | - (edit distance, LCS, palindrome partitioning) |

---

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty, patterns
FROM "problems"
FLATTEN ds-used AS ds
WHERE ds = "string"
SORT problem asc
```
