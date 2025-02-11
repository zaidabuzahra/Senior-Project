using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace RunTime.Utilities.Magnet
{
    public class ConstrainedDoor : Magnetizable
    {
        public override void Interact(Vector3 playerPosition, MagnetPole magnetPole)
        {
            int dir;
            if (playerPosition.x - transform.position.x > 0)
            {
                dir = playerPosition.y - transform.position.y < 0 ? 1 : -1;
            }
            else
            {
                dir = playerPosition.y - transform.position.y > 0 ? 1 : -1;
            }
            dir *= magnetPole != pole ? 1 : -1;
            ApplyPower(dir);
        }

        private void ApplyPower(int direction)
        {
            StopAllCoroutines();
            rb.isKinematic = false;
            rb.AddForce(Vector3.right * (direction * forceStrength), ForceMode.Force);
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