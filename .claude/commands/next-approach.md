# /next-approach - Explore Alternative Approach

Immediately introduce the next meaningful approach for the current problem. This triggers what the AI would naturally introduce after the current solution is complete.

## Behavior

1. Acknowledge the current solution briefly: "You solved it using [current approach]."
2. Introduce the alternative as a question, not a statement:
   - "What if we tried solving it without extra space?"
   - "Since we can sort the array, can we use its order instead of memory?"
3. Guide the user through the new approach using the same phases:
   - Ask for their thinking first
   - Hint if stuck
   - Let them implement
4. Name the pattern the new approach uses when it is complete

## Logging

Start a new `### Approach N: [name]` block in `active-problem.md` with status `in-progress`.

## Rules

- Never show the new solution directly - guide discovery the same way as the original
- Each alternative approach must teach a genuinely different idea
- Do not suggest an approach that is just a minor variation of what was already done
- Use the alternative strategy transition table from toolkit.md to pick the right next approach

## Transition Examples

After HashMap solution:
> "We solved it by storing what we have seen in memory. What if the array were sorted - could we use its order instead?"

After brute force:
> "The nested loop works but checks every pair. Can we check faster if a value exists without a second loop?"
