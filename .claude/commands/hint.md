# /hint - Escalate Hint Level

Give the user the next level hint for the current problem. Do not reveal the full solution.

## Behavior

Determine what hint level the user is currently at based on the conversation history, then escalate one level:

- Level 1: Conceptual nudge - ask what value they actually need to find
- Level 2: Directional hint - ask about remembering or storing previous values
- Level 3: Pattern hint - name the pattern category without naming the exact data structure
- Level 4: Near-solution hint - describe the key data structure and how to use it
- Level 5 (Bridge): Before revealing the full solution, try one or both:
  - Guided dry run: walk through a concrete example step by step, ask the user to generalize
  - Code skeleton: provide the structure without the key logic, ask the user to fill in the gap

If already at Level 5 and user is still stuck, ask if they want the full solution (/solution).

## Logging

After giving the hint, append `- Level N: "[hint text]"` to `#### Hints Given` in the current approach block of `active-problem.md`.

## Rules

- Never skip levels
- Never give the full solution unless Level 5 bridge has been tried and user confirms they want it
- Keep the hint as a question when possible
- One hint per invocation - do not give multiple hints at once

## Example Escalation for Two Sum

- Level 1: "What exact value are you looking for when you pick the number 2?"
- Level 2: "What if we could check whether that value already exists without scanning the whole array again?"
- Level 3: "This is a case where storing things we have already seen gives us instant lookup. Which data structure does O(1) lookup?"
- Level 4: "Try storing number -> index in a Dictionary as you scan. Check if the complement exists before you store each number."
