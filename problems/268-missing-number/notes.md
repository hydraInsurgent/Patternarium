# Missing Number - Notes

## Mistakes Made

### Approach 1 - Boolean Flag Array
- **flags[i] instead of flags[nums[i]]** - marked the loop position as seen, not the actual number. Result: slots 0..n-1 were always marked true, and slot n was always false, so the code always returned n regardless of input. Fix: use the value as the index - `seen[nums[i]] = true`.

## Key Insights
- Came in with the presence array approach immediately because the problem requires tracking absence across a known range - not just what was seen. That's the distinction between presence array and HashSet.
- XOR felt unintuitive from regular numbers. The two core properties (n XOR n = 0, n XOR 0 = n) are what make it click. Once you think in terms of bit flips, the logic becomes concrete.

## Patterns Used
- **Odd One Out** (all three approaches) - complete set expected, one element missing, use a property of the full set to isolate it
- **Presence Array - Boolean Presence** (Approach 1) - fixed-size boolean array indexed by value

## Mantras
- Mark the VALUE, not the position: `seen[nums[i]]`, not `seen[i]`
- When you need to check absence across a full range, presence array fits better than HashSet - HashSet only tracks what you've seen, not what's missing from the range
- Reach for XOR when two values should cancel each other out, or when the problem can be thought of in terms of bit flips
