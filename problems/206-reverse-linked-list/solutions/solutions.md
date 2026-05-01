---
problem: 206
problem-title: Reverse Linked List
difficulty: Easy
category: solutions
patterns: [In-Place Linked List Reversal]
constructs: []
ds-used: [linked-list]
algorithms: []
techniques: [trailing-pointer]
concepts: []
approaches:
  - name: Iterative (Three-Pointer Reversal)
    file: solutions/iterative-three-pointer-reversal.cs
    patterns: [In-Place Linked List Reversal]
    variation: Three-Pointer Reversal
    constructs: []
    ds-used: [linked-list]
    techniques: [trailing-pointer]
    time: "O(n)"
    space: "O(1)"
  - name: Recursive (Tail Recursion with Accumulator)
    file: solutions/recursive-tail-recursion.cs
    patterns: [In-Place Linked List Reversal]
    variation: Three-Pointer Reversal
    constructs: []
    ds-used: [linked-list]
    techniques: [trailing-pointer]
    time: "O(n)"
    space: "O(n)"
---

# Reverse Linked List - Solutions

## Approaches

### Approach 1: Iterative (Three-Pointer Reversal)
**Code:** [iterative-three-pointer-reversal.cs](iterative-three-pointer-reversal.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Reversing pointer direction at each node. Overwriting a node's `next` destroys forward access, so the original `next` must be saved first. The first node becomes the new tail so its reversed pointer must be null. Three pointers track state: `last` (where we came from), `head` (where we are), `next` (where we're going). The list gives `head` and `next` for free; `last` is the one that must be manufactured, initialized to null so the first node's reversal is handled implicitly.

**Mistakes:**
- Variable naming kept shifting across multiple pseudocode attempts - the same name held different things on adjacent lines, making convergence impossible. Fixed by committing to three named slots before writing any code.
- Framed the operation as a "swap" (two-way), which locked the mental model into 2 variables + a temp. Once reframed as a "rewrite" (one-way), the third independent pointer (`last`) became visible. The wrong verb dictated the wrong variable count.
- Tried to set `head.next = null` as a pre-loop special case for the first node - unnecessary; initializing `last = null` makes the first iteration handle it implicitly.
- Drafted a second version using `while (next != null)` with `next` initialized before the loop - fails on the single-node case (next is null before loop entry, returns null instead of the node).

---

### Approach 2: Recursive (Tail Recursion with Accumulator)
**Code:** [recursive-tail-recursion.cs](recursive-tail-recursion.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** Same three-pointer reversal as iterative, but recursion replaces the while loop. `last` is passed as an accumulator parameter carrying the reversed prefix down the call chain. Each frame does one unit of work (save next, rewrite pointer, advance last) and recurses on the remaining list. Base case: `head == null` returns `last`, which is the new head of the fully reversed list. Space is O(n) because n call-stack frames pile up before any unwind - each paused frame holds its own copy of `head`, `last`, and `next`.

---

## Patterns

- In-Place Linked List Reversal - Three-Pointer Reversal (Approaches 1, 2) - walk the list once and rewrite each node's pointer backward using three tracked positions: trailing, current, and lookahead

## Techniques

- Trailing Pointer (Approaches 1, 2) - maintain a pointer one step behind current to enable backward reference without an extra data structure

## Reflection

- **Key insight:** Before overwriting a pointer, save what it currently points to. Overwriting `head.next` destroys forward access to the rest of the list - this is the single rule behind why three pointers are needed.
- **Pattern mantra:** To reverse a linked list, walk it once and rewrite each node's `next` to point backward. You need three positions: where you came from, where you are, where you're going. Iterative and recursive are just two ways of driving the walk - the rewrite logic is identical.
- **Root mistake:** Framed the operation as a "swap" (two-way), which mentally locked into 2 variables + a temp. Once reframed as a "rewrite" (one-way), the third independent pointer (`last`) became visible.
- **Future strategy:** When seeing an in-place pointer rewrite, ask: "what does this currently point to, and do I still need it?" If yes, save it before overwriting.
- **Recursion takeaway:** Three components - base case, unit of work, smaller call. Space is not free: n call-stack frames pile up before unwinding, so a recursive solution mirroring an O(1) iterative one becomes O(n) space.
- **Trickiest bug:** Alternative iterative version using `while (next != null)` with `next` initialized before the loop. Single-node case fails - `next` is null before the loop, function returns null. The original `while (head != null)` is the clean form.
