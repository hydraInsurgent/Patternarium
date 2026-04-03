// ==== Approach 1 ====
// Approach: ?
// Time:  ?
// Space: ?
// Key Idea: ?

public class Solution1 {
    public int LengthOfLongestSubstring(string s) {
        // brute force
        int maxCount = 1;
        if(s.Length == 0){
            return 0;
        }else if (s.Length == 1){
            return 1;
        }
        for(int i=0; i<s.Length-1; i++){
            HashSet<char> set = new HashSet<char>();
            set.Add(s[i]);
            for(int j=i+1;j<s.Length;j++){
                if(!set.Contains(s[j])){
                    set.Add(s[j]);
                }else{
                    break;
                }
            }
            if(set.Count > maxCount){
                maxCount = set.Count;
            }
        }
        return maxCount;
    }
}

// ==== Approach 2 - Dry Run ====
// Input: "pwwkew" (expected: 3)
// Tracked: i, s[i], start, end, set, maxLength, action
//
// | Step | i | s[i] | start | end | set | maxLength | Action |
// |------|---|------|-------|-----|-----|-----------|--------|
// |   1  | 0  |      |       |     |     |           |        |
// |      |   |      |       |     |     |           |        |
// |      |   |      |       |     |     |           |        |
// |      |   |      |       |     |     |           |        |
// |      |   |      |       |     |     |           |        |
// |      |   |      |       |     |     |           |        |
//
// Bug found:
//

// ==== Approach 2 ====
// Approach: ?
// Time:  ?
// Space: ?
// Key Idea: ?

public class Solution2 {
    public int LengthOfLongestSubstring(string s) {
        // sliding window
        
        if(s.Length == 0){
            return 0;
        }else if (s.Length == 1){
            return 1;
        }
        int start = 0;
        int end = 0;
        int maxLength = 1;
        Dictionary<char, int> map = new Dictionary<char, int>();
        for(int i=0; i<s.Length; i++){
            if(!map.ContainsKey(s[i])){
                map.Add(s[i],i);
            }else{
                
                if(map[s[i]]>=start) {
                    start = map[s[i]] + 1 ;
                } // update start to old index +1
                map[s[i]] = i;
            }
            end = i;
            maxLength = end-start+1 > maxLength ? end-start+1 : maxLength;
        }
        return maxLength;
    }
}

// ==== Approach 2.1 ====
// Approach: Sliding Window with ASCII Array (Span<int>)
// Time:  O(n)
// Space: O(1) - fixed 128-int array, independent of input size
// Key Idea: Replace Dictionary with a stack-allocated int[128] indexed by ASCII char value.
//           Store end+1 so default value 0 safely means "never seen."

public class Solution21 {
    public int LengthOfLongestSubstring(string s) {
        // Stack-allocate a 128-int array covering the full ASCII range.
        // Span<int> + stackalloc avoids heap allocation - size is constant so this is O(1) space.
        // All slots default to 0, which we treat as "this char has never been seen."
        Span<int> lastSeen = stackalloc int[128];

        var start = 0;
        var end = 0;
        var maxLength = 0;

        while (end < s.Length) {
            // s[end] implicitly casts char -> int (its ASCII value), used directly as array index.
            // lastSeen[s[end]] holds the previous (end+1) for this char, or 0 if never seen.
            // Only jump start forward if the previous occurrence is inside the current window.
            // Equivalent to: start = Math.Max(start, lastSeen[s[end]])
            if (lastSeen[s[end]] > start) {
                start = lastSeen[s[end]];
            }

            maxLength = Math.Max(maxLength, end - start + 1);

            // Store end+1, not end, so that 0 unambiguously means "never seen."
            // Storing end directly would make index 0 indistinguishable from "not seen."
            lastSeen[s[end]] = end + 1;

            end++;
        }

        return maxLength;
    }
}