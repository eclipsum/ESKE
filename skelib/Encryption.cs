using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace skelib
{
    public class Encryption
    {
        private static string temp = Environment.GetEnvironmentVariable("temp");

        public static void Encrypt(string EncryptedFilePath, Key key)
        {
            string EFName = Path.GetFileNameWithoutExtension(EncryptedFilePath);
            string EFNameExt = Path.GetFileName(EncryptedFilePath);
            string EFNameDir = Path.GetDirectoryName(EncryptedFilePath);
            File.Copy(EncryptedFilePath, temp + @"\" + EFName + "def.sketemp", true);
            string PreviousFilePath = temp + @"\" + EFName + "def.sketemp";
            string CurrentFilePath = null;
            for (int i = 0; i < 1/*key.Loops + 4*/; i++)
            {
                CurrentFilePath = temp + @"\" + EFName + i + ".sketemp";
                FileStream Previous = new FileStream(PreviousFilePath, FileMode.Open);
                FileStream Current = new FileStream(CurrentFilePath, FileMode.Create);
                FileInfo PreviousFile = new FileInfo(PreviousFilePath);
                FileInfo CurrentFile = new FileInfo(CurrentFilePath);
                Current.SetLength(PreviousFile.Length);

                long Pos = key.Start;
                for (long o = 0; o < PreviousFile.Length; o++)
                {
                    Pos = Misc.Mod(Pos, PreviousFile.Length);
                    Previous.Seek(Pos, SeekOrigin.Begin);

                    int First = Previous.ReadByte();
                    int Second;
                    if ((key.Direction && Misc.Mod(Pos - key.Start, PreviousFile.Length) < Misc.Mod(Pos + key.Jump - key.Start, PreviousFile.Length)) ||
                       (!key.Direction && Misc.Mod(Pos + PreviousFile.Length - 1 - key.Start, PreviousFile.Length) > Misc.Mod(Pos + key.Jump - PreviousFile.Length - 1 - key.Start, PreviousFile.Length)))
                    {
                        Previous.Seek(Misc.Mod(Pos + key.Jump, PreviousFile.Length), SeekOrigin.Begin);
                        Second = Previous.ReadByte();
                        Console.WriteLine("Previous: " + Misc.Mod(key.Start, PreviousFile.Length) + " | " + Pos + " | " + Misc.Mod(Pos + key.Jump, PreviousFile.Length) + " | " + Second);
                    }
                    else
                    {
                        Current.Seek(Misc.Mod(Pos + key.Jump, PreviousFile.Length), SeekOrigin.Begin);
                        Second = Current.ReadByte();
                        Console.WriteLine("Current: " + Misc.Mod(key.Start, PreviousFile.Length) + " | " + Pos + " | " + Misc.Mod(Pos + key.Jump, PreviousFile.Length) + " | " + Second);
                    }

                    byte New;
                    if (new BitArray(new byte[] { key.Operations }).Get((int)(Pos % 8)))
                        New = (byte)Misc.Mod(First + (Second * key.Multiplier), 256);
                    else
                        New = (byte)Misc.Mod(First - (Second * key.Multiplier), 256);

                    Current.WriteByte(New);

                    if (key.Direction)
                        Pos++;
                    else
                        Pos--;
                }

                Previous.Close();
                Current.Close();
                File.Delete(PreviousFilePath);
                PreviousFilePath = CurrentFilePath;
            }

            File.Copy(PreviousFilePath, EFNameDir + "\\" + EFNameExt + ".ske", true);
        }

        public static void Decrypt(string EncryptedFilePath, Key key)
        {
            string EFName = Path.GetFileNameWithoutExtension(EncryptedFilePath);
            string EFNameExt = Path.GetFileName(EncryptedFilePath);
            string EFNameDir = Path.GetDirectoryName(EncryptedFilePath);
            File.Copy(EncryptedFilePath, temp + @"\" + EFName + "def.sketemp", true);
            string PreviousFilePath = temp + @"\" + EFName + "def.sketemp";
            string CurrentFilePath = null;
            for (int i = 0; i < 1/*key.Loops + 4*/; i++)
            {
                CurrentFilePath = temp + @"\" + EFName + i + ".sketemp";
                FileStream Previous = new FileStream(PreviousFilePath, FileMode.Open);
                FileStream Current = new FileStream(CurrentFilePath, FileMode.Create);
                FileInfo PreviousFile = new FileInfo(PreviousFilePath);
                FileInfo CurrentFile = new FileInfo(CurrentFilePath);
                Current.SetLength(PreviousFile.Length);

                long Pos = key.Start + (key.Direction ? -1 : 1);
                for (long o = 0; o < PreviousFile.Length; o--)
                {
                    Pos = Misc.Mod(Pos, PreviousFile.Length);
                    Previous.Seek(Pos, SeekOrigin.Begin);

                    int First = Previous.ReadByte();
                    int Second;
                    if ((key.Direction && Misc.Mod(Pos - key.Start, PreviousFile.Length) < Misc.Mod(Pos + key.Jump - key.Start, PreviousFile.Length)) ||
                        (!key.Direction && Misc.Mod(Pos + PreviousFile.Length - 1 - key.Start, PreviousFile.Length) > Misc.Mod(Pos + key.Jump - PreviousFile.Length - 1 - key.Start, PreviousFile.Length)))
                    {
                        Previous.Seek(Misc.Mod(Pos + key.Jump, PreviousFile.Length), SeekOrigin.Begin);
                        Second = Previous.ReadByte();
                    }
                    else
                    {
                        Current.Seek(Misc.Mod(Pos + key.Jump, PreviousFile.Length), SeekOrigin.Begin);
                        Second = Current.ReadByte();
                    }

                    byte New;
                    if (new BitArray(new byte[] { key.Operations }).Get((int)(Pos % 8)))
                        New = (byte)Misc.Mod(First - (Second * key.Multiplier), 256);
                    else
                        New = (byte)Misc.Mod(First + (Second * key.Multiplier), 256);

                    Current.Seek(Pos, SeekOrigin.Begin);
                    Current.WriteByte(New);

                    if (key.Direction)
                        Pos--;
                    else
                        Pos++;
                }

                Previous.Close();
                Current.Close();
                File.Delete(PreviousFilePath);
                PreviousFilePath = CurrentFilePath;
            }
            
            File.Copy(PreviousFilePath, EFNameDir + "\\decrypted_" + EFNameExt.Replace(".ske", ""), true);
        }
    }
}
