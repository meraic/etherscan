using System;
using System.Collections.Generic;
using System.Text;

namespace Btc.Etherscan.Api.ViewModels
{
    /// <summary>
    /// Transaction View Model
    /// </summary>
    public class TransactionVewModel
    {
        /// <summary>
        /// Transaction Hash (e.g. 0x827c9a4a1ae2cf9c20fa1dad305b4b8f8f336aab52ff9563379a8a9dbf0d1727)
        /// </summary>
        public string TransactionHash { get; set; }

        /// <summary>
        /// Transaction Index (e.g 0 or 1 etc)
        /// </summary>
        public string TransactionIndex { get; set; }

        /// <summary>
        /// Block Hash (e.g. 0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1)
        /// </summary>
        public string BlockHash { get; set; }

        /// <summary>
        /// Block Number (e.g. 9148873)
        /// </summary>
        public string BlockNumber { get; set; }

        /// <summary>
        /// From address (e.g. 0xc779a4bdc3696baf2a6d62ddfc2d0664d3c4fd7f)
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// To address (e.g. 0x92d44b6b3a23b48a93b1bce4d206c0280f96b1cd)
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gas value in decimal (e.g 0.000000000000021)
        /// </summary>
        public decimal Gas { get; set; }

        /// <summary>
        /// Gas Price in decimal (e.g 0.000000000000021)
        /// </summary>
        public decimal GasPrice { get; set; }

        /// <summary>
        /// Value in Ether (e.g. .01832)
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Input
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Nonce
        /// </summary>
        public decimal Nonce { get; set; }

        /// <summary>
        /// R
        /// </summary>
        public string R { get; set; }

        /// <summary>
        /// S
        /// </summary>
        public string S { get; set; }

        /// <summary>
        /// V
        /// </summary>
        public string V { get; set; }
    }
}
