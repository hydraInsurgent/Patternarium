---
problem: 21
problem-title: Merge Two Sorted Lists
difficulty: Easy
category: solutions
patterns: [Two Pointers]
constructs: []
ds-used: [linked-list]
algorithms: []
techniques: [head-tail-tracking]
concepts: []
approaches:
  - name: Iterative Splicing
    file: iterative-splicing.cs
    patterns: [Two Pointers]
    variation: Parallel Merge
    constructs: []
    ds-used: [linked-list]
    techniques: [head-tail-tracking]
    time: "O(m + n)"
    space: "O(1)"
  - name: Recursive Splicing
    file: recursive-splicing.cs
    patterns: [Two Pointers]
    variation: Parallel Merge
    constructs: []
    ds-used: [linked-list]
    techniques: [head-tail-tracking]
    time: "O(m + n)"
    space: "O(m + n)"
---

# Merge Two Sorted Lists - Solutions

## Approaches

### Approach 1: Iterative Splicing
**Code:** [iterative-splicing.cs](iterative-splicing.cs)
**Time:** O(m + n) | **Space:** O(1)

**Thinking:** Walk both sorted lists with one pointer each. At every step, pick whichever current head is smaller (tie -> take from list1) and splice it onto a tracked tail. The output reuses the existing nodes - no new allocation - because the problem says "splicing." Track two variables: `mergedList` (frozen at the head, used as the return value) and a moving `output`/`tail` pointer that advances each iteration. Continue while either source still has nodes; when one is exhausted, take from the other unconditionally. The Assign helper centralizes the four null/non-null cases of (list1, list2) and uses `ref` for list1/list2 (read-then-write to advance the source) and `out` for the picked node (pure write).

**Mistakes:**
- Misframed the problem: assumed new nodes had to be allocated. The word "splicing" in the statement was the key signal that nodes are reused. Required a re-read prompt to spot.
- Single-variable confusion: tried using one `output` as both saved head and moving cursor. The cursor must advance each iteration, which loses the head reference. Solution: a frozen `mergedList` for return, separate moving `output`/`tail`.
- V1: pre-loop null deref. `list1.val <= list2.val` without null-checking either side crashes on `[]` inputs. Caused by assuming both inputs are non-empty when seeding the head outside the loop.
- V2: `output.next = null; output = output.next` orphans every pick after the first, leaving `mergedList` holding only one node. Caused by confusing the cursor's role with an end-of-list sentinel.
- O(n) vs O(m + n): two independent inputs do not collapse to one variable. Big-O literacy gap, surfaced after tracing how many nodes each list contributes.
- Helper attempt with `out` everywhere: `out` forbids reading before assigning, but the helper reads `list1 == null` first thing, triggering "use of unassigned out parameter". Correct shape: `ref` for list1/list2 (read AND write), `out` for output (pure write). C# parameter passing semantics must be matched to actual usage in the body.

---

### Approach 2: Recursive Splicing
**Code:** [recursive-splicing.cs](recursive-splicing.cs)
**Time:** O(m + n) | **Space:** O(m + n)

**Thinking:** Same Assign helper, called recursively. Each frame writes the picked node directly into the previous tail's `.next` field via `out tail.next`, then advances the local `tail` to the just-placed node so the next frame extends the chain from there. The base case (both lists null) leaves the last picked node's `.next` untouched - which happens to be null because it was the last node of its source list. Side effect builds the chain; the return value is unused. Space is O(m + n) due to one call-stack frame per picked node.

**Mistakes:**
- Initial frame error: first instinct was to return `bool` and maintain a merged pointer externally. Wrong abstraction for list-building recursion - each parent frame needs a value (a node) wired into the chain, not a side-effect signal.
- `ref output.next` treated as a moving cursor: it is an alias to ONE storage slot (the original `output`'s `.next` field), not a cursor. Every recursive frame's `tail` parameter aliased the same slot; every write overwrote it; the final `tail = tail.next` wrote null. Surfaced from a `Console.WriteLine` dry run that showed `tail` walked 1, 2, 3, 4, 4 correctly while `output.next` ended empty. Reading the code looked plausible because `ref` "passes a reference" - the realization that the reference points to ONE slot, not a moving position, only clicked once the stack-overwrite pattern was named explicitly.
- Dropping the wiring step along with the bad cursor advance: the next attempt removed both `tail = tail.next` (the bug) AND the (different) wiring move that connects the new pick to the previous tail's `.next`. Each frame picked the right node into a local but nothing connected it onto the chain. Conflated two distinct moves and deleted both at once.

---

## Patterns

- Two Pointers - Parallel Merge (Approaches 1, 2) - one pointer per sorted sequence, advance the pointer whose head is smaller, splice the picked node into the output. Iterative drives the walk with a loop; recursive drives it with the call stack - same pattern, two implementations.

## Techniques

- Head-Tail Tracking (Approaches 1, 2) - freeze the picked first node as `mergedList` for the return value; advance a separate `output`/`tail` cursor through the chain. One variable for both jobs would lose the head once the cursor moves.

## Reflection

- **Key insight:** `out` vs `ref` distinction in C# - `out` is "I'll fill this, don't bother giving me a value", `ref` is "caller and method share this slot, both can read and write." The recursive wiring-step realization was the harder and stickier of the two: you cannot just pick into a temp; you need a line that connects the new pick to the previous tail's `.next`.
- **Future strategy:** When two sorted sequences need to merge into one, the first move is Parallel Merge under Two Pointers - one pointer per sequence, advance the pointer whose head is smaller, pick that head into the output. The phrase "compare two heads, advance the smaller" is the trigger.
- **Slot-vs-cursor mental model:** `ref` to a field is an alias to one storage slot, not a moving cursor. Recursion through a ref-to-field parameter does not "track" a moving position - every frame writes to the same slot. To build a chain, each frame must wire onto a different slot (the previous tail's `.next`), which means the helper writes into the field directly via `out tail.next`, and a separate value-typed local advances the cursor between frames.
- **Trickiest bug:** The recursive `ref output.next` aliasing bug only surfaced from the `Console.WriteLine` dry run. The log showed `tail` walked 1, 2, 3, 4, 4 correctly but `output.next` was empty at the end. **Lesson:** when state seems "almost right but disconnected," log the field that should be changing AND the variable doing the writing - if the field stops at one value while the variable walks through many, you have an aliasing/slot-vs-cursor problem.
