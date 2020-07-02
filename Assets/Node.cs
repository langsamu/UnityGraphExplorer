namespace X
{
    using UnityEngine;
    using UnityEngine.Animations;

    public class Node : MonoBehaviour
    {
        public void Start()
        {
            var aimConstraint = this.GetComponent<AimConstraint>();
            aimConstraint.SetSource(0, new ConstraintSource { sourceTransform = Camera.main.transform, weight = 1f });
            aimConstraint.worldUpObject = Camera.main.transform;
        }
    }
}
