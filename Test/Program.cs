using System;
using System.Collections.Generic;
using System.IO;
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
            while (true)
            {
                Console.WriteLine(Key.Random());
                Console.ReadKey();
                Console.Write("\b");
            }
            /*Key key = new Key(true, true, true, 0, 1, 2, 1);
            Encryption.Encrypt(@"C:\FAKEPNGTEST\etd.txt", key);
            Decryption.Decrypt(@"C:\FAKEPNGTEST\etd.txt.ske", key);
            Console.ReadLine();*/
        }
    }
}
