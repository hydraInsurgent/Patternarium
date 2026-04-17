// ==== Approach 1 ====
// Approach: Brute Force (Try All Substrings)
// Time:  O(n^2)
// Space: O(1) - at most 26 keys in freq map
// Key Idea: Try every substring [i...j]; track most frequent char count incrementally; check windowSize - maxFreq <= k

public class Solution1
{
    public int CharacterReplacement(string s, int k)
    {
        int maxLength = 0;
        for (int i = 0; i < s.Length; i++)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            int maxFreq = 0;
            for (int j = i; j < s.Length; j++)
            {
                if (!map.ContainsKey(s[j]))
                {
                    map.Add(s[j], 1);
                }
                else
                {
                    map[s[j]]++;
                }
                maxFreq = Math.Max(maxFreq, map[s[j]]);
                int length = j - i + 1;
                if (length - maxFreq <= k)
                {
                    maxLength = Math.Max(maxLength, length);
                }
            }
        }
        return maxLength;
    }
}


// ==== Approach 2 ====
// Approach: Sliding Window - two pointers still
// Time:  O(n)
// Space: O(1) - at most 26 keys in freq map
// Key Idea: Maintain one window - expand r each step, shrink from l when invalid, never decrease maxFreq (stale is safe)

public class Solution2
{
    public int CharacterReplacement(string s, int k)
    {
        int left = 0;
        int right = 0;
        Dictionary<char, int> map = new Dictionary<char, int>();
        int maxFreq = 0;
        int maxLength = 0;
        while (right < s.Length)
        {
            if (!map.ContainsKey(s[right]))
            {
                map.Add(s[right], 1);
            }
            else
            {
                map[s[right]]++;
            }
            maxFreq = Math.Max(maxFreq, map[s[right]]);

            while (!(right - left + 1 - maxFreq <= k))
            {
                map[s[left]]--;
                left++;
            }

            maxLength = Math.Max(maxLength, right - left + 1);
            right++;
        }
        return maxLength;
    }
}

// Approach 3 
// True sliding window window never shrinks.
public class Solution {
    public int CharacterReplacement(string s, int k) {
        int left=0;
        int right=0;
        Dictionary<char,int> map = new Dictionary<char,int>();
        int maxFreq = 0;
        int maxLength =0;
        while(right < s.Length){
            if(!map.ContainsKey(s[right])){
                    map.Add(s[right],1);
                }else{
                    map[s[right]]++;
                }
            maxFreq = Math.Max(maxFreq,map[s[right]]);
            
            if(right-left+1 - maxFreq > k){
                map[s[left]]--;
                left++;
            }
            
            maxLength = Math.Max(maxLength,right-left+1);
            right ++;
        }
        return maxLength;
    }
}

// approach 4 
// fastest uses int array less hashing overhead 
public class Solution {
    public int CharacterReplacement(string s, int k) {
        int left=0;
        int right=0;
        int[] map = new int[26];
        int maxFreq = 0;
        int maxLength =0;
        while(right < s.Length){
            map[s[right] - 'A']++;
            
            maxFreq = Math.Max(maxFreq,map[s[right] - 'A']);
            
            if(right-left+1 - maxFreq > k){
                map[s[left]-'A']--;
                left++;
            }
            
            maxLength = Math.Max(maxLength,right-left+1);
            right ++;
        }
        return maxLength;
    }
}