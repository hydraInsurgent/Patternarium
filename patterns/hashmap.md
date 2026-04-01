# HashMap Pattern

**display_name:** HashMap - Complement Lookup

## Core Idea

Store previously seen elements (or computations) in a HashMap for O(1) lookup. Trade space for time. This eliminates the need for a second loop by remembering what you have already seen.

## When to Reach for This

- Pair sum problems ("find two numbers that add up to X")
- Complement search ("does the value I need already exist?")
- Frequency counting ("how many times have I seen this?")
- "Have I seen this before?" questions
- Problems that would otherwise need nested loops

## Mental Trigger

> "Can I store what I have already seen so I do not have to scan again?"
> "If I know the current value, what other value do I need? Have I seen it?"
> "Would a second loop be searching for something I could have remembered? If so what data structure should i use to remember."

## Template

```csharp
// Complement lookup pattern
var map = new Dictionary<int, int>(); // value -> index (or value -> count)

for (int i = 0; i < nums.Length; i++)
{
    int needed = target - nums[i];

    // Check FIRST, then store
    // This prevents using the same element twice
    if (map.TryGetValue(needed, out int storedIndex))
    {
        return new int[] { storedIndex, i };
    }

    map[nums[i]] = i;
}
```

## Tradeoffs

| | Value |
|--|--|
| Time | O(n) - single pass |
| Space | O(n) - stores up to n elements |
| vs Brute Force | Eliminates the inner loop |
| vs Two Pointers | Works on unsorted input; Two Pointers requires sorted and uses O(1) space |

## Critical Rule: Check First, Then Store

Always check for the complement BEFORE storing the current element. If you store first:
- `[3,3]` with target=6 would store map[3]=0, then find map[3] and return [0,0] (same element used twice - wrong)

If you check first:
- At i=0: map is empty, check fails, store map[3]=0
- At i=1: check map[3] -> found at index 0 -> return [0,1] (correct)

## Solved Problems

- **Two Sum** (problems/1-two-sum/solutions/hashmap.cs) - complement lookup, store number -> index

## Try Next

- Subarray Sum = K - prefix sum + HashMap, store sum -> count
- Group Anagrams - frequency counting, store sorted_word -> list

---

## Sub-Pattern: HashSet - Existence Lookup

### Core Idea
Load all values into a HashSet for O(1) membership testing. Unlike the complement lookup pattern, you store no values - only keys. Use when you need to answer "does this number exist?" not "where is it?" or "how many times?".

### When to Reach for This
- Consecutive sequence problems ("does n-1 or n+1 exist?")
- Deduplication before processing
- Replacing O(n) list scans with O(1) set lookups inside a loop

### Mental Trigger
> "Do I need to know if a value exists, with no other information needed?"
> "Am I scanning a list repeatedly just to check membership?"

### Template
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

### Solved Problems
- **Longest Consecutive Sequence** (Approach 2) - detect sequence starts by checking n-1 absence, count forward

---

## Common Mistakes

- **Storing first, checking second** - causes false positive matches on duplicates
- **Using GetValueOrDefault** - returns 0 for missing keys, which is indistinguishable from index 0
- **Wrong map structure** - storing index as key when you need to look up by value
- **Forgetting to store** - checking but never inserting means the map stays empty forever
