using MediatorLib;
using MediatorLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorConsoleTest
{
    public class DefaultPipelineBehavior<TRequest, TResponse> : IMediatorPipelineBehavior<TRequest, TResponse>
        where TRequest : IMediatorRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            Console.WriteLine("Default pipeline.");

            return await next(request);
        }
    }
}
