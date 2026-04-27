---
problem: 20
problem-title: Valid Parentheses
difficulty: Easy
category: solutions
patterns: [Reverse Order Matching]
constructs: [stack, dictionary]
ds-used: [string, stack, hashmap, array]
algorithms: []
tags: [bracket-matching, lifo, stack-matching]
approaches:
  - name: Stack with HashMap complement lookup
    file: solutions/hashmap-complement.cs
    patterns: [Reverse Order Matching]
    variation: HashMap Complement
    constructs: [stack, dictionary]
    ds-used: [string, stack, hashmap]
    ds-notes:
      stack: "LIFO bracket matching"
      hashmap: "closing -> opening bracket complement map"
    time: "O(n)"
    space: "O(n)"
  - name: Stack with direct complement push
    file: solutions/complement-push.cs
    patterns: [Reverse Order Matching]
    variation: Complement Push
    constructs: []
    ds-used: [string, array]
    ds-notes:
      array: "char[] used as manual stack; push expected closing bracket on open"
    time: "O(n)"
    space: "O(n)"
---

# Valid Parentheses - Solutions

## Approaches

### Approach 1: Stack with HashMap complement lookup
**Code:** [hashmap-complement.cs](solutions/hashmap-complement.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** Use a stack to track opening brackets. Push openers onto the stack, and when a closing bracket appears, pop the stack and check if the popped opener maps to the current closer using a HashMap. If the stack is empty when a closer appears, or the complement does not match, it is invalid. After processing all characters, the string is valid only if the stack is empty.

---

### Approach 2: Stack with direct complement push
**Code:** [complement-push.cs](solutions/complement-push.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** Instead of pushing the opening bracket and translating later via a HashMap, push the expected closing complement directly onto the stack. When a closing bracket arrives, pop and compare directly - no map needed. Uses a char array as a manual stack with a top pointer for a different perspective on how stacks work under the hood.

---

## Patterns

- [Reverse Order Matching - Complement Push](../../patterns/reverse-order-matching.md#variation-complement-push) (Approach 1, 2) - Stack enforces LIFO ordering so the last bracket opened must be the first one closed. Both approaches use this core pattern; they differ only in what gets pushed (raw opener vs complement).

## Reflection

- **Key insight:** The complement bug was not a conceptual gap - I understood brackets need complements, but forgot to translate in code. Direct comparison of opener to closer will never match.
- **Future strategy:** Whenever something that came last needs to be matched first, reach for a stack. LIFO = reverse order matching.
- **Trickiest bug:** Comparing raw `buffer.Peek()` to `s[i]` instead of looking up the complement via a map. Silly mistake, but easy to make when coding fast.
