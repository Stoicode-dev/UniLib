using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using Stoicode.UniLib.Ntp;
using UnityEngine;

namespace Stoicode.UniLib.Net
{
    public abstract class NetClient : MonoBehaviour, INetEventListener
    {
        public NetManager Manager { get; protected set; }
        public NetPacketProcessor Processor { get; protected set; }

        public NetPeer ServerConnection { get; set; }

        public bool Initialized { get; set; }


        public void Initialize()
        {
            Manager = new NetManager(this);
            Processor = new NetPacketProcessor();

            ConfigureListeners();
        }

        public void Connect(string address, int port, string key)
        {
            Manager.Start();
            Manager.Connect(address, port, key);

            Initialized = true;
        }

        public void Disconnect()
        {
            Manager.Stop();
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
                Manager.PollEvents();
        }
    }
}