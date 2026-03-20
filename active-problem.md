## Problem
- **Name:** Roman to Integer
- **Difficulty:** Easy
- **Source:** LeetCode #13
- **Tags:** String, HashMap
- **Status:** in-progress
- **Date Started:** 2026-03-19

## Statement
Given a roman numeral string, convert it to an integer.

Roman numerals use seven symbols: I (1), V (5), X (10), L (50), C (100), D (500), M (1000). Usually written largest to smallest left to right, but six subtraction cases exist:
- I before V (4) or X (9)
- X before L (40) or C (90)
- C before D (400) or M (900)

**Constraints:**
- 1 <= s.length <= 15
- s contains only valid roman numeral characters
- s is a valid roman numeral in range [1, 3999]

### Approach 1: Right-to-left scan with subtraction rule
**Status:** solved

#### Thinking
- Replace each character with its numeric value
- Walk right to left: if the current value is less than the previous (to its right), subtract it; otherwise add it
- Example: MCMXCIV -> 1000 - 100 + 1000 - 10 + 100 - 1 + 5 = 1994

#### Hints Given
- None

#### Bugs
- Used `int current = s[i]` instead of looking up the mapped value - got char's ASCII code instead of roman numeral value
- Compared `last` (numeric) against `current` (char) - type mismatch in logic
- Loop condition `i > 0` skipped first character; fixed to `i >= 0`
- Condition `last >= current` was backwards for the initial case where last=0 - subtracted instead of added

#### Solution
**Time:** O(n) | **Space:** O(1)
**Key Idea:** Scan right to left; if current value is smaller than the value to its right, subtract it; otherwise add it

---

### Approach 2: Left-to-right scan
**Status:** in-progress

#### Thinking
- Same subtraction logic but scanning left to right
- Compare current value with the next value instead of the previous one
