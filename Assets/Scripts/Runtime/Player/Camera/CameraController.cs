using Cinemachine;
using Cinemachine.Editor;
using UnityEditor;
using UnityEngine;

namespace RunTime.Cam
{
    public class CameraController : MonoSingleton<CameraController>
    {
        [SerializeField] private CameraDataSO cameraData;

        [SerializeField] private CinemachineVirtualCamera standardCamera;
        [SerializeField] private CinemachineVirtualCamera aimingCamera;

        private GameObject _followTarget;
        private GameObject _lookAt;

        private float _rotationPowerX;
        private float _rotationPowerY;
        private float _upLimit;
        private float _downLimit;
        private Camera _cam;

        private Vector2 _lookValue;
        private float _shakeTimer;

        private void Start()
        {
            _cam = Camera.main;
            _followTarget = standardCamera.Follow.gameObject;
            _lookAt = standardCamera.LookAt.gameObject;

            _rotationPowerX = cameraData.horizontalSensetivity;
            _rotationPowerY = cameraData.verticalSensetivity;
            _upLimit = cameraData.downLimit;
            _downLimit = cameraData.upLimit;
        }

        private void Update()
        {
            HandleCameraRotation();
            HandleCameraShake();
        }

        private void HandleCameraRotation()
        {
            _followTarget.transform.rotation *= Quaternion.AngleAxis(_lookValue.x * _rotationPowerX, Vector3.up);
            _followTarget.transform.rotation *= Quaternion.AngleAxis(_lookValue.y * _rotationPowerY, Vector3.right);

            var angles = _followTarget.transform.localEulerAngles;
            angles.z = 0;

            var angle = _followTarget.transform.localEulerAngles.x;

            if (angle > 180 && angle < _downLimit)
            {
                angles.x = _downLimit;
            }
            else if (angle < 180 && angle > _upLimit)
            {
                angles.x = _upLimit;
            }

            _followTarget.transform.localEulerAngles = angles;
        }

        private void HandleCameraShake()
        {
            if (_shakeTimer <= 0)
            {
                standardCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                return;
            }
            _shakeTimer -= Time.deltaTime;
        }
        private void SwitchCamera(CameraEnum state)
        {
            switch (state)
            {
                case CameraEnum.Standard:
                    standardCamera.gameObject.SetActive(true);
                    aimingCamera.gameObject.SetActive(false);

                    _rotationPowerX = cameraData.horizontalSensetivity;
                    _rotationPowerY = cameraData.verticalSensetivity;
                    _upLimit = cameraData.downLimit;
                    _downLimit = cameraData.upLimit;
                    break;
                case CameraEnum.Aim:
                    aimingCamera.gameObject.SetActive(true);
                    standardCamera.gameObject.SetActive(false);

                    _rotationPowerX = cameraData.aimHorizontalSensetivity;
                    _rotationPowerY = cameraData.aimVerticalSensitivty;
                    _upLimit = cameraData.aimDownLimit;
                    _downLimit = cameraData.aimUpLimit;
                    break;
                default:
                    break;
            }
        }
        private void LookUpdate(Vector2 value)
        {
            _lookValue = value;
        }

        private void OnEnable()
        {
            InputSignals.Instance.OnInputeLookUpdate += LookUpdate;
            CameraSignals.Instance.OnSwitchCamera += SwitchCamera;
        }

        public void ShakeCamera(float amplitude, float timer)
        {
            standardCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
            _shakeTimer = timer;
        }
    }
}