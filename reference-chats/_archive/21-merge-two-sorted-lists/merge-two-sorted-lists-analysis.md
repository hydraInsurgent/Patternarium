---
problem: 21
title: "Merge Two Sorted Lists"
slug: merge-two-sorted-lists
source: LeetCode
difficulty: Easy
import-file: merge-two-sorted-linked-lists.txt
category: analysis
---

# Analysis: Merge Two Sorted Lists

**Chat covers:** Full reasoning of the iterative splicing approach (head/tail pointer logic, complexity), and the start of the recursive approach (base case, return type) before the chat was cut off. No code was written inside the chat - all three iterative attempts in `active-solution.cs` were typed by the user separately.

---

## Thinking Journey

The user started with a near-correct restatement: take elements from both lists, compare, build a sorted output. But the first instinct was to **create new nodes** (allocating a fresh list and copying values). The single nudge that corrected this was pointing back at the word "splicing" in the problem statement. Once that landed, the user immediately saw that the output reuses the existing nodes and only rewires `next` pointers - so space drops to O(1).

The next round of confusion was about how to **track both the head and the moving tail**. The user initially talked about "output" as one variable doing two jobs (the head to return, and the cursor that advances). The question "you need to return the head but also keep appending to its tail - how are you tracking both?" forced the split into two variables: `mergedList` (saved head) and `output`/`tail` (the moving pointer).

Within that split, the user kept oscillating on **what the tail points to before the loop starts**. Initial idea: `null`. Then: initialize from the smaller of the two heads before the loop. The user landed on the latter, but the actual code reflects this only partially - V1 initializes outside the loop and breaks on null lists, V2 tries deferring init via a `count == 0` flag, V3 handles null cases explicitly before the loop.

For the **null cases inside the loop**, the user correctly reasoned: when one list is exhausted, take from the other; when both are null, the loop ends. The condition `head1 != null || head2 != null` was settled deliberately (either still has nodes -> keep going).

On **complexity**, the user said O(n) and was nudged: "what is n here?" - leading to the realization that two independent lengths means O(m + n), not O(n). The Google reference inside the chat reinforced this and added the recursive-stack space caveat (O(m + n) for recursive vs O(1) for iterative).

The **recursive approach** was started: base case = both lists null (or actually: one list null returns the other), recursive step = take the smaller head and recurse on the rest. The chat cut off mid-discussion of what the recursive function returns - the user was leaning toward "return true" with an external merged pointer, which is the wrong frame. The right frame is: return a `ListNode` (the merged head from this point on).

---

## Core Formula / Key Condition

There is no single formula here - the central invariant is:

> At every step of the merge, the smaller of the two current heads is the next node of the merged list, and that node's old `next` pointer is overwritten without losing the rest of either list.

Two pieces make this safe:
1. **Advance the source pointer first** (`list1 = list1.next`) before rewriting `tail.next` - so you never lose access to the rest of `list1`.
2. **Track the head separately** from the moving tail so the return value is preserved across all the `next` rewrites.

---

## Approaches

### Approach 1: Iterative Splicing (Two-Pointer Merge)
**Complexity:** O(m + n) time, O(1) space
**Structure:**
```
mergedList = null
tail = null
init: pick smaller head, advance that source, set output = picked node, mergedList = output
while list1 != null OR list2 != null:
    if one is null: tail = the other; advance it
    else: pick smaller; tail = picked; advance it
    output.next = tail
    output = output.next
return mergedList
```
**Key optimization / insight:** No new nodes - the output is just a re-routing of `next` pointers across the existing nodes of list1 and list2. Head and tail must be tracked as separate variables. (The dummy-node variant - which collapses initialization into the loop - was not discussed in the chat but is the canonical cleaner form.)

### Approach 2: Recursive Merge
**Complexity:** O(m + n) time, O(m + n) space (call stack)
**Structure:**
```
MergeTwoLists(l1, l2):
    if l1 == null: return l2
    if l2 == null: return l1
    if l1.val <= l2.val:
        l1.next = MergeTwoLists(l1.next, l2)
        return l1
    else:
        l2.next = MergeTwoLists(l1, l2.next)
        return l2
```
**Key optimization / insight:** Each call picks the smaller head and lets the recursion handle the rest. Status in chat: only the base case and "what to return" question reached - the user's instinct of returning `true` was the wrong abstraction; the recursive function must return a `ListNode` so each parent call can wire it into `.next`.

---

## Mistakes and Lessons

