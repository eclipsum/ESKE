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
            CryptTest();
        }

        static void CryptTest()
        {
            while (true)
            {
                Console.Write("> ");
                string Command = Console.ReadLine();
                if (Command.Trim().Split(' ')[0] == "key")
                {
                    Console.WriteLine(Key.Random().ToString());
                }
                if (Command.Trim().Split(' ')[0] == "encrypt")
                {
                    Encryption.Encrypt(Command.Trim().Split(' ')[1], new Key(Command.Trim().Split(' ')[2]));
                    Console.WriteLine("File Encrypted.");
                }
                if (Command.Trim().Split(' ')[0] == "decrypt")
                {
                    Encryption.Decrypt(Command.Trim().Split(' ')[1], new Key(Command.Trim().Split(' ')[2]));
                    Console.WriteLine("File Decrypted.");
                }
            }
        }

        static void FetchTest()
        {
            Random r = new Random();
            while (true)
            {
                Console.Clear();
                bool Direction = (r.Next(0, 2) == 0 ? false : true);
                string[] buffer = new string[16];
                for (int i = 0; i < 16; i++)
                    buffer[i] = "";

                for (int i = 0; i < 3; i++)
                {
                    int rr = r.Next(0, 16);
                    if (buffer[rr] == "")
                        buffer[rr] = (i == 0 ? "Start" : "") + (i == 1 ? "Pos" : "") + (i == 2 ? "Jump" : "");
                    else
                        i--;
                }

                int l = 1;
                foreach (string str in buffer)
                {
                    Console.WriteLine(l + ": " + str);
                    l++;
                }

                int Start = buffer.ToList().IndexOf("Start");
                int Pos = buffer.ToList().IndexOf("Pos");
                int Jump = buffer.ToList().IndexOf("Jump");

                Console.WriteLine("\nDirection: " + Direction);
                Console.WriteLine("Start: " + Start);
                Console.WriteLine("Pos: " + Pos);
                Console.WriteLine("Jump: " + Jump);

                if (Direction)
                {

                }
                else
                {

                }

                Console.ReadLine();
            }
        }

        static void FSTest()
        {
            FileStream a = new FileStream(@"C:\ESKE\test.txt", FileMode.Create);
            a.SetLength(10);
            /*a.Seek(5, SeekOrigin.Begin);
            a.WriteByte(65);
            a.Seek(2, SeekOrigin.Begin);
            a.WriteByte(65);
            a.Seek(0, SeekOrigin.Begin);
            a.WriteByte(65);*/
        }

        static void EncryptionTesting()
        {
            while (true)
            {
                Console.Write("> ");
                string Command = Console.ReadLine();
                if (Command.Trim().Split(' ')[0] == "key")
                {
                    Console.WriteLine(Key.Random().ToString());
                }
                if (Command.Trim().Split(' ')[0] == "encrypt")
                {
                    Encryption.Encrypt(Command.Trim().Split(' ')[1], new Key(Command.Trim().Split(' ')[2]));
                    Console.WriteLine("File Encrypted.");
                }
                if (Command.Trim().Split(' ')[0] == "decrypt")
                {
                    Encryption.Decrypt(Command.Trim().Split(' ')[1], new Key(Command.Trim().Split(' ')[2]));
                    Console.WriteLine("File Decrypted.");

                }
            }
        }

        static void KeyGeneration()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Press any key to generate new key...");
                if (Console.ReadKey().Key == ConsoleKey.Backspace)
                {
                    Console.Clear();
                }
                else
                {
                    for (int i = 0; i < 37; i++)
                        Console.Write("\b");
                    string RandomKeyString = Key.Random().ToString();
                    Key RandomKey = new Key(RandomKeyString);
                    Console.WriteLine(RandomKeyString);
                    Console.WriteLine("Direction: " + RandomKey.Direction);
                    Console.WriteLine("Operations: " + RandomKey.Operations);
                    Console.WriteLine("Loops: " + RandomKey.Loops);
                    Console.WriteLine("Multiplier: " + RandomKey.Multiplier);
                    Console.WriteLine("Start: " + RandomKey.Start);
                    Console.WriteLine("Jump: " + RandomKey.Jump);
                }
            }
        }
    }
}
