// solved center expansion

public class Solution {
    public int CountSubstrings(string s) {
        int totalCount = 0;
        for(int i=0;i<s.Length;i++){
            // assume center at current position and count odd palindromic substrings
            int oddCount = PalindromicSubstringCount(i,i,s);
            int evenCount = 0;
            if(i <= s.Length-2 && s[i] == s[i+1]){
                // assume center and center +1  position and count even centre palindromic substrings
                evenCount = PalindromicSubstringCount(i,i+1,s);
            }
            totalCount = totalCount + oddCount + evenCount;
        }
        return totalCount;

    }

    public int PalindromicSubstringCount(int left, int right, string s){
        int count = 0;
        while(left>=0 && right<=s.Length-1 && s[left] == s[right]){
            count++;
            left--;
            right++;
        }
        return count;
    }
}
