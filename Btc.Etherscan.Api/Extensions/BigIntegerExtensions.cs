using Nethereum.Web3;
using System.Numerics;

namespace Btc.Etherscan.Api.Extensions
{
    public static class BigIntegerExtensions
    {
        public static decimal ToDecimal(this BigInteger val)
        {
            return Web3.Convert.FromWei(val);
        }
    }
}
