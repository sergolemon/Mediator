using MediatorLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorConsoleTest.Multiply
{
    public class MultiplyRequest : IMediatorRequest<int>
    {
        public int A { get; set; }
        public int B { get; set; }
    }
}
