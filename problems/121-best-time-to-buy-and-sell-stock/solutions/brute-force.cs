// Approach: Brute Force
// Time:  O(n^2)
// Space: O(1)
// Key Idea: Try every buy-sell pair, track global max profit

public class Solution1
{
    public int MaxProfit(int[] prices)
    {
        int globalProfit = 0;

        // Try each day as a potential buy day
        for (int buyDay = 0; buyDay < prices.Length; buyDay++)
        {
            int buyPrice = prices[buyDay];
            int localProfit = 0;

            // Check all future days as potential sell days
            for (int sellDay = buyDay; sellDay < prices.Length; sellDay++)
            {
                int sellPrice = prices[sellDay];
                int profit = sellPrice - buyPrice;

                if (profit > 0 && profit > localProfit)
                {
                    localProfit = profit;
                }
            }

            if (localProfit > globalProfit)
            {
                globalProfit = localProfit;
            }
        }

        return globalProfit;
    }
}
