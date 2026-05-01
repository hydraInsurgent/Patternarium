// Approach: Recursive (Tail Recursion with Accumulator)
// Time:  O(n)
// Space: O(n) - n call-stack frames pile up before unwinding
// Key Idea: Same three-pointer reversal as iterative, but recursion replaces the while loop. Pass head and last as parameters; each frame reverses one pointer and recurses on the rest.

public class Solution
{
    public ListNode ReverseList(ListNode head)
    {
        ListNode last = null;
        return ReverseLinkedList(head, last);
    }

    public ListNode ReverseLinkedList(ListNode head, ListNode last)
    {
        // Base case: list exhausted; last is the new head of the reversed list
        if (head == null) return last;

        // Reverse this node's pointer, recurse on the remaining list
        ListNode next = head.next;
        head.next = last;
        last = head;
        return ReverseLinkedList(next, last);
    }
}

// Why O(n) space when the iterative version is O(1)?
// Each recursive call pauses waiting for the deeper call to return. The system
// holds the current frame (head, last, next) until all deeper calls finish.
// For n nodes, n frames pile up on the call stack before any unwind - O(n) total.
// The iterative version reuses one stack frame throughout the loop.
