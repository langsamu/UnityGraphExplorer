namespace X
{
    using System;
    using UnityEngine;

    public class Move : MonoBehaviour
    {
        private const float speed = 4f;

        private CharacterController controller;
        private new Transform camera;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            this.controller = this.GetComponent<CharacterController>();
            this.camera = this.GetComponentInChildren<Camera>().transform;
        }

        void Update()
        {
            var logitudinal = this.camera.forward * Math.Sign(Input.GetAxis("Vertical"));
            var lateral = this.camera.right * Math.Sign(Input.GetAxis("Horizontal"));
            var direction = logitudinal + lateral;

            if (Input.GetKey(KeyCode.Q))
            {
                direction -= this.camera.up;
            }

            if (Input.GetKey(KeyCode.E))
            {
                direction += this.camera.up;
            }

            this.controller.Move(direction * speed);
        }
    }
}
