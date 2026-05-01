---
problem: 1929
problem-title: Concatenation of Array
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
  - name: Single-Pass Dual Write
    file: single-pass-dual-write.cs
    patterns: []
    constructs: []
    ds-used: [array]
    techniques: [index-mapping]
    time: "O(n)"
    space: "O(n)"
---

# Concatenation of Array - Solutions

## Approaches

### Approach 1: Single-Pass Dual Write
**Code:** [single-pass-dual-write.cs](single-pass-dual-write.cs)
**Time:** O(n) | **Space:** O(n)

**Key Idea:** Iterate once over `nums`; for each index `i`, write `nums[i]` to both `output[i]` and `output[n+i]`. Avoids a second loop pass.

**Thinking:** Allocate the `2n` output array up front. For each input index, write the value to its two known target slots in the same iteration. The arithmetic (`i` and `n+i`) does the concat directly.

---

## Techniques

- Index Mapping (Approach 1) - compute both target positions per input index in one pass
