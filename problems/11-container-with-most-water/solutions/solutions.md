---
problem: 11
problem-title: Container With Most Water
difficulty: Medium
category: solutions
patterns: [Two Pointers]
constructs: []
ds-used: [array]
algorithms: []
concepts: []
techniques: [running-max, constraint-ceiling-pruning]
approaches:
  - name: Brute Force - Two Nested Loops
    file: brute-force.cs
    patterns: []
    constructs: []
    ds-used: [array]
    techniques: [running-max]
    time: "O(n^2)"
    space: "O(1)"
  - name: Two Pointers - Opposite Ends
    file: two-pointers.cs
    patterns: [Two Pointers]
    variation: Sorted Pair
    constructs: []
    ds-used: [array]
    techniques: [running-max]
    time: "O(n)"
    space: "O(1)"
  - name: Two Pointers - Constraint-Ceiling Pruning
    file: two-pointers-pruned.cs
    patterns: [Two Pointers]
    variation: Sorted Pair
    constructs: []
    ds-used: [array]
    techniques: [running-max, constraint-ceiling-pruning]
    time: "O(n)"
    space: "O(1)"
---

# Container With Most Water - Solutions

## Approaches

### Approach 1: Brute Force - Two Nested Loops
**Code:** [brute-force.cs](brute-force.cs)
**Time:** O(n^2) | **Space:** O(1)

**Thinking:** Two nested loops covering every pair (i, j). At each pair, area = min(height[i], height[j]) * (j - i). Checked all possible containers and tracked the running maximum. Area formula derived independently - initially said "volume" but self-corrected to "area". Complexity confirmed as O(n^2) because all pairs are visited.

---

### Approach 2: Two Pointers - Opposite Ends
**Code:** [two-pointers.cs](two-pointers.cs)
**Time:** O(n) | **Space:** O(1)

**Key Condition:** always move the shorter pointer - `if height[right] > height[left]: left++, else right--`

**Thinking:** Explored sliding window first (carried over from problem 424) but concluded it doesn't fit - validity depends only on the two endpoints, not on elements between them. Returned to two pointers: start at opposite ends for maximum width. Pointer movement rule - moving the taller pointer is guaranteed no improvement (height still capped by the shorter side, width shrinks). Moving the shorter pointer is the only move with any upside. When heights are equal, either move is valid.

---

### Approach 3: Two Pointers - Constraint-Ceiling Pruning
**Code:** [two-pointers-pruned.cs](two-pointers-pruned.cs)
**Time:** O(n) | **Space:** O(1)

**Key Condition:** `(right - left) * 10_000 > maxArea` - exits when theoretical max remaining area cannot beat current best

**Thinking:** Same two-pointer structure as Approach 2 with an early exit added: `(end - start) * 10000 > maxWater`. The 10000 is the problem constraint on maximum height. So `(right - left) * 10_000` is the ceiling - the best area this width could ever produce. If the ceiling is already below the current best, stop. Discovered by reading a LeetCode solution and reasoning through what the condition was doing.

---

## Patterns

- Two Pointers - Sorted Pair (Approaches 2, 3) - start at opposite ends for maximum width, converge inward by always moving the shorter pointer

## Techniques

- Running Max (Approaches 1, 2, 3) - carry the best area seen so far as a running maximum instead of rescanning
- Constraint-Ceiling Pruning (Approach 3) - exit early when remaining width * 10000 (max possible height) cannot beat current best

## Reflection

- **Key insight:** The pointer movement rule is the test for two pointers - if you can define a clear condition for when each pointer moves, the approach will work. Here it was: always move the shorter pointer because it is the only one that can improve the area.
- **Trickiest divergence:** Landed on sliding window first because the container visually looks like a window. Realized on my own that it doesn't fit - there is no condition that can be checked incrementally on window contents. The validity depends only on the two endpoints.
- **Future strategy:** Ask if exactly two positions determine the answer. If yes, ask: is there a condition that tells me when and which pointer to move? If that condition exists, two pointers will cover the necessary range in one pass.
- **Notes Insights:**
  - The pointer movement rule is the test for two pointers: if you can define a clear condition for when each pointer moves, the approach will work.
  - Sliding window requires a validity condition on window *contents*. If the problem only cares about the two boundary positions (not what is between them), two pointers applies instead.
  - Problem constraints are not just test input limits - they are information you can use inside your algorithm. The max height constraint (10^4) becomes a ceiling for early-exit pruning.
- **Mantras:**
  - "Two positions determine the answer? Ask: can I define when each pointer moves?"
  - "Constraint = ceiling. If remaining capacity * max constraint can't beat your best, stop."
