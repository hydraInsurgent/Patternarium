---
name: "String.Replace"
slug: string-replace
category: strings
tags: [string, replace, preprocessing, substitution]
language: csharp
related: [char-methods]
---

# String.Replace

## What It Is
Replaces all occurrences of a substring (or char) with another value, returning a new string. Used as a preprocessing step to normalize input before the main scan.

## C# Syntax
```csharp
// Replace substring
string result = s.Replace("IV", "IIII");

// Replace char
string result = s.Replace('I', 'X');

// Chain multiple replacements
string encoded = s
    .Replace("IV", "IIII")
    .Replace("IX", "IIIIIIIII")
    .Replace("XL", "XXXX")
    .Replace("XC", "XXXXXXXXX");
```

## When to Reach For It
- Preprocessing step: encode edge cases before scanning (e.g., Roman numeral subtractive pairs)
- Normalizing separators or delimiters before splitting
- Eliminating known patterns that would complicate the main loop

## vs Alternatives
- vs manual scan with lookahead: Replace is cleaner when you need to handle several multi-char patterns before a uniform scan. Lookahead is better when you need to react to the pattern without consuming it.
- vs Regex.Replace: String.Replace is faster for literal substitutions. Use Regex only when the pattern is variable or complex.

## Gotchas
- `String.Replace` is case-sensitive. There is no built-in case-insensitive overload in .NET Standard - use `Regex.Replace` with `RegexOptions.IgnoreCase` if needed.
- Each call allocates a new string. Chaining many replacements on a large string creates multiple intermediate allocations. `StringBuilder` or `Regex.Replace` with a `MatchEvaluator` can avoid this.
- Order of replacements matters when patterns overlap (e.g., replacing "IX" before "I" vs after changes results).

## See Also
- [char-methods.md](char-methods.md) - per-character checks and conversions

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "string-replace"
SORT problem asc
```
