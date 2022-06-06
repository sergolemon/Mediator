using MediatorLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediatorLib
{
    public class Mediator : IMediator
    {
        public Mediator(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        private readonly Assembly[] _assemblies;

        public async Task<TResponse> Send<TResponse>(IMediatorRequest<TResponse> request)
        {
            Type requestType = request.GetType();
            Type responseType = typeof(TResponse);
            Type handlerInterfaceType = typeof(IMediatorHandler<,>).MakeGenericType(requestType, responseType);
            Type handlerType = _assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Single(type => 
                    type.IsClass &&
                    !type.IsAbstract &&
                    type.IsAssignableTo(handlerInterfaceType)
                );

            string handlerMethodName = handlerInterfaceType.GetMethods().Single().Name;

            object handlerInstance = Activator.CreateInstance(handlerType)!;
            MethodInfo handlerMethod = handlerType.GetMethod(handlerMethodName)!;

            return await (Task<TResponse>)handlerMethod.Invoke(handlerInstance, new object[] { request })!;
        }
    }
}
