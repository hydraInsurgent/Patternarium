---
name: head-tail-tracking
slug: head-tail-tracking
display_name: Head-Tail Tracking
category: technique
tags: [linked-list, pointer, traversal, build-up]
---

# Head-Tail Tracking

## What It Is
Use two distinct variables when building or walking a linked list: one frozen at the **head** (for the return value or for iteration restart), and one moving **tail/cursor** that advances through the list each step.

## Core Reasoning
A linked list cursor that advances forward (`output = output.next`) loses access to its starting position the moment it moves. If the start position is needed later (almost always - either as the return value, or to re-traverse the list), it must be saved into a separate variable **before** the first advance. One variable cannot do both jobs at once.

This is the "save the entry point" move. The head variable is read-only after the initial assignment; the tail does all the work of stepping through.

## When to Apply
- Building a new linked list by appending nodes - tail is where you append, head is what you return
- Splicing/merging two lists into one - same shape: head saved, tail advances per pick
- Traversing a list while needing the original head reference for any post-loop operation
- Any algorithm where "where I am now" and "where I started" are both needed

## Template
```csharp
ListNode mergedList = output;   // freeze the head for return - never reassigned after this
ListNode tail = output;          // moving cursor - advances each iteration

while (more_to_add)
{
    tail.next = nextNode;
    tail = tail.next;            // advance the cursor; mergedList stays put
}

return mergedList;
```

## Variations
- **Pre-loop seeded** - initialize both head and tail to the first picked node before the loop starts. Used when the first node is decided by special-case logic that does not fit the loop body.
- **Dummy-head** - allocate a sentinel node, point `tail` at the dummy, append everything to `tail.next`, return `dummy.next`. Trades one node of allocation for cleaner pre-loop initialization.
- **Recursive head-tail** - the head is captured before recursion; the "tail" is passed down as a value parameter to each recursive frame, advancing one step per call. Same decomposition, different driver.

## Tradeoffs
- Adds one pointer variable - O(1) space overhead
- Eliminates a class of bugs where a single overloaded variable fails to be both head and cursor
- The order matters: assign `head = X` once before the loop; `tail` is the only pointer that moves
- For build-up problems, the dummy-head variant removes pre-loop initialization branches at the cost of one sentinel allocation

## Common Mistakes
- **Using one variable for both jobs** - reassigning `output` to advance loses the head. The return statement at the end gives back the tail (or null) instead of the actual start of the list.
- **Reassigning the head variable inside the loop** - if the head is reassigned even once during traversal, the saved head no longer points to the actual start of the chain.
- **Forgetting to advance the tail** - `tail.next = newNode` without `tail = tail.next` keeps appending every new node onto the same position; previous appends are overwritten.

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN techniques AS technique
WHERE technique = "head-tail-tracking"
SORT number asc
```
