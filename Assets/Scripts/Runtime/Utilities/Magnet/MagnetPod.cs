using System.Collections;
using System.Net;
using UnityEngine;

namespace RunTime
{
    public class MagnetPod : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform offset;
        [SerializeField] private MagnetPole magnetPole;

        private Vector3 targetPoint;
        private Vector3 controlPoint, midPoint;
        private float elapsedTime, travelTime;
        private AnimationCurve speedCurve;
        private bool shoot;

        public bool isLaunched;

        void Update()
        {
            if (!shoot) return;
            controlPoint = midPoint + offset.localPosition;
            if (elapsedTime < travelTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / travelTime;

                // Adjust time using the animation curve for dynamic speed
                float adjustedT = speedCurve.Evaluate(t);

                // Calculate position on the Bezier curve
                Vector3 position = CalculateBezierPoint(adjustedT, startPoint.position, controlPoint, targetPoint);
                transform.position = position;
            }
            else shoot = false;
        }

        Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            // Quadratic Bezier formula
            return (1 - t) * (1 - t) * p0 +
                   2 * (1 - t) * t * p1 +
                   t * t * p2;
        }

        public void SetTrajectory(Vector3 destination, AnimationCurve curve, float time)
        {
            targetPoint = destination;
            speedCurve = curve;
            travelTime = time;
            midPoint = (startPoint.position + targetPoint) / 2;
            controlPoint = midPoint + offset.localPosition;
            elapsedTime = 0f;
            shoot = true;
            isLaunched = true;
        }

        public void SetTrajectory()
        {
            Transform tempStart = startPoint;
            startPoint = transform;
            SetTrajectory(tempStart.position, speedCurve, travelTime);
            StartCoroutine(ResetStartPoint(tempStart));
        }

        IEnumerator ResetStartPoint(Transform temp)
        {
            yield return new WaitForSeconds(travelTime);
            startPoint = temp;
        }

        public void SwitchPoles()
        {
            magnetPole = magnetPole == MagnetPole.Red ? MagnetPole.Blue : MagnetPole.Red;
        }
    }
}