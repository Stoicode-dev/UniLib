using UnityEngine;

namespace Stoicode.UniLib.Ntp
{
    public class SimpleMovePackage
    {
        public int Id { get; set; }
        public float[] Position { get; set; }

        public SimpleMovePackage()
        {
            
        }

        public SimpleMovePackage(Vector3 position)
        {
            Position = new[] { position.x, position.y, position.z };
        }
    }
}