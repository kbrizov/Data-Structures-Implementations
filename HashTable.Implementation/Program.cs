using System;
using System.Collections.Generic;

namespace HashTable.Implementation
{
    class Program
    {
        public static void Main()
        {
            HashTable<string, int> hashtable = new HashTable<string, int>();

            hashtable.Add("England", 1);
            hashtable.Add("France", 2);
            hashtable.Add("USA", 10);
            hashtable.Add("Russia", 11);
            hashtable.Add("Bulgaria", 12);

            Console.WriteLine("\n-------Add() test-------\n");
            Console.WriteLine("Count: {0}", hashtable.Count);
            Console.WriteLine("Capacity: {0}", hashtable.Capacity);

            Console.WriteLine("\n-------Contains() and Remove() test-------\n");
            Console.WriteLine(hashtable.ContainsKey("England"));
            hashtable.Remove("England");
            Console.WriteLine(hashtable.ContainsKey("England"));

            Console.WriteLine("\n-------Indexer test-------\n");
            hashtable["Bulgaria"] = 100;
            Console.WriteLine(hashtable["Bulgaria"]);
            Console.WriteLine(hashtable.Find("Bulgaria"));

            Console.WriteLine("\n-------Keys property test-------\n");
            ICollection<string> keys = hashtable.Keys;
            foreach (var key in keys)
            {
                Console.Write("{0} ", key);
            }

            Console.WriteLine("\n-------Values property test-------\n");
            ICollection<int> values = hashtable.Values;
            foreach (var value in values)
            {
                Console.Write("{0} ", value);
            }

            Console.WriteLine("\n-------IEnumerable test-------\n");
            foreach (var item in hashtable)
            {
                Console.Write("{0} ", item.Key);
            }

            Console.WriteLine("\n-------method Clear() test-------\n");
            hashtable.Clear();
            Console.WriteLine("Count: {0}", hashtable.Count);
            Console.WriteLine("Capacity: {0}", hashtable.Capacity);

            Console.WriteLine();

            Console.WriteLine("\n-------Expand test-------\n");
            for (int i = 1; i <= 16; i++)
            {
                hashtable.Add("Key" + i, i);
            }

            Console.WriteLine("Count: {0}", hashtable.Count);
            Console.WriteLine("Capacity: {0}", hashtable.Capacity);
        }
    }
}
