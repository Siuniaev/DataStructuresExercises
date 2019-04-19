/*
Переставить элементы заданного массива чисел так, чтобы он удовлетворял свойству мин-кучи.

Формат входа. Первая строка содержит число n. Следующая строка задаёт массив чисел A[0]; : : : ; A[n − 1].

Формат выхода. Первая строка выхода должна содержать число обменов m, которое должно удовлетворять неравенству 0 ≤ m ≤4n.
Каждая из последующих m строк должна задавать обмен двух элементов массива A. Каждый обмен задаётся парой различных
индексов 0 ≤ i != j ≤ n − 1. После применения всех обменов в указанном порядке массив должен превратиться в мин-кучу,
то есть для всех 0 ≤ i ≤ n − 1 должны выполняться следующие два условия:
• если 2i + 1 ≤ n − 1, то A[i] < A[2i + 1]
• если 2i + 2 ≤ n − 1, то A[i] < A[2i + 2]

Ограничения. 1 ≤ n ≤ 10^5; 0 ≤ A[i] ≤ 10^9 для всех 0 ≤ i ≤ n − 1; все A[i] попарно различны; i != j.

Пример.
Вход:
5
5 4 3 2 1
Выход:
3
1 4
0 1
1 3
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace _6.Построение_кучи
{
    class Program
    {
        static void Main()
        {
            int N = int.Parse(Console.ReadLine());
            int[] a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            List<string> pairs = new List<string>();

            for (int i = N / 2 - 1; i >= 0; i--)
                Heapify(a, i, N, pairs);

            Console.WriteLine(pairs.Count);
            pairs.ForEach(Console.WriteLine);

            Console.Read();
        }

        static void Heapify(int[]a, int i, int N, List<string> pairs)
        {
            int temp;
            int iMin = i;
            int m1 = i * 2 + 1;
            int m2 = i * 2 + 2;

            if (m1 < N && a[iMin] > a[m1])
                iMin = m1;

            if (m2 < N && a[iMin] > a[m2])
                iMin = m2;

            if (i != iMin)
            {
                temp = a[i];
                a[i] = a[iMin];
                a[iMin] = temp;

                pairs.Add(i + " " + iMin);

                Heapify(a, iMin, N, pairs);
            }
        }
    }
}
