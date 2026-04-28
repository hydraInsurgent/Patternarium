---
problem: 268
problem-title: Missing Number
difficulty: Easy
category: solutions
patterns: [Odd One Out, Presence Array]
constructs: []
ds-used: [array]
algorithms: []
concepts: [xor]
approaches:
  - name: Boolean Flag Array
    file: boolean-flag-array.cs
    patterns: [Odd One Out, Presence Array]
    variation: Boolean Flag Array
    constructs: []
    ds-used: [array]
    ds-notes:
      array: "bool[n+1] marks each seen number; scan for false to find missing"
    time: "O(n)"
    space: "O(n)"
  - name: Gauss Sum
    file: gauss-sum.cs
    patterns: [Odd One Out]
    variation: Gauss Sum
    constructs: []
    ds-used: [array]
    ds-notes:
      array: "expected sum n*(n+1)/2 minus actual sum equals missing number"
    time: "O(n)"
    space: "O(1)"
  - name: XOR Cancellation
    file: xor-cancellation.cs
    patterns: [Odd One Out]
    variation: XOR Cancellation
    constructs: []
    ds-used: [array]
    ds-notes:
      array: "XOR all indices 0..n and all values; paired elements cancel, missing remains"
    time: "O(n)"
    space: "O(1)"
---

# Missing Number - Solutions

## Approaches

### Approach 1: Boolean Flag Array
**Code:** [boolean-flag-array.cs](boolean-flag-array.cs)
**Time:** O(n) | **Space:** O(n)
**Thinking:** Allocate a boolean array of size n+1, using each number's value as its index. Mark each seen value as true. The index that stays false is the missing number.

**Mistakes:**
- **flags[i] instead of flags[nums[i]]** - marked the loop position as seen, not the actual number. Result: slots 0..n-1 were always marked true, and slot n was always false, so the code always returned n regardless of input. Fix: use the value as the index - `seen[nums[i]] = true`.

---

### Approach 2: Gauss Sum
**Code:** [gauss-sum.cs](gauss-sum.cs)
**Time:** O(n) | **Space:** O(1)
**Thinking:** The sum of all integers from 0 to n is n*(n+1)/2. Subtract the actual sum of the array. The difference is exactly the missing number.

---

### Approach 3: XOR Cancellation
**Code:** [xor-cancellation.cs](xor-cancellation.cs)
**Time:** O(n) | **Space:** O(1)
**Thinking:** XOR all actual values, then XOR all expected indices [0..n]. Any number that appears in both cancels out (n XOR n = 0, n XOR 0 = n). The missing number has no pair and survives as the result.

---

## Patterns
- Odd One Out (Approaches 1, 2, 3) - a complete set is expected and one element is missing; use a property of the full set to isolate it
  - Approach 1 uses the Boolean Presence variation
  - Approach 2 uses the Gauss Sum variation
  - Approach 3 uses the XOR Cancellation variation
- Presence Array (Approach 1) - values used directly as indices into a fixed-size boolean array

## Reflection
**Key insight:** The pattern signal is "complete set with one element missing." Once you see that, the question becomes - what property of the full set can I exploit? Math (Gauss), storage (presence array), or bitwise cancellation (XOR) all answer the same question differently.

**Future strategy:** When you see a bounded range with exactly one missing or unpaired element, reach for Odd One Out. Choose the variation based on space constraints - presence array if O(n) is fine, Gauss or XOR if O(1) is needed. Reach for XOR when you want O(1) space without arithmetic, or when the problem involves bits/flips explicitly.

- **Notes Insights:**
  - Came in with the presence array approach immediately because the problem requires tracking absence across a known range - not just what was seen. That's the distinction between presence array and HashSet.
  - XOR felt unintuitive from regular numbers. The two core properties (n XOR n = 0, n XOR 0 = n) are what make it click. Once you think in terms of bit flips, the logic becomes concrete.
- **Mantras:**
  - Mark the VALUE, not the position: `seen[nums[i]]`, not `seen[i]`
  - When you need to check absence across a full range, presence array fits better than HashSet - HashSet only tracks what you've seen, not what's missing from the range
  - Reach for XOR when two values should cancel each other out, or when the problem can be thought of in terms of bit flips
