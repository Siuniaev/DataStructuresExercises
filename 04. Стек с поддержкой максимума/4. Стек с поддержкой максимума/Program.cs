/*
Реализовать стек с поддержкой операций push, pop и max.
В данной задача ваша цель — расшить интерфейс стека так, чтобы он дополнительно поддерживал операцию max
и при этом чтобы время работы всех операций по-прежнему было константным.

Формата входа. Первая строка содержит число запросов q. 
Каждая из последующих q строк задаёт запрос в одном из следующих форматов: push v, pop, or max.

Формат выхода. Для каждого запроса max выведите (в отдельной строке) текущий максимум на стеке.

Ограничения. 1 ≤ q ≤ 400 000, 0 ≤ v ≤ 100 000

Пример.
Вход:
5
push 2
push 1
max
pop
max
Выход:
22
*/

using System;
using System.Collections.Generic;

namespace _4.Стек_с_поддержкой_максимума
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            Stack<int> max = new Stack<int>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ');

                switch (input[0])
                {
                    case "push":
                        int value = int.Parse(input[1]);
                        stack.Push(value);
                        max.Push(max.Count > 0 ? Math.Max(value, max.Peek()) : value);                        
                        break;

                    case "pop":
                        stack.Pop();
                        max.Pop();
                        break;

                    case "max":
                        if (max.Count > 0)
                            Console.WriteLine(max.Peek());
                        break;
                }
            }

            Console.Read();
        }
    }
}
