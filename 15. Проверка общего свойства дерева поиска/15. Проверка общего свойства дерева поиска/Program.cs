/*
Данная задача полностью аналогична предыдущей, но проверять теперь нужно более общее свойство. Дереву разрешается содержать
равные ключи, но они всегда должны находиться в правом поддереве. Формально, двоичное дерево называется деревом поиска, если для
любой вершины её ключ больше всех ключей из её левого поддерева и не меньше всех ключей из правого поддерева.

Пример.
Вход:
5
1 -1 1
2 -1 2
3 -1 3
4 -1 4
5 -1 -1
Выход:
CORRECT
*/

using System;
using System.Linq;

namespace _15.Проверка_общего_свойства_дерева_поиска
{
    class Program
    {
        public static bool Ok = true;

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            Node[] nodes = new Node[n];

            for (int i = 0; i < n; i++)
            {
                long[] input = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();
                nodes[i] = new Node(input[0], (int)input[1], (int)input[2]);
            }

            PreOrderTraverse(nodes, 0, long.MinValue, long.MaxValue);

            Console.WriteLine(Ok ? "CORRECT" : "INCORRECT");
            Console.Read();
        }

        static void PreOrderTraverse(Node[] nodes, int key, long min, long max)
        {
            if (key == -1 || Ok == false || key >= nodes.Length || key < -1) return;

            if (nodes[key].Left < -1 || nodes[key].Left >= nodes.Length || nodes[key].Right < -1 || nodes[key].Right >= nodes.Length || nodes[key].Value < min || nodes[key].Value > max)
            {
                Ok = false;
                return;
            }

            //Console.WriteLine("key = " + key + " value = " + nodes[key].Value + " min = " + min + " max = " + max);            

            PreOrderTraverse(nodes, nodes[key].Left, min, nodes[key].Value - 1);
            PreOrderTraverse(nodes, nodes[key].Right, nodes[key].Value, max);
        }

        public struct Node
        {
            public long Value;
            public int Left;
            public int Right;

            public Node(long value, int left, int right)
            {
                Value = value;
                Left = left;
                Right = right;
            }
        }
    }
}
