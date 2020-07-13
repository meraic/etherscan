using AutoMapper;
using Btc.Etherscan.Api.ViewModels;
using Nethereum.RPC.Eth.DTOs;

namespace Btc.Etherscan.Api.Extensions
{
    public static class TransactionMappingExtension
    {
        public static TransactionVewModel ToViewModel(this Transaction model, IMapper mapper)
        {
            return mapper.Map<TransactionVewModel>(model);
        }
    }
}
