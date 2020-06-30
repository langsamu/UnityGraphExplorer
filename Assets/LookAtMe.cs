namespace X
{
    using UnityEngine;

    public class LookAtMe : MonoBehaviour
    {
        public void Update()
        {
            this.transform.LookAt(Camera.main.transform, Camera.main.transform.up);
        }
    }
}
