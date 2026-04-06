# Missing Number - Solutions

## Approaches

### Approach 1: Boolean Flag Array
**Code:** [boolean-flag-array.cs](solutions/boolean-flag-array.cs)
**Time:** O(n) | **Space:** O(n)
**Thinking:** Allocate a boolean array of size n+1, using each number's value as its index. Mark each seen value as true. The index that stays false is the missing number.

---

### Approach 2: Gauss Sum
**Code:** [gauss-sum.cs](solutions/gauss-sum.cs)
**Time:** O(n) | **Space:** O(1)
**Thinking:** The sum of all integers from 0 to n is n*(n+1)/2. Subtract the actual sum of the array. The difference is exactly the missing number.

---

### Approach 3: XOR Cancellation
**Code:** [xor-cancellation.cs](solutions/xor-cancellation.cs)
**Time:** O(n) | **Space:** O(1)
**Thinking:** XOR all actual values, then XOR all expected indices [0..n]. Any number that appears in both cancels out (n XOR n = 0, n XOR 0 = n). The missing number has no pair and survives as the result.

---

## Patterns
- [Odd One Out](../../patterns/odd-one-out.md) (Approaches 1, 2, 3) - a complete set is expected and one element is missing; use a property of the full set to isolate it
  - Approach 1 uses the Boolean Presence variation
  - Approach 2 uses the Gauss Sum variation
  - Approach 3 uses the XOR Cancellation variation
- [Presence Array](../../patterns/presence-array.md) (Approach 1) - values used directly as indices into a fixed-size boolean array

## Reflection
**Key insight:** The pattern signal is "complete set with one element missing." Once you see that, the question becomes - what property of the full set can I exploit? Math (Gauss), storage (presence array), or bitwise cancellation (XOR) all answer the same question differently.

**Future strategy:** When you see a bounded range with exactly one missing or unpaired element, reach for Odd One Out. Choose the variation based on space constraints - presence array if O(n) is fine, Gauss or XOR if O(1) is needed. Reach for XOR when you want O(1) space without arithmetic, or when the problem involves bits/flips explicitly.
