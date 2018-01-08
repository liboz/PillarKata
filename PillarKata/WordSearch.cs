using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata
{
    public class WordSearch
    {
        public static string[] ParseFirstLine(string wordString)
        {
            var result = wordString.Split(',');
            return result;
        }

        public static string[,] ParseMatrix(string[] data)
        {
            var firstLine = ParseMatrixLine(data[0]);
            var result = new string[firstLine.Length, firstLine.Length];
            return null;
        }

        public static string[] ParseMatrixLine(string data)
        {
            var result = data.Split(',');
            return result;
        }

        public static void InsertLineIntoMatrix(string[,] matrix, string[] data, int v)
        {
            for (int i = 0; i < data.Length; i++)
            {
                matrix[v, i] = data[i];
            }
        }
    }
}
