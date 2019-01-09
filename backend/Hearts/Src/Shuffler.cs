using System;
using System.Collections.Generic;

namespace Hearts
{
    public class Shuffler
    {
        private Random random;

        public Shuffler(Random random)
        {
            this.random = random;
        }

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}