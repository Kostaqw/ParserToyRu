using AngleSharp.Html.Parser;
using CsvHelper;
using CsvHelper.Configuration;
using Parser.Parser;
using Parser.Parser.Implementation.ToyRu;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    internal class Program
    {
        public static List<Toy> toys = new List<Toy>();
        public static int lastPage = 15;
        public static int currentPage = 1;

        static void Main(string[] args)
        {
            Worker<string[]> pages = new Worker<string[]>(new PageParser());
            pages.Settings = new PageSettings(1, lastPage);
            pages.OnNewData += Pages_OnNewData;
            pages.OnCompleted += Pages_OnCompleted;
            pages.Start();
            Task.WaitAll();
            Console.ReadKey();
            WriteInCsv();
        }

        private static void Pages_OnCompleted(object obj)
        {
            Console.WriteLine("work done pages");
        }

        private static async void Pages_OnNewData(object arg1, string[] arg2)
        {
            if (currentPage == lastPage)
            {
                
                var sources = arg2.Distinct();
                foreach (var item in sources)
                {
                    Worker<Toy> parser = new Worker<Toy>(new ToyParser());
                    parser.OnCompleted += Parser_OnCompleted;
                    parser.OnNewData += Parser_OnNewData;
                    parser.Settings = new ToySettings(item);
                    parser.Start();
                    await Task.Delay(500);
                    Console.WriteLine(item);
                }

            }
            currentPage++;
        }

        private static async void Parser_OnNewData(object arg1, Toy arg2)
        {
            toys.Add(arg2);
            Console.WriteLine(arg2.Title);
            await Task.Delay(500);
        }

        private static void Parser_OnCompleted(object obj)
        {
            Console.WriteLine("work done parser");
        }

        private static void WriteInCsv()
        {
            var path = "D:\\parseDate";
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var stream = File.Open(path, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    using (CsvWriter csvw = new CsvWriter(sw, config))
                    {
                        csvw.WriteRecords(toys);
                    }
                }
            }
        }
    }
}
