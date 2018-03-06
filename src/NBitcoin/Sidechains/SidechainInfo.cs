using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NBitcoin
{
    public class SidechainInfoBase<T>
    {
        public string ChainName { get; }
        public string CoinName { get; }
        public int CoinType { get; }
        public T MainNet { get; }
        public T TestNet { get; }
        public T RegTest { get; }

        [JsonConstructor]
        public SidechainInfoBase(string chainName, string coinName, int coinType, T mainNet, T testNet, T regTest)
        {
            this.ChainName = chainName;
            this.CoinName = coinName;
            this.CoinType = coinType;
            this.MainNet = mainNet;
            this.TestNet = testNet;
            this.RegTest = regTest;
        }
    }

    public class SidechainInfoRequest : SidechainInfoBase<NetworkInfoRequest>
    {
        public SidechainInfoRequest(string chainName, string coinName, int coinType,
            NetworkInfoRequest mainNet, NetworkInfoRequest testNet, NetworkInfoRequest regTest)
            : base(chainName, coinName, coinType, mainNet, testNet, regTest)
        { }
    }

    public class SidechainInfo : SidechainInfoBase<NetworkInfo>
    {
        public SidechainInfo(string chainName, string coinName, int coinType,
            NetworkInfoRequest mainNet, NetworkInfoRequest testNet, NetworkInfoRequest regTest)
            : base(chainName, coinName, coinType,
                NetworkInfo.FromNetworkInfoRequest("SidechainMain", mainNet),
                NetworkInfo.FromNetworkInfoRequest("SidechainTestNet", testNet),
                NetworkInfo.FromNetworkInfoRequest("SidechainRegTest", regTest))
        { }
    }
}