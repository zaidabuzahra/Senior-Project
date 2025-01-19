using UnityEngine;

namespace RunTime
{
    public class WindyCollision : MonoBehaviour
    {
        public float force;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Windy"))
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.forward * -force);
            }
            else if (other.gameObject.CompareTag("Animation"))
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("Activate");
            }
            Debug.Log(other.tag);
        }
    }
}