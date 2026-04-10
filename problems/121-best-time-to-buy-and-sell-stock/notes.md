# Best Time to Buy and Sell Stock - Notes

## Key Insights
- The solution compressed step by step: brute force (all pairs) to two-pass (store intermediates) to single pass (two running variables). Each step asked "do I really need all that data?"
- Order of operations matters: compute profit before updating minimum to ensure buy happens before sell
- When past data can be summarized (min, max, best-so-far), you don't need to store it - running variables suffice

## Mantras
- "Am I storing data or just tracking a running condition?"
- "Do I really need all past values, or just a summary?"

## Patterns Used
- **Linear Scan - Running State** (Approach 2)
