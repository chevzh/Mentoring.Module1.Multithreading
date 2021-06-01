/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static Semaphore semaphore = new Semaphore(9, 9);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            Console.WriteLine("Part a:");
            CreateThreads(10);

            Console.WriteLine("Part b:");
            
            ThreadPoolCreateThreads(10);
            semaphore.Release();

            Console.ReadLine();
        }

        private static void ThreadPoolCreateThreads(object state)
        {
            semaphore.WaitOne();

            int remainingThreads = (int)state;

            remainingThreads -= 1;

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} was created. {remainingThreads} threads needs to be created.");

            ThreadPool.QueueUserWorkItem(ThreadPoolCreateThreads, remainingThreads);            

            Thread.Sleep(200);
        }

        private static void CreateThreads(object state)
        {
            int remainingThreads = (int)state;

            if (remainingThreads > 0)
            {
                Thread.Sleep(200);

                remainingThreads -= 1;

                Thread thread = new Thread(new ParameterizedThreadStart(CreateThreads));
                thread.Start(remainingThreads);

                Console.WriteLine($"Thread {thread.ManagedThreadId} was created. {remainingThreads} threads needs to be created.");

                thread.Join();
            }
        }
    }
}
