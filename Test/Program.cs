using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skelib;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Key key = new Key(true, true, true, 0, 1, 10, 1);
            Encryption.Encrypt(@"C:\FAKEPNGTEST\Eclippi.exe", key);
        }
    }
}
