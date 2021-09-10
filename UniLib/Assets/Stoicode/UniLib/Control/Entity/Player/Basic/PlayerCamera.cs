using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Control.Entity
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform entity;

        [SerializeField] private float sensitivityX = 1f;
        [SerializeField] private float sensitivityY = 1f;

        [SerializeField] private bool smooth;
        [SerializeField] private float smoothTime = 0.2f;

        private const float minX = -90f;
        private const float maxX = 90f;

        private Quaternion entityTargetRotation;
        private Quaternion camTargetRotation;

        private float rotX;
        private float rotY;


        private void Start()
        {
            entityTargetRotation = entity.localRotation;
            camTargetRotation = transform.localRotation;
        }

        private void Update()
        {
            GetInput();
            LookRotation();
        }

        /// <summary>
        /// Camera rotation
        /// </summary>
        private void LookRotation()
        {
            entityTargetRotation *= Quaternion.Euler(0f, rotX, 0f);
            camTargetRotation *= Quaternion.Euler(-rotY, 0f, 0f);

            camTargetRotation = ClampRotationXAxis(camTargetRotation);

            if (smooth)
                Smooth();
            else
            {
                entity.localRotation = entityTargetRotation;
                transform.localRotation = camTargetRotation;
            }
        }

        /// <summary>
        /// Get rotation input
        /// </summary>
        private void GetInput()
        {
            rotX = Input.GetAxis("Mouse X") * sensitivityX;
            rotY = Input.GetAxis("Mouse Y") * sensitivityY;
        }

        /// <summary>
        /// Smoothing
        /// </summary>
        private void Smooth()
        {
            entity.localRotation = Quaternion.Slerp(entity.localRotation,
                entityTargetRotation, smoothTime * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation,
                camTargetRotation, smoothTime * Time.deltaTime);
        }

        /// <summary>
        /// Clamp rotation around X axis
        /// </summary>
        /// <param name="q">Target quaternion</param>
        /// <returns>Clamped rotation</returns>
        private static Quaternion ClampRotationXAxis(Quaternion q)
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