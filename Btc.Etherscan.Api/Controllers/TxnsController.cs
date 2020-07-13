using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Btc.Etherscan.Api.Services;
using Btc.Etherscan.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Btc.Etherscan.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class TxnsController : ControllerBase
    {
        ILogger<TxnsController> logger;
        private readonly IEthereumService ethereumService;

        public TxnsController(ILogger<TxnsController> logger, IEthereumService ethereumService)
        {
            this.logger = logger;
            this.ethereumService = ethereumService;
        }

        /// <summary>
        /// Gets the transactions for given block number
        /// </summary>
        /// <param name="block">Block number to retrieve transactions for</param>
        /// <returns>IEnumerable<<see cref="TransactionVewModel"/>></returns>
        [HttpGet]
        [Route("{block}")]
        [ProducesResponseType(typeof(IEnumerable<TransactionVewModel>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<TransactionVewModel>> GetTxns([FromRoute] ulong block)
        {
            return await ethereumService.GetTxns(block).ConfigureAwait(false);
        }
    }
}
