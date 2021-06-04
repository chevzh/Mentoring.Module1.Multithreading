/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            AutoResetEvent additionWaitHandle = new AutoResetEvent(true);
            AutoResetEvent printWaitHandle = new AutoResetEvent(false);

            List<int> list = new List<int>();

            Task additionTask = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    additionWaitHandle.WaitOne();

                    lock(list)
                    {
                        list.Add(i);
                    }

                    printWaitHandle.Set();
                }                
            });

            Task printTask = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    printWaitHandle.WaitOne();

                    lock (list)
                    {
                        foreach (int item in list)
                        {
                            Console.Write($"{item} ");
                        }

                        Console.WriteLine();
                    }

                    additionWaitHandle.Set();
                }
                
            });

            Console.ReadLine();
        }
    }
}
