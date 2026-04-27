---
name: "Character Frequency Array"
slug: char-frequency-array
category: collections
tags: [array, frequency, character, fixed-alphabet, hashmap-substitute]
language: csharp
related: [dictionary]
---

# Character Frequency Array

## What It Is
A fixed-size `int[]` array indexed by character value, used as a faster alternative to `Dictionary<char, int>` when the key space is small and known (e.g., 26 uppercase letters, 128 ASCII characters).

## C# Syntax
```csharp
// For uppercase letters only (A-Z)
int[] freq = new int[26];

// Increment
freq[s[i] - 'A']++;

// Decrement
freq[s[i] - 'A']--;

// Read count
int count = freq[s[i] - 'A'];

// For full ASCII (printable chars)
int[] freq = new int[128];
freq[s[i]]++;
```

## When to Reach For It
- You need a frequency map over a fixed, bounded character set (uppercase only, lowercase only, ASCII)
- You want to avoid hashing overhead - array index computation is one subtraction, no hash function
- Inner-loop frequency tracking where constant-factor performance matters

## vs Alternatives
- vs `Dictionary<char, int>`: Dictionary supports any key type and size but carries hashing overhead. Use this array when the key space is bounded and known at compile time.
- vs `bool[]` (Presence Array): Use `int[]` when you need counts, not just presence.

## Gotchas
- Only works when the key space is fixed and known. Using `s[i] - 'A'` on lowercase or mixed input will produce wrong indices or go out of bounds.
- Array is not zeroed between uses if reused across multiple windows - decrement correctly or allocate fresh per use.

## See Also
- [dictionary.md](dictionary.md) - the general-purpose alternative; prefer when key space is unbounded or unknown

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "char-frequency-array"
SORT problem asc
```
