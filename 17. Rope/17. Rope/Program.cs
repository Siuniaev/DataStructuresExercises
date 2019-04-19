/*
Ваша цель в данной задаче — реализовать структуру данных Rope.
Данная структура данных хранит строку и позволяет эффективно вырезать кусок строки и переставить его в другое место.

Формат входа. Первая строка содержит исходную строку S, вторая — число запросов q. Каждая из последующих q строк задаёт
запрос тройкой чисел i, j, k и означает следующее: вырезать подстроку S[i..j] (где i и j индексируются с нуля) и вставить её после
k-го символа оставшейся строки (где k индексируется с единицы), при этом если k = 0, то вставить вырезанный кусок надо в начало.

Формат выхода. Выведите полученную (после всех q запросов) строку.

Ограничения. S содержит только буквы латинского алфавита. 1 ≤ |S| ≤ 300 000; 1 ≤ q ≤ 100 000; 0 ≤ i ≤ j ≤ n−1; 0 ≤ k ≤ n−(j−i+1).

Пример.
Вход:
hlelowrold
2
1 1 2
6 6 7
Выход:
helloworld
*/
using System;
using System.Linq;

namespace _17.Rope
{
    class Program
    {
        static void Main()
        {
            string s = Console.ReadLine();

            SplayTree tree = new SplayTree();

            foreach (char ch in s)
                tree.Add(ch);

            int n = int.Parse(Console.ReadLine());

            int[] arr;
            for (int i = 0; i < n; i++)
            {
                arr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                tree.Replace(arr[0], arr[1], arr[2]);
            }

            tree.ShowString();
            Console.Read();
        }

        public class SplayTree
        {
            public class Node
            {
                public char Value;                
                public int Count; //количество элементов в поддереве с корнем в этой ноде                

                public Node Parent;
                public Node Left;
                public Node Right;

                public Node(char value, Node parent = null)
                {
                    Value = value;
                    Count = 1;
                    Parent = parent;
                }

                public Node(Node other)
                {
                    Value = other.Value;
                    Count = other.Count;                    
                    Left = new Node(other.Left);
                    Right = new Node(other.Right);
                }

                public Node CopyFrom(Node other)
                {
                    if (this != null && other != null)
                    {
                        Value = other.Value;
                        Count = other.Count;                        
                        Left = new Node(other.Left);
                        Right = new Node (other.Right);
                    }

                    return this;
                }

                public void Update()
                {
                    Count = 1 + (Left != null ? Left.Count : 0) + (Right != null ? Right.Count : 0);                    
                }                
            }            

            public Node Root;            

            public void ShowString()
            {
                Console.WriteLine("");
                InOrderTraverse(Root);
            }

            public void ShowTree()
            {
                Console.WriteLine("Tree: ");
                PreOrderTraverse(Root);
            }

            private void PreOrderTraverse(Node node)
            {
                if (node == null) return;

                Console.Write(node.Value + "(" + node.Count + ") ");
                PreOrderTraverse(node.Left);
                PreOrderTraverse(node.Right);
            }

            static void InOrderTraverse(Node node)
            {
                if (node == null) return;

                InOrderTraverse(node.Left);
                Console.Write(node.Value);
                InOrderTraverse(node.Right);
            }

            private void Splay(Node root)
            {
                if (root != null)
                {
                    while (root.Parent != null)
                    {
                        Node grandParent = root.Parent.Parent;
                        Node parent = root.Parent;

                        if (parent.Right == root)
                        {                            
                            parent.Right = root.Left;

                            if (parent.Right != null)
                                parent.Right.Parent = parent;

                            parent.Update();

                            root.Left = parent;
                            parent.Parent = root;
                            root.Update();
                        }
                        else
                        {                            
                            parent.Left = root.Right;
                            if (parent.Left != null)
                            {
                                parent.Left.Parent = parent;
                            }
                            parent.Update();

                            root.Right = parent;
                            parent.Parent = root;
                            root.Update();
                        }

                        if (grandParent != null)
                        {
                            if (grandParent.Right == parent)
                                grandParent.Right = root;
                            else
                                grandParent.Left = root;
                        }

                        root.Parent = grandParent;
                    }

                    Root = root;
                }
            }

            public void Add(char value)
            {
                Node cur = new Node(value);

                if (Root == null)
                    Root = cur;
                else
                {
                    Root.Parent = cur;
                    cur.Left = Root;
                    cur.Count += Root.Count;                    
                    Root = cur;
                }
            }

            private Node Find(int index)
            {
                Node root = Root;

                while (root != null)
                {
                    int leftCount = root.Left != null ? root.Left.Count : 0;

                    if (leftCount == index)                                            
                        break;                    
                    else if (leftCount < index)
                    {                        
                        index -= leftCount + 1;
                        root = root.Right;
                    }
                    else                                            
                        root = root.Left;                    
                }
                
                Splay(root);

                return root;
            }

            public void Replace(int i, int j, int k)
            {
                //обзрезка
                Node left = Find(i);
                
                Node leftLeft = left.Left;
                if (leftLeft != null)                
                    leftLeft.Parent = null;
                
                left.Left = null;
                left.Update();

                Root = left;
                
                Node right = Find(j - i);
                
                Node rightRight = right.Right;
                if (rightRight != null)                
                    rightRight.Parent = null;
                
                right.Right = null;
                right.Update();

                //объединение
                if (leftLeft != null)
                {
                    Node c = leftLeft;
                    while (c.Right != null)                    
                        c = c.Right;
                    
                    c.Right = rightRight;

                    if (rightRight != null)                    
                        rightRight.Parent = c;
                    
                    do
                    {
                        c.Update();
                        c = c.Parent;
                    } while (c != null);
                }
                else                
                    leftLeft = rightRight;
                
                //вставка
                if (k > 0)
                {
                    Root = leftLeft;
                    Node c = Find(k - 1);

                    //вставляем центральную часть после k-ого
                    right.Right = c.Right;

                    if (right.Right != null)                    
                        right.Right.Parent = right;
                    
                    right.Update();
                    c.Right = right;
                    right.Parent = c;
                    c.Update();
                }
                else
                {
                    Root = right;
                    right.Right = leftLeft;

                    if (leftLeft != null)                    
                        leftLeft.Parent = right;
                    
                    right.Update();
                }
            }
        }
    }
}
