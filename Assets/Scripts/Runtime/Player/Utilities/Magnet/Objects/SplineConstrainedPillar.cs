using TreeEditor;
using UnityEngine;
using UnityEngine.Splines;

namespace RunTime.Utilities.Magnet
{
    [RequireComponent(typeof(SplineAnimate))]
    public class SplineConstrainedPillar : MonoBehaviour, IMagnetizable
    {
        [SerializeField] private MagnetPole pole;
        [SerializeField] private float forceStrength;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject highlightObject;

        private SplineAnimate splineAnimation;

        public void Interact(Vector3 direction, MagnetPole magnetPole)
        {
            int dir = 1;

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

        private void ApplyPower(int dir)
        {
            splineAnimation.ElapsedTime = 1f;
        }
    }
}