using RunTime.Cam;
using UnityEngine;

namespace RunTime.Puzzle.PillarAnomaly
{
    [RequireComponent(typeof(Animator))]
    public class AnomalyRockController : ResolvedEvent
    {
        [SerializeField] private Animator shockwave;
        private Animator _animator;
        private float _countDown = 5f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            _countDown -= Time.deltaTime;
            if (_countDown <= 0)
            {
                _animator.SetTrigger("Attack");
                shockwave.SetTrigger("Attack");
                CameraController.Instance.ShakeCamera(1f, 0.3f);
                _countDown = 5f;
            }
        }

        public override void Resolved()
        {
            _animator.SetBool("Done", true);
        }
    }
}
