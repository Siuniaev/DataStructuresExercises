/*
Реализовать симуляцию объединения таблиц в базе данных.

В базе данных есть n таблиц, пронумерованных от 1 до n, над одним и тем же множеством столбцов (атрибутов).
Каждая таблица содержит либо реальные записи в таблице, либо символьную ссылку на другую таблицу.
Изначально все таблицы содержат реальные записи, и i-я таблица содержит ri записей. 
Ваша цель — обработать m запросов типа (destinationi; sourcei):
1. Рассмотрим таблицу с номером destinationi. Пройдясь по цепочке символьных ссылок, найдём номер реальной таблицы, на которую ссылается эта таблица:
пока таблица destinationi содержит символическую ссылку:
destinationi <- symlink(destinationi)
2. Сделаем то же самое с таблицей sourcei.
3. Теперь таблицы destinationi и sourcei содержат реальные записи. Если destinationi != sourcei, скопируем все записи из таблицы
sourcei в таблицу destinationi, очистим таблицу sourcei и пропишем в неё символическую ссылку на таблицу destinationi.
4. Выведем максимальный размер среди всех n таблиц. Размером таблицы называется число строк в ней. Если таблица содержит
символическую ссылку, считаем её размер равным нулю.

Формат входа. Первая строка содержит числа n и m — число таблиц и число запросов, соответственно.
Вторая строка содержит n целых чисел r1; : : : ; rn — размеры таблиц. 
Каждая из последующих m строк содержит два номера таблиц destinationi и sourcei, которые необходимо объединить.

Формат выхода. Для каждого из m запросов выведите максимальный размер таблицы после соответствующего объединения.

Ограничения. 1 ≤ n; m ≤ 100 000; 0 ≤ ri ≤ 10 000; 1 ≤ destinationi; sourcei ≤ n.

Пример.
Вход:
5 5
1 1 1 1 1
3 5
2 4
1 4
5 4
5 3
Выход:
22355
*/

using System;
using System.Linq;

namespace _8.Объединение_таблиц
{
    class Program
    {
        static void Main()
        {
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int n = input[0]; //число таблиц
            int m = input[1]; //число запросов

            //массив размеров таблиц (индекс: номер таблицы, значение: размер таблицы)
            int[] sizes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            //массив ссылок на таблицы (индекс: номер таблицы, значение: номер таблицы, на которую она ссылается)
            int[] tables = new int[n];
            for (int i = 0; i < tables.Length; i++)
                tables[i] = i;

            int currentMax = sizes.Max(); //масимальный размер среди таблиц

            for (int i = 0; i < m; i++)
            {
                input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                int d = input[0] - 1; //номер таблицы-назначения
                int s = input[1] - 1; //номер таблицы  источника

                int dp = Find(tables, d);
                int sp = Find(tables, s);

                if (dp != sp)
                {   
                    sizes[tables[dp]] += sizes[tables[sp]];
                    tables[sp] = tables[dp];

                    if (sizes[tables[d]] > currentMax)
                        currentMax = sizes[tables[d]];
                }
                //Console.WriteLine("sizes: " + string.Join(" ", sizes) + " tables: " + string.Join(" ", tables));
                Console.WriteLine(currentMax);
            }

            Console.Read();
        }

        static int Find(int[] tables, int i)
        {
            if (i == tables[i])
                return i;

            return tables[i] = Find(tables, tables[i]);
        }
    }
}
