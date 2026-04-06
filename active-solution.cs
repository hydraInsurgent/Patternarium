// ==== Approach 1 ====
// Approach: ?
// Time:  ?
// Space: ?
// Key Idea: ?

public class Solution1 {
    public int MissingNumber(int[] nums) {
        bool[] flags = new bool[nums.Length+1];
        for(int i=0; i<nums.Length; i++){
            flags[nums[i]] = true;  // We have seen this number
        }

        for(int i=0;i<flags.Length;i++){
            if(flags[i]==false){
                return i;
            }
        }
        return 0;
    }
}

// ==== Approach 2 ====
// Approach: ?
// Time:  ?
// Space: ?
// Key Idea: ?

public class Solution2 {
    public int MissingNumber(int[] nums) {
        // Solution using only O(1) extra space complexity and O(n) runtime complexity
        
        int n = nums.Length; // numbers in range 0 to n and 
        int gaussSum = ((n)*(n+1))/2;
        int sum = 0;
        for(int i=0; i<nums.Length; i++){
            sum += nums[i];
        }
        return gaussSum-sum;
    }
}

// ==== Approach 3 ====
// Approach: ?
// Time:  ?
// Space: ?
// Key Idea: ?

public class Solution3 {
    public int MissingNumber(int[] nums) {
        // Solution using XOR - bit flips
        
        int n = nums.Length; // numbers in range 0 to n and 
        
        int output = 0;
        for(int i=0; i<nums.Length; i++){
            output=output^nums[i];
        }

        for(int i=0;i<=n;i++){
            output=output^i;
        }
        return output;
    }
}
