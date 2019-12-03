using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;

namespace Stoicode.UniLib.Net
{
    public abstract class NetServer : MonoBehaviour, INetEventListener
    {
        public int Port { get; set; }
        public int MaxConnections { get; set; }

        public string Key { get; set; }

        public NetManager NetManager { get; set; }
        public NetPacketProcessor NetProcessor { get; set; }
        public NetDataWriter DataWriter { get; set; }

        public bool Initialized { get; set; }


        public void Initialize(int port, int maxConnections, string key)
        {
            Port = port;
            MaxConnections = maxConnections;
            Key = key;

            NetManager = new NetManager(this)
            {
                DiscoveryEnabled = true,
                UpdateTime = 15
            };

            NetProcessor = new NetPacketProcessor();
            DataWriter = new NetDataWriter();

            NetManager.Start(Port);

            ConfigureListeners();

            Initialized = true;
        }

        public abstract void OnPeerConnected(NetPeer peer);

        public abstract void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo);

        public abstract void OnNetworkError(IPEndPoint endPoint, SocketError socketError);

        public abstract void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod);

        public abstract void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader,
            UnconnectedMessageType messageType);

        public abstract void OnNetworkLatencyUpdate(NetPeer peer, int latency);

        public abstract void OnConnectionRequest(ConnectionRequest request);

        public abstract void ConfigureListeners();

        private void Update()
        {
            if (Initialized)
                NetManager?.PollEvents();
        }
    }
}
