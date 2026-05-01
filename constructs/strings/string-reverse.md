---
name: "String Reversal"
slug: string-reverse
category: strings
tags: [string, reverse, linq, enumerable]
language: csharp
related: [string-substring]
---

# String Reversal

## What It Is
Reverse the characters of a string in C#.

## C# Syntax
```csharp
// Correct form - LINQ Reverse returns IEnumerable<char>, must convert back
string reversed = new string(s.Reverse().ToArray());

// Alternative - explicit array manipulation
char[] chars = s.ToCharArray();
Array.Reverse(chars);
string reversed = new string(chars);
```

## When to Reach For It
- Palindrome check via comparison: `s == reversed`
- Brute force palindrome detection on extracted substrings

## vs Alternatives
- vs two-pointer in-place check: `new string(s.Reverse().ToArray())` allocates memory; two-pointer comparison is O(1) space. Prefer two-pointer when checking palindromes inside a loop.

## Gotchas
- **`s.Reverse().ToString()` does not work.** `.Reverse()` (LINQ) returns `IEnumerable<char>`, not a string. Calling `.ToString()` on it returns the type name, not the reversed characters. Must use `new string(s.Reverse().ToArray())`.
- Allocates two objects (the array and the new string) - O(n) space per call.

## See Also
- [string-substring.md](string-substring.md) - often used together when extracting and reversing substrings

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "string-reverse"
SORT number asc
```
