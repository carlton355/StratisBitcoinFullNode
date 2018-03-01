using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;

namespace NBitcoin
{

    public class NetworkInfo : NetworkInfoRequest
    {
        [JsonIgnore]    //this is calculated
        public uint256 GenesisHash { get; }
        public string GenesisHashHex { get; }
        public string NetworkName { get; }
        public string CoinSymbol { get; }
        public byte[] Magic { get; }
        public int ApiPort { get; }


        [JsonConstructor]
        public NetworkInfo(string networkName, uint time, uint nonce, int port, int rpcPort, int addressPrefix, 
                            string genesisHashHex, string coinSymbol, byte[] magic, int apiPort)
            : this (networkName, time, nonce, port, rpcPort, addressPrefix, coinSymbol, magic, apiPort)
        {
            //when we deserialize the hex hash from our store we check the
            //calculated hash against the stored hash.
            if (this.GenesisHashHex != genesisHashHex)
                throw new ArgumentException("The genesis hash input was not equal to the computed hash.");
        }

        public NetworkInfo(string networkName, uint time, uint nonce, int port, int rpcPort, int addressPrefix,
                            string coinSymbol, byte[] magic, int apiPort) 
            : base(time, nonce, port, rpcPort, addressPrefix)
        {
            this.NetworkName = networkName;

            //calculate genesis block hash to store with the info.
            //our intent is to use the genesis hash as a hash. novel!
            Block genesis = Network.StratisMain.GetGenesis().Clone();
            genesis.Header.Time = time;
            genesis.Header.Nonce = nonce;
            genesis.Header.Bits = this.GetPowLimit();
            this.GenesisHash = genesis.GetHash();
            this.GenesisHashHex = this.GenesisHash.ToString();

            CoinSymbol = coinSymbol;
            Magic = magic ?? new byte[4];
            ApiPort = apiPort;
        }

        private Target GetPowLimit()
        {
            if (this.NetworkName == "SidechainMain") return new Target(new uint256("00000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffff"));
            if (this.NetworkName == "SidechainTestNet") return new Target(uint256.Parse("0000ffff00000000000000000000000000000000000000000000000000000000"));
            if (this.NetworkName == "SidechainRegTest") return new Target(new uint256("7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"));
            throw new ArgumentException("invalid sidechain network name");
        }

        internal static NetworkInfo FromNetworkInfoRequest(string networkName, NetworkInfoRequest request, string coinSymbol, byte[] magic, int apiPort)
        {
            return new NetworkInfo(networkName, request.Time, request.Nonce, request.Port, request.RpcPort, request.AddressPrefix, coinSymbol, magic, apiPort);
        }
    }
}