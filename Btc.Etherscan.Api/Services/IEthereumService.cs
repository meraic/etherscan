using Btc.Etherscan.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Btc.Etherscan.Api.Services
{
    /// <summary>
    /// This is Ethereum service that will connect using Ethereum RPC API endpoint and authentication details
    /// </summary>
    public interface IEthereumService
    {
        Task<IEnumerable<TransactionVewModel>> GetTxns(ulong block);
    }
}
