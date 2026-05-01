---
name: index-mapping
slug: index-mapping
display_name: Index Mapping
category: technique
tags: [index-mapping, linear-scan, in-place, optimization]
---

# Index Mapping

## What It Is
Compute the target slot directly from the input index using arithmetic, then write each input element to its mapped position(s) in a single pass. Skips scratch buffers and avoids multiple passes.

## Core Reasoning
When the relationship between input position and output position is a known function of the index (e.g., `out[i] = in[i]` and `out[i+n] = in[i]`, or `out[2i] = in[i]` and `out[2i+1] = in[i+n]`), there is no need to copy, append, or shuffle. The arithmetic *is* the algorithm. One read per input slot, one or more direct writes per step.

## When to Apply
- The output's structure is a deterministic rearrangement of the input
- You can express each output index as a closed-form function of input index(es)
- You want to avoid a temp buffer or chaining multiple passes (e.g., copy then concat)

## Template
```csharp
int[] output = new int[outputSize];
for (int i = 0; i < n; i++)
{
    output[mapToFirstSlot(i)]  = input[i];
    output[mapToSecondSlot(i)] = input[i]; // if multiple writes per index
}
```

Concrete examples:
- Concatenation (LC 1929): `out[i] = nums[i]` and `out[n+i] = nums[i]`
- Interleave (LC 1470): `out[2*i] = nums[i]` and `out[2*i+1] = nums[n+i]`

## Tradeoffs
- Avoids the second pass that a copy-then-rearrange approach would need
- Still allocates the output array, but no scratch buffer beyond it
- Limit: only works when output indices are computable from input indices alone. Does not apply when placement depends on element values (e.g., counting sort)

## Seen In

```dataview
TABLE problem-title AS "Problem", problem AS "#", difficulty
FROM "problems"
FLATTEN techniques AS technique
WHERE technique = "index-mapping"
SORT problem asc
```
