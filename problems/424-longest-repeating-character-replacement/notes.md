# Longest Repeating Character Replacement - Notes

## Mistakes Made

### Approach 1 - Brute Force
- **Global vs local maxFreq** - maxFreq must reset per outer loop (per starting index). A global max causes `windowSize - globalMax` to go negative, making every window look valid.
- **Full map scan for maxFreq update** - only one character changes per step. Only compare `max(maxFreq, freq[s[j]])` - no need to scan the full map.

### Approach 2 - Sliding Window
- **Starting at l=0, r=n-1** - tried to start with the full string and shrink inward. Must start at l=0, r=0 and grow outward.
- **Checking validity before adding s[r]** - the window must be fully formed (s[r] added to freq) before validity is checked.
- **Proposed decrementing maxFreq on shrink** - breaks on ties: if {A:3, B:3} and A exits, actual max is still 3 (B). Fix: never decrement maxFreq.
- **Single if instead of while for shrinking** - one expansion can require multiple contractions. Must shrink until the condition holds.
- **Moving l++ and r++ together when invalid** - just shifts the same-size invalid window; does not fix invalidity.
- **Forgot right++** - infinite loop; the right pointer must advance each outer loop iteration.
- **Did not decrement freq[s[left]] before left++** - broke window consistency; map no longer reflected the actual window contents.
- **Wrong boundary `right < s.Length - 1`** - off-by-one, missed the last element.
- **Inverted condition** - accidentally shrunk when valid instead of when invalid; caused index drift and index-out-of-bounds crash.

## Key Insights

- `windowSize - maxFreq <= k` is character-agnostic - only the count of the most frequent character matters, not which character it is.
- maxFreq is monotonically non-decreasing. Stale is safe: any length we accept was achievable when maxFreq was genuinely that high.
- Sliding window is O(n) because both pointers only move forward. Locally it feels like expand-shrink-expand, but globally each pointer moves at most n times.
- A window of size 1 always satisfies `1 - 1 = 0 <= k`, so left can catch up to right but never cross it.

## Mantras

- "Add first, check second." - window state updates before validity check.
- "Grow right, shrink left, never reverse."
- "Stale maxFreq is safe - it only lets through sizes already achievable."

## Patterns Used

- **Sliding Window - Shrink-Based** (Approaches 2 and 3)

## Connected Problems

- **3 - Longest Substring Without Repeating Characters** - same sliding window structure, different validity condition (`no repeats` vs `windowSize - maxFreq <= k`)
- **76 - Minimum Window Substring** - sliding window with more complex validity condition; find smallest valid window instead of largest
