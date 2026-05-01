---
name: "string.Substring"
slug: string-substring
category: strings
tags: [string, substring, extraction, index]
language: csharp
related: [char-methods]
---

# string.Substring

## What It Is
Extracts a portion of a string by start index and length.

## C# Syntax
```csharp
string sub = s.Substring(startIndex, length);   // extract 'length' chars from startIndex
string sub = s.Substring(startIndex);            // from startIndex to end of string
```

## When to Reach For It
- Extracting a substring between two indices for comparison or processing
- Brute force substring enumeration

## vs Alternatives
- vs `s[i..j]` (Range syntax, C# 8+): `s[i..j]` is cleaner but uses exclusive end index. `Substring(i, j-i)` is explicit but verbose.
- vs manual character loop: use Substring when you need a string object; loop when comparing character-by-character in-place.

## Gotchas
- **Second argument is length, not end index.** To get `s[start..end]` inclusive: `s.Substring(start, end - start + 1)`. Passing the end index directly causes `ArgumentOutOfRangeException`.
- Allocates a new string object on every call - O(length) time and space. Avoid inside tight loops when possible.

## See Also
- [char-methods.md](char-methods.md) - character-level operations

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "string-substring"
SORT number asc
```
