namespace NBitcoin
{
    public class NetworkInfoRequest
    {
        public uint Time { get; set; }

        public uint Nonce { get; set; }

        public int Port { get; set; }

        public int RpcPort { get; set; }

        public int AddressPrefix { get; set; }

        public NetworkInfoRequest(uint time, uint nonce, int port, int rpcPort, int addressPrefix)
        {
            this.Time = time;
            this.Nonce = nonce;
            this.Port = port;
            this.RpcPort = rpcPort;
            this.AddressPrefix = addressPrefix;
        }

        public NetworkInfoRequest()
        {

        }
    }
}