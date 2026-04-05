# Roman to Integer - Solutions

## Approaches

### Approach 1: Right-to-left scan
**Code:** [right-to-left-scan.cs](solutions/right-to-left-scan.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Replace each character with its numeric value. Walk right to left - if the current value is less than the previous (to its right), subtract it; otherwise add it.

**Example:** MCMXCIV -> 1000 - 100 + 1000 - 10 + 100 - 1 + 5 = 1994

---

### Approach 2: Left-to-right scan
**Code:** [left-to-right-scan.cs](solutions/left-to-right-scan.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Same subtraction logic but scanning left to right. Compare current value with the next value instead of the previous one. Last character is always added since there's nothing after it to compare against.

---

### Approach 3: String replacement
**Code:** [string-replacement.cs](solutions/string-replacement.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** There are only 6 subtraction pairs (IV, IX, XL, XC, CD, CM). Replace each two-character pair with a single placeholder character mapped to its value. After replacement, every character maps to a positive value - just sum them. No comparison logic needed.

---

### Approach 4: Two-character lookahead
**Code:** [two-char-lookahead.cs](solutions/two-char-lookahead.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Check if current + next form a subtraction pair (current < next). If yes, consume both characters and add the combined value. If no, consume just the current character. After the loop, check if the last character was already consumed or still needs adding.

---

## Patterns

- [Linear Scan - Neighbor Comparison](../../patterns/linear-scan.md#variation-neighbor-comparison) (Approaches 1 & 2) - value depends on adjacent element, decide operation by comparing neighbors
- [Preprocessing - Normalize Before Compute](../../patterns/preprocessing.md#variation-normalize-before-compute) (Approach 3) - eliminate special cases before main logic, works when cases are fixed and known
- [Chunked Iteration - Variable Step](../../patterns/chunked-iteration.md#variation-variable-step) (Approach 4) - consume 1 or 2 elements per step, must handle leftover at the end

## Reflection

- **Key insight:** "Did my loop handle everything, or is there a leftover?" - critical question for variable-step iteration
- **Future strategy:** Use neighbor comparison when dependencies are dynamic; use replacement when special cases are fixed and small in number
- **Trickiest bug:** Approach 4 - handling the end scenario when the last character may or may not have been consumed
- **Most natural approach:** Right-to-left scan (first instinct), but string replacement feels even more intuitive in hindsight
