namespace NBitcoin
{
    public class NetworkInfoRequest
    {
        public uint Time { get; set; }
        public uint Nonce { get; set; }
        public int Port { get; set; }
        public int RpcPort { get; set; }
        public int AddressPrefix { get; set; }
        public string CoinSymbol { get; }
        public uint MessageStart { get; }
        public int ApiPort { get; }

        public NetworkInfoRequest(uint time, uint nonce, string coinSymbol, uint messageStart, int addressPrefix, int port, int rpcPort, int apiPort)
        {
            this.Time = time;
            this.Nonce = nonce;
            this.CoinSymbol = coinSymbol;
            this.MessageStart = messageStart;
            this.AddressPrefix = addressPrefix;
            this.Port = port;
            this.RpcPort = rpcPort;
            this.ApiPort = apiPort;
        }

        public NetworkInfoRequest()
        {

        }
    }
}