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
        private static Random r = new Random();
        private static string temp = Environment.GetEnvironmentVariable("temp");

        public static void Encrypt(string EncryptedFilePath, Key key)
        {
            string EFName = Path.GetFileNameWithoutExtension(EncryptedFilePath);
            string EFNameExt = Path.GetFileName(EncryptedFilePath);
            File.Copy(EncryptedFilePath, temp + @"\" + EFName + "def.sketemp");
            string PreviousFilePath = temp + @"\" + EFName + "def.sketemp";
            string CurrentFilePath = null;
            for (int i = 0; i < key.Loops; i++)
            {
                CurrentFilePath = temp + @"\" + EFName + i + ".sketemp";
                FileStream Previous = new FileStream(PreviousFilePath, FileMode.Open);
                FileStream Current = new FileStream(CurrentFilePath, FileMode.OpenOrCreate);
                FileInfo PreviousFile = new FileInfo(PreviousFilePath);
                FileInfo CurrentFile = new FileInfo(CurrentFilePath);

                long Pos = key.Start;
                for (long o = 0; o < PreviousFile.Length; o++)
                {
                    Pos = EMath.Mod(Pos, PreviousFile.Length);
                    Previous.Position = Pos;

                    byte First = (byte)Previous.ReadByte();
                    byte Second;
                    if (EMath.Mod(Pos + key.Jump, PreviousFile.Length) >= CurrentFile.Length)
                    {
                        Previous.Position = EMath.Mod(Pos + key.Jump, PreviousFile.Length);
                        Second = (byte)Previous.ReadByte();
                    }
                    else
                    {
                        Current.Position = EMath.Mod(Pos + key.Jump, CurrentFile.Length);
                        Second = (byte)Current.ReadByte();
                    }

                    byte New;
                    if (new BitArray(new byte[] { key.Operations }).Get((int)(o % 8)))
                        New = (byte)EMath.Mod(First + Second * key.Multiplier, 256);
                    else
                        New = (byte)EMath.Mod(First - Second * key.Multiplier, 256);

                    Current.WriteByte(New);

                    if (key.Direction)
                        Pos++;
                    else
                        Pos--;
                }

                PreviousFilePath = temp + @"\" + EFName + i + ".sketemp";
            }
        }

        /*public static void PEncrypt(string FilePath, Key key)
        {
            FileInfo FI = new FileInfo(FilePath);
            string[] Debug = new string[FI.Length];
            string LastFile = null;
            for (int l = 0; l < key.Loops; l++)
            {
                FileStream fs = null;
                if (LastFile == null)
                    fs = new FileStream(FilePath, FileMode.Open);
                else
                    fs = new FileStream(LastFile, FileMode.Open);
                string TempFile = Environment.GetEnvironmentVariable("temp") + FI.Name + r.Next(0, 10000) + ".sketemp";
                BinaryWriter bw = new BinaryWriter(new FileStream(TempFile, FileMode.Create), Encoding.UTF8);
                long o = key.Start;
                long s = 0;
                for (long i = 0; i < FI.Length; i++)
                {
                    o = EMath.Mod(o, FI.Length);
                    fs.Position = o;
                    long First = fs.ReadByte();
                    if (l == 0)
                        Debug[i] = First + " >\t";
                    if (key.Side)
                        fs.Position = EMath.Mod(o + key.Jump, FI.Length);
                    else
                        fs.Position = EMath.Mod(o - key.Jump, FI.Length);
                    long Second = fs.ReadByte();
                    if (key.Operation)
                        First += (i == FI.Length - 1 ? s : Second) * key.Multiplier;
                    else
                        First -= (i == FI.Length - 1 ? s : Second) * key.Multiplier;
                    First = EMath.Mod(First, 256);
                    if (i == 0)
                        s = First;
                    bw.Write((byte)First);
                    if (key.Direction)
                        o++;
                    else
                        o--;

                    Debug[i] += First + " >\t";
                }
                Console.WriteLine();
                fs.Close();
                bw.Close();
                if (LastFile != null)
                    File.Delete(LastFile);
                LastFile = TempFile;
            }
            foreach (string x in Debug)
                Console.WriteLine(x);
            Console.WriteLine();
            if (File.Exists(FilePath + ".ske"))
                File.Delete(FilePath + ".ske");
            File.Copy(LastFile, FilePath + ".ske");
        }*/
    }
}
