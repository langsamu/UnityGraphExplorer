namespace X
{
    using System;
    using UnityEngine;
    using UnityEngine.Animations;

    public class Edge : MonoBehaviour
    {
        private const float width = 10f;
        private const float spring = 25f;
        private const float tolerance = 350f;
        private Transform line;
        private Transform text;

        internal GameObject Subject { get; set; }

        internal GameObject Object { get; set; }

        public void Start()
        {
            SetupConstraints();
            SetupComponents();
            SetupSpring();
        }

        public void Update()
        {
            ScaleLine();
            AlignText();
        }

        private void SetupConstraints()
        {
            var positionConstraint = this.GetComponent<PositionConstraint>();
            positionConstraint.SetSource(0, new ConstraintSource { sourceTransform = this.Subject.transform, weight = 1f });
            positionConstraint.SetSource(1, new ConstraintSource { sourceTransform = this.Object.transform, weight = 1f });

            var lookConstraint = this.GetComponent<LookAtConstraint>();
            lookConstraint.SetSource(0, new ConstraintSource { sourceTransform = this.Subject.transform, weight = 1f });
            lookConstraint.worldUpObject = Camera.main.transform;
        }

        private void SetupComponents()
        {
            this.line = this.transform.Find("Line");
            this.text = this.transform.transform.Find("Text");
        }

        private void SetupSpring()
        {
            var joint = this.Subject.AddComponent<SpringJoint>();
            joint.connectedBody = this.Object.GetComponent<Rigidbody>();
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = Vector3.zero;
            joint.connectedAnchor = Vector3.zero;
            joint.spring = spring;
            joint.tolerance = tolerance;
        }

        private void ScaleLine()
        {
            var from = this.Subject.transform.position;
            var to = this.Object.transform.position;
            var midpoint = (to - from) / 2f;
            var factor = midpoint.magnitude;

            // 100 is scale of node?
            this.line.localScale = new Vector3(width, factor, width) / 100f;
        }

        private void AlignText()
        {
            if (Vector3.Dot(this.text.up, Camera.main.transform.up) < 0f)
            {
                this.text.Rotate(Vector3.forward, 180f, Space.Self);
            }
        }
    }
}
