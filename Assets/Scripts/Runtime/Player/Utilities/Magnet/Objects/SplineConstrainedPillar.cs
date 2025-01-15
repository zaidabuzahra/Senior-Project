using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

namespace RunTime.Utilities.Magnet
{
    public class SplineConstrainedPillar : Magnetizable
    {
        [SerializeField] private SplineAnimate splineAnimation;
        [SerializeField] private float tolerence = 0.5f;
        private float _max;
        private float _min;

        private void Start()
        {
            _max = splineAnimation.Duration + 5f;
            _min = -5f;
        }

        public override void Interact(Vector3 direction, MagnetPole magnetPole)
        {
            //FirstInteraction();
            float dir;
            if (direction.x - transform.position.x > 0)
            {
                if (direction.x - transform.position.x < 0.1f) return;
                dir = direction.y - transform.position.y < 0 ? _max : _min;
            }
            else
            {
                if (direction.y - transform.position.y < 0.1f) return;
                dir = direction.y - transform.position.y > 0 ? _max : _min;
            }
            //dir = magnetPole != pole ? _max : _min;
            if (magnetPole != pole)
            {
                dir = _max == dir ? _min : _max;
            }

            ApplyPower(dir);
        }

        private void ApplyPower(float dir)
        {
            Debug.Log(splineAnimation.ElapsedTime + " " + dir);
            if (splineAnimation.ElapsedTime < _min + tolerence && dir == _min) splineAnimation.ElapsedTime = _min + tolerence;
            if (splineAnimation.ElapsedTime > _max - tolerence && dir == _max) splineAnimation.ElapsedTime = _max - tolerence;
            splineAnimation.ElapsedTime = Mathf.Lerp(splineAnimation.ElapsedTime, dir, forceStrength * Time.deltaTime);
        }
    }
}