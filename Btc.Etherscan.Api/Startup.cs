using Btc.Etherscan.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Btc.Etherscan.Api
{
    public class Startup : Common.Core.StartupBase
    {
        protected override Assembly Assembly => GetType().Assembly;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }

        protected override void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddScoped<IEthereumService, EthereumService>();
        }
    }
}
