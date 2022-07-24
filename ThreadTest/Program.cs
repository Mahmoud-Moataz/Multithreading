using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ThreadTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var threads = new List<Thread>();

            for (var i = 0; i < 20; i++)
            {
                var t = new Thread(Test.FileWrite) {Name = $"t{i + 1}"};
                t.Start();
                threads.Add(t);
            }

            do
            {
                Console.WriteLine("Enter thread name \n");
                var input = Console.ReadLine();

                Console.WriteLine("Enter 's' to start a thread or 'p' to stop the thread \n");
                var letter = Console.ReadLine();
                Console.WriteLine("\n");


                try
                {
                    var myThread = threads.First(t => t.Name == input);
                    if (letter == "p")
                    {
                        myThread.Abort();
                        threads.Remove(myThread);
                    }
                    else if (letter == "s")
                    {
                        Console.WriteLine("Thread has already started");
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input");
                    }
                }
                catch (Exception e)
                {
                    if (letter == "s")
                    {
                        var t = new Thread(Test.FileWrite) {Name = input};
                        t.Start();
                        threads.Add(t);
                    }
                    else if (letter == "p")
                    {
                        Console.WriteLine("Thread has already terminated or aborted");
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input");
                    }
                }
            } while (true);

            foreach (var thread in threads) thread.Join();
        }
    }


    public static class Test
    {
        public static void FileWrite()
        {
            var folder = @"C:\Users\mahmo\Desktop\threadtest\";
            var fileName = Thread.CurrentThread.Name + ".txt";
            var fullpath = folder + fileName;
            string text;

            while (true)
            {
                text = $"{Thread.CurrentThread.Name}  {DateTime.Now}  \n";
                File.AppendAllText(fullpath, text);
                Thread.Sleep(3000);
            }
        }
    }
}