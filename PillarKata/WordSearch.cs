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
            InsertLineIntoMatrix(result, firstLine, 0);
            for (int i = 1; i < data.Length; i++)
            {
                var line = ParseMatrixLine(data[i]);

                InsertLineIntoMatrix(result, line, i);
            }
            return result;
        }

        public static string[] ParseMatrixLine(string data)
        {
            var result = data.Split(',');
            return result;
        }

        public static void InsertLineIntoMatrix(string[,] matrix, string[] data, int v)
        {
            var d1 = matrix.GetLength(0);
            var d2 = matrix.GetLength(1);

            if (data.Length != d1)
            {
                throw new ArgumentException("Length of data longer than row length of matrix.");
            }
            else if (v >= d1)
            {
                throw new IndexOutOfRangeException("Row to insert is not within the matrix.");
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    matrix[v, i] = data[i];
                }
            }
        }
    }
}
