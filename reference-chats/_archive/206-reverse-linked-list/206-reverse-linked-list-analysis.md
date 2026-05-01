---
problem: 206
title: "Reverse Linked List"
slug: 206-reverse-linked-list
source: LeetCode
difficulty: Easy
import-file: reverse-linked-list.txt
category: analysis
---

# Analysis: Reverse Linked List

**Chat covers:** Iterative reversal reasoned through verbally up to the final three-pointer formulation. Chat ends with Claude asking the user to dry-run `[1 -> 2 -> 3]` - user did not answer in chat ("couldn't explain but it lined up when coding"). Code was written separately in `active-solution.cs` after the chat. Complexity was never discussed in the chat. Recursive approach is the planned next step, not yet started.

---

## Thinking Journey

The user opened with their existing mental model of a linked list (value + `next` pointer) and asked whether C# has a built-in equivalent. Claude confirmed there is none for LeetCode-style problems and showed the `ListNode` class. The user's framing of the core operation - reverse the pointer direction at each node - was correct from the start.

The first wedge was Claude's question: "when you flip a node's next pointer, what do you lose access to?" The user reasoned in their own words ("the next person's next person") and identified that overwriting `next` destroys forward access, so you must save it before reassigning.

Claude then asked about the first node specifically - "what does its `next` point to after reversal?" The user answered correctly: null, since the first node becomes the new tail. They flagged this as something they had originally missed.

The longest section of the chat was the user attempting pseudocode and **getting tangled with variable naming**. Across multiple paragraphs the names shifted: `current`, `current.next`, `current head`, `current next`, `coming next`, `temp`, `head.next.next`, `head.next.next.next` - each meaning something different on adjacent lines. The user repeatedly restarted the pseudocode and never landed a stable formulation.

Claude pushed back: "you're tangled... three variables is all you need. Name the three things you need to track and what each holds." The user attempted twice and only described two distinct things plus a temp.

Claude reframed: "you need to know where you came from, where you are, and where you're going. Which does your list not give you for free?" That clicked - the user identified `last` as the variable they had been missing, and articulated a final pseudocode using `last`, `head`, and `next`.

The chat ends at the next ask: dry-run `[1 -> 2 -> 3]` and report the variable values after the first iteration. The user **did not answer this in the chat**. According to the user's note, they could not explain it verbally but the logic lined up once they wrote the code.

Complexity was never discussed in the chat. It must be confirmed live.

---

## Core Formula / Key Condition

The chat surfaced this as a verbal model rather than a written formula:

- Three pointers: `last` (where you came from), `head` (where you are), `next` (where you're going)
- The list gives you `head` and `next` for free; `last` is the one you have to manufacture
- Initialize `last = null` so the first node naturally points to null after reversal

The exact loop body order was not pinned down in the chat - the user's final pseudocode was a paragraph of natural language, not a sequenced list. The order that was eventually committed to code (in `active-solution.cs`) is: save `next`, rewrite `head.next`, advance `last`, advance `head`. That order is correct, but its correctness was not articulated in the chat - it became visible to the user only at code-writing time.

---

## Approaches

### Approach 1: Iterative (Three-Pointer Reversal)
**Complexity:** Not discussed in chat
**Structure:** Initialize `last = null`. While `head != null`: save `head.next` into `next`, set `head.next = last`, slide `last` to `head`, slide `head` to `next`. Return `last`.
**Key optimization / insight:** No node allocation - only the existing `next` field is rewritten in place. Initializing `last = null` makes the "first node points to null" requirement implicit; no special-case for the head.

### Approach 2: Recursive
**Status:** Not started. User noted recursion as the planned next approach. The chat does not cover it - the user said early on, "I don't have any idea of recursion as of now."

---

## Mistakes and Lessons

| Mistake | Category | What happened |
|---------|----------|---------------|
| Variable naming kept shifting mid-thought (`current`, `current.next`, `current head`, `current next`, `coming next` all meant different things across adjacent lines) | Variable lifecycle | Each restart of the pseudocode redefined what `current` held; the user lost track of which slot held which node and could not converge on stable code |
| Tried to formulate the loop with two variables plus a temp | Mental model carryover | The user treated `last` as derivable rather than as a third independent pointer. The "where you came from / where you are / where you're going" reframe was needed before the third variable was nameable |
| Tried to explicitly set `head.next = null` as a special pre-loop step for the first node | Unnecessary complexity | This is handled implicitly by initializing `last = null`; the first iteration writes `head.next = last`, which is null. No special case needed |
| Reached two levels deep (`current.next.next`, `head.next.next.next`) in pseudocode | Variable lifecycle | Chained `.next.next` made the code unreadable. Collapsing to three named pointers (one node per name) fixed it |
| Could not verbally trace `[1 -> 2 -> 3]` when asked - only verified the logic by writing code | Mental model not yet portable | The mechanics work but the user cannot yet narrate them without the code in front of them. Worth a live dry-run to close this gap |

---

## Key Insights for Pattern Library

- **"What do you lose access to when you reverse a pointer?"** is a generic wedge for any in-place pointer-rewrite problem. The answer is always the variable that needs to be saved before the rewrite.
- **"Where you came from / where you are / where you're going"** is the universal frame for in-place linked list traversal. The list hands you the middle and the front-of-future for free; the trailing pointer (`last` / `prev`) is the one you manufacture.
- The pattern `last = null` + four-statement loop body is a memorable unit for in-place linked-list reversal. The user has the unit but not yet the verbal narration of it.

---

## Constructs Used

- `ListNode` (LeetCode-provided) - singly-linked node with `int val` and `ListNode next`. The chat introduced this class explicitly for the first time.
- `while` loop with `head != null` guard
- Three local pointer variables (`last`, `head`, `next`)

---

## Pattern Signals

- Problem mutates pointers without allocating new nodes -> in-place pointer manipulation
- Direction of traversal must reverse -> a trailing pointer (`prev` / `last`) is needed
- Reading and overwriting the same field in one step -> save the read value before the write

The pattern this problem demonstrates does not yet have a file in `patterns/`. Candidate names: "in-place linked list reversal", "three-pointer linked list traversal". Confirm at `/save-problem` time.

---

## Connections to Other Problems

No connections came up in the chat.

---

## Session Checklist

### Overall
- [x] Problem statement understood
- [x] Mental model solidified (verbally landed on the three-pointer frame)
- [ ] Reflection completed
- [ ] Saved to repo (`/save-problem`)

### Approach 1: Iterative (Three-Pointer Reversal)
- [x] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user (not discussed in chat)
- [ ] Space complexity confirmed by user (not discussed in chat)
- [ ] Dry run completed - the chat ended on this exact ask and the user could not narrate it. Trace `[1 -> 2 -> 3]` and report `last`, `head`, `next` after each iteration.
- [ ] Verify: empty list (`head = null`) - does the loop guard handle it cleanly? What does the function return?
- [ ] Verify: single-node list (`[1]`) - one iteration with `next = null`. Walk through the four statements and confirm the result.
- [ ] Articulate: why does the four-statement order matter? What breaks if you swap `last = head` and `head.next = last`?

### Approach 2: Recursive
- [ ] Approach reasoned through (user has no recursion exposure yet)
- [ ] Recursion base case identified (when does the recursion stop?)
- [ ] Pointer-rewrite logic identified (what does each frame do on the way back up?)
- [ ] Code written in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user (note: recursion adds O(n) call-stack space - new vs. iterative)
- [ ] Dry run completed
