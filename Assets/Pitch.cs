namespace X
{
    using UnityEngine;

    public class Pitch : MonoBehaviour
    {
        private void Update()
        {
            this.transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y"));
        }
    }
}
