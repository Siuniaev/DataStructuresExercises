/*
Проверить, можно ли присвоить переменным целые значения, чтобы выполнить заданные равенства вида xi = xj и неравенства вида xp != xq.

Формат входа. Первая строка содержит числа n, e, d. Каждая из следующих e строк содержит два числа i и j и задаёт равенство xi = xj.
Каждая из следующих d строк содержит два числа i и j и задаёт неравенство xi != xj. Переменные индексируются с 1: x1; : : : ; xn.

Формат выхода. Выведите 1, если переменным x1; : : : ; xn можно присвоить целые значения, чтобы все равенства и неравенства
выполнились. В противном случае выведите 0.

Ограничения. 1 ≤ n ≤ 10^5; 0 ≤ e,d; e + d ≤ 2*10^5; 1 ≤ i,j ≤ n.
Пример.
Вход:
4 6 0
1 2
1 3
1 4
2 3
2 4
3 4
Выход:
1
*/
using System;
using System.Linq;

namespace _9.Автоматический_анализ_программ
{
    class Program
    {
        static void Main()
        {
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int n = input[0]; //кол-во чисел
            int e = input[1]; //кол-во строк с равенством
            int d = input[2]; //кол-во строк с неравенством

            int[] parents = new int[n + 1];
            for (int i = 1; i < n + 1; i++)            
                parents[i] = i;            

            for (int i = 0; i < e; i++)
            {
                input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                int a = input[0];
                int b = input[1];

                parents[b] = parents[a];
            }

            int[][] ds = new int[d][];
            for (int i = 0; i < d; i++)            
                ds[i] = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            CheckForEquals(ds, parents);

            Console.Read();
        }

        static void CheckForEquals(int[][] ds, int[] parents)
        {
            foreach (int[] a in ds)
            {
                if (parents[a[0]] == parents[a[1]])
                {
                    Console.WriteLine(0);
                    return;
                }
            }

            Console.WriteLine(1);
        }
    }
}
