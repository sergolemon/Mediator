using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorLib.Interfaces
{
    public interface IMediatorHandler<TRequest, TResponse>
        where TRequest : IMediatorRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request);
    }
}
