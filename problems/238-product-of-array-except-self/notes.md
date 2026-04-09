# Product of Array Except Self - Notes

## Mistakes Made

### Approach 2 - Prefix/Suffix Product Arrays
- **Left array included current index:** `left[i-1] * nums[i]` includes the element at index i in its own left product. Should be `left[i-1] * nums[i-1]` to only include elements before i.
- **Right array included current index:** Same bug mirrored - right product included the current element instead of only elements after it.
- **Output used same index for both arrays:** `left[i] * right[i]` pairs same-direction indices. Since right[] is built forwards but represents suffix products, need complementary index: `left[i] * right[nums.Length-1-i]`.

### Approach 3 - Two-Pointer Converging/Diverging
- **Odd-length center element unhandled:** For odd-length arrays, the center element was never visited during Phase 1 (while loop exits when left == right), so output[center] was 0. Needed a special case: when left == right, assign `leftProduct * rightProduct` directly instead of multiplying against the stored value.

## Key Insights
- "Product of everything except self" decomposes into "left product * right product" - change perspective from the whole to the parts
- You don't need to solve both halves at once; deposit one half first, complete with the other half later
- Writing exact formulas for one position (e.g., `left[i] = left[i-1] * nums[i-1]`) before coding makes bugs identifiable as formula errors
- Console dry runs against hand-written expected arrays catch formula bugs fast

## Mantras
- "Focus on what one element's answer is made of, not how to compute all answers at once"
- "Write the formula for one position, and the algorithm follows"
- "Deposit one half, complete the other half later"

## Patterns Used
- **Prefix/Suffix Decomposition** (Approaches 2, 3, 4)
- **Two Pointers** (Approach 3)
- **Preprocessing** (Approach 2)
- **Multi-Pass Construction** (Approaches 3, 4)
