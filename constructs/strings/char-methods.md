---
name: "Char Methods"
slug: char-methods
category: strings
tags: [char, alphanumeric, case-conversion, ascii, character-check]
language: csharp
related: [string-replace]
---

# Char Methods

## What It Is
Static utility methods on `char` for testing character properties (letter, digit, whitespace) and converting case - no allocation, no regex.

## C# Syntax
```csharp
// Type checks
char.IsLetterOrDigit('a');   // true - letter or digit
char.IsLetter('a');          // true
char.IsDigit('3');           // true
char.IsWhiteSpace(' ');      // true

// Case conversion
char.ToLower('A');           // 'a'
char.ToUpper('a');           // 'A'
char.ToLowerInvariant('A');  // 'a' - culture-safe, preferred for ASCII

// Comparison after normalization
char.ToLowerInvariant(s[left]) == char.ToLowerInvariant(s[right])

// ASCII range check (manual - no allocation)
bool IsAlphanumeric(char c) =>
    (c >= 'a' && c <= 'z') ||
    (c >= 'A' && c <= 'Z') ||
    (c >= '0' && c <= '9');
```

## When to Reach For It
- Palindrome problems where you need to skip non-alphanumeric characters
- Case-insensitive comparison without creating a new string
- Filtering characters in a two-pointer loop without allocating intermediate arrays

## vs Alternatives
- vs `string.ToLower()`: Char methods operate per-character with no allocation. `string.ToLower()` allocates a new string - fine for preprocessing but wasteful inside a loop.
- vs regex: Char methods are faster and simpler for basic checks. Use regex only for complex patterns.
- vs ASCII range check: `char.IsLetterOrDigit` is cleaner but the manual range check is slightly faster and avoids culture edge cases.

## Gotchas
- `char.ToLower` is culture-sensitive. Prefer `char.ToLowerInvariant` for ASCII comparisons to avoid locale-specific behavior.
- `char.IsLetter` returns true for accented letters (e.g., 'é') - may not match LeetCode's definition of "alphanumeric" which is strictly ASCII. Use the manual range check when ASCII-only is required.

## See Also
- [string-replace.md](string-replace.md) - string-level manipulation vs char-level checks

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "char-methods"
SORT number asc
```
