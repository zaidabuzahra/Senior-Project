using Unity.VisualScripting;
using UnityEngine;

namespace RunTime
{
    public class Spideranimationtrigger : MonoBehaviour
    {
        public Animator anim;
        private bool hasActivated = false;

       

        private void OnTriggerEnter(Collider other)
        {
            if (!hasActivated && other.CompareTag("Player"))
            {
                Debug.LogWarning("ENTERED");
                hasActivated = true;
                anim.SetTrigger("SpiderActive");
            }
        }
    }
}
