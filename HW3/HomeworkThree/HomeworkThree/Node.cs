using System;

/** Singly linked node class. */
namespace HomeworkThree
{

    public class Node<T>
    {
        public T Data;
        public Node<T> Next;

        public Node(T Data, Node<T> Next)
        {
            this.Data = Data;
            this.Next = Next;
        }
    }

}