using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace skelib
{
    public class Key
    {
        public Key()
        {
            this.Direction = true;
            this.Side = true;
            this.Operator = true;
            this.Start = 0;
            this.Jump = 1;
            this.Loops = 1;
            this.Multiplier = 1;
        }

        public Key(bool Direction, bool Side, bool Operator, int Start, int Jump, int Loops, int Multiplier)
        {
            this.Direction = Direction;
            this.Side = Side;
            this.Operator = Operator;
            this.Start = Start;
            this.Jump = Jump;
            this.Loops = Loops % 16 + 1;
            this.Multiplier = Multiplier;
        }

        public bool Direction { get; set; }
        public bool Side { get; set; }
        public bool Operator { get; set; }
        public int Multiplier { get; set; }
        public int Loops { get; set; }
        public int Start { get; set; }
        public int Jump { get; set; }
    }

    public class Encryption
    {
        private static Random r = new Random();

        public static void Encrypt(string FilePath, Key key)
        {
            _Encrypt(FilePath, key);
        }
        
        private static void _Encrypt(string FilePath, Key key)
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
                        i = Mod(i, FI.Length);
                        fs.Position = Mod(i, FI.Length);
                        int First = fs.ReadByte();
                        if (key.Side)
                            fs.Position = Mod((i + key.Jump), FI.Length);
                        else
                            fs.Position = Mod((i - key.Jump), FI.Length);
                        int Second = fs.ReadByte() * key.Multiplier;
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
                        i = Mod(i, FI.Length);
                        fs.Position = Mod(i, FI.Length);
                        int First = fs.ReadByte();
                        if (key.Side)
                            fs.Position = Mod((i + key.Jump), FI.Length);
                        else
                            fs.Position = Mod((i - key.Jump), FI.Length);
                        int Second = fs.ReadByte() * key.Multiplier;
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

        public static long Mod(long n, long x)
        {
            return ((n % x) + x) % x;
        }
    }
}
