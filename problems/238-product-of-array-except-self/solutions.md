---
problem: 238
problem-title: Product of Array Except Self
difficulty: Medium
category: solutions
patterns: [Prefix/Suffix Decomposition, Two Pointers, Preprocessing, Multi-Pass Construction]
constructs: []
ds-used: [array]
algorithms: []
approaches:
  - name: Brute Force
    file: solutions/brute-force.cs
    patterns: []
    constructs: []
    ds-used: [array]
    time: "O(n^2)"
    space: "O(n)"
  - name: Prefix/Suffix Product Arrays
    file: solutions/prefix-suffix-arrays.cs
    patterns: [Prefix/Suffix Decomposition, Preprocessing]
    variation: Prefix Suffix Arrays
    constructs: []
    ds-used: [array]
    ds-notes:
      array: "two separate prefix and suffix product arrays; output is element-wise product"
    time: "O(n)"
    space: "O(n)"
  - name: Two-Pointer Converging/Diverging
    file: solutions/two-pointer-converging.cs
    patterns: [Prefix/Suffix Decomposition, Two Pointers, Multi-Pass Construction]
    variation: Two Pointer Converging
    constructs: []
    ds-used: [array]
    ds-notes:
      array: "two passes with converging pointers; output array reused as prefix accumulator"
    time: "O(n)"
    space: "O(1) extra"
  - name: Two-Pass Sequential
    file: solutions/two-pass-sequential.cs
    patterns: [Prefix/Suffix Decomposition, Multi-Pass Construction]
    variation: Two Pass Sequential
    constructs: []
    ds-used: [array]
    ds-notes:
      array: "output built in two sequential passes; running prefix then running suffix"
    time: "O(n)"
    space: "O(1) extra"
---

# Product of Array Except Self - Solutions

## Approaches

### Approach 1: Brute Force
**Code:** [brute-force.cs](solutions/brute-force.cs)
**Time:** O(n^2) | **Space:** O(n)

**Thinking:** For each element, iterate through the entire array and multiply everything except the current index. Two nested loops, skip when i == j. Valid solution but causes TLE on LeetCode.

---

### Approach 2: Prefix/Suffix Product Arrays
**Code:** [prefix-suffix-arrays.cs](solutions/prefix-suffix-arrays.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** The product at each index is made of two parts: the product of everything to the left and the product of everything to the right. Precompute both in separate arrays using a single loop that builds them simultaneously, then combine using complementary indices. Writing down the expected arrays on paper (`[1,1,2,6]` and `[24,12,4,1]` for input `[1,2,3,4]`) and comparing with console log output was key to finding three formula bugs.

---

### Approach 3: Two-Pointer Converging/Diverging
**Code:** [two-pointer-converging.cs](solutions/two-pointer-converging.cs)
**Time:** O(n) | **Space:** O(1) extra

**Thinking:** Use a single output array. Two pointers start at opposite ends and converge toward the center, depositing partial products (one half of the answer) into each slot. After crossing, they diverge outward, and each slot already has one half stored from Phase 1 - multiply in the other half from the running product variable. For odd-length arrays, the center element needs special handling since it was never visited in Phase 1.

---

### Approach 4: Two-Pass Sequential
**Code:** [two-pass-sequential.cs](solutions/two-pass-sequential.cs)
**Time:** O(n) | **Space:** O(1) extra

**Thinking:** Simplified version of Approach 3 - same core insight of depositing one half into the output array and completing it with the other half, but uses two sequential passes instead of converging/diverging pointers. First pass left-to-right builds prefix products, second pass right-to-left multiplies in suffix products. No pointer tracking or center-element handling needed.

---

## Patterns

- [Prefix/Suffix Decomposition](../../patterns/prefix-suffix-decomposition.md) (Approaches 2, 3, 4) - the core insight: "product of everything except self" decomposes into "left product * right product"
- [Two Pointers - Converging/Diverging](../../patterns/two-pointers.md#variation-convergingdiverging) (Approach 3) - pointers start at edges, converge depositing partial products, then diverge completing each position
- [Preprocessing - Build Derived Data](../../patterns/preprocessing.md#variation-build-derived-data) (Approach 2) - precompute left and right product arrays before building the final answer
- [Multi-Pass Construction](../../patterns/multi-pass-construction.md) (Approaches 3, 4) - use the output array as working space, depositing one half of the answer per pass

## Reflection

- **Key insight:** Decomposing "product of everything except self" into "left product * right product" - changing perspective from the whole to the parts. You don't need to solve both halves at once; solve one, then the other, then combine.
- **Future strategy:** When a problem asks for a value that depends on "everything else," try decomposing it into directional halves (prefix and suffix). Focus on what one element's answer is made of, write the formula for one position, and the algorithm follows.
- **What helped:** Writing down exact formulas (e.g., `left[i] = left[i-1] * nums[i-1]`) before coding made bugs identifiable as formula errors. Console dry runs against hand-written expected arrays caught every bug.
- **Surprise:** Discovered the converging/diverging two-pointer approach while walking - solved it mentally before touching code. The struggle was in translating the mental model to code, but pseudocode and paper tracing bridged that gap.
