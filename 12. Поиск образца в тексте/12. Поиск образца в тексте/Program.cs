/*
Найти все вхождения строки Pattern в строку Text.
Реализуйте алгоритм Карпа–Рабина.

Формат входа. Образец Pattern и текст Text.

Формат выхода. Индексы вхождений строки Pattern в строку Text в возрастающем порядке, используя индексацию с нуля.

Ограничения. 1 ≤ |Pattern| ≤ |Text| ≤ 5*10^5.
Суммарная длина всех вхождений образца в текста не превосходит 10^8. Обе строки содержат буквы латинского алфавита.

Пример.
Вход:
aba
abacaba
Выход:
0 4
*/

using System;

namespace _12.Поиск_образца_в_тексте
{
    class Program
    {
        public static void Main()
        {
            string pattern = Console.ReadLine();
            string text = Console.ReadLine();
            
            RabinKarp(text, pattern);

            Console.Read();
        }

        static void RabinKarp(string text, string pattern)
        {
            int Q = 101;
            int D = 256; //кол-во возможных символов
            int hashPattern = 0;
            int hashText = 0;

            int h = 1;
            for (int i = 0; i < pattern.Length - 1; i++)
                h = h * D % Q;

            for (int i = 0; i < pattern.Length; i++)
            {
                hashPattern = (D * hashPattern + pattern[i]) % Q;
                hashText = (D * hashText + text[i]) % Q;
            }

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                if (hashPattern == hashText)
                {
                    for (int j = 0; j < pattern.Length; j++)
                    {
                        if (text[i + j] != pattern[j]) break;

                        if (j == pattern.Length-1)
                            Console.Write(i + " ");
                    }
                }

                if (i < text.Length - pattern.Length)
                {
                    hashText = (D * (hashText - text[i] * h) + text[i + pattern.Length]) % Q;

                    if (hashText < 0)
                        hashText += Q;
                }
            }
        }
    }
}
