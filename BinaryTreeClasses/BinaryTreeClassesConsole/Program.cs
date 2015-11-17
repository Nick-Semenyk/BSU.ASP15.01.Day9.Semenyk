using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTreeClasses;

namespace BinaryTreeClassesConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            tree.Add(6);
            tree.Add(2);
            tree.Add(8);
            tree.Add(1);
            tree.Add(4);
            tree.Add(7);
            tree.Add(9);
            tree.Add(3);
            tree.Add(5);
            foreach (int item in tree)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            foreach (int item in tree.AsPreorder)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            foreach (int item in tree.AsPostorder)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
