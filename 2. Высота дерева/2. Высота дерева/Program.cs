/*
Вычислить высоту данного дерева

Формата входа. Первая строка содержит натуральное число n. Вторая строка содержит n целых чисел parent0, . . . , parentn−1. 
Для каждого 0 ≤ i ≤ n − 1, parenti — родитель вершины i; если parenti = −1, то i является корнем. 
Гарантируется, что корень ровно один. Гарантируется, что данная последовательность задаёт дерево.

Формат выхода. Высота дерева.

Ограничения. 1 ≤ n ≤ 10^5.

Пример.
Вход:
5
4 -1 4 1 1
Выход:
3
*/

using System;
using System.Linq;

namespace _2.Высота_дерева
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int height = GetTreeHeight(arr);

            Console.WriteLine(height);
            Console.Read();            
        }

        static int GetTreeHeight(int[] a)
        {
            int[] heights = new int[a.Length];
            int maxHeight = 0;            

            for (int i = 0; i < a.Length; i++)
            {
                if (heights[i] > 0) continue;

                int current = i;
                while(a[current] != -1)
                {
                    int nextHeight = heights[current] + 1;
                    heights[a[current]] = nextHeight;

                    if (nextHeight > maxHeight)
                        maxHeight = nextHeight;

                    current = a[current];
                }
            }

            return maxHeight + 1;
        }
    }
}
