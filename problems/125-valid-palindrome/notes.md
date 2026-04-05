# Valid Palindrome - Notes

## Mistakes Made

### Approach 2 - Two Pointer Without Extra Space

- **Wrong boundary in inner while loops** - Used `start < s.Length-1` and `end > 0` (array bounds) instead of `start < end` and `end > start` (relative bounds). This caused a cascade: needed an extra `IsLetterOrDigit` guard before comparison, and had to conditionally advance both pointers at the end. Once the boundary was corrected, all extra conditions became dead code.
- Root cause: thinking "don't go out of bounds" instead of "don't cross the other pointer."

## Key Insights

- The two pointers define their own boundary. Once you use relative bounds (left < right) in the inner loops, the outer while handles termination and the rest of the logic becomes unconditional.
- A clean solution is usually a sign that the boundary condition is correct. Needing many extra guards often means the wrong edge is being checked.

## Mantras

- "Use the correct boundary condition - even if wrong bounds pass some cases, they create regression risk."
- "When your solution needs too many conditions, the boundary is probably wrong - not the logic."

## Patterns Used

- **Two Pointers** (Approach 1) - inward convergence after cleaning string
- **Two Pointers** (Approach 2) - inward convergence with on-the-fly skipping, O(1) space
- **Two Pointers** (Approach 3) - same as Approach 2 with ASCII arithmetic instead of built-in methods
