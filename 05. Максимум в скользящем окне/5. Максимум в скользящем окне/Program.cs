/*
Найти максимум в каждом окне размера m данного массива чисел A[1 : : : n].
Наивный способ решить данную задачу — честно просканировать каждое окно и найти в нём максимум.
Время работы такого алгоритма — O(nm). Ваша задача — реализовать алгоритм со временем работы O(n).

Формат входа. Первая строка входа содержит число n, вторая — массив A[1 : : : n], третья — число m.

Формат выхода. n − m + 1 максимумов, разделённых пробелами.

Ограничения. 1 ≤ n ≤ 10^5, 1 ≤ m ≤ n, 0 ≤ A[i] ≤ 10^5 для всех 1 ≤ i ≤ n.

Пример.
Вход:
8
2 7 3 1 5 2 6 2
4
Выход:
7 7 5 6 6
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace _5.Максимум_в_скользящем_окне
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int m = int.Parse(Console.ReadLine());
            
            Deque window = new Deque();            

            for(int i = 0; i < n; i++)
            {
                int value = a[i];

                if (i-m > -1 && window.Count > 0 && window.PeekLast == a[i - m])
                    window.DequeueLast();
                
                while (window.Count > 0 && window.PeekFirst < value)
                    window.DequeueFirst();                

                window.EnqueueFirst(value);

                if (i >= m - 1)
                    Console.Write(window.PeekLast + (i != n - 1 ? " " : ""));
            }
            
            Console.Read();
        }

        //очередь на стеках с поддержкой максимума (здесь что-то неправильно, работает, но медленно, через деку в итоге решил)
        public class QueueCool
        {
            private Stack<int> back = new Stack<int>();
            private Stack<int> front = new Stack<int>();
            private int maxLength = 1;
            private Stack<int> backMax = new Stack<int>();
            private Stack<int> frontMax = new Stack<int>();            

            public QueueCool(int length)
            {
                this.maxLength = length;
            }

            public void Enqueue(int value)
            {
                while (back.Count > 0)
                {
                    front.Push(back.Pop());
                    frontMax.Push(backMax.Pop());
                }                

                back.Push(value);
                backMax.Push(value);

                while (front.Count > 0)
                {
                    back.Push(front.Pop());

                    int oldMax = backMax.Peek();
                    backMax.Push(Math.Max(frontMax.Pop(), oldMax));
                }

                if (back.Count > maxLength)
                    this.Dequeue();
            }

            public int Dequeue()
            {                
                if (back.Count == 0)
                    throw new Exception("Queue is Empty");

                backMax.Pop();

                return back.Pop();
            }

            public int Peek()
            {
                if (back.Count == 0)
                    throw new Exception("Queue is Empty");

                return back.Peek();
            }

            public int Count => back.Count + front.Count;

            public int Max()
            {                
                if (backMax.Count == 0)
                    throw new Exception("Queue is Empty");

                return backMax.Peek();
            }            
        }

        //двусторонняя очередь на двусвязном списке
        public class Deque
        {
            public class Node
            {
                public int Data;
                public Node Previous;
                public Node Next;

                public Node(int value)
                {
                    Data = value;
                }
            }

            private Node Head { get; set; } //голова в начале очереди
            private Node Tail { get; set; } //хвост в конце очереди
            public int Count { get; private set; }

            //добавление в начало (в голову)
            public void EnqueueFirst(int value)
            {
                Node node = new Node(value);                

                node.Next = Head;

                if (Head != null)
                    Head.Previous = node;
                else
                    Tail = node;

                Head = node;                

                Count++;
            }

            //добавление в конец (в хвост)
            public void EnqueueLast(int value)
            {
                Node node = new Node(value);

                if (Head == null)
                    Head = node;
                else
                {
                    Tail.Next = node;
                    node.Previous = Tail;
                }

                Tail = node;

                Count++;
            }

            //извлечение из начала (из головы)
            public int DequeueFirst()
            {
                if (Count == 0)
                    throw new InvalidOperationException();

                int value = Head.Data;

                if (Count == 1)
                    Head = Tail = null;
                else
                {
                    Head = Head.Next;
                    Head.Previous = null;
                }

                Count--;

                return value;
            }

            //извлечение из конца (из хвоста)
            public int DequeueLast()
            {
                if (Count == 0)
                    throw new InvalidOperationException();
                int value = Tail.Data;

                if (Count == 1)                
                    Head = Tail = null;                
                else
                {
                    Tail = Tail.Previous;
                    Tail.Next = null;
                }

                Count--;

                return value;
            }

            //посмотреть начало (голову)
            public int PeekFirst => Head.Data;
            //посмотреть конец (хвост)
            public int PeekLast => Tail.Data;
        }
    }
}
