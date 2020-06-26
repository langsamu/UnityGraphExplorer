namespace X
{
    using UnityEngine;

    public class Edge : MonoBehaviour
    {
        private LineRenderer line;

        internal GameObject Subject { get; set; }

        internal GameObject Object { get; set; }

        public void Start()
        {
            this.line = this.gameObject.AddComponent<LineRenderer>();

            var joint = this.Subject.AddComponent<SpringJoint>();
            joint.spring = 10000f;
            joint.connectedBody = this.Object.GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            //if (this.line is object)
            //{
                this.line.SetPosition(0, this.Subject.transform.position);
                this.line.SetPosition(1, this.Object.transform.position);
            //}
        }
    }
}
