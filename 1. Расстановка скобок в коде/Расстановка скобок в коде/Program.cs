﻿/*
Проверить, правильно ли расставлены скобки в данном коде.

Вы разрабатываете текстовый редактор для программистов и хотите реализовать проверку корректности расстановки скобок.
В коде могут встречаться скобки []{}(). Из них скобки [,{ и ( считаются открывающими, а соответствующими им закрывающими
скобками являются ],} и ).
В случае, если скобки расставлены неправильно, редактор должен также сообщить пользователю первое место, где обнаружена ошибка.
В первую очередь необходимо найти закрывающую скобку, для которой либо нет соответствующей открывающей (например, скобка ] в
строке “]()”), либо же она закрывает не соответствующую ей открывающую скобку (пример: “()[}”).
Если таких ошибок нет, необходимо найти первую открывающую скобку, для которой нет соответствующей закрывающей (пример: скобка ( в строке “{}([]”).
Помимо скобок, исходный код может содержать символы латинского алфавита, цифры и знаки препинания.

Формат входа. Строка s[1 : : : n], состоящая из заглавных и прописных букв латинского алфавита, цифр, знаков препинания и скобок из множества []{}().

Формат выхода. Если скобки в s расставлены правильно, выведите
строку “Success". В противном случае выведите индекс (используя индексацию с единицы) первой закрывающей скобки, для
которой нет соответствующей открывающей. Если такой нет,
выведите индекс первой открывающей скобки, для которой нет
соответствующей закрывающей.

Ограничения. 1 ≤ n ≤ 10^5

Пример.
Вход:
{[]}()
Выход:
Success
*/

using System;
using System.Collections.Generic;

namespace Расстановка_скобок_в_коде
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            int result = Check(input);

            Console.WriteLine(result == -1 ? "Success" : result.ToString());
            Console.Read();
        }

        static int Check(string s)
        {
            Stack<char> stack = new Stack<char>();
            Stack<int> stackIndexes = new Stack<int>();

            for (int i = 0; i< s.Length; i++)
            {
                char ch = s[i];

                if (ch == '(' || ch == '[' || ch == '{')
                {
                    stack.Push(ch);
                    stackIndexes.Push(i);
                }
                else if (ch == ')' || ch == ']' || ch == '}')
                {
                    if (stack.Count == 0)
                        return i + 1;

                    char top = stack.Pop();
                    int temp = stackIndexes.Pop();

                    if ((top == '[' && ch != ']') || (top == '{' && ch != '}') || (top == '(' && ch != ')'))
                        return i + 1;
                }
            }

            return stack.Count == 0 ? -1 : stackIndexes.Pop() + 1;
        }
    }
}
