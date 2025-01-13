using TreeEditor;
using UnityEngine;
using UnityEngine.Splines;

namespace RunTime.Utilities.Magnet
{
    public class SplineConstrainedPillar : Magnetizable
    {
        [SerializeField] private SplineAnimate splineAnimation;

        [Range(0f, 10f)]
        public float num;
        public bool check;
        public float time;

        private void Update()
        {
            if (check) { return; }
            splineAnimation.ElapsedTime = Mathf.Lerp(splineAnimation.ElapsedTime, num, Time.deltaTime);
        }

        public override void Interact(Vector3 direction, MagnetPole magnetPole)
        {
            int dir = 1;

            dir = magnetPole != pole ? 1 : -1;
            ApplyPower(dir);
        }

        private void ApplyPower(int dir)
        {
            splineAnimation.ElapsedTime = Mathf.Lerp(splineAnimation.ElapsedTime, 1, time);
        }
    }
}