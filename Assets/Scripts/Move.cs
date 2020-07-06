namespace X
{
    using System;
    using UnityEngine;

    public class Move : MonoBehaviour
    {
        private const float originalSpeed = 4f;
        private const float speedIncrease = 1.02f;

        private float speed;
        private CharacterController controller;

        public void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            this.controller = this.GetComponent<CharacterController>();
        }

        public void Update()
        {
            Rotate();
            Displace();
        }

        private void Rotate()
        {
            this.transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y"), Space.Self);
            this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"), Space.Self);
        }

        private void Displace()
        {
            var longitudinalInput = Input.GetAxis("Vertical");
            var lateralInput = Input.GetAxis("Horizontal");
            var verticalInput = (Input.GetKey(KeyCode.Q) ? -1f : 0f) + (Input.GetKey(KeyCode.E) ? 1f : 0f);

            if (longitudinalInput == default && lateralInput == default && verticalInput == default)
            {
                this.speed = originalSpeed;
            }
            else
            {
                this.speed *= speedIncrease;
            }

            var logitudinalDirection = this.transform.forward * longitudinalInput;
            var lateralDirection = this.transform.right * lateralInput;
            var verticalDirection = this.transform.up * verticalInput;

            var direction = logitudinalDirection + lateralDirection + verticalDirection;

            this.controller.Move(direction * speed);
        }
    }
}
