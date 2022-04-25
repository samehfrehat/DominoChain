using System;
using System.Collections.Generic;

namespace DominoChain
{
    //Challenge:
    //Given a random set of dominos, compute a way to order the set in such a way
    //that they form a correct circular domino chain. For every stone the dots on
    //one half of a stone match the dots on the neighboring half of an adjacent stone.
    //For example given the stones [2|1], [2|3] and[1 | 3] you should compute something
    //like [1|2] [2|3] [3|1] or[3 | 2][2 | 1][1 | 3] or[1 | 3][3 | 2][2 | 1] etc,
    //where the first and last numbers are the same meaning they’re in a circle.
    //For stones [1|2], [4|1] and[2 | 3] the resulting chain is not valid:
    //[4|1] [1|2] [2|3]'s first and last numbers are not the same so it’s not a circle.

    //Write a program in C# which computes the chain for a random set of dominos.
    //If a circular chain is not possible the program should output this.

    public class Program
    {
        static void Main(string[] args)
        {
            //Invalid Input {2,1} {2,3} {1,3} {3,4}
            // Valid Input {2,1} {2,1} {1,2} {1,2}
            // Valid Input
            //var dominos = new (int, int)[] { (2, 1), (2,3), (1,3)};
            var dominos = new (int, int)[] { (2, 1), (2, 3), (1, 3), (3,4) };
            var circularChain = GetCircularChain(dominos);
            if(circularChain.Length == 0)
            {
                Console.WriteLine("Not a Circular Chain");
            }
            else
            {
                for(int i = 0; i < circularChain.Length; i++)
                {
                    Console.Write($"[{circularChain[i].Item1}|{circularChain[i].Item2}] ");
                }
            }
        }

        private static (int, int)[] GetCircularChain((int,int)[] dominos)
        {
            if (!IsCircularChain(dominos)) return Array.Empty<(int,int)>();
            for (int i = 0; i < dominos.Length; i++)
            {
                for (int j = i+1; j < dominos.Length; j++)
                {
                    if(dominos[i].Item2 == dominos[j].Item1)
                    {
                        var tempDominos = dominos[i+1];
                        dominos[i + 1] = dominos[j];
                        dominos[j] = tempDominos;
                    }else if(dominos[i].Item2 == dominos[j].Item2)
                    {
                        var tempInt = dominos[j].Item1;
                        dominos[j].Item1 = dominos[j].Item2;
                        dominos[j].Item2 = tempInt;

                        var tempDominos = dominos[i + 1];
                        dominos[i + 1] = dominos[j];
                        dominos[j] = tempDominos;
                    }
                }
            }
            return dominos;
        }

        private static bool IsCircularChain((int, int)[] dominos)
        {
            var dictionary = new Dictionary<int, int>(dominos.Length * 2);
            for (int i = 0; i < dominos.Length; i++)
            {
                if (dictionary.ContainsKey(dominos[i].Item1))
                {
                    dictionary[dominos[i].Item1] += 1;
                }
                else
                {
                    dictionary.Add(dominos[i].Item1, 1);
                }

                if (dictionary.ContainsKey(dominos[i].Item2))
                {
                    dictionary[dominos[i].Item2] += 1;
                }
                else
                {
                    dictionary.Add(dominos[i].Item2, 1);
                }
            }

            foreach (var item in dictionary)
            {
                if (item.Value % 2 != 0) return false;
            }

            return true;
        }
    }
}