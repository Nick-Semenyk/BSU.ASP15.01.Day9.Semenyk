using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeClasses
{
    public class BinarySearchTree<T> : ICollection<T>
    {
        private class Item<T> :IEnumerable<T>
        {
            public T Data { get; set; }
            public Item<T> Left { get; set; }
            public Item<T> Right { get; set; }
            public Item<T> Parent { get; set; } 
            public IEnumerator<T> AsInorder {
                get
                {
                    return GetEnumerator();
                }
            } 

            public IEnumerator<T> AsPreorder
            {
                get
                {
                    yield return Data;
                    if (Left != null)
                        foreach (T item in Left)
                        {
                            yield return item;
                        }
                    if (Right != null)
                        foreach (T item in Right)
                        {
                            yield return item;
                        }
                }
            }

            public IEnumerator<T> AsPostorder
            {
                get
                {
                    if (Left != null)
                        foreach (T item in Left)
                        {
                            yield return item;
                        }
                    if (Right != null)
                        foreach (T item in Right)
                        {
                            yield return item;
                        }
                    yield return Data;
                }
            }

            public Item(T data)
            {
                this.Data = data;
                Left = null;
                Right = null;
                Parent = null;
            }
            public IEnumerator<T> GetEnumerator()
            {
                if (Left != null)
                    foreach(T item in Left)
                    {
                        yield return item;
                    }
                yield return Data;
                if (Right != null)
                    foreach (T item in Right)
                    {
                        yield return item;
                    }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public int Count { get; private set; } = 0;
        public bool IsReadOnly { get; } = false;
        public Comparer<T> Comparer { get; }
        public IEnumerable<T> AsInorder
        {
            get { return GetItemsInorder(); }
        } 
        public IEnumerable<T> AsPreorder
        {
            get { return GetItemsPreorder(); }
        }
        public IEnumerable<T> AsPostorder
        {
            get { return GetItemsPostorder(); }
        }

        private Item<T> root; 


        public BinarySearchTree()
        {
            Comparer = Comparer<T>.Default;
        }

        public BinarySearchTree(Comparer<T> comparer)
        {
            root = null;
            if (comparer == null)
                throw new ArgumentNullException();
            this.Comparer = comparer;
        }

        public void Add(T item)
        {
            if (root == null)
            {
                root = new Item<T>(item);
                root.Parent = null;
                Count++;
                return;
            }
            Item<T> current = root;
            while (true)
            {
                switch (Math.Sign(Comparer.Compare(item, current.Data)))
                {
                    case -1:
                        if (current.Left == null)
                        {
                            current.Left = new Item<T>(item);
                            current.Left.Parent = current;
                            Count++;
                            return;
                        }
                        current = current.Left;
                        break;
                    case 0:
                    case 1:
                        if (current.Right == null)
                        {
                            current.Right = new Item<T>(item);
                            current.Right.Parent = current;
                            Count++;
                            return;
                        }
                        current = current.Right;
                        break;
                }
            }
        }

        public void Clear()
        {
            root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            Item<T> current = root;
            if (root == null)
                return false;
            while (true)
            {
                switch (Math.Sign(Comparer.Compare(item, current.Data)))
                {
                    case -1:
                        if (current.Left == null)
                            return false;
                        current = current.Left;
                        break;
                    case 0:
                        return true;
                    case 1:
                        if (current.Right == null)
                            return false;
                        current = current.Right;
                        break;
                }
            }
        }
        
        //as inorder
        public void CopyTo(T[] array, int arrayIndex)
        {   
            if (array == null)
            {
                throw new ArgumentNullException();
            }
            if (arrayIndex < 0)
                throw new ArgumentException("arrayIndex must be positive value");
            if (array.Count() - arrayIndex < Count)
                throw new ArgumentException("Not enough allowed to copy array elements");
            foreach(T item in root)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            Item<T> current = root;
            while (true)
            {
                switch (Math.Sign(Comparer.Compare(item, current.Data)))
                {
                    case -1:
                        if (current.Left == null)
                            return false;
                        current = current.Left;
                        break;
                    case 0:
                        if (current == root)
                        {
                            //0 sub-trees
                            if (root.Left == null && root.Right == null)
                            {
                                root = null;
                                Count--;
                                return true;
                            }
                            //1 sub-tree
                            if (root.Left == null || root.Right == null)
                            {
                                root = root.Left ?? root.Right;
                                Count--;
                                return true;
                            }
                            //2 sub-trees
                            Item<T> replacement = root.Right;
                            if (replacement.Left != null)
                            {
                                while (replacement.Left != null)
                                {
                                    replacement = replacement.Left;
                                }
                                replacement.Parent.Left = replacement.Right;
                            }
                            else
                                replacement.Parent.Right = replacement.Right;
                            root.Data = replacement.Data;
                            Count--;
                            return true;
                        }
                        else
                        {
                            //0 sub-trees
                            if (current.Left == null && current.Right == null)
                            {
                                if (current.Parent.Left == current)
                                    current.Parent.Left = null;
                                if (current.Parent.Right == current)
                                    current.Parent.Right = null;
                                Count--;
                                return true;
                            }
                            //1 sub-tree
                            if (current.Left == null || current.Right == null)
                            {
                                if (current.Parent.Left == current)
                                    current.Parent.Left = current.Left ?? current.Right;
                                if (current.Parent.Right == current)
                                    current.Parent.Right = current.Left ?? current.Right;
                                Count--;
                                return true;
                            }
                            //2 sub-trees
                            Item<T> replacement = root.Right;
                            if (replacement.Left != null)
                            {
                                while (replacement.Left != null)
                                {
                                    replacement = replacement.Left;
                                }
                                replacement.Parent.Left = replacement.Right;
                            }
                            else
                                replacement.Parent.Right = replacement.Right;
                            current.Data = replacement.Data;
                            Count--;
                            return true;
                        }
                    case 1:
                        if (current.Right == null)
                            return false;
                        current = current.Right;
                        break;
                }
            }
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return root.AsInorder;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<T> GetItemsInorder()
        {
            List<T> result = new List<T>();
            AddItemsToListInorder(root, result);
            return result;
        } 

        public List<T> GetItemsPreorder()
        {
            List<T> result = new List<T>();
            AddItemsToListPreorder(root, result);
            return result;
        } 

        public List<T> GetItemsPostorder()
        {
            List<T> result = new List<T>();
            AddItemsToListPostorder(root, result);
            return result;
        } 

        private void AddItemsToListInorder(Item<T> currentItem, List<T> result)
        {
            if (currentItem == null)
                return;
            if (currentItem.Left != null)
                AddItemsToListInorder(currentItem.Left, result);
            result.Add(currentItem.Data);
            if (currentItem.Right != null)
                AddItemsToListInorder(currentItem.Right, result);
        }

        private void AddItemsToListPreorder(Item<T> currentItem, List<T> result)
        {
            if (currentItem == null)
                return;
            result.Add(currentItem.Data);
            if (currentItem.Left != null)
                AddItemsToListPreorder(currentItem.Left, result);
            if (currentItem.Right != null)
                AddItemsToListPreorder(currentItem.Right, result);
        }

        private void AddItemsToListPostorder(Item<T> currentItem, List<T> result)
        {
            if (currentItem == null)
                return;
            if (currentItem.Left != null)
                AddItemsToListPostorder(currentItem.Left, result);
            if (currentItem.Right != null)
                AddItemsToListPostorder(currentItem.Right, result);
            result.Add(currentItem.Data);
        }

    }
}
