using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BinaryTreeClasses;
using BookClasses;

namespace BinaryTreeClassesTests
{
    [TestFixture]
    public class BinaryTreeTests
    {
        private int[] intExample;
        private string[] stringExample;
        private Book[] booksExample;
        private Point2D[] pointExample;
        private Comparer<int> numberLengthComparer;


        private class IntegerComparer : Comparer<int>
        {
            public override int Compare(int x, int y) => (int) Math.Log10(x) - (int) Math.Log10(y);
        }

        private class StringLengthComparer : Comparer<string>
        {
            public override int Compare(string x, string y) => (x??"").Length - (y??"").Length;
        }

        private class BookYearComparer : Comparer<Book>
        {
            public override int Compare(Book x, Book y) => x.YearOfPublishing - y.YearOfPublishing;
        }

        private struct Point2D
        {
            public int X;
            public int Y;
        }

        [TestFixtureSetUp]
        public void Initialize()
        {
            intExample = new [] {30,68,12,400,11,243,198,69,4,1111,982,18,3,1};
            stringExample = new[] {"Abc", "1211", "0000", "8", "kiu7qomAAt", "09", "jjjjjjjj", "Uynew", "QnhYTbva-", "88kndbs6hb", "1"};
            booksExample = new[]
            {
                new Book
                {
                    Author = "author1",
                    Title = "title1",
                    YearOfPublishing = 1900,
                    EditionNumber = 1,
                    Length = 100
                },
                new Book
                {
                    Author = "author",
                    Title = "title2",
                    YearOfPublishing = 1990,
                    EditionNumber = 3,
                    Length = 150
                },
                new Book
                {
                    Author = "Another Author",
                    Title = "OEIS171901",
                    YearOfPublishing = 2010,
                    EditionNumber = 1,
                    Length = 10
                },
                new Book
                {
                    Author = "Ivan",
                    Title = "About Ivan",
                    YearOfPublishing = 2012,
                    EditionNumber = 10,
                    Length = 42
                },
                new Book
                {
                    Author = "T",
                    Title = "T",
                    YearOfPublishing = 2020,
                    EditionNumber = 1,
                    Length = 1000
                }
            };
            pointExample = new Point2D[]
            {
                new Point2D {X = 10, Y = 30},
                new Point2D {X = -3, Y = 48},
                new Point2D {X = 87, Y = 23},
                new Point2D {X = 65, Y = 34},
                new Point2D {X = 99, Y = -4},
                new Point2D {X = 76, Y = 12},
                new Point2D {X = 0, Y = 31},
                new Point2D {X = 73, Y = 4}
            };
        }

        [Test]
        public void IntegerWithDefaultComparerTests()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            foreach (int integer in intExample)
            {
                tree.Add(integer);
            }
            int[] inOrder = new int[intExample.Count()];
            intExample.CopyTo(inOrder, 0);
            Array.Sort(inOrder);
            Assert.AreEqual(inOrder, tree.AsInorder.ToArray());
        }

        [Test]
        public void IntegerWithCustomComparerTests()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(new IntegerComparer());
            foreach (int integer in intExample)
            {
                tree.Add(integer);
            }
            int[] comparArray = new int[intExample.Count()];
            intExample.CopyTo(comparArray, 0);
            Array.Sort(comparArray, new IntegerComparer());
            Assert.AreEqual(comparArray, tree.AsInorder.ToArray());
        }

        [Test]
        public void StringWithDefaultComparerTests()
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>();
            foreach (string str in stringExample)
            {
                tree.Add(str);
            }
            string[] comparArray = new string[stringExample.Count()];
            stringExample.CopyTo(comparArray, 0);
            Array.Sort(comparArray);
            Assert.AreEqual(comparArray, tree.AsInorder.ToArray());
        }

        [Test]
        public void StringWithCustomComparerTests()
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>(new StringLengthComparer());
            foreach (string str in stringExample)
            {
                tree.Add(str);
            }
            string[] comparArray = new string[stringExample.Count()];
            stringExample.CopyTo(comparArray, 0);
            Array.Sort(comparArray, new StringLengthComparer());
            Assert.AreEqual(comparArray, tree.AsInorder.ToArray());
        }

        [Test]
        public void BookWithDefaultComparerTests()
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>();
            foreach (Book book in booksExample)
            {
                tree.Add(book);
            }
            Book[] comparArray = new Book[booksExample.Count()];
            booksExample.CopyTo(comparArray, 0);
            Array.Sort(comparArray);
            Assert.AreEqual(comparArray, tree.AsInorder.ToArray());
        }

        [Test]
        public void BookWithCustomComparerTests()
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(new BookYearComparer());
            foreach (Book book in booksExample)
            {
                tree.Add(book);
            }
            Book[] comparArray = new Book[booksExample.Count()];
            booksExample.CopyTo(comparArray, 0);
            Array.Sort(comparArray, new BookYearComparer());
            Assert.AreEqual(comparArray, tree.AsInorder.ToArray());
        }

        [Test]
        public void PointWithDefaultComparerTests()
        {
            BinarySearchTree<Point2D> tree = new BinarySearchTree<Point2D>();
            foreach (Point2D p in pointExample)
            {
                tree.Add(p);
            }
            Point2D[] comparArray = new Point2D[pointExample.Count()];
            pointExample.CopyTo(comparArray, 0);
            Array.Sort(comparArray, new BookYearComparer());
            Assert.AreEqual(comparArray, tree.AsInorder.ToArray());
        }
    }
}
