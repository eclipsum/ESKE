using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skelib;
using System.Collections;
using extlib.Types;

namespace Test
{
    public class Program
    {
        static Random r = new Random();
        static void Main(string[] args)
        {
            CryptTest();
        }

        static void CryptTest()
        {
            string latestFile = @"C:\ESKE\tit.exe";
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
                        Console.WriteLine("Start: " + RandomKey.Start);
                        Console.WriteLine("Jump: " + RandomKey.Jump);
                        Console.WriteLine("Operations: " + RandomKey.Operations);
                        Console.WriteLine("Multiplier: " + RandomKey.Multipliers);
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
                if (Command.Trim().Split(' ')[0] == "encrypt2")
                {
                    Encryption.Encrypt((Command.Trim().Split(' ')[1] == "latest" ? latestFile : Command.Trim().Split(' ')[1]), new Key(Command.Trim().Split(' ')[2]));
                    if (Command.Trim().Split(' ')[1] != "latest") latestFile = Command.Trim().Split(' ')[1];
                    Console.WriteLine("File Encrypted.");
                }
                if (Command.Trim().Split(' ')[0] == "decrypt2")
                {
                    Encryption.Decrypt((Command.Trim().Split(' ')[1] == "latest" ? (latestFile.EndsWith(".ske") ? latestFile : latestFile + ".ske") : (Command.Trim().Split(' ')[1]).EndsWith(".ske") ? Command.Trim().Split(' ')[1] : Command.Trim().Split(' ')[1] + ".ske"), new Key(Command.Trim().Split(' ')[2]));
                    if (Command.Trim().Split(' ')[1] != "latest") latestFile = Command.Trim().Split(' ')[1];
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
                    Console.WriteLine("Start: " + RandomKey.Start);
                    Console.WriteLine("Jump: " + RandomKey.Jump);
                    Console.WriteLine("Operations: " + RandomKey.Operations);
                    Console.WriteLine("Multiplier: " + RandomKey.Multipliers);
                }
            }
        }
    }
}
