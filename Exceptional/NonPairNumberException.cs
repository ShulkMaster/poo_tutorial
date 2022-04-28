using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional
{
    internal class NonPairNumberException : Exception
    {

    }

    internal class InvalidWordException : Exception
    {
        public string Word { get; }

        public InvalidWordException(string badWord): base($"Una palabra prohibida fue detectada '{badWord}'") {
            Word = badWord;
        }
    }
}
