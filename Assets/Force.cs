namespace X
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Force : MonoBehaviour
    {
        private const float G = 667.4f;
        private static readonly ICollection<Force> Forces = new List<Force>();

        public void OnEnable()
        {
            Forces.Add(this);
        }

        public void OnDisable()
        {
            Forces.Remove(this);
        }

        public void FixedUpdate()
        {
            foreach (var force in Forces)
            {
                if (!ReferenceEquals(force, this))
                {
                    this.Apply(force);
                }
            }
        }

        private void Apply(Force target)
        {
            var targetBody = target.GetComponent<Rigidbody>();
            var thisBody = this.GetComponent<Rigidbody>();

            var direction = thisBody.position - targetBody.position;
            var distance = direction.magnitude;

            if (distance == default)
            {
                return;
            }

            var forceMagnitude = G * (thisBody.mass * targetBody.mass) / Mathf.Pow(distance, 2);
            var force = direction.normalized * -forceMagnitude;

            targetBody.AddForce(force);
        }
    }
}
