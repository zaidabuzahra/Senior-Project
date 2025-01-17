using UnityEditor;
using UnityEngine;

namespace RunTime
{
    public class MagnetPodManager : MonoSingleton<MagnetPodManager>
    {
        [SerializeField] private MagnetPod[] podList;
        [SerializeField] private AnimationCurve speedCurve;
        [SerializeField] private float travelTime = 1f;

        private int _podCounter;

        private void OnEnable()
        {
            InputSignals.Instance.OnInputRetrieveMagnetPods += RetrievePods;
        }

        void Start()
        {
            _podCounter = podList.Length;
        }

        public void LaunchPod(Vector3 destination)
        {
            if (_podCounter == 0) return;
            _podCounter--;
            podList[_podCounter].SetTrajectory(destination, speedCurve, travelTime);
        }


        public void RetrievePods()
        {
            _podCounter = podList.Length;
            for (int i = 0; i < _podCounter; i++)
            {
                podList[i].SetTrajectory();
            }
        }
    }
}