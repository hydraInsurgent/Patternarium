// Approach: Iterative (Three-Pointer Reversal)
// Time:  O(n)
// Space: O(1)
// Key Idea: Walk head forward with a trailing pointer (last) and a saved lookahead (next). At each node, rewrite head.next to point at last before advancing.

public class Solution
{
    public ListNode ReverseList(ListNode head)
    {
        ListNode last = null;
        ListNode next;

        // Rewrite each node's pointer backward, slide the three-pointer window forward
        while (head != null)
        {
            next = head.next;    // save before overwrite - overwriting head.next destroys forward access
            head.next = last;    // reverse this node's pointer
            last = head;         // slide trailing pointer forward
            head = next;         // advance to the saved next node
        }

        return last;
    }
}

// Why initialize last = null?
// The original head becomes the new tail. A list terminates at null, so the
// new tail's reversed pointer must be null. Initializing last = null makes the
// first iteration write head.next = null implicitly - no special case needed.

// Why while (head != null) and not while (next != null)?
// next is assigned null at the start of the last iteration (last node's next is null).
// The condition is checked before next is updated, so head != null still fires and
// the last node gets processed. Using while (next != null) would skip it.
