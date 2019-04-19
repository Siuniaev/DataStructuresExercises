/*
Построить in-order, pre-order и post-order обходы данного двоичного дерева.

Формат входа. Первая строка содержит число вершин n. Вершины дерева пронумерованы числами от 0 до n−1. Вершина 0 является корнем.
Каждая из следующих n строк содержит информацию о вершинах 0; 1; : : : ; n− 1: i-я строка задаёт числа keyi, lefti и righti,
где keyi — ключ вершины i, lefti — индекс левого сына вершины i, а righti — индекс правого сына вершины i. 
Если у вершины i нет одного или обоих сыновей, соответствующее значение равно −1.

Формат выхода. Три строки: in-order, pre-order и post-order обходы.

Ограничения. 1 ≤ n ≤ 10^5; 0 ≤ keyi ≤ 10^9; −1 ≤ lefti; righti ≤ n − 1.
Гарантируется, что вход задаёт корректное двоичное дерево: в частности, если lefti != −1 и righti != −1, то lefti != righti; 
никакая вершина не является сыном двух вершин; каждая вершина является потомком корня.

Пример.
Вход:
5
4 1 2
2 3 4
5 -1 -1
1 -1 -1
3 -1 -1
Выход:
1 2 3 4 5
4 2 1 3 5
1 3 2 5 4
*/

using System;
using System.Linq;

namespace _13.Обход_двоичного_дерева
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            Node[] nodes = new Node[n];

            for (int i = 0; i < n; i++)
            {
                int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();                
                nodes[i] = new Node(input[0], input[1], input[2]);
            }

            InOrderTraverse(nodes, 0);
            Console.WriteLine("");

            PreOrderTraverse(nodes, 0);
            Console.WriteLine("");

            PostOrderTraverse(nodes, 0);            

            Console.Read();
        }

        static void InOrderTraverse(Node[] nodes, int key)
        {
            if (key == -1) return;

            InOrderTraverse(nodes, nodes[key].Left);
            Console.Write(nodes[key].Value + " ");
            InOrderTraverse(nodes, nodes[key].Right);
        }

        static void PreOrderTraverse(Node[] nodes, int key)
        {
            if (key == -1) return;

            Console.Write(nodes[key].Value + " ");
            PreOrderTraverse(nodes, nodes[key].Left);
            PreOrderTraverse(nodes, nodes[key].Right);
        }

        static void PostOrderTraverse(Node[] nodes, int key)
        {
            if (key == -1) return;
            
            PostOrderTraverse(nodes, nodes[key].Left);
            PostOrderTraverse(nodes, nodes[key].Right);
            Console.Write(nodes[key].Value + " ");
        }
               
        public struct Node
        {
            public int Value;
            public int Left;
            public int Right;

            public Node(int value, int left, int right)
            {
                Value = value;
                Left = left;
                Right = right;
            }
        }        
    }
}
