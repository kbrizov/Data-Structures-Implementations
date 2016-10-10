using System;

namespace Tree.Implementation
{
    class Program
    {
        public static void Main()
        {
            var root =
                new TreeNode<int>(7,
                    new TreeNode<int>(19,
                        new TreeNode<int>(1),
                        new TreeNode<int>(12),
                        new TreeNode<int>(31)),
                    new TreeNode<int>(21),
                    new TreeNode<int>(14,
                        new TreeNode<int>(23),
                        new TreeNode<int>(6)));

            var tree = new Tree<int>(root);

            tree.TraverseUsingDepthFirst((T) => Console.Write("{0} ", T.Value));
            Console.WriteLine();
            tree.TraverseUsingBreathFirst((T) => Console.Write("{0} ", T.Value));
        }
    }
}
