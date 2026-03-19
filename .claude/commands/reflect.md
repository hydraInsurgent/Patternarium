# /reflect - Trigger Reflection Step

Prompt the user to reflect on what happened in this problem session.

## Behavior

Ask these questions one at a time, waiting for the user's response before moving to the next:

1. "What mistake did you make or almost make in this session?"
2. "What is the key insight you want to remember next time you see a similar problem?"
3. "What pattern did you use? Can you state it as a rule you would tell someone else?"
4. "Is there anything that still feels unclear or fuzzy?"

## Logging

After the user answers the reflection questions, write their responses to the `## Reflection` section of `active-problem.md`.

## After Reflection

- If the user identified a mistake worth recording, suggest updating LESSONS.md
- If the user stated a clear pattern mantra, acknowledge it and confirm it matches the pattern in `patterns/`
- Suggest running `/save-problem` to persist everything

## Rules

- Do not skip to notes-writing - let the user verbalize first
- If the user's reflection is shallow ("I learned how to use HashMap"), ask them to be more specific ("What rule would you now apply when you see a pair sum problem?")
- Keep this conversational, not like a quiz
