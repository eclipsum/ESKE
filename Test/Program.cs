using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skelib;
using System.Collections;

namespace Test
{
    static class Extensions
    {
        public static int Mod(this int x, int n)
        {
            return (x % n + n) % n;
        }

        public static string Combine(this char[] array)
        {
            string str = "";
            for (int i = 0; i < array.Length; i++)
            {
                str += array[i] + "";
            }
            return str;
        }

        public static T[] Offset<T>(this T[] array, int n)
        {
            T[] New = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                New[(i + n) % array.Length] = array[i];
            }

            return New;
        }

    }

    public class Program
    {
        static Random r = new Random();
        static void Main(string[] args)
        {
            CryptTest();
        }

        static void ModTest()
        {
            int a = 5438538;
            int b = 2746764;

            Console.ReadLine();
        }

        static void PrevCurrTest()
        {
            string[] e = new string[10];
            e[0] = "hej";
            e[5] = "hallå där";
            e[9] = "hejdå";

            for (int i = 0; i < 10; i++)
                Console.WriteLine(i + ": " + e[i]);

            Console.WriteLine();
            e = e.Offset(5);
            

            for (int i = 0; i < 10; i++)
                Console.WriteLine(i + ": " + e[i]);

            Console.ReadLine();

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

                

                Console.ReadLine();
            }
        }

        static void CryptTest()
        {
            string latestFile = @"C:\ESKE\tit.txt";
            string latestKey = "";

            while (true)
            {
                Console.Write("> ");
                string Command = Console.ReadLine();
                if (Command.Trim().Split(' ')[0] == "key")
                {
                    if (Command.Trim().Split(' ').Length == 1)
                    {
                        Key RandomKey = Key.Random();
                        Console.WriteLine(RandomKey.ToString());
                        latestKey = RandomKey.ToString();
                    }
                    else
                    {
                        Key RandomKey;
                        if (Command.Trim().Split(' ')[1] == "latest")
                            RandomKey = new Key(latestKey);
                        else
                            RandomKey = new Key(Command.Trim().Split(' ')[1]);
                        Console.WriteLine("Direction: " + RandomKey.Direction);
                        Console.WriteLine("Operations: " + RandomKey.Operations);
                        Console.WriteLine("Loops: " + RandomKey.Loops);
                        Console.WriteLine("Multiplier: " + RandomKey.Multiplier);
                        Console.WriteLine("Start: " + RandomKey.Start);
                        Console.WriteLine("Jump: " + RandomKey.Jump);
                    }
                }
                if (Command.Trim().Split(' ')[0] == "encrypt")
                {
                    Encryption.Encrypt((Command.Trim().Split(' ')[1] == "latest" ? latestFile : Command.Trim().Split(' ')[1]), (Command.Trim().Split(' ')[2] == "latest" ? new Key(latestKey) : new Key(Command.Trim().Split(' ')[2])));
                    if (Command.Trim().Split(' ')[1] != "latest") latestFile = Command.Trim().Split(' ')[1];
                    Console.WriteLine("File Encrypted.");
                }
                if (Command.Trim().Split(' ')[0] == "decrypt")
                {
                    Encryption.Decrypt((Command.Trim().Split(' ')[1] == "latest" ? (latestFile.EndsWith(".ske") ? latestFile : latestFile + ".ske") : (Command.Trim().Split(' ')[1]).EndsWith(".ske") ? Command.Trim().Split(' ')[1] : Command.Trim().Split(' ')[1] + ".ske"), (Command.Trim().Split(' ')[2] == "latest" ? new Key(latestKey) : new Key(Command.Trim().Split(' ')[2])));
                    if (Command.Trim().Split(' ')[1] != "latest") latestFile = Command.Trim().Split(' ')[1];
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
