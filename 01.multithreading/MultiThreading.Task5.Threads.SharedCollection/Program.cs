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

            Semaphore semaphore = new Semaphore(1, 1);
            Semaphore semaphore2 = new Semaphore(0, 1); // in signal state, one entry is in use.

            List<int> list = new List<int>();

            Task additionTask = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    semaphore.WaitOne();

                    list.Add(i);

                    semaphore2.Release();
                }
            });


            Task printTask = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    semaphore2.WaitOne();

                    foreach (int item in list)
                    {
                        Console.Write($"{item} ");
                    }

                    Console.WriteLine();

                    semaphore.Release();
                }

            });

            additionTask.Start();
            printTask.Start();

            Console.ReadLine();
        }
    }
}
