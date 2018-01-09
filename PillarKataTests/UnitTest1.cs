using PillarKata;
using Xunit;
using System;
using System.Text.RegularExpressions;

namespace PillarKataTests
{
    public class UnitTest1
    {
        public static readonly string testData =
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

        public static readonly string[] testDataArr = Regex.Split(testData, "\r\n|\r|\n");


        [Fact]
        public void ReadFirstLineHappyPath()
        {
            var testString = "BONES,KHAN,KIRK,SCOTTY,SPOCK,SULU,UHURA";
            var result = WordSearch.ParseFirstLine(testString);
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
            var word = "BONES";
            var result = WordSearch.SearchFromPosition(data, word, 1, 0, SearchType.Vertical, false);
            Assert.False(result.Found);
            result = WordSearch.SearchFromPosition(data, word, 0, 0, SearchType.Vertical, false);
            Assert.True(result.Found);
            var location = new int[] { 6, 7, 8, 9, 10 };
            for (int i = 0; i < location.Length; i++)
            {
                Assert.Equal(location[i], result.Location[i]);
            }
        }

        [Fact]
        public void SearchVerticallyInLineBackwards()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var word = "KHAN";
            var result = WordSearch.SearchFromPosition(data, word, 0, 0, SearchType.Vertical, true);
            Assert.False(result.Found);
            result = WordSearch.SearchFromPosition(data, word, 5, 0, SearchType.Vertical, true);
            Assert.True(result.Found);
            var location = new int[] { 9, 8, 7, 6 };
            for (int i = 0; i < location.Length; i++)
            {
                Assert.Equal(location[i], result.Location[i]);
            }
        }


        [Fact]
        public void SearchHorizontallyInLine()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var word = "SCOTTY";
            var result = WordSearch.SearchFromPosition(data, word, 0, 0, SearchType.Horizontal, false);
            Assert.False(result.Found);
            result = WordSearch.SearchFromPosition(data, word, 0, 5, SearchType.Horizontal, false);
            Assert.True(result.Found);
            var location = new int[] { 0, 1, 2, 3, 4, 5 };
            for (int i = 0; i < location.Length; i++)
            {
                Assert.Equal(location[i], result.Location[i]);
            }
        }

        [Fact]
        public void SearchHorizontallyInLineBackwards()
        {
            var data = WordSearch.ParseMatrix(testDataArr);
            var word = "KIRK";
            var result = WordSearch.SearchFromPosition(data, word, 0, 0, SearchType.Horizontal, true);
            Assert.False(result.Found);
            result = WordSearch.SearchFromPosition(data, word, 0, 7, SearchType.Horizontal, true);
            Assert.True(result.Found);
            var location = new int[] { 4, 3, 2, 1 };
            for (int i = 0; i < location.Length; i++)
            {
                Assert.Equal(location[i], result.Location[i]);
            }
        }

        [Fact]
        public void MakeLineFromMatrix()
        {
            var startX = 0;
            var startY = 0;
            var data = WordSearch.ParseMatrix(testDataArr);
            var result = WordSearch.MakeLineFromMatrix(data, startX, startY, SearchType.Vertical);

        }
    }
}
