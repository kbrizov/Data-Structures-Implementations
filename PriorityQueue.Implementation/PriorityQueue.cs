using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PriorityQueue<T> where T : IComparable<T>
{
    private OrderedBag<T> multiset;

    public PriorityQueue()
    {
        this.multiset = new OrderedBag<T>();
    }

    public PriorityQueue(IComparer<T> comparer)
    {
        this.multiset = new OrderedBag<T>(comparer);
    }

    public PriorityQueue(IEnumerable<T> collection)
    {
        this.multiset = new OrderedBag<T>(collection);
    }

    public PriorityQueue(IEnumerable<T> collection, IComparer<T> comparer)
    {
        this.multiset = new OrderedBag<T>(collection, comparer);
    }

    public int Count
    {
        get
        {
            return multiset.Count;
        }
    }

    public void Enqueue(T element)
    {
        multiset.Add(element);
    }

    public T Dequeue()
    {
        T element = multiset.RemoveFirst();

        return element;
    }

    public override string ToString()
    {
        return this.multiset.ToString();
    }
}