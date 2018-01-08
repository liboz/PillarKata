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

        [Fact]
        public void ReadFirstLineHappyPath()
        {
            var testString = "BONES,KHAN,KIRK,SCOTTY,SPOCK,SULU,UHURA";
            var result = WordSearch.ParseFirstLine(testString);
            Assert.Equal(7, result.Length);
            Assert.Contains("BONES", result);
            Assert.Contains("KHAN", result);
            Assert.Contains("KIRK", result);
            Assert.Contains("SCOTTY", result);
            Assert.Contains("SPOCK", result);
            Assert.Contains("SULU", result);
            Assert.Contains("UHURA", result);
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
            var arr = Regex.Split(testData, "\r\n|\r|\n");
            var result = WordSearch.ParseMatrix(arr);
            Assert.Equal(15, result.GetLength(0));
            Assert.Equal(15, result.GetLength(1));

        }

        [Fact]
        public void InsertLineIntoMatrixHappyPath()
        {
            var matrix = new string[2, 2];
            var data = new string[] { "a", "b" };
            WordSearch.InsertLineIntoMatrix(matrix, data, 0);
            Assert.Equal("a", matrix[0, 0]);
            Assert.Equal("b", matrix[0, 1]);
            Assert.Null(matrix[1, 0]);
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
    }
}
