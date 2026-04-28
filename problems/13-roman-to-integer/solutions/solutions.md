---
problem: 13
problem-title: Roman to Integer
difficulty: Easy
category: solutions
patterns: [Linear Scan, Preprocessing, Chunked Iteration]
constructs: [dictionary, string-replace]
ds-used: [string, hashmap]
algorithms: []
concepts: []
approaches:
  - name: Right-to-left scan
    file: right-to-left-scan.cs
    patterns: [Linear Scan]
    variation: Right to Left
    constructs: [dictionary]
    ds-used: [string, hashmap]
    ds-notes:
      hashmap: "char -> int lookup table for roman numeral values"
    time: "O(n)"
    space: "O(1)"
  - name: Left-to-right scan
    file: left-to-right-scan.cs
    patterns: [Linear Scan]
    variation: Left to Right Lookahead
    constructs: [dictionary]
    ds-used: [string, hashmap]
    ds-notes:
      hashmap: "char -> int lookup table for roman numeral values"
    time: "O(n)"
    space: "O(1)"
  - name: String replacement
    file: string-replacement.cs
    patterns: [Preprocessing]
    variation: String Replacement
    constructs: [dictionary, string-replace]
    ds-used: [string, hashmap]
    ds-notes:
      hashmap: "char -> int lookup table for roman numeral values"
      string: "string.Replace encodes subtractive pairs as single chars before scanning"
    time: "O(n)"
    space: "O(n)"
  - name: Two-character lookahead
    file: two-char-lookahead.cs
    patterns: [Chunked Iteration]
    variation: Two Char Lookahead
    constructs: [dictionary]
    ds-used: [string, hashmap]
    ds-notes:
      hashmap: "char -> int lookup table for roman numeral values"
    time: "O(n)"
    space: "O(1)"
---

# Roman to Integer - Solutions

## Approaches

### Approach 1: Right-to-left scan
**Code:** [right-to-left-scan.cs](right-to-left-scan.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Replace each character with its numeric value. Walk right to left - if the current value is less than the previous (to its right), subtract it; otherwise add it.

**Example:** MCMXCIV -> 1000 - 100 + 1000 - 10 + 100 - 1 + 5 = 1994

**Mistakes:**
- Used `int current = s[i]` instead of looking up the mapped value - got char's ASCII code instead of roman numeral value
- Compared `last` (numeric) against `current` (char) - type mismatch in logic
- Loop condition `i > 0` skipped first character; fixed to `i >= 0`
- Condition `last >= current` was backwards for the initial case where last=0 - subtracted instead of added

---

### Approach 2: Left-to-right scan
**Code:** [left-to-right-scan.cs](left-to-right-scan.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Same subtraction logic but scanning left to right. Compare current value with the next value instead of the previous one. Last character is always added since there's nothing after it to compare against.

**Mistakes:**
- Loop condition `i < s.Length - 1` skipped last character; fixed to `i < s.Length`

---

### Approach 3: String replacement
**Code:** [string-replacement.cs](string-replacement.cs)
**Time:** O(n) | **Space:** O(n)

**Thinking:** There are only 6 subtraction pairs (IV, IX, XL, XC, CD, CM). Replace each two-character pair with a single placeholder character mapped to its value. After replacement, every character maps to a positive value - just sum them. No comparison logic needed.

**Mistakes:**
- Forgot C# strings are immutable - `s.Replace()` returns a new string, doesn't modify in place
- Used uninitialized local `int num;` - C# requires explicit initialization for locals even though default is 0

---

### Approach 4: Two-character lookahead
**Code:** [two-char-lookahead.cs](two-char-lookahead.cs)
**Time:** O(n) | **Space:** O(1)

**Thinking:** Check if current + next form a subtraction pair (current < next). If yes, consume both characters and add the combined value. If no, consume just the current character. After the loop, check if the last character was already consumed or still needs adding.

**Mistakes:**
- Always added last character after loop even when it was already consumed by a subtraction pair
- Used `>` instead of `>=` for comparison - equal values (like XX) fell into subtraction branch

---

## Patterns

- Linear Scan - Neighbor Comparison (Approaches 1 & 2) - value depends on adjacent element, decide operation by comparing neighbors
- Preprocessing - Normalize Before Compute (Approach 3) - eliminate special cases before main logic, works when cases are fixed and known
- Chunked Iteration - Variable Step (Approach 4) - consume 1 or 2 elements per step, must handle leftover at the end

## Reflection

- **Key insight:** "Did my loop handle everything, or is there a leftover?" - critical question for variable-step iteration
- **Future strategy:** Use neighbor comparison when dependencies are dynamic; use replacement when special cases are fixed and small in number
- **Trickiest bug:** Approach 4 - handling the end scenario when the last character may or may not have been consumed
- **Most natural approach:** Right-to-left scan (first instinct), but string replacement feels even more intuitive in hindsight
- **Notes Insights:**
  - Trickiest bug was in Approach 4 - handling the end scenario when the last character may or may not have been consumed. Taught the importance of tracking index state with variable-step loops
  - Right-to-left scan was the first instinct, but string replacement feels even more intuitive in hindsight
  - Use neighbor comparison when dependencies are dynamic; use replacement when special cases are fixed and small in number
- **Mantras:**
  - "Did my loop handle everything, or is there a leftover?"
  - C# strings are immutable - `Replace()` returns a new string
  - C# local variables must be explicitly initialized before use
