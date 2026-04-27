---
name: xor
slug: xor
display_name: XOR
category: concept
tags: [xor, bitwise, cancellation]
---

# XOR (Exclusive OR)

## What It Is
A bitwise operation that returns 1 when two bits are different and 0 when they are the same. Applied to integers, it operates on each bit position independently.

## Truth Table
| A | B | A XOR B |
|---|---|---------|
| 0 | 0 | 0 |
| 0 | 1 | 1 |
| 1 | 0 | 1 |
| 1 | 1 | 0 |

Rule: same inputs -> 0, different inputs -> 1.

## Key Properties
- `n XOR n = 0` - a number XOR'd with itself always cancels to zero
- `n XOR 0 = n` - a number XOR'd with zero is unchanged
- XOR is commutative: `a XOR b = b XOR a`
- XOR is associative: `(a XOR b) XOR c = a XOR (b XOR c)`

These four properties together mean: if you XOR a collection of numbers where every number appears an even number of times, the result is 0. Any number appearing an odd number of times survives.

## How It Works on Integers
Each integer is represented in binary. XOR is applied bit by bit across all positions.

```
5 = 101
3 = 011
    ---
    110 = 6
```

## In C#
The XOR operator is `^`.

```csharp
int result = a ^ b;
```

## Common Use Cases in DSA
- **Find a missing or unpaired number** - XOR all expected values with all actual values; paired ones cancel, the survivor is the answer
- **Detect a duplicate** - same idea in reverse
- **Swap two variables without a temp** - `a ^= b; b ^= a; a ^= b;`

## Practice Problems
- Missing Number (LeetCode #268) - XOR all indices with all values; the missing one survives

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN concepts AS concept
WHERE concept = "xor"
SORT problem asc
```
