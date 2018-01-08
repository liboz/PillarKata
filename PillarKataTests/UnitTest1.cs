using PillarKata;
using Xunit;
using System;

namespace PillarKataTests
{
    public class UnitTest1
    {
        [Fact]
        public void ReadFirstLineHappyPath()
        {
            var testString = "BONES,KHAN,KIRK,SCOTTY,SPOCK,SULU,UHURA";
            var result = WordSearch.Parse(testString);
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
            var result = WordSearch.Parse("");
            Assert.Single(result);

            result = WordSearch.Parse("BONES;KHAN;KIRK;SCOTTY;SPOCK;SULU;UHURA");
            Assert.Single(result);

            Assert.Throws<NullReferenceException>(() => WordSearch.Parse(null));
        }
    }
}
