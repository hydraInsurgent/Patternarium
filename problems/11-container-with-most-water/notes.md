# Container With Most Water - Notes

## Key Insights

- The pointer movement rule is the test for two pointers: if you can define a clear condition for when each pointer moves, the approach will work.
- Sliding window requires a validity condition on window *contents*. If the problem only cares about the two boundary positions (not what is between them), two pointers applies instead.
- Problem constraints are not just test input limits - they are information you can use inside your algorithm. The max height constraint (10^4) becomes a ceiling for early-exit pruning.

## Mantras

- "Two positions determine the answer? Ask: can I define when each pointer moves?"
- "Constraint = ceiling. If remaining capacity * max constraint can't beat your best, stop."

## Patterns Used

- **Two Pointers - Sorted Pair** (Approaches 2, 3) - converge from opposite ends, move the pointer that limits the value

## Connected Problems

- **424 - Longest Repeating Character Replacement** - contrast case: sliding window fits there because validity depends on window contents (character frequency inside the window). Here validity depends only on the two endpoints.
- **1 - Two Sum** - same converging two-pointer skeleton (start at ends, move one pointer per step based on a condition). Different decision rule: sum vs target vs which side limits height.
