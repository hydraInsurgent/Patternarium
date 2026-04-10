---
title: Two Sum
number: 1
slug: two-sum
category: DSA-Practice
difficulty: Easy
source: LeetCode
status: solved
lists: [blind-75]
ds-used: [array, hashmap]
patterns: [HashMap, Two Pointers]
constructs: [dictionary, array-sort]
tags: [complement-lookup, index-tracking, target-sum]
---

# Two Sum

## Statement

Given an array of integers `nums` and an integer `target`, return indices of the two numbers such that they add up to `target`.

You may assume that each input would have exactly one solution, and you may not use the same element twice.

You can return the answer in any order.

## Examples

**Example 1:**
```
Input:  nums = [2,7,11,15], target = 9
Output: [0,1]
Reason: nums[0] + nums[1] == 9
```

**Example 2:**
```
Input:  nums = [3,2,4], target = 6
Output: [1,2]
```

**Example 3:**
```
Input:  nums = [3,3], target = 6
Output: [0,1]
```

## Constraints

- 2 <= nums.length <= 10^4
- -10^9 <= nums[i] <= 10^9
- -10^9 <= target <= 10^9
- Only one valid answer exists

## Key Observations

- The problem guarantees exactly one solution - no need to handle "not found" edge case
- The same element cannot be used twice - index [0,0] is never valid
- Duplicates can exist in the array (see Example 3 with [3,3])
- The output is indices, not values - any solution must track original positions

## Tricky Test Cases

```
[3,3], target = 6        -> [0,1] (duplicates)
[3,2,4], target = 6      -> [1,2] (complement is not adjacent)
[0,4,3,0], target = 0    -> [0,3] (zeros)
```

---

## Solutions

- [Solution approaches & learning journey](solutions.md)
- [Mistakes & key insights](notes.md)

tags :: Array, HashMap, Two Pointers
