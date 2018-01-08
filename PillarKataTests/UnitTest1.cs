using PillarKata;
using Xunit;

namespace PillarKataTests
{
    public class UnitTest1
    {
        [Fact]
        public void readFirstLine()
        {
            var testString = "BONES,KHAN,KIRK,SCOTTY,SPOCK,SULU,UHURA";
            var result = WordSearch.Parse(testString);
            Assert.Equal(7, result);
        }
    }
}
