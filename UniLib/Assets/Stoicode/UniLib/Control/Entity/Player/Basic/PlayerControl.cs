using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stoicode.UniLib.Control.Entity
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControl : MonoBehaviour
    {
        public bool Enabled { get; protected set; }

        private enum MoveState
        {
            Idle,
            Walk,
            Sprint
        }

        [SerializeField] private MoveState state;
        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;

        private CharacterController controller;

        private Vector2 inputVector;
        private Vector3 moveDirection;
        private CollisionFlags collisionFlags;

        private bool jumpNext;
        private bool inJump;


        /// <summary>
        /// Jump action
        /// </summary>
        public void Jump()
        {
            if (inJump)
                return;

            jumpNext = true;
        }


        /// <summary>
        /// Start event
        /// </summary>
        private void Start()
        {
            controller = GetComponent<CharacterController>();

            state = MoveState.Idle;
            Enabled = true;
            inputVector = new Vector2(0, 0);
        }

        /// <summary>
        /// Update event
        /// </summary>
        private void Update()
        {
            if (!Enabled) return;

            SetInput();
            SetState();
            Movement();
        }

        /// <summary>
        /// Detect & set movement input values
        /// </summary>
        private void SetInput()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            inputVector.x = h;
            inputVector.y = v;

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }

        /// <summary>
        /// Find proper movement state
        /// </summary>
        private void SetState()
        {
            if (!(inputVector.magnitude > 0f) && !(inputVector.magnitude < 0f))
            {
                state = MoveState.Idle;
                return;
            }

            state = Input.GetKeyDown(KeyCode.LeftShift) ? MoveState.Sprint : MoveState.Walk;
        }

        /// <summary>
        /// Movement logic
        /// </summary>
        private void Movement()
        {
            var move = CalculateMove();

            SetMoveDirection(move);
            Jumping();
            Move();
        }

        private Vector3 CalculateMove()
        {
            var t = transform;

            // Move along transform forward
            var move = t.forward * inputVector.y
                       + t.right * inputVector.x;

            // Get a normal for the surface to move along
            Physics.SphereCast(t.position, controller.radius, Vector3.down, out var normalHit,
                controller.height / 2, ~(1 << 10), QueryTriggerInteraction.Ignore);

            // Normalized plane projection
            move = Vector3.ProjectOnPlane(move, normalHit.normal).normalized;

            return move;
        }

        private void SetMoveDirection(Vector3 move)
        {
            moveDirection.x = move.x * speed;
            moveDirection.z = move.z * speed;

            // Gravity
            if (!controller.isGrounded)
                moveDirection += Physics.gravity;
        }

        private void Move()
        {
            collisionFlags = controller.Move(moveDirection * Time.deltaTime);
        }

        private void Jumping()
        {
            if (jumpNext && !inJump)
            {
                moveDirection.y = jumpPower;
                inJump = true;
                jumpNext = false;
            }
            else if (inJump && controller.isGrounded)
                inJump = false;
        }
    }
}
