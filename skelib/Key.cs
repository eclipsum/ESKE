using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skelib.Exceptions;

namespace skelib
{
    public class Key
    {
        public Key()
        {
            this.Direction = true;
            this.Operations = 0;
            this.Start = 0;
            this.Jump = 1;
            this.Loops = 1;
            this.Multiplier = 1;
        }

        public Key(string key)
        {
            if (key.Length != 39)
                throw new InvalidKeyException();
            char[] chars = key.ToCharArray();

            if (byte.Parse(chars[0] + "", System.Globalization.NumberStyles.HexNumber) % 2 == 0)
                this.Direction = false;
            else
                this.Direction = true;
            this.Operations = byte.Parse(chars[1] + chars[2] + "", System.Globalization.NumberStyles.HexNumber);
            this.Loops = byte.Parse(chars[3] + chars[4] + "", System.Globalization.NumberStyles.HexNumber);
            this.Multiplier = byte.Parse(chars[5] + chars[6] + "", System.Globalization.NumberStyles.HexNumber);
            this.Start = long.Parse(chars[7] + chars[8] + chars[9] + chars[10] + chars[11] + chars[12] + chars[13] + chars[14] + 
                                     chars[15] + chars[16] +chars[17] +chars[18] +chars[19] +chars[20] +chars[21] +chars[22] + "", 
                                     System.Globalization.NumberStyles.HexNumber);
            this.Jump = long.Parse(chars[23] + chars[24] + chars[25] + chars[26] + chars[27] + chars[28] + chars[29] + chars[30] +
                                   chars[31] + chars[32] + chars[33] + chars[34] + chars[35] + chars[36] + chars[37] + chars[38] + "",
                                   System.Globalization.NumberStyles.HexNumber);
        }

        public Key(byte Operations, byte Loops, byte Multiplier, long Start, long Jump, bool Direction)
        {
            this.Direction = Direction;//
            this.Operations = Operations;
            this.Start = Start;//
            this.Multiplier = Multiplier;
            this.Jump = Jump;//
            this.Loops = Loops;//
        }

        public byte Operations { get; set; }
        public byte Multiplier { get; set; }
        public byte Loops { get { return Loops; } set { Loops = (byte)(value % 16 + 4); } }
        public long Jump { get; set; }
        public long Start { get { return Start; } set { if (value < 0) Start = 0; else Start = value; } }
        public bool Direction { get; set; }

        private static readonly Random r = new Random();

        public static string Random()
        {
            string key = "";
            for(int i = 0; i < 39; i++)
                key += r.Next(0, 16).ToString("X");
            return key;
        }
    }
}
