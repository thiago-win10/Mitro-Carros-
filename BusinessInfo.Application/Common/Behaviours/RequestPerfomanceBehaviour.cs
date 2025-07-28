using BusinessInfo.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BusinessInfo.Application.Common.Behaviours
{
    public class RequestPerfomanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                where TRequest : IRequest<TResponse>

    {
        private string[] _labels;
        private readonly Stopwatch _timer;
        private readonly ILogger<object> _logger;
        private readonly List<RequestPerfomanceBehaviourSetting> _settings;

        public RequestPerfomanceBehaviour(ILogger<object> logger)
        {
            _labels = new List<string>() { "name" }.ToArray();
            _timer = new Stopwatch();
            _settings = Configuration.RequestPerfomanceBehaviourSettings;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var name = typeof(TRequest).Name;

            var limit = _settings.First(x => x.Resource == "Default").ExecutionLimit;

            var setting = _settings.FirstOrDefault(x => x.Resource == name);

            if (setting != null)
            {
                limit = setting.ExecutionLimit;
            }

            if (_timer.ElapsedMilliseconds > limit)
            {
                _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds}) {@Request}",
                    name, _timer.ElapsedMilliseconds, request);
            }

            return response;
        }
    }
}
