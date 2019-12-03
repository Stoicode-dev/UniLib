using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Control
{
    [RequireComponent(typeof(Camera))]
    public class ThirdPersonAltCamera : MonoBehaviour
    {
        private Transform player;

        private float distance;

        private float rotX;
        private float rotY;

        private bool mouseMode;


        public void Initialize(Transform player, float distance)
        {
            this.player = player;
            this.distance = distance;
        }

        public void SetDistance(float value)
        {
            distance -= value;

            if (distance < 0.2f)
                distance = 0.2f;
        }

        public void SetMouseMode(bool status)
        {
            mouseMode = status;
        }

        public void RotationX(float dir)
        {
            rotX += dir;
            if (Math.Abs(transform.eulerAngles.y) < 0f)
                rotX = 0;
        }

        public void RotationY(float dir)
        {
            if (transform.eulerAngles.x <= 1f && dir < 0f)
                return;
            if (transform.eulerAngles.x >= 88f && dir > 0f)
                return;

            rotY += dir;
        }

        private void LateUpdate()
        {
            if (!player)
                return;

            if (mouseMode)
            {
                var mx = Input.GetAxis("Mouse X");
                var my = Input.GetAxis("Mouse Y");

                if (mx > 0f)
                    RotationX(60f);
                if (mx < 0f)
                    RotationX(-60f);

                if (my > 0f)
                    RotationY(-40f);
                if (my < 0f)
                    RotationY(40f);
            }

            var rotation = Quaternion.Euler(rotY * .01f, -rotX * -.01f, 0);

            transform.position = player.position - rotation * (Vector3.forward * distance);
            transform.rotation = rotation;
        }
    }
}