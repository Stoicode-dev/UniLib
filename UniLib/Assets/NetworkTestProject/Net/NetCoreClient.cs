using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using Stoicode.UniLib.Net;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Stoicode.UniLib.Ntp
{
    public class NetCoreClient : NetClient
    {
        public static NetCoreClient Instance;
        
        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject proxyObject;

        private Dictionary<int, OtherPlayer> Others;
        
        
        private void Start()
        {
            Instance = this;
            
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;
            
            Others = new Dictionary<int, OtherPlayer>();
            
            Initialize();
            Connect("127.0.0.1", 25565, "TestKey");
        }
        
        public override void OnPeerConnected(NetPeer peer)
        {
            ServerConnection = peer;
            Spawn();
        }

        public override void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            ServerConnection = null;
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
            
        }

        public override void ConfigureListeners()
        {
            Processor.SubscribeReusable<SpawnPackage>(SpawnOther);
            Processor.SubscribeReusable<MovePackage>(MoveOther);
            Processor.SubscribeReusable<SimpleMovePackage>(SimpleMoveOther);
        }

        public void Spawn()
        {
            var pos = new Vector3(16 + Random.Range(-4, 4), 2, -16 + Random.Range(-4, 4));
            Instantiate(playerObject, pos, Quaternion.identity);
            
            var package = new SpawnPackage(new [] {pos.x, pos.y, pos.z});
            Processor.Send(ServerConnection, package, DeliveryMethod.ReliableOrdered);
        }

        public void Move(List<Vector3> positions)
        {
            var package = new MovePackage(positions);
            Processor.Send(ServerConnection, package, DeliveryMethod.ReliableOrdered);
        }

        public void MoveSimple(Vector3 position)
        {
            var package = new SimpleMovePackage(position);
            Processor.Send(ServerConnection, package, DeliveryMethod.ReliableOrdered);
        }
        
        public void SpawnOther(SpawnPackage package)
        {
            var o= Instantiate(proxyObject, 
                new Vector3(package.Position[0], package.Position[1] + 0.5f, package.Position[2]), 
                Quaternion.identity);
            var other = o.GetComponent<OtherPlayer>();
            Others.Add(package.Id, other);
        }

        public void MoveOther(MovePackage package)
        {
            Others[package.Id].AddToCache(package.Get());
        }

        public void SimpleMoveOther(SimpleMovePackage package)
        {
            Others[package.Id].AddToCache(new Vector3(
                package.Position[0], package.Position[1], package.Position[2]));
        }
    }
}