using Btc.Etherscan.Api.Controllers;
using Btc.Etherscan.Api.Services;
using Btc.Etherscan.Api.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Btc.Etherscan.Api.Tests.Controllers
{
    public class TxnsControllerTests : TestBase
    {
        [Fact]
        public async Task ShouldGetTransactions()
        {
            ulong block = 9148873;

            var txns = new List<TransactionVewModel>()
            {
                new TransactionVewModel() {
                    TransactionHash = "0x827c9a4a1ae2cf9c20fa1dad305b4b8f8f336aab52ff9563379a8a9dbf0d1727",
                    TransactionIndex = "0",
                    BlockHash = "0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1",
                    BlockNumber = "9148873",
                    From = "0xc779a4bdc3696baf2a6d62ddfc2d0664d3c4fd7f",
                    To = "0x92d44b6b3a23b48a93b1bce4d206c0280f96b1cd",
                    Gas = 0.000000000000021M,
                    GasPrice = 0.00000008M,
                    Value = 0.01832M,
                    Input = "0x",
                    Nonce = .000000000000000154M,
                    R = "0x6055ae2794fad915c84b8ad5c60cc09522609c509f05177d8a5f65fd9025ab0e",
                    S = "0x334f79392ec759ea4d4baa7bbd55cd3f37900c7ba2ed10a56de7b2c32d01feea",
                    V = "0x25"
                }
            };
            
            // Create mock service object 
            var ethereumServiceMock = new Mock<IEthereumService>();

            // Setup return objects
            ethereumServiceMock.Setup(x => x.GetTxns(It.IsAny<ulong>())).ReturnsAsync(txns);

            // Setup controller with mock object dependecies 
            var controller = new TxnsController(GetLogger<TxnsController>(), ethereumServiceMock.Object);

            SetupControllerHttpResponse(controller);

            var result = await controller.GetTxns(block) as IEnumerable<TransactionVewModel>;

            result.Should().NotBeNull();
            result.Count().Should().Equals(1);
            result.FirstOrDefault().BlockNumber.Should().Equals(9148873);
        }

        private void SetupControllerHttpResponse(TxnsController controller)
        {
            var headerDictionary = new HeaderDictionary();
            var response = new Mock<HttpResponse>();
            response.SetupGet(r => r.Headers).Returns(headerDictionary);

            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Response).Returns(response.Object);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext.Object
            };
        }
    }
}
