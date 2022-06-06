using MediatorLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorConsoleTest.Sum
{
    public class SumHandler : IMediatorHandler<SumRequest, int>
    {
        public async Task<int> Handle(SumRequest request)
        {
            return request.A + request.B;
        }
    }
}
