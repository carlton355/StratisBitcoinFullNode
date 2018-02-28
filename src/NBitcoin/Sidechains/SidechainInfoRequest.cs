using Newtonsoft.Json;

namespace NBitcoin
{
    public class SidechainInfoRequest : SidechainInfo<NetworkInfoRequest>
    {
        public SidechainInfoRequest(string name, NetworkInfoRequest mainNet, NetworkInfoRequest testNet, NetworkInfoRequest regTest)
            : base(name, mainNet, testNet, regTest)
        { }
    }

}