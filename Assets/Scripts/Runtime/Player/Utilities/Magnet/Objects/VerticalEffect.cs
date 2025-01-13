using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace RunTime.Utilities.Magnet
{
    public class VerticalEffect : Magnetizable
    {
        [SerializeField] private Transform[] points;
        private float _movementPosition;

        private void Update()
        {
            _movementPosition = transform.localPosition.y;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(_movementPosition, points[0].localPosition.y, points[1].localPosition.y), transform.localPosition.z);
        }

        public override void Interact(Vector3 playerPosition, MagnetPole magnetPole)
        {
            int dir = playerPosition.y - transform.position.y > 0 ? 1 : -1;
            dir *= magnetPole != pole ? 1 : -1;
            ApplyPower(dir);
        }

        private void ApplyPower(int direction)
        {
            StopAllCoroutines();
            rb.isKinematic = false;
            rb.AddForce(Vector3.up * (direction * forceStrength), ForceMode.Force);
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