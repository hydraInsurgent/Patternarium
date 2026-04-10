// Approach: Running State - Track Min So Far
// Time:  O(n)
// Space: O(1)
// Key Idea: Track running minimum price, compute profit at each step - only need a summary of the past, not the full history

public class Solution2
{
    public int MaxProfit(int[] prices)
    {
        int minPriceSoFar = prices[0];
        int maxProfit = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            // Compute profit first, then update minimum
            // This ensures buy always happens before sell
            maxProfit = Math.Max(prices[i] - minPriceSoFar, maxProfit);
            minPriceSoFar = Math.Min(prices[i], minPriceSoFar);
        }

        return maxProfit;
    }
}

// Why compute profit before updating minimum?
// - If you update minimum first, you could end up comparing a price
//   against itself (buying and selling on the same day). Computing
//   profit first guarantees the buy day is strictly before the sell day.
