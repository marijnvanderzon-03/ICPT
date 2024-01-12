namespace ConsoleApp1;

public struct Queue<T>
{
    public List<T> Items;
}

public static class QueueFunctions<T>
{
    public static Queue<T> GenerateQueue(int capacity)
    {
        Queue<T> queue = new Queue<T>();
        queue.Items = new List<T>(capacity);
        return queue;
    }

    public static void Enqueue(ref Queue<T> queue, T item)
    {
        queue.Items.Add(item);
    }

    public static T Dequeue(ref Queue<T> queue)
    {
        if (IsEmpty(ref queue))
        {
            throw new InvalidOperationException("can't remove item from queue when queue is empty");
        }

        T dequedItem = queue.Items[0];
        queue.Items.RemoveAt(0);
        return dequedItem;
    }

    public static T Peek(ref Queue<T> queue)
    {
        if (IsEmpty(ref queue))
        {
            throw new InvalidOperationException("Can't see item in queue when there are no Items in queue");
        }

        return queue.Items[0];
    }

    public static bool IsEmpty(ref Queue<T> queue)
    {
        return queue.Items.Count == 0;
    }
}
