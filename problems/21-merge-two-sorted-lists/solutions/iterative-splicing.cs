// Approach: Iterative Splicing (Parallel Merge)
// Time:  O(m + n)
// Space: O(1)
// Key Idea: Walk both sorted lists with one pointer each. The Assign helper picks the smaller current head and writes it directly into the caller's slot via `out`. Pre-loop call seeds the head; loop calls splice each subsequent pick onto the moving tail. `ref` on list1/list2 lets the helper advance the source pointers; `out` on output is pure write.

public class Solution
{
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // Seed the head from whichever list has the smaller first node (or the only non-empty list)
        ListNode output = null;
        Assign(ref list1, ref list2, out output);
        if (output == null) return null;

        ListNode mergedList = output;

        // Splice each subsequent pick onto the chain by advancing output forward
        while (list1 != null || list2 != null)
        {
            ListNode tail;
            Assign(ref list1, ref list2, out tail);
            output.next = tail;
            output = output.next;
        }

        return mergedList;
    }

    // Picks the smaller head from list1/list2 into `output`, advances the source pointer.
    // ref on list1/list2: helper reads them (null check, value compare) AND advances them.
    // out on output: helper only writes; caller's prior value is irrelevant.
    public void Assign(ref ListNode list1, ref ListNode list2, out ListNode output)
    {
        if (list1 == null && list2 == null)
        {
            output = null;
        }
        else if (list1 == null && list2 != null)
        {
            output = list2;
            list2 = list2.next;
        }
        else if (list1 != null && list2 == null)
        {
            output = list1;
            list1 = list1.next;
        }
        else
        {
            if (list1.val <= list2.val)
            {
                output = list1;
                list1 = list1.next;
            }
            else
            {
                output = list2;
                list2 = list2.next;
            }
        }
    }
}

// Why ref on list1/list2 and out on output?
// The helper needs to (a) read the source pointers to compare their values, (b) advance them
// (list1 = list1.next), and (c) make those advances visible to the caller. That requires read +
// write semantics with a value flowing in - which is `ref`. The output parameter is the opposite:
// the helper never reads it, only writes the picked node into it, and the caller's prior value
// is irrelevant - that fits `out`. The first attempts marked everything `out` and crashed at
// compile time because `out` forbids reading before assigning, and the helper's first line
// (`list1 == null`) is a read.

// Why one variable for the head (mergedList) and a separate moving cursor (output)?
// Each iteration overwrites `output` to advance forward. Without saving the head before the loop,
// the return value would be lost - by the time the loop exits, `output` is the tail, not the head.
// Two variables are required: one frozen at the head for the return, one moving as the cursor.
