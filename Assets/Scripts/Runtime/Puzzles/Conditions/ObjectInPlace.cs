using DG.Tweening;
using UnityEngine;

namespace RunTime
{
    public class ObjectInPlace : MonoBehaviour, ICondition
    {
        public bool isDone;

        [SerializeField] private PuzzleController controller;
        [SerializeField] private GameObject target;
        [SerializeField] private Transform targetPosition;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float tolerence;
        [SerializeField] private Transform anamoly;

        private float _distance;

        private void Update()
        {
            if (Check() && !isDone)
            {
                isDone = true;
                controller.Notify();
            }
        }

        public bool Check()
        {
            _distance = Vector3.Distance(transform.position.normalized, targetPosition.position.normalized);
            UpdateLine();
            if (_distance < tolerence)
            {
                Debug.Log(transform.parent.name + "DONE");
                return true;
            }
            isDone = false;
            return false;
        }

        private void UpdateLine()
        {
            lineRenderer.SetPosition(0, target.transform.position);
            lineRenderer.SetPosition(1, anamoly.position);
            lineRenderer.material.SetFloat("_Red", Mathf.Lerp(0, 1, (Mathf.Abs(_distance - 1) / 1) * 100 * Time.deltaTime));
            lineRenderer.material.SetFloat("_Green", Mathf.Lerp(1, 0, (Mathf.Abs(_distance - 1) / 1) * 100 * Time.deltaTime));
            //lineRenderer.material.color = Color.Lerp(Color.red, Color.green, (Mathf.Abs(_distance -1) / 1));
            //lineRenderer.DOColor(new Color2(Color.red, Color.red), new Color2(Color.green, Color.green), (Mathf.Abs(_distance - 1) / 1));
        }
    }
}