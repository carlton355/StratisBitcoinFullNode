using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;

namespace NBitcoin
{
   public class NetworkInfo : NetworkInfoRequest
    {
        [JsonIgnore]    //this is calculated
        public uint256 GenesisHash { get; private set; }

        public string NetworkName { get; }

        [JsonConstructor]
        public NetworkInfo(string networkName, uint time, uint nonce, uint messageStart, int addressPrefix, int port, int rpcPort, int apiPort, string coinSymbol, string genesisHashHex = null)
            : base(time, nonce, messageStart, addressPrefix, port, rpcPort, apiPort, coinSymbol, genesisHashHex)
        {
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
            //uint time, uint nonce, int addressPrefix, uint messageStart, int port, int rpcPort, int apiPort, string coinSymbol
            return new NetworkInfo(networkName, request.Time, request.Nonce, request.MessageStart, request.AddressPrefix, request.Port, request.RpcPort, request.ApiPort, request.CoinSymbol, request.GenesisHashHex);
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