using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using skelib.Exceptions;

namespace skelib
{
    public class Key
    {
        public static readonly Random r = new Random();

        static string Sha256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public Key(string key)
        {
            if (!(key.Length >= 6 && key.Length <= 32 || key.Length == 64))
                throw new InvalidKeyException();
            char[] chars = key.ToCharArray();
            
            if (key.Length < 64)
            {
                chars = Sha256(key).ToUpper().ToCharArray();
            }

            long Start = Convert.ToInt64(Misc.CharArrayToString(chars, 0, 16), 16);
            long Jump = Convert.ToInt64(Misc.CharArrayToString(chars, 16, 16), 16);
            long Operations = Convert.ToInt64(Misc.CharArrayToString(chars, 32, 16), 16);
            long Multipliers = Convert.ToInt64(Misc.CharArrayToString(chars, 48, 16), 16);

            this.Start = Start;
            this.Jump = Jump;
            this.Operations = Operations;
            this.Multipliers = Multipliers;
        }

        public Key(long Start, long Jump, long Operations, long Multipliers)
        {
            this.Start = Start;
            this.Jump = Jump;
            this.Operations = Operations;
            this.Multipliers = Multipliers;
        }
        
        public long Start { get; set; }
        public long Jump { get; set; }
        public long Operations { get; set; }
        public long Multipliers { get; set; }

        public static Key Random()
        {
            string key = "";
            key += Misc.Fill(Misc.LongRandom().ToString("X"), 16);
            key += Misc.Fill(Misc.LongRandom().ToString("X"), 16);
            key += Misc.Fill(Misc.LongRandom().ToString("X"), 16);
            key += Misc.Fill(Misc.LongRandom().ToString("X"), 16);
            return new Key(key);
        }

        public override string ToString()
        {
            string key = "";
            key += Misc.Fill(Convert.ToString(this.Start, 16).ToUpper(), 16);
            key += Misc.Fill(Convert.ToString(this.Jump, 16).ToUpper(), 16);
            key += Misc.Fill(Convert.ToString(this.Operations, 16).ToUpper(), 16);
            key += Misc.Fill(Convert.ToString(this.Multipliers, 16).ToUpper(), 16);
            return key;
        }
    }
}
