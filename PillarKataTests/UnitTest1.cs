using PillarKata;
using Xunit;

namespace PillarKataTests
{
    public class UnitTest1
    {
        [Fact]
        public void ReadFirstLine()
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
    }
}
