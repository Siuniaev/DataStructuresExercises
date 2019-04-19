/*
Реализуйте структуру данных для хранения множества целых чисел, поддерживающую запросы добавления, удаления, поиска, а также суммы на отрезке.
На вход в данной задаче будет дана последовательность таких запросов. 
Чтобы гарантировать, что ваша программа обрабатывает каждый запрос по мере поступления (то есть онлайн),
каждый запрос будет зависеть от результата выполнения одного из предыдущих запросов.
Если бы такой зависимости не было, задачу можно было бы решить оффлайн: сначала прочитать весь вход и сохранить все запросы в каком-нибудь виде,
а потом прочитать вход ещё раз, параллельно отвечая на запросы.

Формат входа. Изначально множество пусто. Первая строка содержит число запросов n. 
Каждая из n следующих строк содержит запрос в одном из следующих четырёх форматов:
• + i: добавить число f(i) в множество (если оно уже есть, проигнорировать запрос);
• - i: удалить число f(i) из множества (если его нет, проигнорировать запрос);
• ? i: проверить принадлежность числа f(i) множеству;
• s l r: посчитать сумму всех элементов множества, попадающих в отрезок [f(l); f(r)].
Функция f определяется следующим образом. Пусть s — результат последнего запроса суммы на отрезке (если таких запросов ещё не было, то s = 0).
Тогда f(x) = (x + s) mod 1 000 000 001.

Формат выхода. Для каждого запроса типа ? i выведите «Found» или «Not found». 
Для каждого запроса суммы выведите сумму всех элементов множества, попадающих в отрезок [f(l); f(r)]. 
Гарантируется, что во всех тестах f(l) ≤ f(r). 

Ограничения. 1 ≤ n ≤ 10^5; 0 ≤ i ≤ 10^9

Пример.
Вход:
15
? 1
+ 1
? 1
+ 2
s 1 2
+ 1000000000
? 1000000000
- 1000000000
? 1000000000
s 999999999 1000000000
- 2
? 2
- 0
+ 9
s 0 9
Выход:
Not found
Found
3
Found
Not found
1
Not found
10
*/
using System;

namespace _16.Множество_с_запросами_суммы_на_отрезке
{
    class Program
    {
        static void Main()
        {            
            string[] input = Console.ReadLine().Split(' ');
            int n = int.Parse(input[0]);

            AVLTree tree = new AVLTree();

            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine().Split(' ');
                long value = long.Parse(input[1]);

                switch (input[0])
                {
                    case "?":
                        Console.WriteLine(tree.Find(tree.GetModifiedValue(value)) != null ? "Found" : "Not found");
                        //Console.WriteLine(tree.Find(value) != null ? "Found" : "Not found");
                        break;

                    case "+":
                        tree.Add(tree.GetModifiedValue(value));
                        //tree.Add(value);
                        break;

                    case "-":
                        tree.Remove(tree.GetModifiedValue(value));
                        //tree.Remove(value);
                        break;

                    case "s":
                        tree.Sum(tree.GetModifiedValue(value), tree.GetModifiedValue(long.Parse(input[2])));
                        break;
                }
            }

            Console.Read();
        }

        public class AVLTree
        {
            public Node Root;
            public int Size;
            private long _sum;

            public AVLTree()
            {
                Size = 0;
                _sum = 0;
            }

            public long GetModifiedValue(long value)
            {
                return (value + _sum) % 1000000001;
            }            

            private void ShowTree()
            {
                Console.WriteLine("Tree: ");
                PreOrderTraverse(Root);
            }

            private void PreOrderTraverse(Node node)
            {
                if (node == null) return;

                Console.Write(node.Value + "(" + node.Height + "):" + node.Sum + " ");
                PreOrderTraverse(node.Left);
                PreOrderTraverse(node.Right);
            }

            public void Add(long value)
            {                
                Node node = new Node(value);
                this.AddNode(node); //добавление ноды с просеиванием вниз                

                //балансировка дерева снизу вверх, начиная с родителя добавленной ноды
                Node parent = node.Parent;
                while (parent != null)
                {
                    int balance = parent.GetBalance();

                    if (Math.Abs(balance) == 2) //-2 или 2 = разбалансировано                    
                        BalanceAt(parent, balance);                    
                    else
                        parent.RecalculateHeightAndSum(); //если не было балансировки(где пересчитывается высота с суммой родителя), то нужно отдельно запустить пересчет высоты с суммой

                    parent = parent.Parent;
                }

                //ShowTree();
            }

