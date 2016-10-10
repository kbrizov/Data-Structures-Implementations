using System;
using LinkedList.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedList.Tests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void AddTest_AddingAnItem()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.Add(5);

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void AddTest_AddingSeveralItems()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            Assert.AreEqual(10, list.Count);
        }

        [TestMethod]
        public void RemoveTest_RemovingTheOnlyItem()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.Add(6);

            bool isRemoved = list.Remove(6);
            Assert.AreEqual(true, isRemoved);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void RemoveTest_RemovingAnExistingItem()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            bool isRemoved = list.Remove(1);
            Assert.AreEqual(true, isRemoved);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        public void RemoveTest_RemovingNonExistingItem()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            bool isRemoved = list.Remove(100);
            Assert.AreEqual(false, isRemoved);
            Assert.AreEqual(10, list.Count);
        }

        [TestMethod]
        public void RemoveTest_RemovingSeveralItems()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            for (int i = 1; i <= 9; i++)
            {
                list.Remove(i);
            }

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void RemoveAtTest_RemovingTheFirstElement()
        {
            LinkedList<int> list = new LinkedList<int>();

            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            list.RemoveAt(0);
            Assert.AreEqual(9, list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(i + 2, list[i]);
            }
        }

        [TestMethod]
        public void RemoveAtTest_RemovingTheLastElement()
        {
            LinkedList<int> list = new LinkedList<int>();

            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            list.RemoveAt(list.Count - 1);
            Assert.AreEqual(9, list.Count);
            for (int i = 1; i <= list.Count; i++)
            {
                Assert.AreEqual(i, list[i-1]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerTest_IncorrectIndex()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            int incorrect = list[-1];
        }

        [TestMethod]
        public void IndexerTest_AllIndexesAreCorrect()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(list[i], i + 1);
            }
        }

        [TestMethod]
        public void IndexOfTest_indexOfFirstElement()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(i);
            }

            int indexOfFirst = list.IndexOf(1);
            Assert.AreEqual(0, indexOfFirst);
        }

        [TestMethod]
        public void IndexOfTest_indexOfLastElement()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(i);
            }

            int indexOfLast = list.IndexOf(5);
            Assert.AreEqual(list.Count - 1, indexOfLast);
        }

        [TestMethod]
        public void ContainsTest_ItemIsNotContained()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(i);
            }

            bool isContained = list.Contains(1000);
            Assert.AreEqual(false, isContained);
        }

        [TestMethod]
        public void ContainsTest_ItemIsContained()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(i);
            }

            bool isContained = list.Contains(3);
            Assert.AreEqual(true, isContained);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InsertTest_InsertInEmptyList()
        {
            LinkedList<string> list = new LinkedList<string>();
            list.Insert(0, "inserted");
        }

        [TestMethod]
        public void InsertTest_InsertAtFirstIndex()
        {
            LinkedList<string> list = new LinkedList<string>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add("element" + i);
            }

            list.Insert(0, "inserted");
            Assert.AreEqual("inserted", list[0]);
        }

        [TestMethod]
        public void InsertTest_InsertAtLastIndex()
        {
            LinkedList<string> list = new LinkedList<string>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add("element" + i);
            }

            int lastIndex = list.Count - 1;
            list.Insert(lastIndex, "inserted");
            Assert.AreEqual("inserted", list[lastIndex]);
        }

        [TestMethod]
        public void EnumeratorTest()
        {
            LinkedList<string> list = new LinkedList<string>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add("element" + i);
            }

            int index = 0;
            foreach (var item in list)
            {
                Assert.AreEqual(item, list[index]);
                index++;
            }
        }
    }
}
