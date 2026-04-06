# Lessons Learned

<!-- DSA-specific lessons only. Updated after mistakes or conceptual breakthroughs. -->
<!-- Active lessons are read every session. When a lesson is internalized (user solves a problem without making that mistake), move it to Graduated. -->

## Conceptual Mistakes
<!-- Misunderstandings about how an algorithm or pattern works -->


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

## Pattern Misidentifications
<!-- Times the wrong pattern was chosen, or a pattern was missed -->


---

## Graduated
<!-- Lessons the user has demonstrated mastery of. Reference only, not read every session. -->
