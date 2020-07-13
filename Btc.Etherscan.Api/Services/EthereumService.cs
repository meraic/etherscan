using AutoMapper;
using Btc.Etherscan.Api.Extensions;
using Btc.Etherscan.Api.ViewModels;
using Microsoft.Extensions.Configuration;
using Nethereum.Hex.HexTypes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btc.Etherscan.Api.Services
{
    /// <summary>
    /// Ethereum Service
    /// </summary>
    public class EthereumService : EthereumServiceBase, IEthereumService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="mapper"></param>
        public EthereumService(IConfiguration configuration, IMapper mapper) : base(configuration, mapper) {}

        /// <summary>
        /// Get transactions for given block number
        /// </summary>
        /// <param name="block">Block number</param>
        /// <returns>IEnumerable<<see cref="TransactionVewModel"/>></returns>
        public async Task<IEnumerable<TransactionVewModel>> GetTxns(ulong block)
        {
            var blockData = await EthApiBlockService.GetBlockWithTransactionsByNumber
                .SendRequestAsync(block.ToHexBigInteger())
                .ConfigureAwait(false);

            var txns = blockData.Transactions.Select(txn => txn.ToViewModel(Mapper));

            return txns;
        }
    }
}
