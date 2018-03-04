using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;

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

    public class NetworkInfo
    {
        public uint Time { get; }

        public uint Nonce { get; }

        public int Port { get; }

        public int RpcPort { get; }

        public int AddressPrefix { get; }

        [JsonIgnore]    //this is calculated
        public uint256 GenesisHash { get; private set; }

        public string GenesisHashHex { get; private set; }

        public string NetworkName { get; }

        [JsonConstructor]
        public NetworkInfo(string networkName, uint time, uint nonce, int port, int rpcPort, int addressPrefix, string genesisHashHex)
            : this(networkName, time, nonce, port, rpcPort, addressPrefix)
        {
            this.GenesisHashHex = genesisHashHex;
        }

        public NetworkInfo(string networkName, uint time, uint nonce, int port, int rpcPort, int addressPrefix)
        {
            this.Time = time;
            this.Nonce = nonce;
            this.Port = port;
            this.RpcPort = rpcPort;
            this.AddressPrefix = addressPrefix;
            this.NetworkName = networkName;
        }

        private Target GetPowLimit()
        {
            if (this.NetworkName == "SidechainMain") return new Target(new uint256("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"));
            if (this.NetworkName == "SidechainTestNet") return new Target(uint256.Parse("0000ffff00000000000000000000000000000000000000000000000000000000"));
            if (this.NetworkName == "SidechainRegTest") return new Target(new uint256("7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"));
            throw new ArgumentException("invalid sidechain network name");
        }

        internal static NetworkInfo FromNetworkInfoRequest(string networkName, NetworkInfoRequest request)
        {
            return new NetworkInfo(networkName, request.Time, request.Nonce, request.Port, request.RpcPort, request.AddressPrefix);
        }

        internal static void ComputeGenesisHash(NetworkInfo networkInfo)
        {
            if (networkInfo.NetworkName == "SidechainMain")
            {
                Block.BlockSignature = true;
                Transaction.TimeStamp = true;

                Block genesis = Network.CreateSidechainGenesisBlock(networkInfo.Time, networkInfo.Nonce, 0x1e0fffff, 1, Money.Zero);
                uint256 ui1 = genesis.GetHash();
                genesis.Header.Time = networkInfo.Time;
                genesis.Header.Nonce = networkInfo.Nonce;
                genesis.Header.Bits = networkInfo.GetPowLimit();
                networkInfo.GenesisHash = genesis.GetHash();
                networkInfo.GenesisHashHex = networkInfo.GenesisHash.ToString();
            }
            else
            {
                Block genesis = networkInfo.NetworkName == "SidechainTestNet" ? Network.SidechainMain.GetGenesis().Clone() : Network.SidechainTestNet.GetGenesis().Clone();
                genesis.Header.Time = networkInfo.Time;
                genesis.Header.Nonce = networkInfo.Nonce;
                genesis.Header.Bits = networkInfo.GetPowLimit();
                networkInfo.GenesisHash = genesis.GetHash();
                networkInfo.GenesisHashHex = networkInfo.GenesisHash.ToString();
            }
        }
    }
}