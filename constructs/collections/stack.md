---
name: "Stack"
slug: stack
category: collections
tags: [stack, lifo, push, pop, matching, reverse-order]
language: csharp
related: [dictionary]
---

# Stack

## What It Is
A last-in, first-out (LIFO) collection. The last element added is the first one removed.

## C# Syntax
```csharp
// Create empty
var stack = new Stack<char>();

// Push element onto top
stack.Push('(');

// Pop element from top (throws if empty)
char top = stack.Pop();

// Peek at top without removing (throws if empty)
char top = stack.Peek();

// Check size
stack.Count;

// Check if empty
if (stack.Count == 0) { ... }

// Iterate (top to bottom order)
foreach (char c in stack) { ... }
```

## When to Reach For It
- Matching pairs in reverse order (brackets, tags)
- Undo/history operations
- Depth-first traversal without recursion
- Expression evaluation (operators, operands)

## vs Alternatives
- vs Queue: Stack is LIFO (last in, first out); Queue is FIFO (first in, first out)
- vs List: Stack enforces LIFO discipline; List allows access anywhere
- vs char[] manual stack: Stack<T> handles resizing and provides Push/Pop/Peek. Manual array is slightly faster but requires managing the top pointer yourself

## Gotchas
- `Pop()` and `Peek()` throw `InvalidOperationException` on empty stack. Always check `Count > 0` first.
- No index access - you cannot read the middle of a stack without popping

## See Also
- [dictionary.md](dictionary.md) - often used together when stack elements need complement translation

## Seen In

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN constructs AS construct
WHERE construct = "stack"
SORT number asc
```
