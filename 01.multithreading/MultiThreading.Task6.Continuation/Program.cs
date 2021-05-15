/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Task task = Task.Run(() =>
            {
                Console.WriteLine($"Parent task. Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                while (true)
                {
                    token.ThrowIfCancellationRequested();
                }

            }, token);

            Console.ReadKey();
            tokenSource.Cancel();

            task.ContinueWith((parent) =>
            {
                Console.WriteLine($"Continuation task that executes regardless of the result of the parent task. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("\n");

            }, TaskContinuationOptions.None);

            task.ContinueWith((parent) =>
            {
                Console.WriteLine($"Continuation task that executes when the parent task finished without success. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("\n");

            }, TaskContinuationOptions.OnlyOnFaulted);

            task.ContinueWith((parent) =>
            {
                Console.WriteLine($"Continuation task that executes when the parent task finished with fail and parent task thread should be reused for continuation. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("\n");

            }, default, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);

            task.ContinueWith((parent) =>
            {
                Console.WriteLine($"Continuation task that executes outside of the thread pool when the parent task would be cancelled. Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine();

                Console.WriteLine($"This task running outside the thread pool - IsThreadPoolThread property is {Thread.CurrentThread.IsThreadPoolThread}");
                Console.WriteLine("\n");

            }, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);

            Console.ReadLine();
        }
    }
}
