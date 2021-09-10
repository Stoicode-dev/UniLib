using LiteNetLib.Utils;
using UnityEngine;

namespace Stoicode.UniLib.Ntp
{
    public class SpawnPackage
    {
        public int Id { get; set; }
        
        public float[] Position { get; set; }

        
        public SpawnPackage() { }

        public SpawnPackage(float[] position)
        {
            Position = position;
        }
    }
}