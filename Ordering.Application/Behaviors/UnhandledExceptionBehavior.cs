using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Excepcion no controlada para la peticion {typeof(TRequest).Name}");
                throw;
            }
        }
    }
}

/*
 * 1.- crear 2 servicios rest (libre)
 * 2.- conectarlos y enviar un mensaje por Rabbit MQ (NOTA: Usar el rabbit que ya tenemos en docker)
 * 
 */