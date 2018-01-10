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

    public class Coordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static class ResultExtensions
    {
        public static Result<V> Map<U, V>(this Result<U> input, Func<U, V> action)
        {
            if (input.Found)
            {
                return new Result<V>(true, action(input.Location));
            }
            else
            {
                return new Result<V>(false);
            }
        }
    }
}
