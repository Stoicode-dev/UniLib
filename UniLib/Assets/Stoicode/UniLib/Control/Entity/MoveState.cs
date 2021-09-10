using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Control.Entity
{
    public class MoveState
    {
        public const byte Idle = 0;
        public const byte Walk = 1;
        public const byte Run = 2;
        public const byte Sprint = 3;
        public const byte Crouch = 4;
        public const byte Prone = 5;
        public const byte Slide = 6;

        public byte State { get; set; }

        public bool Grounded { get; set; }
        public bool Jumping { get; set; }


        public MoveState(float speedWalk, float speedRun, float speedSprint, float speedCrouch)
        {
            State = Idle;

            Grounded = false;
            Jumping = false;
        }
    }
}