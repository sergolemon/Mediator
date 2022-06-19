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
            Type handlerInterfaceType = typeof(IMediatorHandler<,>)
                .MakeGenericType(requestType, responseType);

            Type handlerType = _assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Single(type => 
                    type.IsClass &&
                    !type.IsAbstract &&
                    type.IsAssignableTo(handlerInterfaceType)
                );

            object handlerInstance = Activator.CreateInstance(handlerType)!;
            MethodInfo handleMethodInfo = handlerType.GetMethod("Handle")!;
            RequestHandlerDelegate<TResponse> handleDelegate = 
                async (request) => 
                await (Task<TResponse>)handleMethodInfo.Invoke(handlerInstance, new object[] { request })!;

            Type pipelineInterfaceType = typeof(IMediatorPipelineBehavior<,>)
                .MakeGenericType(requestType, responseType);

            Type? pipelineType = _assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .SingleOrDefault(type => 
                    type.IsClass &&
                    !type.IsGenericType &&
                    !type.IsAbstract &&
                    type.IsAssignableTo(pipelineInterfaceType)
                ) ??
                _assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .SingleOrDefault(type =>
                    type.IsClass &&
                    type.IsGenericType &&
                    !type.IsAbstract &&
                    type.GetInterfaces()
                        .Any(i => 
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition()
                                .IsAssignableTo(typeof(IMediatorPipelineBehavior<,>))
                        )
                )?.MakeGenericType(requestType, responseType);

            object? pipelineInstance = pipelineType != null ? Activator.CreateInstance(pipelineType) : null;
            MethodInfo? pipelineMethodInfo = pipelineType?.GetMethod("Handle")!;

            return pipelineMethodInfo != null ? 
                await (Task<TResponse>)pipelineMethodInfo.Invoke(pipelineInstance, new object[] { request, handleDelegate })! :
                await (Task<TResponse>)handleMethodInfo.Invoke(handlerInstance, new object[] { request })!;
        }
    }
}