            private void AddNode(Node node) //добавление с просеиванием вниз
            {
                if (Root == null)
                {
                    Root = node;
                    Size++;
                }
                else
                {
                    if (node.Parent == null)
                        node.Parent = Root; //подвешивается к корню

                    if (node.Value < node.Parent.Value) //если меньше родителя - влево падает //(в нормальном дереве тут <=, и последнего условия нет, чтоб дуликаты можно было добавлять)
                    {
                        if (node.Parent.Left == null)
                        {
                            node.Parent.Left = node;
                            Size++;
                        }
                        else
                        {
                            node.Parent = node.Parent.Left; //переподвешивается к левому ребенку своего родителя
                            AddNode(node); //просеивает себя дальше
                        }
                    }
                    else if (node.Value > node.Parent.Value) //если больше родителя - вправо падает
                    {
                        if (node.Parent.Right == null)
                        {
                            node.Parent.Right = node;
                            Size++;
                        }
                        else
                        {
                            node.Parent = node.Parent.Right; //переподвешивается к правому ребенку своего родителя
                            AddNode(node); //просеивает себя дальше
                        }
                    }
                    else //уже есть такая нода, эту отбрасываем
                    {
                        node.Parent = null;
                        return;
                    }
                }
            }

            private void BalanceAt(Node node, int balance)
            {
                if (balance == 2) //справа перевес
                {
                    int rightBalance = node.Right.GetBalance();

                    if (rightBalance == 1 || rightBalance == 0)
                    {
                        //вращение влево вокруг текущей ноды
                        RotateLeft(node);
                    }
                    else if (rightBalance == -1)
                    {
                        //вращение вправо вокруг правой ноды
                        RotateRight(node.Right);

                        //вращение влево вокруг текущей ноды
                        RotateLeft(node);
                    }
                }
                else if (balance == -2) //слева перевес
                {
                    int leftBalance = node.Left.GetBalance();
                    if (leftBalance == 1)
                    {
                        //вращение влево вокруг левой ноды
                        RotateLeft(node.Left);

                        //вращение вправо вокруг текущей ноды
                        RotateRight(node);
                    }
                    else if (leftBalance == -1 || leftBalance == 0)
                    {
                        //вращение вправо вокруг текущей ноды
                        RotateRight(node);
                    }
                }
            }            

            public void Remove(long value)
            {
                Node node = Find(value);
                if (node == null) return;                

                RemoveNode(node);                

                //ShowTree();
            }

            private void RemoveNode(Node node)
            {
                Node parent = node.Parent; //перед удалением ноды, запоминаем её родителя, чтоб балансировку там проверять
                bool wasRoot = Root == node;

                if (Size == 1) //удаление корня
                {
                    Root = null;
                    Size--;
                }
                else if (node.Left == null && node.Right == null) //если нет детей
                {
                    if (node.Parent != null)
                    {
                        if (node.Parent.Left == node)
                            node.Parent.Left = null;
                        else
                            node.Parent.Right = null;

                        node.Parent = null;
                        Size--;
                    }
                }
                else if (node.Left != null && node.Right != null) //есть 2 детей
                {                    
                    Node successorNode = node.Left;

                    while (successorNode.Right != null)
                        successorNode = successorNode.Right;

                    node.Value = successorNode.Value;
                    //node.RecalculateHeightAndSum();

                    RemoveNode(successorNode);//рекурсия
                }
                else //есть 1 ребенок
                {
                    if (node.Left != null)
                    {                        
                        node.Left.Parent = node.Parent;

                        if (wasRoot)
                            Root = node.Left;

                        if (node.Parent != null)
                        {
                            if (node.Parent.Left == node)
                                node.Parent.Left = node.Left;
                            else
                                node.Parent.Right = node.Left;
                        }
                    }
                    else
                    {
                        node.Right.Parent = node.Parent;

                        if (wasRoot)
                            Root = node.Right;

                        if (node.Parent != null)
                        {
                            if (node.Parent.Left == node)
                                node.Parent.Left = node.Right;
                            else
                                node.Parent.Right = node.Right;
                        }
                    }

                    node.Parent = null;
                    node.Left = null;
                    node.Right = null;
                    Size--;
                }

                //балансировка дерева, если высота изменилась
                while (parent != null)
                {
                    int balance = parent.GetBalance();                    

                    if (Math.Abs(balance) == 2)
                        BalanceAt(parent, balance);
                    else
                        parent.RecalculateHeightAndSum(); //если не будет балансировки(где пересчитывается высота с суммой родителя), то нужно отдельно запустить пересчет высоты с суммой

                    parent = parent.Parent;
                }
            }

