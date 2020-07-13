using AutoFixture;
using AutoMapper;
using AutoMapper.Configuration;
using Btc.Etherscan.Api.Controllers;
using Btc.Etherscan.Common.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.IO;

namespace Btc.Etherscan.Api.Tests
{
    public class TestBase
    {
        protected Mock<ILogger> MockLogger = new Mock<ILogger>();
        protected ILogger Logger => MockLogger.Object;

        protected readonly Fixture Fixture = new Fixture();

        protected IMapper Mapper { get; }

        protected UserIdentity UserIdentity = new UserIdentity("dev", "dev@test.com");

        protected IConfigurationRoot Configuration { get; }

        protected ILogger<TService> GetLogger<TService>()
        {
            return new Mock<ILogger<TService>>().Object;
        }

        public TestBase()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TxnsController).Assembly));
            config.AssertConfigurationIsValid();

            Mapper = config.CreateMapper();

            Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .Build();
        }
    }
}
