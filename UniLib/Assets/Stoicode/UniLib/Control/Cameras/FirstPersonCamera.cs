using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Control
{
    [RequireComponent(typeof(Camera))]
    public class FirstPersonCamera : MonoBehaviour
    {
        public float SensitivityX { get; set; }
        public float SensitivityY { get; set; }

        public bool Smooth { get; set; }
        public float SmoothTime { get; set; }

        private const float minX = -90f;
        private const float maxX = 90f;

        private Quaternion entityTargetRotation;
        private Quaternion camTargetRotation;

        private Transform entity;

        private bool initialized;


        public void Initialize(Transform entity)
        {
            this.entity = entity;
            entityTargetRotation = entity.localRotation;
            camTargetRotation = transform.localRotation;

            initialized = true;
        }

        private void Update()
        {
            if (!initialized)
                return;

            LookRotation();
        }

        private void LookRotation()
        {
            var rotX = Input.GetAxis("Mouse X") * SensitivityX;
            var rotY = Input.GetAxis("Mouse Y") * SensitivityY;

            entityTargetRotation *= Quaternion.Euler(0f, rotX, 0f);
            camTargetRotation *= Quaternion.Euler(-rotY, 0f, 0f);

            camTargetRotation = ClampRotationAroundXAxis(camTargetRotation);

            if (Smooth)
            {
                entity.localRotation = Quaternion.Slerp(entity.localRotation, 
                    entityTargetRotation, SmoothTime * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, 
                    camTargetRotation, SmoothTime * Time.deltaTime);
            }
            else
            {
                entity.localRotation = entityTargetRotation;
                transform.localRotation = camTargetRotation;
            }
        }

        private static Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            var angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, minX, maxX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}