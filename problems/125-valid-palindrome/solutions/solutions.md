---
problem: 125
problem-title: Valid Palindrome
difficulty: Easy
category: solutions
patterns: [Two Pointers]
constructs: [char-methods, linq]
ds-used: [string, array]
algorithms: []
concepts: [palindrome]
approaches:
  - name: Inward Two Pointer
    file: inward-two-pointer.cs
    patterns: [Two Pointers]
    variation: Clean Then Scan
    constructs: [linq, char-methods]
    ds-used: [string, array]
    ds-notes:
      string: "cleaned and lowercased before scan"
      array: "char[] built from LINQ-filtered string"
    time: "O(n)"
    space: "O(n)"
  - name: Two Pointer Without Extra Space
    file: two-pointer-without-extra-space.cs
    patterns: [Two Pointers]
    variation: In-Place Pointer
    constructs: [char-methods]
    ds-used: [string]
    time: "O(n)"
    space: "O(1)"
  - name: ASCII Two Pointer
    file: ascii-two-pointer.cs
    patterns: [Two Pointers]
    variation: ASCII Range Check
    constructs: []
    ds-used: [string]
    time: "O(n)"
    space: "O(1)"
---

# Valid Palindrome - Solutions

## Approaches

### Approach 1: Inward Two Pointer
**Code:** [inward-two-pointer.cs](inward-two-pointer.cs)
**Time:** O(n) | **Space:** O(n)
**Thinking:** Clean the string first by filtering non-alphanumeric characters and lowercasing, then use inward two pointers to compare characters from both ends toward the center. Simple logic once the string is normalized, but allocates a new string.

---

### Approach 2: Two Pointer Without Extra Space
**Code:** [two-pointer-without-extra-space.cs](two-pointer-without-extra-space.cs)
**Time:** O(n) | **Space:** O(1)
**Thinking:** Same inward two-pointer structure but skip non-alphanumeric characters on the fly using inner while loops. No cleaned string allocated. Key realization: inner loop bounds should be relative (left < right) not absolute (array edges), which keeps the rest of the logic unconditional and clean.

**Mistakes:**
- **Wrong boundary in inner while loops** - Used `start < s.Length-1` and `end > 0` (array bounds) instead of `start < end` and `end > start` (relative bounds). This caused a cascade: needed an extra `IsLetterOrDigit` guard before comparison, and had to conditionally advance both pointers at the end. Once the boundary was corrected, all extra conditions became dead code.
- Root cause: thinking "don't go out of bounds" instead of "don't cross the other pointer."

---

### Approach 3: ASCII Two Pointer
**Code:** [ascii-two-pointer.cs](ascii-two-pointer.cs)
**Time:** O(n) | **Space:** O(1)
**Thinking:** Same structure as Approach 2 but replaces built-in char methods with raw ASCII arithmetic. IsAlphaNumeric checks numeric ranges (48-57, 65-90, 97-122). ToLower adds 32 - the exact gap between uppercase and lowercase ASCII values. Valid because the constraints guarantee printable ASCII only.

---

## Patterns

Two Pointers - Symmetry Check (All approaches) - inward two pointers converging from both ends to verify string symmetry

## Reflection

**Key insight:** The inner while loop boundary controls everything. Using relative bounds (left < right) instead of array bounds eliminates the need for extra guards and conditional advancement - the outer while handles termination naturally.
**Key mistake:** Approach 2 started with too many edge case conditions. The algorithm was actually simple once the boundary condition was correct.
**Future strategy:** For palindrome checks, reach for two-pointer first. Skip-and-compare is cleaner than clean-then-compare when space matters.
- **Notes Insights:**
  - The two pointers define their own boundary. Once you use relative bounds (left < right) in the inner loops, the outer while handles termination and the rest of the logic becomes unconditional.
  - A clean solution is usually a sign that the boundary condition is correct. Needing many extra guards often means the wrong edge is being checked.
- **Mantras:**
  - "Use the correct boundary condition - even if wrong bounds pass some cases, they create regression risk."
  - "When your solution needs too many conditions, the boundary is probably wrong - not the logic."
