---
name: palindrome
slug: palindrome
display_name: Palindrome
category: concept
tags: [palindrome, two-pointers, string-comparison]
---

# Palindrome

## What It Is
A string (or sequence) that reads the same forwards and backwards. The left and right sides are mirror images around a center point.

## How to Verify
Compare characters from both ends moving inward. If every pair matches until the pointers meet or cross, it is a palindrome. If any pair mismatches, it is not.

## Approaches

### 1. Inward (Two Pointers)
Start from both ends, move toward the center.

```
left = 0, right = n - 1
while left < right:
    if s[left] != s[right]: return false
    left++, right--
return true
```

### 2. Reverse and Compare
Reverse the string and check if it equals the original.

```
reversed = Reverse(s)
return s == reversed
```

In C#: `new string(s.Reverse().ToArray()) == s`

### 3. Outward Expansion from Center
Start just inside the center, expand outward checking each pair.

```
if n is odd:  left = n/2 - 1, right = n/2 + 1
if n is even: left = n/2 - 1, right = n/2

while left >= 0 and right <= n - 1:
    if s[left] != s[right]: return false
    left--, right++

return true
```

### 4. Recursion
Same logic as inward or outward - the while loop is replaced by a recursive call.

```
isPalindrome(s, left, right):
    if left >= right: return true          // base case - pointers met or crossed
    if s[left] != s[right]: return false   // mismatch
    return isPalindrome(s, left+1, right-1)
```

## Examples
- "racecar" - palindrome (odd length, center = 'e')
- "abba" - palindrome (even length, center between the two 'b's)
- "abc" - not a palindrome ('a' != 'c')
- "a" - palindrome (single character is always a palindrome)

## Practice Problems
- Valid Palindrome (LeetCode #125) - check with two pointers
- Valid Palindrome II (LeetCode #680) - one character removal allowed
- Longest Palindromic Substring (LeetCode #5) - outward expansion from every center

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN concepts AS concept
WHERE concept = "palindrome"
SORT problem asc
```
