/*
По данным n процессорам и m задач определите, для каждой из задач, каким процессором она будет обработана.

У вас имеется n процессоров и последовательность из m задач. Для каждой задачи дано время, необходимое на её обработку.
Очередная работа поступает к первому доступному процессору (то есть если доступных процессоров несколько, то доступный процессор
с минимальным номером получает эту работу).

Формат входа. Первая строка входа содержит числа n и m. Вторая содержит числа t0; : : : ; tm−1, где ti — время, необходимое на обработку i-й задачи.
Считаем, что и процессоры, и задачи нумеруются с нуля.

Формат выхода. Выход должен содержать ровно m строк: i-я (считая с нуля) строка должна содержать номер процесса,
который получит i-ю задачу на обработку, и время, когда это произойдёт.

Ограничения. 1 ≤ n ≤ 10^5; 1 ≤ m ≤ 10^5; 0 ≤ ti ≤ 10^9

Пример.
Вход:
2 5
1 2 3 4 5
Выход:
0 0
1 0
0 1
1 2
0 4
*/

using System;
using System.Linq;

namespace _7.Параллельная_обработка
{
    class Program
    {
        static void Main()
        {
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int n = input[0]; //кол-во процессоров
            int m = input[1]; //кол-во задач

            //время, необходимое на обработку задачи
            long[] times = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();

            ProcessorsHeap heap = new ProcessorsHeap(n);            

            for (int i = 0; i < times.Length; i++)
            {   
                Processor min = heap.Peek();
                Console.WriteLine(min.Proc + " " + min.Time);
                heap.SetNewTime(min.Time + times[i]);
            }

            Console.Read();
        }

        public class ProcessorsHeap
        {
            private Processor[] arr;            
            public int Count { get; private set; }

            private ProcessorsHeap() { }
            public ProcessorsHeap(int m)
            {
                Count = m;
                arr = new Processor[m];
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = new Processor(0, i);
            }

            public Processor Peek() => arr[0];            

            public void SetNewTime(long time)
            {
                arr[0].Time = time;
                ShiftDown(0);
            }

            public void ShiftDown(int i)
            {
                Processor temp;
                int iMin = i;
                int m1 = i * 2 + 1;
                int m2 = i * 2 + 2;

                if (m1 < Count && (arr[iMin].Time > arr[m1].Time || (arr[iMin].Time == arr[m1].Time && arr[iMin].Proc > arr[m1].Proc)))
                    iMin = m1;

                if (m2 < Count && (arr[iMin].Time > arr[m2].Time || (arr[iMin].Time == arr[m2].Time && arr[iMin].Proc > arr[m2].Proc)))
                    iMin = m2;

                if (i != iMin)
                {
                    temp = arr[i];
                    arr[i] = arr[iMin];
                    arr[iMin] = temp;

                    ShiftDown(iMin);
                }
            }
        }

        public class Processor
        {
            public long Time;
            public int Proc;            

            public Processor(long time, int proc)
            {                
                Time = time;
                Proc = proc;
            }
        }
    }
}
