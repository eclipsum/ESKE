using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public Key(bool Direction, bool Side, bool Operator, int Start, int Jump, int Loops, int Multiplier)
        {
            this.Direction = Direction;
            this.Side = Side;
            this.Operator = Operator;
            this.Start = Start;
            this.Jump = Jump;
            this.Loops = Loops % 16 + 5;
        }

        public bool Direction { get; set; }
        public bool Side { get; set; }
        public bool Operator { get; set; }
        public int Loops { get; set; }
        public int Start { get; set; }
        public int Jump { get; set; }
    }
}
