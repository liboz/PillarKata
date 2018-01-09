using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarKata
{
    public class Result<T>
    {
        public bool Found { get; private set; }
        public T Location { get; private set; }

        public Result(bool found)
        {
            Found = found;
        }

        public Result(bool found, T location)
        {
            Found = found;
            Location = location;
        }
    }
}
