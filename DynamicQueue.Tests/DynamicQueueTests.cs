using System;
using DynamicQueue.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicQueue.Tests
{
    [TestClass]
    public class DynamicQueueTests
    {
        [TestMethod]
        public void TestEnqueue()
        {
            DynamicQueue<int> queue = new DynamicQueue<int>();
            
            for (int i = 1; i <= 5; i++)
            {
                queue.Enqueue(i);
            }

            Assert.AreEqual(5, queue.Count);
            Assert.AreEqual(1, queue.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDequeue_EmptyQueue()
        {
            DynamicQueue<int> queue = new DynamicQueue<int>();
            int value = queue.Dequeue();
        }

        [TestMethod]
        public void TestDequeue_NonEmptyQueue()
        {
            DynamicQueue<int> queue = new DynamicQueue<int>();
            int[] arr = new int[5];

            for (int i = 1; i <= 5; i++)
            {
                queue.Enqueue(i);
                arr[i - 1] = i;
            }

            int value = 0;
            for (int i = 0; i < 5; i++)
            {
                value = queue.Dequeue();
                Assert.AreEqual(arr[i], value);
            }

            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void TestContains_ValueIsNotContained()
        {
            DynamicQueue<int> queue = new DynamicQueue<int>();

            for (int i = 1; i <= 5; i++)
            {
                queue.Enqueue(i);
            }

            Assert.AreEqual(false, queue.Contains(1000));
        }

        [TestMethod]
        public void TestContains_ValueIsContained()
        {
            DynamicQueue<int> queue = new DynamicQueue<int>();

            for (int i = 1; i <= 5; i++)
            {
                queue.Enqueue(i);
            }

            Assert.AreEqual(true, queue.Contains(5));
        }
    }
}
