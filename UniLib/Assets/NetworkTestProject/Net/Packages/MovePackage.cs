using System.Collections.Generic;
using LiteNetLib.Utils;
using MessagePack;
using UnityEngine;

namespace Stoicode.UniLib.Ntp
{
    public class MovePackage
    {
        public int Id { get; set; }
        public byte[] Positions { get; set; }
        
        
        public MovePackage() { }

        public MovePackage(List<Vector3> positions)
        {
            Positions = MessagePackSerializer.Serialize(positions);
        }

        public List<Vector3> Get()
        {
            return MessagePackSerializer.Deserialize<List<Vector3>>(Positions);
        }
    }
}