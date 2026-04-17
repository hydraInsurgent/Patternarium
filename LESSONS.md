# Lessons Learned

<!-- DSA-specific lessons only. Updated after mistakes or conceptual breakthroughs. -->
<!-- Active lessons are read every session. When a lesson is internalized (user solves a problem without making that mistake), move it to Graduated. -->

## Conceptual Mistakes
<!-- Misunderstandings about how an algorithm or pattern works -->

- **Sliding window grows from l=0, r=0 - not from the full array** - first instinct was to start at l=0, r=n-1 and shrink inward. Sliding window always starts narrow and expands right. (Longest Repeating Character Replacement, Approach 2)
- **Window state must update before validity check** - adding s[r] to the freq map and updating maxFreq must happen before checking if the window is valid. The window must be fully formed first. (Longest Repeating Character Replacement, Approach 2)
- **Monotonic tracking variable: never decrement on shrink** - when a running max tracks only increases (e.g., maxFreq), decrementing it when an element exits breaks correctness on ties. Let it stay stale - any window size it allows was already achievable when the variable was genuinely that high. (Longest Repeating Character Replacement, Approach 2)


## Code Mistakes
<!-- Bugs caused by implementation errors, not conceptual gaps -->

- **C# strings are immutable** - `s.Replace()` returns a new string, does not modify in place. Must assign back: `s = s.Replace(...)` (Roman to Integer, Approach 3)
- **C# local variables need explicit initialization** - class fields default to 0, but local `int num;` without `= 0` won't compile when used with `+=` (Roman to Integer, Approach 3)
- **char vs mapped value** - `int current = s[i]` gives ASCII code, not the dictionary-mapped value. Must use `map[s[i]]` (Roman to Integer, Approach 1)
- **Variable-step loop leftover** - when consuming 1 or 2 elements per iteration, always check if the final element was already consumed before adding it again (Roman to Integer, Approach 4)
- **Boolean array: check value, not neighbor** - when scanning a presence array, use `if(present[i])`, not `if(current == last)`. Neighbor comparison catches false==false as consecutive. (Longest Consecutive Sequence, Approach 3)
- **Reset to 0, not 1, after a gap in a count scan** - resetting count to 1 instead of 0 makes the first element after a gap count as 2. After reset, the next true/match does count++ to reach 1. (Longest Consecutive Sequence, Approach 3)


- **Two-pointer inner loop: use relative bounds, not array bounds** - when skipping characters inside a two-pointer loop, bound the inner while with `left < right` (relative), not `left < s.Length-1` (array edge). Wrong bounds pass some tests but force extra guard conditions and conditional advancement that become dead code once the boundary is corrected. (Valid Palindrome, Approach 2)
- **Sliding window: start must never move left** - when using a HashMap of last-seen indices to jump start, a char's previous occurrence may be before the current window. Updating start to that index+1 would move it backward and corrupt the window. Guard with `Math.Max(start, lastSeen+1)` or an explicit if condition. (Longest Substring Without Repeating Characters, Approach 2)
- **start++ vs index jump in sliding window** - incrementing start by 1 on each repeat keeps the O(n) loop but misses the key optimization: you can jump directly to lastSeenIndex+1 and skip all the stale positions in one move. (Longest Substring Without Repeating Characters, Approach 2)

- **Variable scope in C#** - variables declared inside `if/else {}` blocks don't exist outside those braces. Declare before the block, assign inside. (Longest Palindromic Substring, Approach 1)
- **Don't detect what you can compute** - writing complex even/odd detection to avoid calling a function twice. The detection grew complicated and broke edge cases. Running both paths and comparing is simpler and the same complexity. (Longest Palindromic Substring, Approach 1)
- **Pseudocode before code** - skipping pseudocode led to multiple logic errors (inverted while condition, wrong center index, conflicting blocks). Tracing all scenarios in pseudocode first would have caught most bugs before writing a line of code. (Longest Palindromic Substring, Approach 1)

- **Presence array: mark the value, not the position** - `seen[nums[i]] = true`, not `seen[i] = true`. Using the loop index marks positions 0..n-1 in order regardless of array contents, so slot n is always empty and the code always returns n. (Missing Number, Approach 1)

- **HashSet vs HashMap - pick the lighter tool** - if you only need to know whether a value exists, use HashSet. Only reach for HashMap when you need to store something alongside the key (an index, a count, a mapped value). Using HashMap for existence-only problems adds unnecessary overhead. (Contains Duplicate, Approach 1)

- **Compare complements, not raw characters** - when matching pairs (brackets, tags), the opener and closer are different characters. `'(' != ')'` is always true. You must translate via a map or push the complement upfront. (Valid Parentheses, Approach 1)
- **Sliding window inner loop: use while(invalid), not if(valid)** - the shrink condition checks when the window is invalid. Inverting it (shrinking when valid) drifts the indices and causes crashes. Check the exact polarity before coding. (Longest Repeating Character Replacement, Approach 2)
- **Every traversal pointer must advance in the loop** - forgetting right++ in a while(right < n) sliding window creates an infinite loop. Each outer-loop pointer must have a guaranteed increment path. (Longest Repeating Character Replacement, Approach 2)

## Pattern Misidentifications
<!-- Times the wrong pattern was chosen, or a pattern was missed -->


---

## Graduated
<!-- Lessons the user has demonstrated mastery of. Reference only, not read every session. -->
