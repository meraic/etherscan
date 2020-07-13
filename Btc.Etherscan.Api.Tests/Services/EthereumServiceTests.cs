using Btc.Etherscan.Api.Services;
using Btc.Etherscan.Api.ViewModels;
using FluentAssertions;
using Nethereum.RPC.Eth.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Btc.Etherscan.Api.Tests.Services
{
    public class EthereumServiceTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnEthApiBlockService()
        {
            var ethereumService = new EthereumService(Configuration, Mapper);
            ethereumService.EthApiBlockService.Should().NotBeNull();
            ethereumService.EthApiBlockService.Should().BeOfType<EthApiBlockService>();
        }

        [Fact]
        public async Task ShouldReturnTransactions()
        {
            ulong block = 9148873;

            var ethereumService = new EthereumService(Configuration, Mapper);

            var txns = await ethereumService.GetTxns(block).ConfigureAwait(false);

            txns.Should<TransactionVewModel>();
            txns.Count().Should().BeGreaterOrEqualTo(0);
            // You can add more test conditions here..........
        }
    }
}