            public void Sum(long l, long r)
            {
                long lMin, lMax, rMin, rMax;

                FindLessAndGreaterSums(l, out lMin, out lMax);
                FindLessAndGreaterSums(r, out rMin, out rMax);

                //Console.WriteLine("l = " + l + "(" + lMin + ":" + lMax + "); r = " + r + "(" + rMin + ":" + rMax + ")");
                
                _sum = Root != null ? Root.Sum - lMin - rMax : 0;
                Console.WriteLine(_sum);
            }

            private void FindLessAndGreaterSums(long value, out long sumLess, out long sumGreater)
            {
                bool found = false;
                sumLess = 0;
                sumGreater = 0;

                Node node = Root;

                while (node != null)
                {
                    if (value < node.Value)
                    {
                        if (node.Right != null)                        
                            sumGreater += node.Right.Sum;
                        sumGreater += node.Value;
                        node = node.Left;
                    }
                    else if (value > node.Value)
                    {
                        if (node.Left != null)
                            sumLess += node.Left.Sum;
                        sumLess += node.Value;
                        node = node.Right;
                    }
                    else
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    if (node.Left != null)
                        sumLess += node.Left.Sum;
                    if (node.Right != null)
                        sumGreater += node.Right.Sum;
                }
            }

            public Node Find(long value)
            {
                Node node = Root;

                while (node != null)
                {
                    if (node.Value == value)
                        return node;
                    else
                        node = value >= node.Value ? node.Right : node.Left;
                }

                return null;
            }

            private void RotateLeft(Node node)
            {
                if (node == null || node.Right == null) return;

                Node pivot = node.Right;                
                
                Node parent = node.Parent;
                bool isLeftChild = (parent != null) && parent.Left == node;
                bool makeTreeRoot = Root == node; //нужно обновить корень

                //Вращение
                node.Right = pivot.Left;
                pivot.Left = node;
                
                node.Parent = pivot;
                pivot.Parent = parent;

                if (node.Right != null)
                    node.Right.Parent = node;

                if (makeTreeRoot)
                    Root = pivot; //обновление корня дерева
                
                if (isLeftChild)
                    parent.Left = pivot;
                else if (parent != null)
                    parent.Right = pivot;

                node.RecalculateHeightAndSum();
                pivot.RecalculateHeightAndSum();
            }

            private void RotateRight(Node node)
            {
                if (node == null || node.Left == null) return;

                Node pivot = node.Left;

                Node parent = node.Parent;
                bool isLeftChild = (parent != null) && parent.Left == node;
                bool makeTreeRoot = Root == node; //нужно обновить корень

                //Вращение
                node.Left = pivot.Right;
                pivot.Right = node;

                node.Parent = pivot;
                pivot.Parent = parent;

                if (node.Left != null)
                    node.Left.Parent = node;

                if (makeTreeRoot)
                    Root = pivot; //обновление корня дерева

                if (isLeftChild)
                    parent.Left = pivot;
                else if (parent != null)
                    parent.Right = pivot;

                node.RecalculateHeightAndSum();
                pivot.RecalculateHeightAndSum();
            }

            public class Node
            {
                public long Value;
                public Node Parent;
                public Node Left;
                public Node Right;
                public long Sum; //сумма всех значений поддерева с корнем в этой ноде (минимально равна значению в ноде)
                public int Height; //высота поддерева с корнем в этой ноде (минимально равна 1)

                public Node(long value)
                {
                    Value = value;
                    Sum = value;
                    Height = 1;
                }

                public void RecalculateHeightAndSum()
                {                    
                    Height = 1 + Math.Max(Right != null ? Right.Height : 0, Left != null ? Left.Height : 0);
                    Sum = Value + (Right != null ? Right.Sum : 0) + (Left != null ? Left.Sum : 0);
                }

                public int GetBalance() => (Right != null ? Right.Height : 0) - (Left != null ? Left.Height : 0);
            }
        }
    }
}
