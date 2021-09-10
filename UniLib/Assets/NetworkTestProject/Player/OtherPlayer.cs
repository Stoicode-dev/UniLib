using System;
using System.Collections.Generic;
using Stoicode.UniLib.Utilities;
using UnityEngine;

namespace Stoicode.UniLib.Ntp
{
    [RequireComponent(typeof(CharacterController))]
    public class OtherPlayer : MonoBehaviour
    {
        private CharacterController controller;
        
        private List<Vector3> positionCache;

        private TickExecutor moveExecutor;


        private void Start()
        {
            controller = GetComponent<CharacterController>();
            positionCache = new List<Vector3>();
            moveExecutor = new TickExecutor(120, Target);
            moveExecutor.Start();
        }

        private void Target()
        {
            if (positionCache.Count <= 0)
                return;
            
            var pos = positionCache[0];
            transform.position = pos;
            //controller.Move(pos);
            positionCache.RemoveAt(0);
        }

        public void AddToCache(List<Vector3> positions)
        {
            positionCache.AddRange(positions);
        }

        public void AddToCache(Vector3 position)
        {
            positionCache.Add(position);
        }
        
        private void Update()
        {
            moveExecutor.Update();
        }
    }
}