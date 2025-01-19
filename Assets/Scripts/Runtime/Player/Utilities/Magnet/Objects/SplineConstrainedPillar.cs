using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

namespace RunTime.Utilities.Magnet
{
    public class SplineConstrainedPillar : Magnetizable
    {
        [SerializeField] private Transform pillar;
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
            if (direction.x - pillar.position.x > 0)
            {
                //if (direction.x - transform.position.x < 0.1f) return;
                dir = direction.z - pillar.position.z < 0 ? _max : _min;
            }
            else
            {
                //if (direction.z - transform.position.z < 0.1f) return;
                dir = direction.z - pillar.position.z < 0 ? _max : _min;
            }
            //dir = magnetPole != pole ? _max : _min;
            if (magnetPole != pole)
            {
                dir = _max == dir ? _min : _max;
            }

            ApplyPower(dir, magnetPole);
        }

        private void ApplyPower(float dir, MagnetPole magnetPole)
        {
            if (splineAnimation.ElapsedTime < _min + tolerence && dir == _min) splineAnimation.ElapsedTime = _min + tolerence;
            if (splineAnimation.ElapsedTime > _max - tolerence && dir == _max) splineAnimation.ElapsedTime = _max - tolerence;
            splineAnimation.ElapsedTime = Mathf.Lerp(splineAnimation.ElapsedTime, dir, forceStrength * Time.deltaTime);
        }
    }
}