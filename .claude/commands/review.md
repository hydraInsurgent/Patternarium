# Review - Test Pattern Recall

Pick a past problem from `master-index.json` and test the user's recall.

## How to pick a problem

1. Read `master-index.json` to see all solved problems and their patterns
2. Prefer problems the user has not revisited recently
3. If a pattern appears across multiple problems, pick the one solved longest ago
4. If the user specifies a pattern (e.g., `/review hashmap`), pick a problem that uses that pattern

## How to run the review

1. State the problem name and constraints (read from `problems/<slug>/problem.md`)
2. Ask: "Can you solve this using [pattern]? Try without looking at your notes."
3. Follow the normal workflow from Phase 2 onward (explore thinking, guide, etc.)
4. After the user solves it (or gives up), compare with their original solution
5. Note what they remembered vs. what they forgot

## After the review

- If the user solved it cleanly: reinforce the pattern, note the retention win
- If the user struggled: identify what was forgotten, suggest reviewing `patterns/<pattern>.md` and their `problems/<slug>/notes.md`
- Update `LESSONS.md` if a recurring gap is revealed
