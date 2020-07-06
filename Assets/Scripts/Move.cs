namespace X
{
    using System;
    using UnityEngine;

    public class Move : MonoBehaviour
    {
        private const float speed = 4f;

        private CharacterController controller;

        public void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            this.controller = this.GetComponent<CharacterController>();
        }

        public void Update()
        {
            this.transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y"));
            this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"), Space.Self);
          
            var logitudinal = this.transform.forward * Math.Sign(Input.GetAxis("Vertical"));
            var lateral = this.transform.right * Math.Sign(Input.GetAxis("Horizontal"));
            var direction = logitudinal + lateral;

            if (Input.GetKey(KeyCode.Q))
            {
                direction -= this.transform.up;
            }

            if (Input.GetKey(KeyCode.E))
            {
                direction += this.transform.up;
            }

            this.controller.Move(direction * speed);
        }
    }
}
