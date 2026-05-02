// Approach: Recursive Splicing (Parallel Merge)
// Time:  O(m + n)
// Space: O(m + n) - recursion stack depth equals the total node count
// Key Idea: Same Assign helper as the iterative approach, called recursively. Each frame writes the picked node directly into the previous tail's `.next` field via `out tail.next`, then advances the local tail to the just-placed node so the next frame wires onto it. Side effect builds the chain; the return value is unused.

public class Solution
{
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // Seed the head, identical to the iterative approach
        ListNode output = null;
        Assign(ref list1, ref list2, out output);
        if (output == null) return null;

        ListNode mergedList = output;

        // Recursion does the splicing - output (the head) is also the initial "previous tail"
        RecursiveMerge(ref list1, ref list2, output, ref mergedList);
        return mergedList;
    }

    // Picks the smaller head from list1/list2 into `output`, advances the source pointer.
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

    // tail is passed by value: each frame holds its own "last-placed node"
    // out tail.next: the helper writes the picked node directly into the previous tail's .next field
    public ListNode RecursiveMerge(ref ListNode list1, ref ListNode list2, ListNode tail, ref ListNode mergedList)
    {
        if (list1 == null && list2 == null) return mergedList;

        Assign(ref list1, ref list2, out tail.next);  // pick + wire in one call
        tail = tail.next;                              // advance local cursor onto the just-placed node
        RecursiveMerge(ref list1, ref list2, tail, ref mergedList);
        return null;
    }
}

// Why `out tail.next` instead of picking into a temp and wiring separately?
// The earlier failing version passed `ref tail` so the recursion would advance through nodes -
// but `ref` to a field is an alias to ONE storage slot, not a moving cursor. Every recursive
// frame's `tail` aliased the same slot, and every write overwrote it; the final `tail = tail.next`
// wrote null. The fix: use `out tail.next` so each call writes directly into the previous tail's
// next field (the actual chain), then advance a value-typed `tail` local to the just-placed node
// for the next frame to extend. Pick and wire happen in one helper call; advancing is a separate
// local update.

// Why pass `tail` by value, not ref?
// Each frame needs its own "previous tail" anchor. If tail were ref, every frame would alias
// the caller's slot and the advance would not stick across recursion. Value passing gives each
// frame an independent local that gets advanced once per call.

// Why O(m + n) space for recursive vs O(1) for iterative?
// One stack frame per picked node; m + n nodes are picked total. Each frame holds its own
// list1, list2 references (via ref) and a local tail. The frames pile up before any return,
// so the call stack grows linearly with input size.
