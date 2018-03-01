using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NBitcoin
{

    public class SidechainInfo : SidechainInfo<NetworkInfo>
    {
        public string CoinName { get; }
        public int CoinType { get; }
        public SidechainInfo(string name, NetworkInfoRequest mainNet, NetworkInfoRequest testNet, NetworkInfoRequest regTest,
            string coinSymbol, byte[] magic, int apiPort,
            string coinName = null, int coinType = 0)
            :base(name, 
                 NetworkInfo.FromNetworkInfoRequest("SidechainMain", mainNet, coinSymbol, magic, apiPort),
                 NetworkInfo.FromNetworkInfoRequest("SidechainTestNet", testNet, coinSymbol, magic, apiPort),
                 NetworkInfo.FromNetworkInfoRequest("SidechainRegTest", regTest, coinSymbol, magic, apiPort))
        {
            CoinName = coinName;
            CoinType = coinType;
        }
    }

    public class SidechainInfo<T>
    {
        public string Name { get; }
        public T MainNet { get; }
        public T TestNet { get; }
        public T RegTest { get; }

        [JsonConstructor]
        public SidechainInfo(string name, T mainNet, T testNet, T regTest)
        {
            this.Name = name;
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