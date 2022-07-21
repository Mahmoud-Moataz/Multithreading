using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ThreadTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var threads = new Thread[20];

            for (var i = 0; i < 20; i++)
            {
                var t = new Thread(Test.FileWrite) {Name = $"t{i + 1}"};
                threads[i] = t;
                t.Start();
            }
            
            var input = Console.ReadLine();

            var myThread = threads.First(t => t.Name == input);
            myThread.Abort();

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