using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Numerics;
using extlib.Extensions;
using extlib.Types;

namespace skelib
{
    public class Encryption
    {
        private static string temp = Environment.GetEnvironmentVariable("temp");

        public static async Task EncryptAsync(string EncryptedFilePath, Key key)
        {
            await Task.Delay(0);
            Encrypt(EncryptedFilePath, key);
        }

        public static async Task DecryptAsync(string EncryptedFilePath, Key key)
        {
            await Task.Delay(0);
            Decrypt(EncryptedFilePath, key);
        }

        private static byte[] GetBuffer(FileStream fs, long pos, int length)
        {
            if (length + pos >= fs.Length)
            {
                byte[] buffer1 = new byte[fs.Length - pos];
                byte[] buffer2 = new byte[length + pos - fs.Length];
                fs.Seek(pos, SeekOrigin.Begin);
                fs.Read(buffer1, 0, (int)(fs.Length - pos));
                fs.Read(buffer2, 0, (int)(length + pos - fs.Length));
                return buffer1.CombineWith(buffer2);
            }
            else
            {
                byte[] buffer = new byte[length];
                fs.Seek(pos, SeekOrigin.Begin);
                fs.Read(buffer, 0, length);
                return buffer;
            }
        }

        public static void Encrypt(string EncryptedFilePath, Key key)
        {
            string EFName = Path.GetFileNameWithoutExtension(EncryptedFilePath);
            string EFNameExt = Path.GetFileName(EncryptedFilePath);
            string EFNameDir = Path.GetDirectoryName(EncryptedFilePath);
            File.Copy(EncryptedFilePath, temp + @"\" + EFName + "def.sketemp", true);
            string PreviousFilePath = temp + @"\" + EFName + "def.sketemp";
            string CurrentFilePath = null;
            CurrentFilePath = temp + @"\" + EFName + "enc.sketemp";
            FileStream Previous = new FileStream(PreviousFilePath, FileMode.Open);
            FileStream Current = new FileStream(CurrentFilePath, FileMode.Create);
            FileInfo PreviousFile = new FileInfo(PreviousFilePath);
            FileInfo CurrentFile = new FileInfo(CurrentFilePath);
            Current.SetLength(PreviousFile.Length);

            long Pos = key.Start;
            DoubleBuffer db = null;
            for (long o = 0; o < PreviousFile.Length; o++)
            {
                Pos = Misc.Mod(Pos, PreviousFile.Length);
                if (o % 67108864 == 0)
                {
                    
                }

                //Previous.Seek(Pos, SeekOrigin.Begin);
                //int First = Previous.ReadByte();

                /*int Second;
                long SPos = Misc.Mod((BigInteger)Pos - key.Start, PreviousFile.Length);
                long SJump = Misc.Mod((BigInteger)Pos + key.Jump - key.Start, PreviousFile.Length);
                if (SJump >= SPos)
                {
                    //Previous.Seek(Misc.Mod((BigInteger)Pos + key.Jump, PreviousFile.Length), SeekOrigin.Begin);
                    //Second = Previous.ReadByte();
                }
                else
                {
                    //Current.Seek(Misc.Mod((BigInteger)Pos + key.Jump, PreviousFile.Length), SeekOrigin.Begin);
                    //Second = Current.ReadByte();
                }*/
                int First = db.Get(Pos);
                int Second = db.Get(Misc.Mod((BigInteger)Pos + key.Jump, PreviousFile.Length));

                byte New;
                byte[] Operations = new byte[8];
                byte[] Multipliers = new byte[8];
                for (int p = 0; p < 8; p++)
                    Operations[p] = Convert.ToByte(key.Operations.ToString("X").Split(2)[p], 16);
                for (int p = 0; p < 8; p++)
                    Multipliers[p] = Convert.ToByte(key.Multipliers.ToString("X").Split(2)[p], 16);
                if (new BitArray(Operations).Get((int)(o % 64)))
                {
                    New = (byte)Misc.Mod(First + (Second * Multipliers[o % 8] + 5), 256);
                }
                else
                {
                    New = (byte)Misc.Mod(First - (Second * Multipliers[o % 8] + 5), 256);
                }

                /*Current.Seek(Pos, SeekOrigin.Begin);
                Current.WriteByte(New);*/
                db.Set(Pos, New);

                Pos++;
            }

            Previous.Close();
            Current.Close();
            File.Delete(PreviousFilePath);
            PreviousFilePath = CurrentFilePath;
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
            CurrentFilePath = temp + @"\" + EFName + "enc.sketemp";
            FileStream Previous = new FileStream(PreviousFilePath, FileMode.Open);
            FileStream Current = new FileStream(CurrentFilePath, FileMode.Create);
            FileInfo PreviousFile = new FileInfo(PreviousFilePath);
            FileInfo CurrentFile = new FileInfo(CurrentFilePath);
            Current.SetLength(PreviousFile.Length);

            long Pos = key.Start - 1;
            for (long o = 0; o < PreviousFile.Length; o++)
            {
                Pos = Misc.Mod(Pos, PreviousFile.Length);
                Previous.Seek(Pos, SeekOrigin.Begin);

                int First = Previous.ReadByte();
                int Second;
                long SPos = Misc.Mod((BigInteger)Pos - key.Start, PreviousFile.Length);
                long SJump = Misc.Mod((BigInteger)Pos + key.Jump - key.Start, PreviousFile.Length);
                if (SJump <= SPos)
                {
                    Previous.Seek(Misc.Mod((BigInteger)Pos + key.Jump, PreviousFile.Length), SeekOrigin.Begin);
                    Second = Previous.ReadByte();
                }
                else
                {
                    Current.Seek(Misc.Mod((BigInteger)Pos + key.Jump, PreviousFile.Length), SeekOrigin.Begin);
                    Second = Current.ReadByte();
                }

                byte New;
                byte[] Operations = new byte[8];
                byte[] Multipliers = new byte[8];
                for (int p = 0; p < 8; p++)
                    Operations[p] = Convert.ToByte(key.Operations.ToString("X").Split(2)[p], 16);
                for (int p = 0; p < 8; p++)
                    Multipliers[p] = Convert.ToByte(key.Multipliers.ToString("X").Split(2)[p], 16);
                if (new BitArray(Operations).Get((int)((PreviousFile.Length - o - 1) % 64)))
                {
                    New = (byte)Misc.Mod(First - (Second * Multipliers[(PreviousFile.Length - o - 1) % 8] + 5), 256);
                }
                else
                {
                    New = (byte)Misc.Mod(First + (Second * Multipliers[(PreviousFile.Length - o - 1) % 8] + 5), 256);
                }

                Current.Seek(Pos, SeekOrigin.Begin);
                Current.WriteByte(New);

                Pos--;
            }

            Previous.Close();
            Current.Close();
            File.Delete(PreviousFilePath);
            PreviousFilePath = CurrentFilePath;
            File.Copy(PreviousFilePath, EFNameDir + "\\decrypted_" + EFNameExt.Replace(".ske", ""), true);
        }
    }
}
