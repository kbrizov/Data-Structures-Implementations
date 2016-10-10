using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticStack.Implementation;

namespace StaticStack.Tests
{
    [TestClass]
    public class StaticStackTests
    {
        [TestMethod]
        public void PushTest_PushSeveralItems()
        {
            StaticStack<int> stack = new StaticStack<int>();
            for (int i = 1; i <= 5; i++)
            {
                stack.Push(i);
            }

            Assert.AreEqual(5, stack.Count);
            Assert.AreEqual(5, stack.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopTest_PopFromEmptyStack()
        {
            StaticStack<int> stack = new StaticStack<int>();
            stack.Pop();
        }

        [TestMethod]
        public void PopTest_PoPSeveralItems()
        {
            StaticStack<int> stack = new StaticStack<int>();
            int[] arr = new int[5];
            for (int i = 0; i < 5; i++)
            {
                stack.Push(i + 1);
                arr[arr.Length - 1 - i] = i + 1;
            }

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(stack.Pop(), arr[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToArrayTest_InvalidStack()
        {
            StaticStack<int> stack = new StaticStack<int>();
            int[] arr = stack.ToArray();
        }

        [TestMethod]
        public void ToArrayTest_ValidStack()
        {
            StaticStack<int> stack = new StaticStack<int>();
            for (int i = 1; i <= 5; i++)
            {
                stack.Push(i);
            }

            int[] arr = stack.ToArray();

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(stack.Pop(), arr[arr.Length - 1 - i]);
            }
        }
    }
}
