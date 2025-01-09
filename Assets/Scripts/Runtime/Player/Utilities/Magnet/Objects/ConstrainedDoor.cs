using System.Collections;
using UnityEngine;

namespace RunTime
{
    public class ConstrainedDoor : MonoBehaviour, IMagnetizable
    {
        [SerializeField] private MagnetPole pole;
        [SerializeField] private float forceStrength;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private GameObject highlightObject;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Interact(Vector3 direction, MagnetPole magnetPole)
        {
            direction = magnetPole != pole ? (direction - rb.transform.position).normalized : (rb.transform.position - direction).normalized;
            ApplyPower(direction);
        }

        public void HighlightTarget()
        {
            highlightObject.SetActive(true);
        }
        public void GrayoutTarget()
        {
            highlightObject.SetActive(false);
        }

        private void ApplyPower(Vector3 direction)
        {
            StopAllCoroutines();
            rb.isKinematic = false;
            rb.AddForce(direction * forceStrength, ForceMode.Force);
            Debug.Log("Push");
            StartCoroutine(ResetKinematic());
        }

        IEnumerator ResetKinematic()
        {
            yield return new WaitForSeconds(0.5f);
            rb.isKinematic = true;
        }
    }
}