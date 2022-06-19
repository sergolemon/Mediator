using MediatorLib;
using MediatorLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorConsoleTest.Sum
{
    public class SumPipelineBehavior : IMediatorPipelineBehavior<SumRequest, int>
    {
        public async Task<int> Handle(SumRequest request, RequestHandlerDelegate<int> next)
        {
            Console.WriteLine("Sum pipeline.");

            return await next(request);
        }
    }
}
