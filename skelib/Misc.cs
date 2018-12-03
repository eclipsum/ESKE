using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skelib
{
    public class Misc
    {
        private static Random r = new Random();

        public static long Mod(long n, long x)
        {
            return ((n % x) + x) % x;
        }

        public static long LongRandom()
        {
            byte[] buf = new byte[8];
            r.NextBytes(buf);
            return BitConverter.ToInt64(buf, 0);
        }

        public static string Fill(string Hex, int Length)
        {
            while (Hex.Length < Length)
                Hex = "0" + Hex;
            return Hex;
        }

        public static string CharArrayToString(char[] array, int index, int length)
        {
            string s = "";
            for (int i = index; i < index + length; i++)
            {
                s += array[i] + "";
            }
            return s;
        }
    }
}
