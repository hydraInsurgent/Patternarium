# Longest Palindromic Substring - Notes

## Mistakes Made

### Approach 1 - Expand Around Center
- **Center from n/2 instead of i** - computed left/right from the string's midpoint instead of the current loop index. Root cause: confused "center of the string" with "current center being tested."
- **Variable scope** - declared left/right inside if/else blocks, not accessible outside. Root cause: C# block scoping - variables declared inside {} don't exist after the closing brace.
- **While condition inverted** - wrote `s[left] != s[right]` instead of `s[left] == s[right]`, so the loop ran only when there was a mismatch.
- **Boundary off-by-one** - used `left > 0` and `right < n-1`, stopping one step short of index 0 and the last index.
- **Two conflicting center selection blocks** - wrote a second if/else block that always ran after the first and overwrote left/right. Fixed by removing detection entirely.
- **Dead branch in exit analysis** - condition `!(left >= 0 || right <= n-1)` can never be true for valid indices. The exit check only needs two branches: mismatch and boundary.

## Key Insights
- Expand around center is more natural for palindromes than checking all substrings - the center is the source of truth
- When two possibilities exist (odd/even centers), no upfront detection replaces just running both
- Pseudocode and tracing all scenarios before coding would have caught most of the bugs above

## Mantras
- "Run both, compare after - don't detect in advance"
- "Pseudocode first, code second"

## Patterns Used
- **Two Pointers - Expand Around Center** (Approach 1)
