/*
Реализовать структуру данных, эффективно обрабатывающую запросы вида add number name, del number и find number.
• add number name: добавить запись с именем name и телефонным номером number. 
Если запись с таким телефонным номером уже есть, нужно заменить в ней имя на name.
• del number: удалить запись с соответствующим телефонным номером. Если такой записи нет, ничего не делать.
• find number: найти имя записи с телефонным номером number. Если запись с таким номером есть, вывести имя.
В противном случае вывести «not found» (без кавычек).

Формат входа. Первая строка содержит число запросов n. Каждая из следующих n строк задаёт запрос в одном из трёх описанных выше форматов.

Формат выхода. Для каждого запроса find выведите в отдельной строке либо имя, либо «not found».

Ограничения. 1 ≤ n ≤ 10^5. Телефонные номера содержат не более семи цифр и не содержат ведущих нулей.
Имена содержат только буквы латинского алфавита, не являются пустыми строками и имеют длину не больше 15.
Гарантируется, что среди имён не встречается строка «not found».

Пример.
Вход:
12
add 911 police
add 76213 Mom
add 17239 Bob
find 76213
find 910
find 911
del 910
del 911
find 911
find 76213
add 76213 daddy
find 76213
Выход:
Mom
not found
police
not found
Mom
daddy
*/
using System;

namespace _10.Телефонная_книга
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            PhoneBook book = new PhoneBook();

            for (int i = 0; i < n; i++)
            {
                string command = Console.ReadLine();
                book.Execute(command);
            }

            Console.Read();
        }

        public class PhoneBook
        {            
            string[] names = new string[10000000];

            public void Execute(string com)
            {
                string[] input = com.Split(' ');

                int index = int.Parse(input[1]);

                switch (input[0])
                {
                    case "add":
                        names[index] = input[2];
                        break;

                    case "find":
                        Console.WriteLine(string.IsNullOrEmpty(names[index]) ? "not found" : names[index]);
                        break;

                    case "del":
                        names[index] = "";
                        break;
                }
            }
        }
    }
}
