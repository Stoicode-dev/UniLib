using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Control.Entity
{
    public class StepState
    {
        private float interval;
        private float cycle;
        private float next;


        public StepState(float interval)
        {
            SetInterval(interval);
        }

        public void SetInterval(float interval)
        {
            this.interval = interval;
        }

        public void Step(CharacterController controller, Vector2 input, float speed)
        {
            if (controller.velocity.sqrMagnitude > 0
                && (input.x >= 0f || input.x <= 0f || input.y >= 0f || input.y <= 0f))
            {
                cycle += (controller.velocity.magnitude + speed * 0.5f)
                         * Time.fixedDeltaTime;
            }

            if (!(cycle > next)) return;

            next = cycle + interval;
        }
    }
}