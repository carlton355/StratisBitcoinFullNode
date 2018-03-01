using Newtonsoft.Json;

namespace NBitcoin
{

    public class SidechainInfo : SidechainInfo<NetworkInfo>
    {
        public SidechainInfo(string chainName, string coinName, int coinType,
            NetworkInfoRequest mainNet, NetworkInfoRequest testNet, NetworkInfoRequest regTest)
            :base(chainName, coinName, coinType, 
                 NetworkInfo.FromNetworkInfoRequest("SidechainMain", mainNet),
                 NetworkInfo.FromNetworkInfoRequest("SidechainTestNet", testNet),
                 NetworkInfo.FromNetworkInfoRequest("SidechainRegTest", regTest))
        {}
    }

    public class SidechainInfo<T>
    {
        public string ChainName { get; }
        public string CoinName { get; }
        public int CoinType { get; }
        public T MainNet { get; }
        public T TestNet { get; }
        public T RegTest { get; }

        [JsonConstructor]
        public SidechainInfo(string chainName, string coinName, int coinType, T mainNet, T testNet, T regTest)
        {
            this.ChainName = chainName;
            this.CoinName = coinName;
            this.CoinType = coinType;
            this.MainNet = mainNet;
            this.TestNet = testNet;
            this.RegTest = regTest;
        }
    }

    #region Another implementation
    //public class SidechainInfoRequest2
    //{
    //    public string Name { get; }
    //    public NetworkInfoRequest MainNet { get; }
    //    public NetworkInfoRequest TestNet { get; }
    //    public NetworkInfoRequest RegTest { get; }

    //    [JsonConstructor]
    //    public SidechainInfoRequest2(string name, NetworkInfoRequest mainNet, NetworkInfoRequest testNet, NetworkInfoRequest regTest)
    //    {
    //        this.Name = name;
    //        this.MainNet = mainNet;
    //        this.TestNet = testNet;
    //        this.RegTest = regTest;
    //    }
    //}

    //public class SidechainInfo2 : SidechainInfoRequest2
    //{   
    //    public new NetworkInfo MainNet { get; }
    //    public new NetworkInfo TestNet { get; }
    //    public new NetworkInfo RegTest { get; }

    //    public SidechainInfo2(string name, NetworkInfoRequest mainNetRequest, NetworkInfoRequest testNetRequest, NetworkInfoRequest regTestRequest)
    //        : base(name, mainNetRequest, testNetRequest, regTestRequest)
    //    {
    //        MainNet = NetworkInfo.FromNetworkInfoRequest("SidechainMain", mainNetRequest);
    //        TestNet = NetworkInfo.FromNetworkInfoRequest("SidechainTestNet", testNetRequest);
    //        RegTest = NetworkInfo.FromNetworkInfoRequest("SidechainRegTest", regTestRequest);
    //    }

    //    public SidechainInfo2(string name, NetworkInfo mainNet, NetworkInfo testNet, NetworkInfo regTest)
    //        :base(name, mainNet, testNet, regTest)
    //    {
    //        MainNet = mainNet;
    //        TestNet = testNet;
    //        RegTest = regTest;
    //    }
    //}
    #endregion
}