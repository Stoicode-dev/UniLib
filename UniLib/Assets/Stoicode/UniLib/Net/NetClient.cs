using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;

namespace Stoicode.UniLib.Net
{
    public abstract class NetClient : MonoBehaviour, INetEventListener
    {
        public NetManager NetManager { get; set; }
        public NetPacketProcessor NetProcessor { get; set; }

        public NetPeer ServerConnection { get; set; }

        public bool Initialized { get; set; }


        public void Initialize()
        {
            NetManager = new NetManager(this);
            NetProcessor = new NetPacketProcessor();

            ConfigureListeners();
        }

        public void Connect(string address, int port, string key)
        {
            NetManager.Start();
            NetManager.Connect(address, port, key);

            Initialized = true;
        }

        public void Disconnect()
        {
            NetManager.Stop();
        }

        public abstract void OnPeerConnected(NetPeer peer);

        public abstract void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo);

        public abstract void OnNetworkError(IPEndPoint endPoint, SocketError socketError);

        public abstract void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod);

        public abstract void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType);

        public abstract void OnNetworkLatencyUpdate(NetPeer peer, int latency);

        public abstract void OnConnectionRequest(ConnectionRequest request);

        public abstract void ConfigureListeners();

        private void Update()
        {
            if (Initialized)
                NetManager.PollEvents();
        }
    }
}
