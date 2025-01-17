using UnityEngine;

namespace RunTime
{
    public class Spideranimationtrigger : MonoBehaviour
    {
        public Animator anim;
        private bool hasActivated = false;

       

        private void OnTriggerEnter(Collider other)
        {
            if (!hasActivated && CompareTag("Player"))
            {
                hasActivated = true;
                anim.SetTrigger("SpiderActive");
            }
        }

    }
}
