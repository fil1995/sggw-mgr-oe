using System;

class OwnList<LElement>
{
    ListNode<LElement> head;

    public void Add(LElement data)
    {
        ListNode<LElement> newNode = new ListNode<LElement>();

        if (head == null)
        {
            head = newNode;
            return;
        }
        ListNode<LElement> lastNode = Last();
        lastNode.next = newNode;
    }
    ListNode<LElement> Last()
    {
        ListNode<LElement> last = head;
        while (last.next != null)
        {
            last = last.next;
        }
        return last;
    }
    public int Count
    {
        get
        {
            int current = 0;
            ListNode<LElement> last = head;
            while (last.next != null)
            {
                current++;
            }
            return current;
        }

    }
    public LElement this[int iterator]
    {
        get
        {
            int current = 0;

            ListNode<LElement> last = head;
            while (last.next != null)
            {
                if (iterator==current)
                {
                    return last.value;
                }
                current++;
                last = last.next;
            }
            throw new Exception("Index poza zakresem");
        }
    }
    
    //public int IndexOf(LElement element) {
    //    int current = 0;

    //    ListNode<LElement> last = head;
    //    while (last.next != null)
    //    {
    //        if (element == last.value)
    //        {
    //            return current;
    //        }
    //        current++;
    //        last = last.next;
    //    }
    //    throw new Exception("Nie znaleziono!!!");
    //}
}
class ListNode<NElement>
{
    public NElement value;
    public ListNode<NElement> next;
}