namespace NBitcoin
{
    public class SidechainInfoRequest : SidechainInfo<NetworkInfoRequest>
    {
        public SidechainInfoRequest(string name, string coinName, int coinType, 
            NetworkInfoRequest mainNet, NetworkInfoRequest testNet, NetworkInfoRequest regTest)
            : base(name, coinName, coinType, mainNet, testNet, regTest)
        { }
    }

}