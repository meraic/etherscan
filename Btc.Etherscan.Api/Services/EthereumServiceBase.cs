using AutoMapper;
using Microsoft.Extensions.Configuration;
using Nethereum.RPC.Eth.Services;
using Nethereum.Web3;

namespace Btc.Etherscan.Api.Services
{
    /// <summary>
    /// Ethereum Service Base Class 
    /// </summary>
    public abstract class EthereumServiceBase
    {
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="mapper"></param>
        public EthereumServiceBase(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            Mapper = mapper;
            EthereumEndPoint = new Web3(configuration["AppSettings:InfuraEndPoint"]);
        }

        /// <summary>
        /// Mapper
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Ethereum Service End Point
        /// </summary>
        public Web3 EthereumEndPoint { get; }

        /// <summary>
        /// Ethereum Transaction Service
        /// </summary>
        public IEthApiTransactionsService EthApiTransactionService => EthereumEndPoint.Eth.Transactions;

        /// <summary>
        /// Ethereum Block Service
        /// </summary>
        public IEthApiBlockService EthApiBlockService => EthereumEndPoint.Eth.Blocks;
    }
}
