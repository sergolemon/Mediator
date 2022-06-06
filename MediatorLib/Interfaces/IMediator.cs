using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorLib.Interfaces
{
    public interface IMediator
    {
        public Task<TResponse> Send<TResponse>(IMediatorRequest<TResponse> request);
    }
}
