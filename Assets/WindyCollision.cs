using UnityEngine;

namespace RunTime
{
    public class WindyCollision : MonoBehaviour
    {
        public Vector3 force;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Windy"))
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(force);
            }
        }
    }
}
