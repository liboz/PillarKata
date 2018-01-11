using PillarKata;
using Xunit;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace PillarKataTests
{
    public class UnitTest1
    {
        public static readonly string testMatrix =
@"U,M,K,H,U,L,K,I,N,V,J,O,C,W,E
L,L,S,H,K,Z,Z,W,Z,C,G,J,U,Y,G
H,S,U,P,J,P,R,J,D,H,S,B,X,T,G
B,R,J,S,O,E,Q,E,T,I,K,K,G,L,E
A,Y,O,A,G,C,I,R,D,Q,H,R,T,C,D
S,C,O,T,T,Y,K,Z,R,E,P,P,X,P,F
B,L,Q,S,L,N,E,E,E,V,U,L,F,M,Z
O,K,R,I,K,A,M,M,R,M,F,B,A,P,P
N,U,I,I,Y,H,Q,M,E,M,Q,R,Y,F,S
E,Y,Z,Y,G,K,Q,J,P,C,Q,W,Y,A,K
S,J,F,Z,M,Q,I,B,D,B,E,M,K,W,D
T,G,L,B,H,C,B,E,C,H,T,O,Y,I,K
O,J,Y,E,U,L,N,C,C,L,Y,B,Z,U,H
W,Z,M,I,S,U,K,U,R,B,I,D,U,X,S
K,Y,L,B,Q,Q,P,M,D,F,C,K,E,A,B";

        public static readonly IReadOnlyList<string> testDataArr = Regex.Split(testMatrix, "\r\n|\r|\n");
        public static readonly string testWordList = "BONES,KHAN,KIRK,SCOTTY,SPOCK,SULU,UHURA";

        [Fact]
        public void ReadFirstLineHappyPath()
        {
            var result = WordSearch.ParseFirstLine(testWordList);
            Assert.Equal(7, result.Length);
            Assert.Equal("BONES", result[0]);
            Assert.Equal("KHAN", result[1]);
            Assert.Equal("KIRK", result[2]);
            Assert.Equal("SCOTTY", result[3]);
            Assert.Equal("SPOCK", result[4]);
            Assert.Equal("SULU", result[5]);
            Assert.Equal("UHURA", result[6]);
        }

        [Fact]
        public void ReadFirstLineGarbageData()
        {
            var result = WordSearch.ParseFirstLine("");
            Assert.Single(result);

            result = WordSearch.ParseFirstLine("BONES;KHAN;KIRK;SCOTTY;SPOCK;SULU;UHURA");
            Assert.Single(result);

            Assert.Throws<NullReferenceException>(() => WordSearch.ParseFirstLine(null));
        }

        [Fact]
        public void ReadMatrix()
        {
            var result = WordSearch.ParseMatrix(testDataArr);
            Assert.Equal(15, result.GetLength(0));
            Assert.Equal(15, result.GetLength(1));
            Assert.Equal("U", result[0, 0]);
            Assert.Equal("E", result[14, 0]);
            Assert.Equal("K", result[0, 14]);
            Assert.Equal("B", result[14, 14]);

            var row10 = new string[] { "E", "Y", "Z", "Y", "G", "K", "Q", "J", "P", "C", "Q", "W", "Y", "A", "K" };
            for (int i = 0; i < row10.Length; i++)
            {
                Assert.Equal(row10[i], result[i, 9]);
            }

        }

        [Fact]
        public void InsertLineIntoMatrixHappyPath()
        {
            var matrix = new string[2, 2];
            var data = new string[] { "a", "b" };
            WordSearch.InsertLineIntoMatrix(matrix, data, 0);
            Assert.Equal("a", matrix[0, 0]);
            Assert.Equal("b", matrix[1, 0]);
            Assert.Null(matrix[0, 1]);
            Assert.Null(matrix[1, 1]);
        }

        [Fact]
        public void InsertLineIntoMatrixGarbageData()
        {
            var matrix = new string[2, 2];
            var data = new string[] { "a"};
            Assert.Throws<ArgumentException>(() => WordSearch.InsertLineIntoMatrix(matrix, data, 0));

            data = new string[] { "a", "b" };
            Assert.Throws<IndexOutOfRangeException>(() => WordSearch.InsertLineIntoMatrix(matrix, data, 2));

            Assert.Throws<NullReferenceException>(() => WordSearch.InsertLineIntoMatrix(null, data, 0));
            Assert.Throws<NullReferenceException>(() => WordSearch.InsertLineIntoMatrix(matrix, null, 0));
        }

        [Fact]
        public void SearchVerticallyInLine()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = wordStr[0];
            var result = WordSearch.SearchVertically(data, word, 1, false);
            Assert.False(result.Found);
            result = WordSearch.SearchVertically(data, word, 0, false);
            var locationX = Enumerable.Repeat(0, 5).ToArray();
            var locationY = new int[] { 6, 7, 8, 9, 10 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchVerticallyInLineBackwards()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = wordStr[1];
            var result = WordSearch.SearchVertically(data, word, 0, true);
            Assert.False(result.Found);
            result = WordSearch.SearchVertically(data, word, 5, true);
            var locationX = Enumerable.Repeat(5, 4).ToArray();
            var locationY = new int[] { 9, 8, 7, 6 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchHorizontallyInLineBackwards()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = wordStr[2];
            var result = WordSearch.SearchHorizontally(data, word, 0, true);
            Assert.False(result.Found);
            result = WordSearch.SearchHorizontally(data, word, 7, true);
            var locationX = new int[] { 4, 3, 2, 1 };
            var locationY = Enumerable.Repeat(7, 4).ToArray();
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchHorizontallyInLine()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = wordStr[3];
            var result = WordSearch.SearchHorizontally(data, word, 0, false);
            Assert.False(result.Found);
            result = WordSearch.SearchHorizontally(data, word, 5, false);
            var locationX = new int[] { 0, 1, 2, 3, 4, 5 };
            var locationY = Enumerable.Repeat(5, 6).ToArray();
            AssertCoordinate(result, locationX, locationY);
        }


        [Fact]
        public void SearchDiagonally()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = wordStr[4];
            var result = WordSearch.SearchDiagonally(data, word, 0, true, false);
            Assert.False(result.Found);
            result = WordSearch.SearchDiagonally(data, word, 1, true, false);
            var locationX = new int[] { 2, 3, 4, 5, 6 };
            var locationY = new int[] { 1, 2, 3, 4, 5 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchDiagonallyBackwards()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = wordStr[5];
            var result = WordSearch.SearchDiagonally(data, word, 1, true, true);
            Assert.False(result.Found);
            result = WordSearch.SearchDiagonally(data, word, 0, true, true);
            var locationX = new int[] { 3, 2, 1, 0 };
            var locationY = new int[] { 3, 2, 1, 0 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchDiagonallyNegativeIndex()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var word = "JATN";
            var result = WordSearch.SearchDiagonally(data, word, 0, true, false);
            Assert.False(result.Found);
            result = WordSearch.SearchDiagonally(data, word, -1, true, false);
            var locationX = new int[] { 2, 3, 4, 5 };
            var locationY = new int[] { 3, 4, 5, 6 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchDiagonallyNegativeIndexBackwards()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = "QIY";
            var result = WordSearch.SearchDiagonally(data, word, 0, true, true);
            Assert.False(result.Found);
            result = WordSearch.SearchDiagonally(data, word, -10, true, true);
            var locationX = new int[] { 4, 3, 2 };
            var locationY = new int[] { 14, 13, 12 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchDiagonallyFromRight()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = wordStr[6];
            var result = WordSearch.SearchDiagonally(data, word, 0, false, false);
            Assert.False(result.Found);
            result = WordSearch.SearchDiagonally(data, word, 10, false, false);
            var locationX = new int[] { 4, 3, 2, 1, 0 };
            var locationY = new int[] { 0, 1, 2, 3, 4 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchDiagonallyFromRightBackwards()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var word = "RQKB";
            var result = WordSearch.SearchDiagonally(data, word, 0, false, true);
            Assert.False(result.Found);
            result = WordSearch.SearchDiagonally(data, word, 1, false, true);
            var locationX = new int[] { 8, 9, 10, 11 };
            var locationY = new int[] { 5, 4, 3, 2 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchDiagonallyFromRightNegativeIndex()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var word = "BQ";
            var result = WordSearch.SearchDiagonally(data, word, 0, false, false);
            Assert.False(result.Found);
            result = WordSearch.SearchDiagonally(data, word, -4, false, false);
            var locationX = new int[] { 11, 10 };
            var locationY = new int[] { 7, 8 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void SearchDiagonallyFromRightNegativeIndexBackwards()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var wordStr = WordSearch.ParseFirstLine(testWordList);
            var word = "MRLTM";
            var result = WordSearch.SearchDiagonally(data, word, 0, false, true);
            Assert.False(result.Found);
            result = WordSearch.SearchDiagonally(data, word, -7, false, true);
            var locationX = new int[] { 7, 8, 9, 10, 11 };
            var locationY = new int[] { 14, 13, 12, 11, 10 };
            AssertCoordinate(result, locationX, locationY);
        }

        [Fact]
        public void ParseAndSearch()
        {
            var data = new List<string>{testWordList};
            data.AddRange(testDataArr);
            var result = WordSearch.ParseAndWordSearch(data);

            var firstLine = WordSearch.ParseFirstLine(testWordList);
            Assert.Equal(firstLine.Length, result.Count);

            for (int i = 0; i < firstLine.Length; i++)
            {
                var wordInfo = result[i];
                Assert.Equal(firstLine[i], wordInfo.word);
            }

            var output = WordSearch.SearchResultToOutputStr(result);
            var expected =
@"BONES: (0,6),(0,7),(0,8),(0,9),(0,10)
KHAN: (5,9),(5,8),(5,7),(5,6)
KIRK: (4,7),(3,7),(2,7),(1,7)
SCOTTY: (0,5),(1,5),(2,5),(3,5),(4,5),(5,5)
SPOCK: (2,1),(3,2),(4,3),(5,4),(6,5)
SULU: (3,3),(2,2),(1,1),(0,0)
UHURA: (4,0),(3,1),(2,2),(1,3),(0,4)";
            Assert.Equal(expected, output);
            
        }

        private void AssertCoordinate(Result<IReadOnlyList<Coordinate>> result, IReadOnlyList<int> locationX, IReadOnlyList<int> locationY)
        {
            Assert.True(result.Found);
            Assert.Equal(locationX.Count, locationY.Count);
            Assert.Equal(locationX.Count, result.Location.Count);
            for (int i = 0; i < locationX.Count; i++)
            {
                Assert.Equal(locationX[i], result.Location[i].X);
                Assert.Equal(locationY[i], result.Location[i].Y);
            }
        }
    }
}
