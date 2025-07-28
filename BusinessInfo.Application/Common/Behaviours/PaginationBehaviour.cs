using BusinessInfo.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BusinessInfo.Application.Common.Behaviours
{
    public class PaginationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                where TRequest : IRequest<TResponse>

    {
        private readonly ILogger<object> _logger;
        private readonly int _defaultPagination;

        //public PaginationBehaviour(ILogger<object> logger)
        //{
        //    _logger = logger;
        //    _defaultPagination = int.Parse(Configuration.GetConfiguration()[ConfigurationIdentifiers.DefaultLimitResponse]);

        //}

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IPaginator)
            {
                if ((request as IPaginator).Limit == 0)
                {
                    (request as IPaginator).Limit = _defaultPagination;
                    (request as IPaginator).Offset = 0;
                }
            }

            var response = await next();
            return response;
        }
    }
}
