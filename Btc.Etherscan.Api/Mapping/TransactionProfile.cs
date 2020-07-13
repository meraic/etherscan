using AutoMapper;
using Btc.Etherscan.Api.Extensions;
using Btc.Etherscan.Api.ViewModels;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Btc.Etherscan.Api.Mapping
{
    /// <summary>
    /// Ethereum transaction profile
    /// </summary>
    public class TransactionProfile : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionVewModel>()
                .ForMember(d => d.Gas, o => o.MapFrom(s => s.Gas.Value.ToDecimal()))
                .ForMember(d => d.GasPrice, o => o.MapFrom(s => s.GasPrice.Value.ToDecimal()))
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Value.Value.ToDecimal()))
                .ForMember(d => d.Nonce, o => o.MapFrom(s => s.Nonce.Value.ToDecimal()));
        }
    }
}
