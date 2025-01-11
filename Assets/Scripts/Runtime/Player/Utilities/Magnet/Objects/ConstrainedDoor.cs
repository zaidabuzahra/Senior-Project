using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace RunTime.Utilities.Magnet
{
    public class ConstrainedDoor : MonoBehaviour, IMagnetizable
    {
        [SerializeField] private MagnetPole pole;
        [SerializeField] private float forceStrength;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject highlightObject;
        [SerializeField] private MovementAxis movementAxis;

        private Vector3 _movementLine;

        private void Start()
        {
            switch (movementAxis)
            {
                case MovementAxis.X:
                    _movementLine = Vector3.right;
                    break;
                case MovementAxis.Y:
                    _movementLine = Vector3.up;
                    break;
                case MovementAxis.Z:
                    _movementLine = Vector3.forward;
                    break;
            }
        }

        public void Interact(Vector3 direction, MagnetPole magnetPole)
        {
            int dir;
            dir = magnetPole != pole ? 1 : -1;
            ApplyPower(dir);
        }

        public void HighlightTarget()
        {
            highlightObject.SetActive(true);
        }
        public void GrayoutTarget()
        {
            highlightObject.SetActive(false);
        }

        private void ApplyPower(int direction)
        {
            StopAllCoroutines();
            rb.isKinematic = false;
            rb.AddForce(_movementLine * (direction * forceStrength), ForceMode.Force);
            Debug.Log("Push");
            StartCoroutine(ResetKinematic());
        }

        IEnumerator ResetKinematic()
        {
            yield return new WaitForSeconds(0.5f);
            rb.isKinematic = true;
        }
    }

    public enum MovementAxis
    {
        X, Y, Z
    }
}