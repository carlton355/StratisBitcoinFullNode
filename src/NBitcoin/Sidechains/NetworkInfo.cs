using System;
using System.Collections.Generic;
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
        public uint256 GenesisHash { get; }

        public string GenesisHashHex { get; }

        public string NetworkName { get; }

        [JsonConstructor]
        public NetworkInfo(string networkName, uint time, uint nonce, int port, int rpcPort, int addressPrefix, string genesisHashHex)
            : this(networkName, time, nonce, port, rpcPort, addressPrefix)
        {
            //when we deserialize the hex hash from our store we check the
            //calculated hash against the stored hash.
            if (this.GenesisHashHex != genesisHashHex)
                throw new ArgumentException("The genesis hash input was not equal to the computed hash.");
        }

        public NetworkInfo(string networkName, uint time, uint nonce, int port, int rpcPort, int addressPrefix)
        {
            this.Time = time;
            this.Nonce = nonce;
            this.Port = port;
            this.RpcPort = rpcPort;
            this.AddressPrefix = addressPrefix;
            this.NetworkName = networkName;

            //calculate genesis block hash to store with the info.
            //our intent is to use the genesis hash as a hash.
            this.GenesisHash = this.ComputeGenesisHash(networkName, time, nonce);
            this.GenesisHashHex = this.GenesisHash.ToString();
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

        private uint256 ComputeGenesisHash(string networkName, uint time, uint nonce)
        {
            if (networkName == "SidechainMain")
            {
                Block.BlockSignature = true;
                Transaction.TimeStamp = true;

                Block genesis = Network.CreateSidechainGenesisBlock(time, nonce, 0x1e0fffff, 1, Money.Zero);
                uint256 ui1 = genesis.GetHash();
                genesis.Header.Time = time;
                genesis.Header.Nonce = nonce;
                genesis.Header.Bits = this.GetPowLimit();
                return genesis.GetHash();
            }
            else
            {
                Block genesis = Network.SidechainMain.GetGenesis().Clone();
                genesis.Header.Time = time;
                genesis.Header.Nonce = nonce;
                genesis.Header.Bits = this.GetPowLimit();
                return genesis.GetHash();
            }
        }
    }
}