namespace X
{
    using System;
    using UnityEngine;

    public class Edge : MonoBehaviour
    {
        private const float width = 10f;
        private const float spring = 50f;
        private const float tolerance = 250f;
        private Transform line;
        private Transform orientLine;
        private Transform orientText;

        internal GameObject Subject { get; set; }

        internal GameObject Object { get; set; }

        public void Start()
        {
            this.orientLine = this.transform.Find("Orient line");
            this.line = this.orientLine.Find("Line");
            this.orientText = this.transform.Find("Orient text");

            var joint = this.Subject.AddComponent<SpringJoint>();
            joint.connectedBody = this.Object.GetComponent<Rigidbody>();
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = Vector3.zero;
            joint.connectedAnchor = Vector3.zero;
            joint.spring = spring;
            joint.tolerance = tolerance;
        }

        public void FixedUpdate()
        {
            var from = this.Subject.transform.position;
            var to = this.Object.transform.position;

            var difference = to - from;
            var midpoint = difference / 2f;

            this.transform.position = from + midpoint;

            // 100 is scale of node?
            this.line.localScale = new Vector3(width, midpoint.magnitude, width) / 100f;

            this.orientLine.rotation = Quaternion.LookRotation(difference.normalized);
        }

        public void Update()
        {
            var angleBetweenLineAndCamera = Vector3.SignedAngle(
                this.line.up,
                Camera.main.transform.up,
                Camera.main.transform.forward);

            var angleDirection = Math.Sign(angleBetweenLineAndCamera);

            // align with edge line, adjust so text is not upside down
            var up = this.line.up * angleDirection;

            this.orientText.LookAt(Camera.main.transform, up);
        }
    }
}
