# Valid Parentheses - Notes

## Mistakes Made

### Approach 1 - Stack with HashMap complement lookup
- **Comparing raw characters instead of complements** - Used `buffer.Peek() != s[i]`, which compares the opening bracket directly to the closing bracket (e.g., '(' != ')'). This always fails because they are different characters. Fix: use a HashMap to map each opener to its closer, then compare `map[buffer.Pop()] != s[i]`.

## Key Insights
- The complement bug was a "knew it but forgot to translate" mistake, not a conceptual gap. When matching pairs, you always need a translation step - either a map lookup or pushing the complement upfront.
- A char array with a top pointer is functionally identical to a Stack - same LIFO behavior, just manual management.

## Patterns Used
- **Reverse Order Matching** (Approach 1, 2)
