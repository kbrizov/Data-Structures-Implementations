namespace PriorityQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();

            for (int i = 10; i > 0; i--)
            {
                queue.Enqueue(i);
            }

            while (queue.Count > 0)
            {
                System.Console.WriteLine(queue.Dequeue());
            }
        }
    }
}
