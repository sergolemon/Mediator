using MediatorLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorLib
{
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(IMediatorRequest<TResponse> request);
}
