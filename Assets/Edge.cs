namespace X
{
    using UnityEngine;

    public class Edge : MonoBehaviour
    {
        private const float width = 10f;
        private const float spring = 50f;
        private const float tolerance = 250f;

        internal GameObject Subject { get; set; }

        internal GameObject Object { get; set; }

        public void Start()
        {
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

            var connector = to - from;
            var midpoint = connector * 0.5F;

            this.transform.position = from + midpoint;

            // 100 is scale of node?
            this.transform.localScale = new Vector3(width, midpoint.magnitude, width) / 100f;

            // 90d because original mesh doesn't point towards object rotation
            this.transform.rotation = Quaternion.LookRotation(connector.normalized, Vector3.right) * Quaternion.Euler(90, 0, 0);
        }
    }
}
