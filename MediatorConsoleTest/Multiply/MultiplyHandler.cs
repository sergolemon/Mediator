using MediatorLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorConsoleTest.Multiply
{
    public class MultiplyHandler : IMediatorHandler<MultiplyRequest, int>
    {
        public async Task<int> Handle(MultiplyRequest request)
        {
            return request.A * request.B;
        }
    }
}
