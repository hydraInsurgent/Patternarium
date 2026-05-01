---
problem: 1470
problem-title: Shuffle the Array
difficulty: Easy
category: solutions
flow: quest
patterns: []
constructs: []
ds-used: [array]
algorithms: []
techniques: [index-mapping]
concepts: []
approaches:
  - name: Interleaved Index Mapping
    file: interleaved-index-mapping.cs
    patterns: []
    constructs: []
    ds-used: [array]
    techniques: [index-mapping]
    time: "O(n)"
    space: "O(n)"
---

# Shuffle the Array - Solutions

## Approaches

### Approach 1: Interleaved Index Mapping
**Code:** [interleaved-index-mapping.cs](interleaved-index-mapping.cs)
**Time:** O(n) | **Space:** O(n)

**Key Idea:** Iterate `i` from 0 to `n-1`; place `nums[i]` at `ans[2*i]` (even slots) and `nums[n+i]` at `ans[2*i+1]` (odd slots). The arithmetic does the interleave directly.

**Thinking:** The output's even slots come from the first half of nums (the x's); the odd slots come from the second half (the y's). One closed-form mapping per output index, computed in a single pass. No scratch buffer needed.

---

## Techniques

- Index Mapping (Approach 1) - place x's at even output positions, y's at odd, derived directly from input index
