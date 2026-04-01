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


## Pattern Misidentifications
<!-- Times the wrong pattern was chosen, or a pattern was missed -->


---

## Graduated
<!-- Lessons the user has demonstrated mastery of. Reference only, not read every session. -->
