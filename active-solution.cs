// ==== Approach 1 ====
// Approach: Right to left comparing and adding numbers and subtracting smaller numbers
// Time:  O(n)?
// Space: O(1)
// Key Idea: Smaller number after the greater number is to be subtracted;

public class Solution {
    public int RomanToInt(string s) {
        Dictionary<char,int> map = new Dictionary<char, int>()
        {
            {'M',1000},
            {'D',500},
            {'C',100},
            {'L',50},
            {'X',10},
            {'V',5},
            {'I',1}
        };
        int num = 0;
        int last = 0;
        for(int i = s.Length-1; i >=0 ; i--)
        {
            int current = map[s[i]];
            if (last > current)
            {
                num -= current;
            }
            else
            {
                num += current;
            }
            last = current;
        }
        return num;
    }
}

// ==== Approach 2 ====
// Approach: Right to left comparing and adding numbers and subtracting smaller numbers
// Time:  O(n)
// Space: O(1)
// Key Idea: Check the next number if its greater than current then current number will be subtracted.

public class Solution2
{
    public int RomanToInt(string s) {
        Dictionary<char,int> map = new Dictionary<char, int>()
        {
            {'M',1000},
            {'D',500},
            {'C',100},
            {'L',50},
            {'X',10},
            {'V',5},
            {'I',1}
        };
        int num = 0;
        int last = map[s[0]];
        for(int i = 1; i <s.Length; i++)
        {
            int current = map[s[i]];
            if (current>last)
            {
                num -= last;
            }
            else
            {
                num += last;
            }
            last = current;
        }
        num += last;
        return num;
    }
}

// ==== Approach 3 ====
// Approach: String replacement
// Time:  O(n)
// Space: O(1)
// Key Idea: There are fixed number of subtraction scenarios, replace them first

public class Solution3
{
    public int RomanToInt(string s)
    {
        Dictionary<char,int> map = new Dictionary<char, int>()
        {
            {'M',1000},
            {'D',500},
            {'C',100},
            {'L',50},
            {'X',10},
            {'V',5},
            {'I',1},
        // Subtraction cases
        /*
            - I before V (4)   IV - A
            - I before X (9)   IX - B
            - X before L (40)  XL - Z
            - X before C (90)  XC - E
            - C before D (400) CD - F
            - C befpre M (900) CM - G
        */
            {'A',4},
            {'B',9},
            {'Z',40},
            {'E',90},
            {'F',400},
            {'G',900}
        };
        
        s = s.Replace("IV","A").Replace("IX","B").Replace("XL","Z").Replace("XC","E").Replace("CD","F").Replace("CM","G");
        int num = 0;
        for(int i = 0; i < s.Length; i++)
        {
            num += map[s[i]];
        }
        return num;
    }
}



// ==== Approach 4 ====
// Approach: Two character lookahead
// Time:  ?
// Space: ?
// Key Idea: Look two character at once , if first kis smaller than second then first is subtracted.

public class Solution4
{
    public int RomanToInt(string s)
    {
        Dictionary<char,int> map = new Dictionary<char, int>()
        {
            {'M',1000},
            {'D',500},
            {'C',100},
            {'L',50},
            {'X',10},
            {'V',5},
            {'I',1}
        };
        int num = 0;
        int i = 0;

        while (i < s.Length-1)
        {
            int first = map[s[i]];
            int second = map[s[i + 1]];
            if(first >= second)
            {
                num += first;
                i++;
            }
            else
            {
                num = num + second - first;
                i = i +2;
            }
        }
        if(i != s.Length)
        {
            num += map[s[s.Length-1]];
            i++;
        }
        return num;
    }
}
// ==== Approach 4 - Dry Run ====
// Input: "MCMXCIV" (expected: 1994)
// Tracked: i, s[i], s[i+1], first, second, first < second?, action, num
//
// | Step | i | s[i] | s[i+1] | first | second | first > second? | Action | num |increment
// |------|---|------|--------|-------|--------|-----------------|--------|-----|
// | init | 0 |  M   |   C    |1000   |   100  |      Yes        |sum f   | 1000|1
// | init | 1 |  C   |   M    | 100   |  1000  |      No         |sum s-f | 1900|2
// |      | 2 |  X   |   C    |1000   |   10   |      Yes        |sum f   | 2500|2
// |      | 3 |     |       |10     |   500  |      No         |        |     |
// |      |   |      |        |       |        |                 |        |     |
// |      |   |      |        |       |        |                 |        |     |
// |      |   |      |        |       |        |                 |        |     |
// |      |   |      |        |       |        |                 |        |     |
//
// Bug found:
//