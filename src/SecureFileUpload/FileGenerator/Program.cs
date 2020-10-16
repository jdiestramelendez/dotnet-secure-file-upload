using System;
using System.IO;

namespace FileGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string header = @"ID,Name,Description,Type";
            string filename = "file.csv";
            long numberOfLines = 100;

            Console.WriteLine($"Enter filename ({filename}):");
            var r = Console.ReadLine();
            if (!string.IsNullOrEmpty(r))
                filename = r;
            Console.WriteLine($"Enter number of lines (default {numberOfLines}):");
            r = Console.ReadLine();
            if (!string.IsNullOrEmpty(r))
                numberOfLines = int.Parse(r);

            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine(header);
                for (int i = 1; i <= numberOfLines; i++)
                {
                    string type = "None";
                    if (i % 2 == 0) type = "New";
                    if (i % 3 == 0) type = "Update";
                    sw.WriteLine($"{i},Name of item {i},Description of item {i}, {type}");
                }
            }
        }
    }
}
