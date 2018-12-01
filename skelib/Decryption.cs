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

        /*public static void Decrypt(string FilePath, Key key)
        {
            FileInfo FI = new FileInfo(FilePath);
            string[] Debug = new string[FI.Length];
            string LastFile = null;
            for (int l = 0; l < key.Loops; l++)
            {
                string TempFile = Environment.GetEnvironmentVariable("temp") + "\\" + FI.Name + r.Next(0, 10000) + ".sketemp";
                FileStream fs = null;
                if (LastFile == null)
                    fs = new FileStream(FilePath, FileMode.Open);
                else
                    fs = new FileStream(LastFile, FileMode.Open);
                BinaryWriter bw = new BinaryWriter(new FileStream(TempFile, FileMode.Create), Encoding.UTF8);
                long o = key.Start;
                if (key.Direction)
                    o--;
                else
                    o++;
                long last = -1;
                for (long i = 0; i < FI.Length; i++)
                {
                    o = EMath.Mod(o, FI.Length);
                    fs.Position = o;
                    long First = fs.ReadByte();
                    if (l == 0)
                        Debug[FI.Length - i - 1] = First + " >\t";
                    if (key.Side)
                        fs.Position = EMath.Mod(o + 1, FI.Length);
                    else
                        fs.Position = EMath.Mod(o - 1, FI.Length);
                    long Second;
                    if (last == -1)
                        Second = fs.ReadByte();
                    else
                        Second = last;
                    if (key.Operator)
                        First -= Second * key.Multiplier;
                    else
                        First += Second * key.Multiplier;
                    First = EMath.Mod(First, 256);
                    last = First;
                    bw.Write((byte)First);
                    if (key.Direction)
                        o--;
                    else
                        o++;

                    Debug[FI.Length - i - 1] += First + " >\t";
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
            if (File.Exists(FilePath.Replace(".ske", ".skd")))
                File.Delete(FilePath.Replace(".ske", ".skd"));
            File.Copy(LastFile, FilePath.Replace(".ske", ".skd"));
        }*/
    }
}
