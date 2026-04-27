---
name: hashmap
display_name: HashMap
category: pattern
variations:
  - name: Complement Lookup
    ds: [array, hashmap]
  - name: Last Seen Index
    ds: [string, hashmap]
  - name: HashSet Existence Lookup
    ds: [array, hashset]
  - name: Frequency Count
    ds: [string, hashmap, array]
ds-primary: [array, string, hashmap, hashset]
---

# HashMap Pattern

## Core Idea

Store previously seen elements (or computations) in a HashMap for O(1) lookup. Trade space for time. This eliminates the need for a second loop by remembering what you have already seen.

The key question is always: what am I storing, and what am I looking up?

## Variation: Complement Lookup

**When to reach for this:**
- Pair sum problems ("find two numbers that add up to X")
- Rewrite `a + b = target` as `b = target - a`, then check if `b` was already seen
- Problems that would otherwise need nested loops

**Mental Trigger:**
> "If I know the current value, what other value do I need? Have I seen it?"
> "Can I rewrite this as: given what I have, what am I looking for?"

**Template:**
```csharp
var map = new Dictionary<int, int>(); // value -> index

for (int i = 0; i < nums.Length; i++)
{
    int needed = target - nums[i];

    // Check FIRST, then store - prevents using the same element twice
    if (map.TryGetValue(needed, out int storedIndex))
        return new int[] { storedIndex, i };

    map[nums[i]] = i;
}
```

**Critical Rule: Check First, Then Store**

Always check for the complement BEFORE storing the current element. If you store first:
- `[3,3]` with target=6 would store map[3]=0, then find map[3] and return [0,0] (same element used twice - wrong)

**Solved Problems:**
- **Two Sum** (problems/1-two-sum/solutions/hashmap.cs) - complement lookup, store number -> index

---

## Variation: Last Seen Index

**When to reach for this:**
- Need to know where you last saw a character or value, not just whether you saw it
- Sliding window problems where you want to jump the window start directly
- "Where did I last encounter this?" questions

**Mental Trigger:**
> "Do I need to jump to a previous position based on a stored index?"
> "Am I sliding a window and want to skip stale positions in one move?"

**Template:**
```csharp
var lastSeen = new Dictionary<char, int>(); // char -> last seen index

for (int end = 0; end < s.Length; end++)
{
    if (lastSeen.ContainsKey(s[end]) && lastSeen[s[end]] >= start)
    {
        // Jump start past the previous occurrence - never move left
        start = lastSeen[s[end]] + 1;
    }
    lastSeen[s[end]] = end;
}
```

**Critical Rule: Start Never Moves Left**

Guard the jump with `>= start` to ensure the stored index is still inside the current window. A stale stored index (before the window) must not move start backward.

**Solved Problems:**
- **Longest Substring Without Repeating Characters** (problems/3-longest-substring-without-repeating-characters/solutions/sliding-window-hashmap.cs) - store char -> last seen index for O(1) window jump

---

## Variation: HashSet Existence Lookup

**When to reach for this:**
- Only need to know if a value exists - no index or count needed
- Consecutive sequence problems ("does n-1 or n+1 exist?")
- Replacing O(n) list scans with O(1) set lookups inside a loop
- Deduplication before processing

**Mental Trigger:**
> "Do I need to know if a value exists, with no other information needed?"
> "Am I scanning a list repeatedly just to check membership?"

**Template:**
```csharp
HashSet<int> set = new HashSet<int>(nums); // O(n) build

foreach (int n in nums)
{
    if (!set.Contains(n - 1)) // sequence start: no predecessor
    {
        int current = n;
        int length = 0;
        while (set.Contains(current)) { current++; length++; }
        // update max
    }
}
```

**Solved Problems:**
- **Longest Consecutive Sequence** (problems/128-longest-consecutive-sequence/solutions/hashset-sequence-start.cs) - detect sequence starts by checking n-1 absence, count forward
- **Contains Duplicate** (problems/217-contains-duplicate/solutions/hashset-seen.cs) - add each number to a HashSet; if it's already there, a duplicate exists

---

## Variation: Frequency Count

**When to reach for this:**
- Anagram, permutation, or character distribution problems
- "Do these two collections have the same elements with the same counts?"
- Problems where you need to compare how many times each value appears

**Mental Trigger:**
> "Can I count occurrences in one pass, then cancel them out in another?"
> "Is the question really about how many times each thing appears, not where it appears?"

**Template:**
```csharp
var map = new Dictionary<char, int>();

foreach (char c in s)
    map[c] = map.GetValueOrDefault(c, 0) + 1;

foreach (char c in t)
{
    if (!map.ContainsKey(c)) return false;
    map[c]--;
}

return map.Values.All(v => v == 0);
```

**Optimization: Fixed Array Instead of Dictionary**

When the character set is bounded (e.g. lowercase a-z), replace the Dictionary with `int[26]` indexed by `c - 'a'`. Same logic, no hashing overhead, O(1) space guaranteed.

For stack allocation with no GC pressure, use `Span<int>` with `stackalloc int[26]` - identical behavior, faster in practice for short-lived fixed-size buffers.

**Solved Problems:**
- **Valid Anagram** (problems/242-valid-anagram/solutions/hashmap-frequency.cs) - count char frequencies in s, cancel out with t, verify all zero at end

---

## Try Next

- Subarray Sum = K - prefix sum + HashMap, store sum -> count (Complement Lookup)
- Group Anagrams - frequency counting, store sorted_word -> list (Complement Lookup)
- Minimum Window Substring - sliding window + frequency map (Last Seen Index)

## Common Mistakes

- **Storing first, checking second** - causes false positive matches on duplicates (Complement Lookup)
- **Using GetValueOrDefault** - returns 0 for missing keys, indistinguishable from index 0
- **Wrong map structure** - storing index as key when you need to look up by value
- **Forgetting to store** - checking but never inserting means the map stays empty forever
- **Start moving left** - not guarding the index jump with `>= start` corrupts the window (Last Seen Index)

## Solved Problems

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "HashMap"
SORT problem asc
```
