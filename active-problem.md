## Problem
**Name:** Missing Number
**Difficulty:** Easy
**Tags:** array, math, bit-manipulation
**Lists:** phased-75, blind-75
**Time Started:** 2026-04-06 18:30
**Time Taken:** ?

## Statement
Given an array nums containing n distinct numbers in the range [0, n], return the only number in the range that is missing from the array.

Example 1: nums = [3,0,1] -> 2
Example 2: nums = [0,1] -> 2
Example 3: nums = [9,6,4,2,3,5,7,0,1] -> 8

Constraints:
- n == nums.length
- 1 <= n <= 10^4
- 0 <= nums[i] <= n
- All numbers are unique

### Approach 1: Boolean Flag Array
**Status:** solved
**Time Started:** 2026-04-06 18:30
**Time Taken:** ?

#### Thinking
Create a boolean array of size n+1. Mark each number in nums as seen. The index that remains false is the missing number.

#### Bugs
- Used loop index `i` instead of `nums[i]` when marking flags. Was marking positions 0..n-1 as seen instead of the actual values. Fix: `flags[nums[i]] = true`.

#### Solution
- Time: O(n)
- Space: O(n)
- Key Idea: Use a boolean array as a lookup table. Mark each seen number. The unmarked index is the answer.

### Approach 2: Gauss Sum (Math)
**Status:** solved
**Time Started:** 2026-04-06 18:30
**Time Taken:** ?

#### Thinking
Expected sum of [0..n] = n*(n+1)/2. Subtract the actual sum of nums. The difference is the missing number.

#### Solution
- Time: O(n)
- Space: O(1)
- Key Idea: The difference between the expected total and the actual total is exactly the missing number.

### Approach 3: XOR
**Status:** solved
**Time Started:** 2026-04-06 19:30
**Time Taken:** 2026-04-06 19:58

#### Thinking
Key XOR properties discovered:
- n XOR n = 0 (same number cancels out)
- n XOR 0 = n (zero leaves the number unchanged)

XOR every element in nums with every index in [0..n]. Numbers that appear in both cancel out (n XOR n = 0). The missing number has no pair and survives.

#### Solution
- Time: O(n)
- Space: O(1)
- Key Idea: XOR is self-cancelling. Pair up every index with its matching value - the unpaired one is the answer.

## Patterns
- **Odd One Out** - the core pattern across all three approaches. A complete set is expected; one element is missing. Use a property of the full set to isolate it.
  - Approach 1 uses the Boolean Presence variation - mark what's seen, find what isn't
  - Approach 2 uses the Gauss Sum variation - expected total minus actual total
  - Approach 3 uses the XOR Cancellation variation - paired numbers cancel, survivor is the answer
