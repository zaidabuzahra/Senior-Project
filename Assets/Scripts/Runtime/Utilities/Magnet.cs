using System.Collections;
using UnityEngine;

namespace RunTime
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private GameObject magnetColor;
        [SerializeField] private Material red, blue;
        private MagnetPole currentPole = MagnetPole.Red;

        private void OnEnable()
        {
            InputSignals.Instance.OnInputUseUtilityPressed = UseUtility;
            InputSignals.Instance.OnInputJumpPressed += FlipUtility;
        }

        private void UseUtility()
        {

        }

        private void FlipUtility()
        {
            currentPole = currentPole == MagnetPole.Red ? MagnetPole.Blue : MagnetPole.Red;
            magnetColor.GetComponent<MeshRenderer>().material = currentPole == MagnetPole.Red ? red : blue;
        }

        //Deleteable code
        public Vector3 direction;
        public float forceStrength;
        public Rigidbody rb;
        public Transform playerPosition;

        private void Update()
        {
            direction = (rb.gameObject.transform.position - playerPosition.position).normalized;
        }

        public void Pull()
        {
            StopAllCoroutines();
            GetComponent<Rigidbody>().isKinematic = false;
            direction = (playerPosition.position - rb.gameObject.transform.position).normalized;
            rb.AddForce(direction * forceStrength, ForceMode.Force);
            Debug.Log("Pull");
            StartCoroutine(ResetKinematic());

        }

        public void Push()
        {
            StopAllCoroutines();
            GetComponent<Rigidbody>().isKinematic = false;
            rb.AddForce(direction * forceStrength, ForceMode.Force);
            Debug.Log("Push");
            StartCoroutine(ResetKinematic());
        }

        IEnumerator ResetKinematic()
        {
            yield return new WaitForSeconds(0.5f);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}