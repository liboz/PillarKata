﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata
{
    public class WordSearch
    {
        public static IReadOnlyList<(string word, IReadOnlyList<Coordinate> coordinates)> ParseAndWordSearch(IReadOnlyList<string> data)
        {
            var wordList = ParseFirstLine(data[0]);
            var matrix = ParseMatrix(data.Skip(1).ToArray());
            var result = new List<(string word, IReadOnlyList<Coordinate> coordinates)>();
            foreach (var word in wordList)
            {
                Result<IReadOnlyList<Coordinate>> searchResult;
                for (int i = 0; i < matrix.GetLength(0); i++) //Vertical Search
                {
                    searchResult = SearchVertically(matrix, word, i, false);
                    if (CheckIfFoundAndAddToResultIfItIs(word, searchResult, result)) break;
                    searchResult = SearchVertically(matrix, word, i, true);
                    if (CheckIfFoundAndAddToResultIfItIs(word, searchResult, result)) break;
                }
                for (int i = 0; i < matrix.GetLength(0); i++) //Horizontal Search
                {
                    searchResult = SearchHorizontally(matrix, word, i, false);
                    if (CheckIfFoundAndAddToResultIfItIs(word, searchResult, result)) break;
                    searchResult = SearchHorizontally(matrix, word, i, true);
                    if (CheckIfFoundAndAddToResultIfItIs(word, searchResult, result)) break;
                }
                for (int i = -matrix.GetLength(0) + 1; i < matrix.GetLength(0); i++) //Diagonal Search
                {
                    searchResult = SearchDiagonally(matrix, word, i, true, false);
                    if (CheckIfFoundAndAddToResultIfItIs(word, searchResult, result)) break;
                    searchResult = SearchDiagonally(matrix, word, i, true, true);
                    if (CheckIfFoundAndAddToResultIfItIs(word, searchResult, result)) break;
                    searchResult = SearchDiagonally(matrix, word, i, false, false);
                    if (CheckIfFoundAndAddToResultIfItIs(word, searchResult, result)) break;
                    searchResult = SearchDiagonally(matrix, word, i, false, true);
                    if (CheckIfFoundAndAddToResultIfItIs(word, searchResult, result)) break;
                }
            }
            return result;
        }

        private static bool CheckIfFoundAndAddToResultIfItIs(string word, 
            Result<IReadOnlyList<Coordinate>> searchResult, 
            List<(string word, IReadOnlyList<Coordinate> coordinates)> result)
        {
            if (searchResult.Found)
            {
                result.Add((word, searchResult.Location));
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string SearchResultToOutputStr(IReadOnlyList<(string word, IReadOnlyList<Coordinate> coordinates)> result)
        {
            var strResult = result.Select(i => $"{i.word}: {i.coordinates.ToOutputString()}");
            return string.Join("\r\n", strResult);
        }

        public static string[] ParseFirstLine(string wordString)
        {
            var result = wordString.Split(',');
            return result;
        }

        public static string[,] ParseMatrix(IReadOnlyList<string> data)
        {
            var firstLine = ParseMatrixLine(data[0]);
            var result = new string[firstLine.Length, firstLine.Length];
            InsertLineIntoMatrix(result, firstLine, 0);
            for (int i = 1; i < data.Count; i++)
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
            word = reverse ? word.Reverse() : word;
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

        private static Result<IReadOnlyList<int>> SearchFromPosition(string[,] data, string word, int startX, int startY, SearchType searchType, bool reverse)
        {
            var line = MakeLineFromMatrix(data, startX, startY, searchType);
            var lineStr = MakeStringFromLine(line);
            return SearchInLine(lineStr, word, reverse);
        }

        public static Result<IReadOnlyList<Coordinate>> SearchHorizontally(string[,] data, string word, int startY, bool reverse)
        {
            var result = SearchFromPosition(data, word, 0, startY, SearchType.Horizontal, reverse);
            return result.Map(i => (IReadOnlyList<Coordinate>)i.Select(x => new Coordinate(x, startY)).ToArray());
        }

        public static Result<IReadOnlyList<Coordinate>> SearchVertically(string[,] data, string word, int startX, bool reverse)
        {
            var result = SearchFromPosition(data, word, startX, 0, SearchType.Vertical, reverse);
            return result.Map(i => (IReadOnlyList<Coordinate>)i.Select(y => new Coordinate(startX, y)).ToArray());
        }

        public static Result<IReadOnlyList<Coordinate>> SearchDiagonally(string[,] data, string word, int diagonalIndex, bool countFromLeft, bool reverse)
        {
            var line = GetDiagonal(data, diagonalIndex, countFromLeft);
            var lineStr = MakeStringFromLine(line);
            var result =  SearchInLine(lineStr, word, reverse);
            return result.Map(i => (IReadOnlyList<Coordinate>)i.Select(j => DiagonalIndexToCoordinate(diagonalIndex, countFromLeft, j, data.GetLength(0) - 1)).ToArray());
        }

        public static Coordinate DiagonalIndexToCoordinate(int diagonalIndex, bool countFromLeft, int indexInDiagonal, int xSize)
        {
            var x = 0;
            var y = 0;
            var i = 0;
            if (countFromLeft)
            {
                if (diagonalIndex >= 0)
                {
                    x = diagonalIndex;                  
                }
                else
                {
                    y = diagonalIndex * -1;
                }
                while (i < indexInDiagonal)
                {
                    x += 1;
                    y += 1;
                    i += 1;
                }
                return new Coordinate(x, y);
            }
            else
            {
                x = xSize;
                if (diagonalIndex >= 0)
                {
                    x = x - diagonalIndex;
                }
                else
                {
                    y = diagonalIndex * -1;
                }

                while (i < indexInDiagonal)
                {
                    x -= 1;
                    y += 1;
                    i += 1;
                }
                return new Coordinate(x, y);
            }
        }

        public static IReadOnlyList<string> GetDiagonal(string[,] data, int diagonalIndex, bool countFromLeft)
        {
            var line = new List<string>();
            var x = 0 ;
            var y = 0;
            if (countFromLeft)
            {
                if (diagonalIndex >= 0)
                {
                    x = diagonalIndex;
                    
                }
                else
                {
                    y = diagonalIndex * -1;
                }

                while (x < data.GetLength(0) && y < data.GetLength(1))
                {
                    line.Add(data[x, y]);
                    x += 1;
                    y += 1;
                }
            }
            else
            {
                x = data.GetLength(0) - 1; // Counting from the right!
                if (diagonalIndex >= 0)
                {
                    x = x - diagonalIndex;
                }
                else
                {
                    y = diagonalIndex * -1;
                }

                while (0 <= x && y < data.GetLength(1))
                {
                    line.Add(data[x, y]);
                    x -= 1;
                    y += 1;
                }
            }
            return line;
        }

        public static IReadOnlyList<string> MakeLineFromMatrix(string[,] data, int startX, int startY, SearchType searchType)
        {
            var line = new List<string>();
            switch (searchType)
            {
                case SearchType.Vertical:
                    for (int i = 0; i < data.GetLength(1); i++)
                    {
                        line.Add(data[startX, i]);
                    }
                    break;
                case SearchType.Horizontal:
                    for (int i = 0; i < data.GetLength(0); i++)
                    {
                        line.Add(data[i, startY]);
                    }
                    break;
            }
            return line;
        }

        public static string MakeStringFromLine(IReadOnlyList<string> line)
        {
            var lineStr = string.Join("", line);
            return lineStr;
        }

    }

    public enum SearchType
    {
        Vertical,
        Horizontal,
    }
}
