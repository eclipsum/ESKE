using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skelib
{
    public class EMath
    {
        public static long Mod(long n, long x)
        {
            return ((n % x) + x) % x;
        }
    }
}
