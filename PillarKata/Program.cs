using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] readText = File.ReadAllLines("input.txt");
            var searchResult = WordSearch.ParseAndWordSearch(readText);
            var output = WordSearch.SearchResultToOutputStr(searchResult);

            var outputPath = "output.txt";
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            File.WriteAllText(outputPath, output);
            Console.Write(output);
            Console.WriteLine();
        }
    }
}
