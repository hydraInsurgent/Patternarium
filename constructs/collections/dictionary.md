---
name: "Dictionary"
category: collections
tags: [dictionary, hashmap, key-value, o1-lookup, frequency-map]
language: csharp
related: [hashset]
---

# Dictionary

## What It Is
A key-value store with O(1) average-case lookup, insert, and delete. Keys must be unique. In C#, `Dictionary<TKey, TValue>`.

## C# Syntax
```csharp
// Create empty
Dictionary<char, int> map = new Dictionary<char, int>();

// Add a new key (throws if key already exists)
map.Add('a', 0);

// Add or update (safe - overwrites if key exists)
map['a'] = 0;

// Check existence
map.ContainsKey('a');  // true or false

// Get value (throws KeyNotFoundException if missing)
int val = map['a'];

// Safe get - returns false if not found, sets out param to default
if (map.TryGetValue('a', out int storedIndex)) { ... }

// Remove
map.Remove('a');

// Size
map.Count;

// Iterate
foreach (var kvp in map)
{
    char key = kvp.Key;
    int val = kvp.Value;
}
```

## When to Reach For It
- You need to store a value associated with a key (not just presence)
- Complement lookup: "what index did I last see this at?"
- Frequency counting: "how many times have I seen this value?"
- Mapping one domain to another (char -> int, string -> list)

## vs Alternatives
- vs HashSet: use Dictionary when you need to store a value per key. HashSet is key-only.
- vs array: use Dictionary when keys are not integers in a known bounded range. For ASCII chars (0-127), an int[128] array is faster with no overhead.
- vs List: List.Contains is O(n). Dictionary lookup is O(1).

## Gotchas
- `map.Add(key, val)` throws if key already exists. Use `map[key] = val` to safely overwrite.
- `map[key]` throws KeyNotFoundException if the key is missing. Use TryGetValue for safe access.
- Do not use `GetValueOrDefault` when 0 is a valid stored value - the default 0 is indistinguishable from a stored 0.

## Seen In
- 1 - Two Sum (store number -> index for complement lookup)
- 3 - Longest Substring Without Repeating Characters (store char -> last seen index for window jump)
- 242 - Valid Anagram (store char -> frequency count, cancel out with second string)
