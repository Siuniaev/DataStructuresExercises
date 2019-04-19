/*
Проверить, является ли данное двоичное дерево деревом поиска.

Вы тестируете реализацию двоичного дерева поиска. У вас уже написан код, который ищет, добавляет и удаляет ключи, а также выводит внутреннее
состояние структуры данных после каждой операции. Вам осталось проверить, что в каждый момент дерево остаётся корректным деревом поиска.
Другими словами, вы хотите проверить, что для дерева корректно работает поиск, если ключ есть в дереве, то процедура поиска его обязательно найдёт,
если ключа нет — то не найдёт.

Формат входа. Первая строка содержит число вершин n. Вершины дерева пронумерованы числами от 0 до n−1. Вершина 0 является корнем.
Каждая из следующих n строк содержит информацию о вершинах 0; 1; : : : ; n− 1: i-я строка задаёт числа keyi, lefti и righti,
где keyi — ключ вершины i, lefti — индекс левого сына вершины i, а righti — индекс правого сына вершины i.
Если у вершины i нет одного или обоих сыновей, соответствующее значение равно −1.

Формат выхода. Выведите «CORRECT», если дерево является корректным деревом поиска, и «INCORRECT» в противном случае.

Ограничения. 0 ≤ n ≤ 10^5, −2^31 < keyi < 2^31 − 1, −1 ≤ lefti; righti ≤ n − 1. 
Гарантируется, что вход задаёт корректное двоичное дерево: в частности, если lefti != −1 и righti != −1, то lefti != righti;
никакая вершина не является сыном двух вершин; каждая вершина является потомком корня.

Пример.
Вход:
7
4 1 2
2 3 4
6 5 6
1 -1 -1
3 -1 -1
5 -1 -1
7 -1 -1
Выход:
CORRECT
*/

using System;
using System.Linq;

namespace _14.Проверка_свойства_дерева_поиска
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
                int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                nodes[i] = new Node(input[0], input[1], input[2]);
            }
            
            PreOrderTraverse(nodes, 0, int.MinValue, int.MaxValue);

            Console.WriteLine(Ok ? "CORRECT" : "INCORRECT");
            Console.Read();
        }

        static void PreOrderTraverse(Node[] nodes, int key, int min, int max)
        {
            if (key == -1 || Ok == false || key >= nodes.Length || key < -1) return;

            if (nodes[key].Left < -1 || nodes[key].Left >= nodes.Length || nodes[key].Right < -1 || nodes[key].Right >= nodes.Length || nodes[key].Value < min || nodes[key].Value > max)
            {
                Ok = false;
                return;
            }            

            //Console.WriteLine("key = " + key + " value = " + nodes[key].Value + " min = " + min + " max = " + max);            

            PreOrderTraverse(nodes, nodes[key].Left, min, nodes[key].Value);
            PreOrderTraverse(nodes, nodes[key].Right, nodes[key].Value, max);
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
