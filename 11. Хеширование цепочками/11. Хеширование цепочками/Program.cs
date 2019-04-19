/*
Хеширование цепочками — один из наиболее популярных методов реализации хеш-таблиц на практике.
Ваша цель в данной задаче — реализовать такую схему, используя таблицу с m ячейками и полиномиальной хеш-функцией на строках
(см. https://i.yapx.ru/D7zcX.jpg)
где S[i] — ASCII-код i-го символа строки S, p = 1 000 000 007 — простое число, а x = 263. 
Ваша программа должна поддерживать следующие типы запросов:
• add string: добавить строку string в таблицу. Если такая строка уже есть, проигнорировать запрос;
• del string: удалить строку string из таблицы. Если такой строки нет, проигнорировать запрос;
• find string: вывести «yes» или «no» в зависимости от того, есть в таблице строка string или нет;
• check i: вывести i-й список (используя пробел в качестве разделителя); если i-й список пуст, вывести пустую строку.
При добавлении строки в цепочку, строка должна добавляться в начало цепочки.

Формат входа. Первая строка размер хеш-таблицы m. Следующая строка содержит количество запросов n.
Каждая из последующих n строк содержит запрос одного из перечисленных выше четырёх типов.

Формат выхода. Для каждого из запросов типа find и check выведите результат в отдельной строке.

Ограничения. 1 ≤ n ≤ 10^5; n/5 ≤ m ≤ n. Все строки имеют длину от одного до пятнадцати и содержат только буквы латинского алфавита.

Пример.
Вход:
5
12
add world
add HellO
check 4
find World
find world
del world
check 4
del HellO
add luck
add GooD
check 2
del good
Выход:
HellO world
no
yes
HellO
GooD luck
*/

using System;
using System.Collections.Generic;

namespace _11.Хеширование_цепочками
{
    class Program
    {
        static void Main()
        {
            long m = long.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());

            HashTable table = new HashTable(m);

            for (int i = 0; i < n; i++)
            {
                string command = Console.ReadLine();
                table.Execute(command);
            }

            Console.Read();
        }

        public class HashTable
        {
            const long MagicNumber = 1000000007;
            private readonly long _capacity;
            private LinkedList<string>[] _table;

            public HashTable(long m)
            {
                _capacity = m;
                _table = new LinkedList<string>[m];

                for (long i = 0; i < _table.Length; i++)
                    _table[i] = new LinkedList<string>();
            }

            public void Execute(string com)
            {
                string[] input = com.Split(' ');
                long index;

                switch (input[0])
                {
                    case "add":
                        index = GetHash(input[1]);
                        if (!_table[index].Contains(input[1]))
                            _table[index].AddFirst(input[1]);
                        break;

                    case "find":
                        index = GetHash(input[1]);                        
                        Console.WriteLine(_table[index].Contains(input[1]) ? "yes" : "no");
                        break;

                    case "del":
                        index = GetHash(input[1]);
                        if (_table[index].Contains(input[1]))
                            _table[index].Remove(input[1]);
                        break;

                    case "check":
                        index = long.Parse(input[1]);                        
                        Console.WriteLine(_table[index].Count > 0 ? string.Join(" ", _table[index]) : "");                        
                        break;
                }
            }

            private long GetHash(string s)
            {
                long sum = 0;

                for (int i = 0; i < s.Length; i++)
                    sum += s[i] * MyPower(263, i) % MagicNumber;

                return sum % MagicNumber % _capacity;
            }

            public long MyPower(long A, int n)
            {
                long result = 1;

                if (n > 0)
                {
                    do
                    {
                        result = result * A % MagicNumber;
                        n--;
                    } while (n > 0);
                }
                return result;
            }
        }
    }
}
