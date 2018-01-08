using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata
{
    public class WordSearch
    {
        public static string[] Parse(string testString)
        {
            var result = testString.Split(',');
            return result;
        }
    }
}
