﻿/*
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

            List<int> list = new List<int>();

            Task additionTask = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    lock (list)
                    {
                        list.Add(i);
                        Thread.Sleep(100);
                    }
                }
            });

            Task printTask = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    lock (list)
                    {
                        foreach (int item in list)
                        {
                            Console.Write($"{item} ");
                        }

                        Console.WriteLine();

                        Thread.Sleep(200);
                    }
                }
            });

            Console.ReadLine();
        }
    }
}
