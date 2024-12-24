using TMPro;
using Unity.VisualScripting;
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
            InputSignals.Instance.OnInputRetrieveMagnetPodsPressed += RetrievePods;
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

    [CustomEditor(typeof(MagnetPodManager))]
    public class ColliderCreatorEditor : Editor
    {

        // some declaration missing??

        override public void OnInspectorGUI()
        {
            MagnetPodManager colliderCreator = (MagnetPodManager)target;
            if (GUILayout.Button("End"))
            {
                colliderCreator.RetrievePods(); // how do i call this?
            }
            DrawDefaultInspector();
        }
    }
}