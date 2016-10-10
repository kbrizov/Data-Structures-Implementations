using System;

namespace StaticList.Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            StaticList<int> list = new StaticList<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