| Mistake | Category | What happened |
|---------|----------|---------------|
| Assumed a new list had to be constructed (copy values into fresh nodes) | Problem comprehension | Skipped over the word "splicing" in the statement; needed a direct nudge to re-read it |
| Used a single `output` variable for both the head and the moving cursor | Variable lifecycle | Forgot that overwriting `output = output.next` discards the head reference; was rescued by introducing `mergedList` as a separate saved head |
| Said complexity was O(n) for two independent lists | Math error / Big-O literacy | Treated m and n as one variable; correct answer is O(m + n) since two lengths can vary independently |
| V1: Dereferenced `list1.val` before checking that `list1` was null | Logic error | Pre-loop initialization assumed both heads were non-null; crashes on the `[], [0]` test case |
| V2: Tried deferring initialization with a `count == 0` flag | Unnecessary complexity | Added bookkeeping inside the loop to avoid pre-loop init; ended up with `output.next = null; output = output.next` which corrupts the chain |
| Recursive instinct: return `true` with an external merged pointer | Mental model carryover | Treated recursion as imperative-with-side-effects; the correct frame for list-returning recursion is "this function returns the merged head from this point on" |

---

## Key Insights for Pattern Library

- **Splicing as a recognition signal:** When a problem statement explicitly says "splice" or "in-place," that is the cue to reuse existing nodes - the space-O(1) constraint is implicit. This is worth noting on a `linked-list-merge` or `pointer-rewiring` pattern when one is created.
- **Head + tail decomposition:** Any linked-list build-up requires two pointers - one frozen at the head for return, one moving as the cursor. This is a recurring move worth capturing as a technique (e.g. `head-tail-tracking` or `dummy-head`). The dummy-head idiom (`ListNode dummy = new ListNode(); ListNode tail = dummy; ... return dummy.next;`) collapses the pre-loop initialization the user struggled with into a single line.
- **Order of pointer mutations matters:** Always read `next` (or save it) before overwriting it. The user got this right by advancing `list1 = list1.next` before any rewrite, but the principle deserves an explicit callout.
- **Big-O with independent inputs:** Two unrelated input sizes do not collapse to one variable. Worth pinning as a rule-of-thumb in the complexity-analysis notes.

---

## Constructs Used

- `ListNode` (LeetCode-provided) - the singly-linked-list node type with `val` and `next`
- Conditional re-pointing (`output.next = tail`) - rewriting a `next` field rather than creating a node

(No `Dictionary`, `Stack`, or other collection types - this problem is pure pointer manipulation.)

---

## Pattern Signals

- **Two sorted inputs, one sorted output -> merge.** Same signal as the merge step in merge sort.
- **"Splice" in the statement -> in-place pointer rewiring, not copy.**
- **Linked list with head pointer only -> need to track head and tail separately.**
- Possible pattern fit: a linked-list variation under **Two Pointers** (one pointer per list, advanced by comparison), or a new pattern such as **Linked List Merge / Splicing**. The chat did not name a pattern - this needs to be confirmed with the user at pattern-extraction time.

---

## Connections to Other Problems

- **Merge Sort** - the merge step of merge sort on arrays uses the exact same comparison-and-pick logic, just on indices instead of pointers.
- **23 - Merge k Sorted Lists** - generalizes this to k lists, typically using a min-heap over the heads.
- **In-Place Linked List Reversal pattern (problems/206-reverse-linked-list)** - shares the "rewrite `next` pointers in place" frame, though the goal is different.

(These connections came up implicitly through the iterative pointer logic; only confirm them with the user during reflection.)

---

## Session Checklist

### Overall
- [x] Problem statement understood
- [x] Mental model solidified (after the splicing nudge)
- [ ] Reflection completed
- [ ] Saved to repo (`/save-problem`)

### Approach 1: Iterative Splicing
- [x] Code written in `active-solution.cs` (V3 working; V1 and V2 are earlier attempts)
- [ ] Time complexity confirmed by user (chat says O(m + n) but the user has not stated it inside this session)
- [ ] Space complexity confirmed by user (chat says O(1))
- [ ] Dry run completed for `[1,2,4]` and `[1,3,4]`
- [ ] Verify: V3 handles `[], []` -> returns null, and `[], [0]` -> returns `[0]`
- [ ] Decide: keep V3 as the working version or refactor to the dummy-head form (cleaner; same complexity)
- [ ] Confirm: `output.next = tail; output = output.next` works because `tail` was already advanced from its source list before this rewrite

### Approach 2: Recursive Merge
- [ ] Resolve: what does the recursive function return? (Should be `ListNode`, not `bool`)
- [ ] Write base cases: `l1 == null -> return l2`, `l2 == null -> return l1`
- [ ] Write recursive step: pick smaller head, set its `.next` to recursive call, return that head
- [ ] Code in `active-solution.cs`
- [ ] Time complexity confirmed by user
- [ ] Space complexity confirmed by user (note: O(m + n) due to call stack, not O(1))
- [ ] Dry run on `[1,2,4]` and `[1,3,4]` to see the recursion unwind
