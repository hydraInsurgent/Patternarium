# Multi-Pass Construction

**display_name:** Multi-Pass Construction

## Core Idea

Build the answer incrementally across multiple passes, using the output array itself as working space. Each pass deposits one layer of information. After all passes, every position has its complete answer. This avoids extra arrays by reusing the output as intermediate storage.

## Variation: Layered Deposit

**When to reach for this:**
- The answer at each position is a combination of two or more independently computable values
- You want O(1) extra space but the answer needs information from multiple directions
- Each pass can fully compute one component without needing the other

**Mental Trigger:**
> "Can I deposit one half now and complete it later?"
> "Does the output array have room to serve as intermediate storage?"

**Template:**
```csharp
int[] output = new int[n];

// Pass 1: deposit first layer (e.g., prefix products)
int running = 1;
for (int i = 0; i < n; i++)
{
    output[i] = running;
    running *= nums[i];
}

// Pass 2: multiply in second layer (e.g., suffix products)
running = 1;
for (int i = n - 1; i >= 0; i--)
{
    output[i] *= running;
    running *= nums[i];
}
```

**Tradeoffs:**
- Time: O(n) per pass, O(k*n) total for k passes
- Space: O(1) extra - output array doubles as working space
- Each pass must fully compute its layer independently - no dependency on the other layer during computation
- Number of loops does not affect time complexity - two O(n) passes is still O(n)

**Solved Problems:**
- **Product of Array Except Self** (problems/238-product-of-array-except-self/solutions/two-pass-sequential.cs) - pass 1 deposits prefix products, pass 2 multiplies in suffix products
- **Product of Array Except Self** (problems/238-product-of-array-except-self/solutions/two-pointer-converging.cs) - same layered deposit but via converging/diverging pointers in fewer loops

---

## Try Next

- Trapping Rain Water - deposit left-max in first pass, combine with right-max in second
- Candy problem - deposit left-neighbor requirements first, then right-neighbor

## Common Mistakes

- **Overwriting before reading** - when the second pass modifies a slot, the first pass's value is consumed. Order of operations matters: read the stored value, combine, then write
- **Forgetting center element in pointer-based variants** - if using converging pointers, odd-length arrays have a center element that neither pointer visits during the first phase
