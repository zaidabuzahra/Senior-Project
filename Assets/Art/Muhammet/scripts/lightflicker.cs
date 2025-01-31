using System.Threading;
using UnityEngine;

namespace RunTime
{
    public class lightflicker : MonoBehaviour
    {
        public Light lightOB;
        public float minTime;
        public float maxTime;
        public float timer; 
        void Start()
        {
            timer = Random.Range(minTime, maxTime);
        }

        void Update()
        {
            lightFlickering();
        }

        void lightFlickering ()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if (timer <= 0)
            {
                lightOB.enabled = !lightOB.enabled;
                timer = Random.Range(minTime, maxTime);
            }
        }
    }
}
