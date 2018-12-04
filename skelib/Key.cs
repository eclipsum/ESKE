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
        public static readonly Random r = new Random();

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
            if (key.Length != 38)
                throw new InvalidKeyException();
            if (!(key.ToCharArray()[0] == '0' || key.ToCharArray()[0] == '1'))
                throw new InvalidKeyException();
            char[] chars = key.ToCharArray();
            
            bool Direction = Convert.ToByte(chars[0] + "", 16) % 2 == 1;
            byte Loops = Convert.ToByte(chars[1] + "", 16);
            byte Operations = (byte)Convert.ToInt32(Misc.CharArrayToString(chars, 2, 2), 16);
            byte Multiplier = (byte)Convert.ToInt32(Misc.CharArrayToString(chars, 4, 2), 16);
            long Start = Convert.ToInt64(Misc.CharArrayToString(chars, 6, 16), 16);
            long Jump = Convert.ToInt64(Misc.CharArrayToString(chars, 22, 16), 16);

            this.Direction = Direction;
            this.Operations = Operations;
            this.Loops = Loops;
            this.Multiplier = Multiplier;
            this.Start = Start;
            this.Jump = Jump;
        }

        public Key(byte Operations, byte Loops, byte Multiplier, long Start, long Jump, bool Direction)
        {
            this.Direction = Direction;
            this.Operations = Operations;
            this.Start = Start;
            this.Multiplier = Multiplier;
            this.Jump = Jump;
            this.Loops = Loops;
        }

        private byte _Loops;
        public byte Operations { get; set; }
        public byte Loops { get { return _Loops;  } set { _Loops = (byte)(value % 16); } }
        public byte Multiplier { get; set; }
        public long Jump { get; set; }
        public long Start { get; set; }
        public bool Direction { get; set; }

        public static Key Random()
        {
            string key = "";
            key += r.Next(0, 2).ToString("X");
            key += r.Next(0, 16).ToString("X");
            key += Misc.Fill(r.Next(0, 256).ToString("X"), 2);
            key += Misc.Fill(r.Next(0, 256).ToString("X"), 2);
            key += Misc.Fill(Misc.LongRandom().ToString("X"), 16);
            key += Misc.Fill(Misc.LongRandom().ToString("X"), 16);
            return new Key(key);
        }

        public override string ToString()
        {
            string key = "";
            key += (this.Direction ? 1 : 0);
            key += Convert.ToString(this.Loops, 16).ToUpper();
            key += Misc.Fill(Convert.ToString(this.Operations, 16).ToUpper(), 2);
            key += Misc.Fill(Convert.ToString(this.Multiplier, 16).ToUpper(), 2);
            key += Misc.Fill(Convert.ToString(this.Start, 16).ToUpper(), 16);
            key += Misc.Fill(Convert.ToString(this.Jump, 16).ToUpper(), 16);
            return key;
        }
    }
}
