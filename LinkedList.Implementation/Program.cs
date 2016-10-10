using System;
using System.Linq;

namespace LinkedList.Implementation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            foreach (var item in list)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine();
            for (int i = 1; i <= 9; i++)
            {
                list.Remove(i);
            }

            foreach (var item in list)
            {
                Console.Write("{0} ", item);
            }
        }
    }
}
