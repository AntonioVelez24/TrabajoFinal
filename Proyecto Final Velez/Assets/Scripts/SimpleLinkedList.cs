using System;

class SimpleLinkedList<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
    private Node Head { get; set; }
    private int Count { get; set; }
    public void InsertAtStart(T value)
    {
        if (Head == null)
        {
            Node newNode = new Node(value);
            Head = newNode;
            Count = Count + 1;
        }
        else
        {
            Node newNode = new Node(value);
            newNode.Next = Head;
            Head = newNode;
            Count = Count + 1;
        }
    }
    public void InsertAtEnd(T value)
    {
        if (Head == null)
        {
            InsertAtStart(value);
        }
        else
        {
            Node lastNode = SearchLastNode();
            Node newNode = new Node(value);
            lastNode.Next = newNode;
            Count = Count + 1;
        }
    }
    public void InsertAtPosition(T value, int position)
    {
        if (position == 0)
        {
            InsertAtStart(value);
        }
        else if (position == Count - 1)
        {
            InsertAtEnd(value);
        }
        else if (position >= Count)
        {
            Console.WriteLine("No existe esta posición");
        }
        else
        {
            Node newNode = new Node(value);
            Node previousNode = Head;
            Node positionNode;
            int iterator = 0;
            while (iterator < position - 1)
            {
                previousNode = previousNode.Next;
                iterator = iterator + 1;
            }
            positionNode = previousNode.Next;
            previousNode.Next = null;
            previousNode.Next = newNode;
            newNode.Next = positionNode;
            Count = Count + 1;
        }
    }
    public void ModifyAtStart(T newValue)
    {
        if (Head == null)
        {
            throw new NullReferenceException("No hay elementos en la lista simplemente enlazada");
        }
        else
        {
            Head.Value = newValue;
        }
    }
    public void ModifyAtEnd(T newValue)
    {
        if (Head == null)
        {
            throw new NullReferenceException("No hay elementos en la lista simplemente enlazada");
        }
        else
        {
            Node lastNode = SearchLastNode();
            lastNode.Value = newValue;
        }
    }
    public void ModifyAtPosition(T newValue, int position)
    {
        if (position == 0)
        {
            ModifyAtStart(newValue);
        }
        else if (position == Count)
        {
            ModifyAtEnd(newValue);
        }
        else if (position > Count)
        {
            Console.WriteLine("No existe esa posición");
        }
        else
        {
            Node positionNode = Head;
            int iterator = 0;
            while (iterator < position)
            {
                positionNode = positionNode.Next;
                iterator = iterator + 1;
            }
            positionNode.Value = newValue;
        }
    }
    public T GetAtStart()
    {
        if (Head == null)
        {
            throw new NullReferenceException("No hay elementos en la lista simplemente enlazada");
        }
        else
        {
            return Head.Value;
        }
    }
    public T GetAtEnd()
    {
        if (Head == null)
        {
            throw new NullReferenceException("No hay elementos en la lista simplemente enlazada");
        }
        else
        {
            Node lastNode = SearchLastNode();
            return lastNode.Value;
        }
    }
    public T GetAtPosition(int position)
    {
        if (position == 0)
        {
            return GetAtStart();
        }
        else if (position == Count)
        {
            return GetAtEnd();
        }
        else if (position > Count)
        {
            throw new NullReferenceException("No existe esta posición");
        }
        else
        {
            Node positionNode = Head;
            int iterator = 0;
            while (iterator < position)
            {
                positionNode = positionNode.Next;
                iterator = iterator + 1;
            }
            return positionNode.Value;
        }
    }
    public void DeleteAtStart()
    {
        if (Head == null)
        {
            throw new NullReferenceException("No hay elementos en la lista simplemente enlazada");
        }
        else
        {
            Node newHead = Head;
            Head = newHead.Next;
            newHead.Next = null;
            Count = Count - 1;
        }
    }
    public void DeleteAtEnd()
    {
        if (Head == null)
        {
            throw new NullReferenceException("No hay elementos en la lista simplemente enlazada");
        }
        else
        {
            Node previousLastNode = Head;
            while (previousLastNode.Next.Next != null)
            {
                previousLastNode = previousLastNode.Next;
            }
            Node lastNode = previousLastNode.Next;
            previousLastNode.Next = null;
            lastNode = null;
            Count = Count - 1;
        }
    }
    public void DeleteAtPosition(int position)
    {
        if (position == 0)
        {
            DeleteAtStart();
        }
        else if (position == Count)
        {
            DeleteAtEnd();
        }
        else if (position > Count)
        {
            throw new NullReferenceException("No hay elementos en la lista simplemente enlazada");
        }
        else
        {
            Node previousNode = Head;
            int iterator = 0;
            while (iterator < position - 1)
            {
                previousNode = previousNode.Next;
                iterator = iterator + 1;
            }
            Node positionNode = previousNode.Next;
            Node nextNode = positionNode.Next;
            previousNode.Next = null;
            positionNode.Next = null;
            previousNode.Next = nextNode;
            positionNode = null;
            Count = Count - 1;
        }
    }
    private Node SearchLastNode()
    {
        Node lastNode = Head;
        while (lastNode.Next != null)
        {
            lastNode = lastNode.Next;
        }
        return lastNode;
    }
}