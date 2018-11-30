using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skelib
{
    public class Decryption
    {
        private static Random r = new Random();

        public static void Decrypt(string FilePath, Key key)
        {
            FileInfo FI = new FileInfo(FilePath);
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
                if (key.Direction)
                {
                    for (long i = 0; i < FI.Length; i++)
                    {
                        long pi = i;
                        i += key.Start;
                        i = EMath.Mod(i, FI.Length);
                        fs.Position = EMath.Mod(i, FI.Length);
                        int First = fs.ReadByte();
                        if (key.Side)
                            fs.Position = EMath.Mod((i + key.Jump), FI.Length);
                        else
                            fs.Position = EMath.Mod((i - key.Jump), FI.Length);
                        int Second = fs.ReadByte();
                        if (key.Operator)
                            First += Second;
                        else
                            First -= Second;
                        First %= 256;
                        bw.Write((byte)First);
                        i = pi;
                    }
                }
                else
                {
                    for (long i = FI.Length - 1; i >= 0; i--)
                    {
                        long pi = i;
                        i += key.Start;
                        i = EMath.Mod(i, FI.Length);
                        fs.Position = EMath.Mod(i, FI.Length);
                        int First = fs.ReadByte();
                        if (key.Side)
                            fs.Position = EMath.Mod((i + key.Jump), FI.Length);
                        else
                            fs.Position = EMath.Mod((i - key.Jump), FI.Length);
                        int Second = fs.ReadByte();
                        if (key.Operator)
                            First += Second;
                        else
                            First -= Second;
                        First %= 256;
                        bw.Write((byte)First);
                        i = pi;
                    }
                }
                fs.Close();
                bw.Close();
                if (LastFile != null)
                    File.Delete(LastFile);
                LastFile = TempFile;
            }
            File.Delete(FilePath + ".ske");
            File.Copy(LastFile, FilePath + ".ske");
        }
    }
}
