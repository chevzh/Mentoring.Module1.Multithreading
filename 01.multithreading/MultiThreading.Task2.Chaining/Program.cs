/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Threading.Tasks;
using System.Linq;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            Random random = new Random();

            Task<int[]> task = Task.
            Run(() =>
            {
                int[] arr = new int[10];

                for (int i = 0; i < 10; i++)
                {
                    arr[i] = random.Next(1, 100);
                }

                PrintResult("Array of random ints:", arr);

                return arr;
            }).
            ContinueWith(antecedent =>
            {
                int multiplier = random.Next(1, 100);
                int[] modifiedArray = antecedent.Result;

                for (int i = 0; i < 10; i++)
                {
                    modifiedArray[i] *= multiplier;
                }

                PrintResult($"Array of ints multiplied by {multiplier}:", modifiedArray);

                return modifiedArray;
            }).
            ContinueWith(antecedent =>
            {
                Array.Sort(antecedent.Result);

                PrintResult("Sorted array:", antecedent.Result);

                return antecedent.Result;
            }).
            ContinueWith(antecedent =>
            {
                Console.WriteLine("\n");
                Console.WriteLine("Average value of array items:");
                Console.WriteLine(antecedent.Result.Average());

                return antecedent.Result;
            });

            Console.ReadLine();
        }

        private static void PrintResult(string message, int[] arr)
        {
            Console.WriteLine("\n");
            Console.WriteLine(message);

            foreach (int item in arr)
            {
                Console.Write($"{item} ");
            }
        }
    }
}
