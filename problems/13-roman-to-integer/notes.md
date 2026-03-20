# Roman to Integer - Notes

## Mistakes Made

### Approach 1 - Right-to-left scan
- Used `int current = s[i]` instead of looking up the mapped value - got char's ASCII code instead of roman numeral value
- Compared `last` (numeric) against `current` (char) - type mismatch in logic
- Loop condition `i > 0` skipped first character; fixed to `i >= 0`
- Condition `last >= current` was backwards for the initial case where last=0 - subtracted instead of added

### Approach 2 - Left-to-right scan
- Loop condition `i < s.Length - 1` skipped last character; fixed to `i < s.Length`

### Approach 3 - String replacement
- Forgot C# strings are immutable - `s.Replace()` returns a new string, doesn't modify in place
- Used uninitialized local `int num;` - C# requires explicit initialization for locals even though default is 0

### Approach 4 - Two-character lookahead
- Always added last character after loop even when it was already consumed by a subtraction pair
- Used `>` instead of `>=` for comparison - equal values (like XX) fell into subtraction branch

## Key Insights
- Trickiest bug was in Approach 4 - handling the end scenario when the last character may or may not have been consumed. Taught the importance of tracking index state with variable-step loops
- Right-to-left scan was the first instinct, but string replacement feels even more intuitive in hindsight
- Use neighbor comparison when dependencies are dynamic; use replacement when special cases are fixed and small in number

## Mantras
- "Did my loop handle everything, or is there a leftover?"
- C# strings are immutable - `Replace()` returns a new string
- C# local variables must be explicitly initialized before use

## Patterns Used
- Linear Scan - Neighbor Comparison (Approaches 1 & 2)
- Preprocessing - Normalize Before Compute (Approach 3)
- Chunked Iteration - Variable Step (Approach 4)
