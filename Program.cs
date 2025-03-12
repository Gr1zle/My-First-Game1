using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TextFileAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = "C:\\Users\\dmitr\\Desktop\\test_file";

            string[] files = Directory.GetFiles(directoryPath, "*.txt");

            Console.WriteLine("Анализ текстовых файлов:");

            // Синхронное чтение
            long syncTime = Time(() =>
            {
                long totalChars = 0;
                foreach (string file in files)
                {
                    totalChars += CountCharactersSync(file);
                }
                Console.WriteLine($"Синхронно:  Общее количество символов: {totalChars}");
            });
            Console.WriteLine($"Синхронное время выполнения: {syncTime} мс");

            // Асинхронное чтение
            long asyncTime = Time(async () =>
            {
                long totalChars = 0;
                foreach (string file in files)
                {
                    totalChars += await CountCharactersAsync(file);
                }
                Console.WriteLine($"Асинхронно: Общее количество символов: {totalChars}");
            });
            Console.WriteLine($"Асинхронное время выполнения: {asyncTime} мс");

            // Многопоточное чтение
            long multiThreadedTime = Time(() =>
            {
                long totalChars = 0;
                object lockObject = new object();
                List<Thread> threads = new List<Thread>();

                foreach (string file in files)
                {
                    Thread thread = new Thread(() =>
                    {
                        long charCount = CountCharactersSync(file); 
                                                                    
                        lock (lockObject)
                        {
                            totalChars += charCount;
                        }
                    });
                    threads.Add(thread);
                    thread.Start();
                }

                foreach (Thread thread in threads)
                {
                    thread.Join();
                }

                Console.WriteLine($"Многопоточно: Общее количество символов: {totalChars}");
            });
            Console.WriteLine($"Многопоточное время выполнения: {multiThreadedTime} мс");
        }


        // Синхронный подсчет символов в файле
        static long CountCharactersSync(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return text.Length;
        }

        // Асинхронный подсчет символов в файле
        static async Task<long> CountCharactersAsync(string filePath)
        {
            string text = await File.ReadAllTextAsync(filePath);
            return text.Length;
        }


        static long Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        static long Time(Func<Task> asyncAction)
        {
            var stopwatch = Stopwatch.StartNew();
            asyncAction().Wait();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}