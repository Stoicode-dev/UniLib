using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using Stoicode.UniLib.Net;
using UnityEngine;

namespace Stoicode.UniLib.Ntp
{
    public class NetCoreServer : NetServer
    {
        private void Start()
        {
            Initialize(25565, 32, "TestKey");
        }
        
        public override void OnPeerConnected(NetPeer peer)
        {
            Debug.Log($"Peer ({peer.Id}) connected -> {peer.EndPoint.Address}");
        }

        public override void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Debug.Log($"Peer ({peer.Id}) disconnected -> {peer.EndPoint.Address}");
        }

        public override void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            Debug.Log($"Network error : 0x{socketError}");
        }

        public override void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            Processor.ReadAllPackets(reader, peer);
        }

        public override void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            
        }

        public override void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            
        }

        public override void OnConnectionRequest(ConnectionRequest request)
        {
            if (Manager.ConnectedPeersCount < MaxConnections)
                request.AcceptIfKey("TestKey");
            else
                request.Reject();
        }

        public override void ConfigureListeners()
        {
            Processor.SubscribeReusable<SpawnPackage, NetPeer>(SpawnPlayer);
            Processor.SubscribeReusable<MovePackage, NetPeer>(MovePlayer);
            Processor.SubscribeReusable<SimpleMovePackage, NetPeer>(SimpleMovePlayer);
        }

        public void SpawnPlayer(SpawnPackage package, NetPeer sender)
        {
            for (var index = 0; index < Manager.ConnectedPeerList.Count; index++)
            {
                var p = Manager.ConnectedPeerList[index];
                
                if (p.Id == sender.Id) continue;
                
                package.Id = sender.Id;
                Processor.Send(p, package, DeliveryMethod.ReliableOrdered);
            }
        }

        public void MovePlayer(MovePackage package, NetPeer sender)
        {
            for (var index = 0; index < Manager.ConnectedPeerList.Count; index++)
            {
                var p = Manager.ConnectedPeerList[index];
                
                if (p.Id == sender.Id) continue;
                
                package.Id = sender.Id;
                Processor.Send(p, package, DeliveryMethod.ReliableOrdered);
            }
        }

        public void SimpleMovePlayer(SimpleMovePackage package, NetPeer sender)
        {
            for (var index = 0; index < Manager.ConnectedPeerList.Count; index++)
            {
                var p = Manager.ConnectedPeerList[index];
                
                if (p.Id == sender.Id) continue;
                
                package.Id = sender.Id;
                Processor.Send(p, package, DeliveryMethod.ReliableOrdered);
            }
        }
    }
}