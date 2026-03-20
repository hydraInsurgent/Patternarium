Also, tell me now where all the files should be updated. # Dry Run Template

A dry run is a step-by-step mental simulation of code using a concrete input. It is the most reliable way to find bugs before or after running code.

## Format

1. Pick a small, concrete input (e.g., `nums = [2, 7, 11, 15]`, `target = 9`)
2. List the variables being tracked (e.g., `i`, `map`, `needed`)
3. Step through iteration by iteration
4. At each step, write the current state of all tracked variables
5. Mark where expectation diverges from reality

## Example: HashMap Two Sum

Input: `nums = [2, 7, 11, 15]`, `target = 9`

| Step | i | nums[i] | needed | map contains needed? | map (after step) |
|------|---|---------|--------|---------------------|------------------|
| 1 | 0 | 2 | 7 | No | {2: 0} |
| 2 | 1 | 7 | 2 | Yes (index 0) | {2: 0, 7: 1} |
| Result: return [0, 1] |

## When to Dry Run

- Before writing code (to verify your approach works)
- When code produces wrong output (to find where it breaks)
- When you are unsure about edge cases (empty input, duplicates, single element)

## Tips

- Use the smallest input that exercises the logic
- Track every variable, not just the "important" ones
- If the bug is not obvious, try an input that should fail (edge case)
