---
problem: 121
problem-title: Best Time to Buy and Sell Stock
difficulty: Easy
category: solutions
patterns: [Linear Scan]
constructs: [math-functions]
ds-used: [array]
algorithms: []
tags: [running-minimum, max-profit, single-pass]
approaches:
  - name: Brute Force
    file: solutions/brute-force.cs
    patterns: []
    constructs: []
    ds-used: [array]
    ds-notes:
      array: "nested loops check every buy-sell pair"
    time: "O(n^2)"
    space: "O(1)"
  - name: Running State - Track Min So Far
    file: solutions/running-state.cs
    patterns: [Linear Scan]
    variation: Running State
    constructs: [math-functions]
    ds-used: [array]
    ds-notes:
      array: "single pass tracking min price seen and max profit achievable"
    time: "O(n)"
    space: "O(1)"
---

# Best Time to Buy and Sell Stock - Solutions

## Approaches

### Approach 1: Brute Force
**Code:** [brute-force.cs](solutions/brute-force.cs)
**Time:** O(n^2) | **Space:** O(1)

**Thinking:** Start by trying every possible buy-sell pair. For each day, assume you bought on that day, then check all future days to find the maximum positive difference. Track the global maximum across all buy days. Guarantees correctness but is inefficient.

---

### Approach 2: Running State - Track Min So Far
**Code:** [running-state.cs](solutions/running-state.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Started from a two-pass idea (track min before and max after each index), then considered storing profit at each index. Progressively compressed: realized all past values aren't needed - just a running minimum and running max profit. At each index (treating it as a sell day), only need the minimum price seen so far. Compute profit first, then update minimum - this ensures buy happens before sell. Single pass with two variables.

---

### Approach 3: Two-Pass (Min Before / Max After) *(not yet explored)*
**Time:** O(n) | **Space:** O(n)
**Idea:** Precompute minimum price before each index and maximum price after each index in two passes, then find the index with the best difference. Valid but uses unnecessary storage compared to the running state approach.

---

## Patterns

- [Linear Scan - Running State](../../patterns/linear-scan.md#variation-running-state) (Approach 2) - Single pass maintaining running minimum price and computing max profit at each step, summarizing past data into two variables instead of storing it.

## Reflection

- **Key insight:** Before introducing extra structures (arrays, passes), ask: "Am I storing data or just tracking a running condition?" and "Do I really need all past values, or just a summary?" The solution compressed step by step - from storing everything, to storing less, to just two running variables.
- **Future strategy:** When looking for a best pair/relationship across an array, ask first: "Can this be solved in a single scan by tracking just the state I need?" That's the mental trigger for Running State.
- **Most natural approach:** The optimal approach trickled down from brute force through progressive compression - brute force to two-pass with storage to single pass with running variables.
