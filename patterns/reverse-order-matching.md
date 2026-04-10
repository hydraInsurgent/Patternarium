---
name: reverse-order-matching
display_name: Reverse Order Matching
category: pattern
variations:
  - name: Complement Push
    ds: [string, stack, hashmap, array]
ds-primary: [string, stack, hashmap, array]
---

# Reverse Order Matching

**display_name:** Reverse Order Matching

## Core Idea

Use a stack to match elements that must pair up in reverse order - the most recently opened thing must be closed first (LIFO). The stack enforces this ordering constraint naturally: push when something opens, pop when something closes, and check that the popped element is the correct match.

The key question is always: does the most recent unresolved item match what I'm seeing now?

## Variation: Complement Push

**When to reach for this:**
- Matching pairs that must close in reverse order (brackets, tags, nested structures)
- You need to validate that every opener has a matching closer in the correct sequence

**Mental Trigger:**
> "Things are opening and closing in nested order - the last thing opened must be the first thing closed."
> "I need to remember what's still open and check matches in reverse."

**Template:**
```csharp
var stack = new Stack<char>();

for (int i = 0; i < s.Length; i++)
{
    char c = s[i];
    if (IsOpener(c))
    {
        stack.Push(GetComplement(c)); // push what you EXPECT to see later
    }
    else
    {
        if (stack.Count == 0 || stack.Pop() != c)
            return false;
    }
}
return stack.Count == 0;
```

**Why push the complement?** Instead of pushing the opener and translating later (via HashMap or switch), push the closer you expect. Then popping and comparing is a single direct check - no lookup needed.

**Alternative:** Push the opener itself and use a HashMap to translate on pop. Same complexity, but requires an extra data structure.

**Tradeoffs:**
- Time: O(n) - single pass through input
- Space: O(n) - stack grows up to n in worst case (all openers)
- Complement Push avoids the HashMap but needs if/else or switch to decide what to push

**Solved Problems:**
- **Valid Parentheses** ([hashmap-complement.cs](../problems/20-valid-parentheses/solutions/hashmap-complement.cs), [complement-push.cs](../problems/20-valid-parentheses/solutions/complement-push.cs)) - match brackets in LIFO order

---

## Try Next
- Valid Parentheses variations with additional rules
- HTML/XML tag matching
- Expression evaluation (operators and operands)

## Common Mistakes
- **Comparing raw opener to closer** - `'(' != ')'` will always be true. You must translate via a map or push the complement. (Valid Parentheses, Approach 1)
- **Forgetting to check stack empty at end** - input like `"((("` has no pop failures but the stack is not empty, so it's invalid.
- **Forgetting to check stack empty before pop** - input like `")"` would pop an empty stack and crash.

## Solved Problems

```dataview
TABLE title AS "Problem", number AS "#", difficulty
FROM "problems"
FLATTEN patterns AS pattern
WHERE pattern = "Reverse Order Matching"
SORT number asc
```
