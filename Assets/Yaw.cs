namespace X
{
    using UnityEngine;

    public class Yaw : MonoBehaviour
    {
        private void Update()
        {
            this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
        }
    }
}
