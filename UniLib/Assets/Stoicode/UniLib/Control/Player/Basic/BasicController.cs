using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Control.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class BasicController : MonoBehaviour
    {
        public bool Enabled { get; set; }

        public enum MoveState
        {
            Idle, Walk, Sprint
        }
        public MoveState State { get; protected set; }

        public bool InJump { get; protected set; }

        private CharacterController controller;

        private Vector2 inputVector;
        private Vector3 moveDirection;
        private CollisionFlags collisionFlags;

        private bool jumpNext;

        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;


        public void Jump()
        {
            if (InJump)
                return;

            jumpNext = true;
        }

        private void Start()
        {
            controller = GetComponent<CharacterController>();

            State = MoveState.Idle;
            Enabled = true;
            inputVector = new Vector2(0, 0);
        }

        private void Update()
        {
            if (!Enabled) return;

            SetInput();
            SetState();
            Move();
        }

        private void SetInput()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            inputVector.x = h;
            inputVector.y = v;

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }

        private void SetState()
        {
            if (!(inputVector.magnitude > 0f) && !(inputVector.magnitude < 0f))
            {
                State = MoveState.Idle;
                return;
            }

            State = Input.GetKeyDown(KeyCode.LeftShift) ? 
                MoveState.Sprint : MoveState.Walk;

        }

        private void Move()
        {
            // Move along transform forward
            var move = transform.forward * inputVector.y
                       + transform.right * inputVector.x;

            // Get a normal for the surface to move along
            Physics.SphereCast(transform.position, controller.radius, Vector3.down, out var normalHit,
                controller.height / 2, ~(1 << 10), QueryTriggerInteraction.Ignore);

            // Normalized plane projection
            move = Vector3.ProjectOnPlane(move, normalHit.normal).normalized;

            // Move direction calculation
            moveDirection.x = move.x * speed;
            moveDirection.z = move.z * speed;

            // Jumping
            if (jumpNext && !InJump)
            {
                moveDirection.y = jumpPower;
                InJump = true;
                jumpNext = false;
            }
            else if (InJump && controller.isGrounded)
                InJump = false;

            // Gravity
            if (!controller.isGrounded)
                moveDirection += Physics.gravity * Time.deltaTime;

            // Move controller
            collisionFlags = controller.Move(moveDirection * Time.deltaTime);
        }
    }
}