using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datatypelib
{
    public struct Dec1
    {
        private const int min = 0;
        private const int max = 1;

        public static implicit operator Dec1(bool v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Dec1(byte v)
        {
            throw new NotImplementedException();
        }
    }
}
