using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using POS.Application.Interfaces;
using POS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Logging
{
    public class POSLoggerProvider : ILoggerProvider
    {
        private readonly ILogRepository _logRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public POSLoggerProvider(ILogRepository logRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logRepository = logRepository;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new POSLogger(categoryName, _logRepository, _httpContextAccessor);
        }

        public void Dispose()
        {
        }
    }
}
