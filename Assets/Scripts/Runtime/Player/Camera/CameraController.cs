using Cinemachine;
using UnityEngine;
using System.Collections;
using System;

namespace RunTime.Cam
{
    public class CameraController : MonoSingleton<CameraController>
    {
        [Header("References")]
        [SerializeField] private CameraDataSO cameraData;
        [SerializeField] private CinemachineVirtualCamera standardCamera;
        [SerializeField] private CinemachineVirtualCamera aimingCamera;

        [Header("Transition Settings")]
        [SerializeField] private float transitionDuration = 0.3f;
        [SerializeField] private AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private GameObject _followTarget;
        private GameObject _lookAt;

        // Camera Settings
        private float _rotationPowerX;
        private float _rotationPowerY;
        private float _upLimit;
        private float _downLimit;

        // State
        private Vector2 _lookValue;
        private float _shakeTimer;
        private Coroutine _activeTransition;
        private bool _isTransitioning;

        private void Start()
        {
            InitializeCameras();
            SetCameraParameters(
                        cameraData.horizontalSensetivity,
                        cameraData.verticalSensetivity,
                        cameraData.downLimit,
                        cameraData.upLimit
                    );
        }

        private void InitializeCameras()
        {
            _followTarget = standardCamera.Follow.gameObject;
            _lookAt = standardCamera.LookAt.gameObject;
        }

        private void Update()
        {
            if (!_isTransitioning)
            {
                HandleCameraRotation();
            }
            HandleCameraShake();
        }

        private void HandleCameraRotation()
        {
            _followTarget.transform.rotation *= Quaternion.AngleAxis(
                _lookValue.x * _rotationPowerX,
                Vector3.up
            );

            _followTarget.transform.rotation *= Quaternion.AngleAxis(
                _lookValue.y * _rotationPowerY,
                Vector3.right
            );

            ClampCameraRotation();
        }

        private void ClampCameraRotation()
        {
            Vector3 angles = _followTarget.transform.localEulerAngles;
            angles.z = 0;

            // Convert to -180~180 range for accurate clamping
            float x = angles.x > 180 ? angles.x - 360 : angles.x;
            x = Mathf.Clamp(x, _upLimit, _downLimit);

            // Convert back to 0-360 range
            angles.x = x < 0 ? x + 360 : x;
            _followTarget.transform.localEulerAngles = angles;
        }

        private void HandleCameraShake()
        {
            if (_shakeTimer > 0)
            {
                _shakeTimer -= Time.deltaTime;
                return;
            }

            var perlin = standardCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlin.m_AmplitudeGain = 0;
        }

        private void SwitchCamera(CameraEnum state)
        {
            if (_activeTransition != null)
            {
                StopCoroutine(_activeTransition);
            }

            Quaternion currentRotation = _followTarget.transform.localRotation;
            UpdateCameraParameters(state);
            ClampCameraRotation();
            Quaternion targetRotation = _followTarget.transform.localRotation;

            _followTarget.transform.localRotation = currentRotation;
            _activeTransition = StartCoroutine(SmoothCameraTransition(targetRotation));
        }

        private IEnumerator SmoothCameraTransition(Quaternion targetRotation)
        {
            _isTransitioning = true;
            float elapsed = 0f;
            Quaternion startRotation = _followTarget.transform.localRotation;

            while (elapsed < transitionDuration)
            {
                elapsed += Time.deltaTime;
                float t = transitionCurve.Evaluate(elapsed / transitionDuration);

                _followTarget.transform.localRotation = Quaternion.Slerp(
                    startRotation,
                    targetRotation,
                    t
                );

                yield return null;
            }

            _followTarget.transform.localRotation = targetRotation;
            _isTransitioning = false;
        }

        private void UpdateCameraParameters(CameraEnum state)
        {
            switch (state)
            {
                case CameraEnum.Standard:
                    ToggleCameras(true, false);
                    SetCameraParameters(
                        cameraData.horizontalSensetivity,
                        cameraData.verticalSensetivity,
                        cameraData.downLimit,
                        cameraData.upLimit
                    );
                    break;

                case CameraEnum.Aim:
                    ToggleCameras(false, true);
                    SetCameraParameters(
                        cameraData.aimHorizontalSensetivity,
                        cameraData.aimVerticalSensitivty,
                        cameraData.aimDownLimit,
                        cameraData.aimUpLimit
                    );
                    break;
            }
        }

        private void ToggleCameras(bool standard, bool aim)
        {
            standardCamera.gameObject.SetActive(standard);
            aimingCamera.gameObject.SetActive(aim);
        }

        private void SetCameraParameters(float horizontal, float vertical, float upLimit, float downLimit)
        {
            _rotationPowerX = horizontal;
            _rotationPowerY = vertical;
            _upLimit = upLimit;
            _downLimit = downLimit;
        }

        private void LookUpdate(Vector2 value)
        {
            if (_isTransitioning)
            {
                return;
            }
            _lookValue = value;
        }

        public void ShakeCamera(float amplitude, float duration)
        {
            var perlin = standardCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlin.m_AmplitudeGain = amplitude;
            _shakeTimer = duration;
        }

        private void OnEnable()
        {
            InputSignals.Instance.OnInputeLookUpdate += LookUpdate;
            CameraSignals.Instance.OnSwitchCamera += SwitchCamera;
        }

        private void OnDisable()
        {
            if (InputSignals.Instance)
            { InputSignals.Instance.OnInputeLookUpdate -= LookUpdate; }
            if (CameraSignals.Instance)
            { CameraSignals.Instance.OnSwitchCamera -= SwitchCamera; }
        }
    }
}