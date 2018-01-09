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
            else if (v >= d2)
            {
                throw new IndexOutOfRangeException("Row to insert is not within the matrix.");
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    matrix[i, v] = data[i];
                }
            }
        }

        public static Result<IReadOnlyList<int>> SearchInLine (string line, string word, bool reverse)
        {
            var startIndex = reverse ? line.LastIndexOf(word) :  line.IndexOf(word);
            if (startIndex == -1)
            {
                return new Result<IReadOnlyList<int>>(false);
            }
            else
            {
                var result = reverse ? Enumerable.Range(startIndex, word.Length).Reverse() : Enumerable.Range(startIndex, word.Length);
                return new Result<IReadOnlyList<int>>(true, result.ToArray());
            }
        }

        public static Result<IReadOnlyList<int>> SearchVerticallyInLine(string[,] data, string word, int x)
        {
            var lineStr = MakeStringFromLine(data, x);
            return SearchInLine(lineStr, word, false);
        }

        public static Result<IReadOnlyList<int>> SearchVerticallyInLineBackwards(string[,] data, string word, int x)
        {
            var lineStr = MakeStringFromLine(data, x);
            return SearchInLine(lineStr, word.Reverse(), true);
        }

        public static string MakeStringFromLine(string[,] data, int x)
        {
            var line = new List<string>();
            for (int i = 0; i < data.GetLength(1); i++)
            {
                line.Add(data[x, i]);
            }

            var lineStr = string.Join("", line);
            return lineStr;
        }
    }
}
