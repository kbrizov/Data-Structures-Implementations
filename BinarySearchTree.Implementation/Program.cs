using System;
using System.Linq;

namespace BinarySearchTree.Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Random randomGenerator = new Random();
            BinarySearchTree<int> binTree = new BinarySearchTree<int>();

            for (int i = 1; i <= 20; i++)
            {
                int randomNumber = randomGenerator.Next(1, 100);
                binTree.Insert(randomNumber);
            }

            BinarySearchTree<int> binTreeClone = binTree.Clone();

            Console.WriteLine("binTree.Equals(binTreeClone)) => {0}\n", binTree.Equals(binTreeClone));
            Console.WriteLine("binTree.GetHashCode() => {0}\n", binTree.GetHashCode());
            Console.WriteLine("binTreeClone.GetHashCode() => {0}\n", binTreeClone.GetHashCode());
            Console.WriteLine("(binTree == binTreeClone) => {0}\n", binTree == binTreeClone);
            Console.WriteLine("(binTree != binTreeClone) => {0}\n", binTree != binTreeClone);

            Console.WriteLine("ToString() and IEnumerable test:");

            Console.WriteLine(binTree);

            foreach (var item in binTree)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine("\n");

            Console.WriteLine("binTree.Insert(1000)");
            binTree.Insert(1000);
            Console.WriteLine(binTree);

            Console.WriteLine();

            Console.WriteLine("binTree.Remove(1000)");
            binTree.Remove(1000);
            Console.WriteLine(binTree + "\n");
        }
    }
}
