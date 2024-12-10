using RunTime.Input;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;

namespace RunTime.Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        public PlayerBaseState currentState;

        [Header("Movement Stats")]
        public float speed;

        [Header("Camera Controller")]
        [SerializeField] private float rotationPower = 3f;
        [SerializeField, Range(0, 180)] private float maxUpperRotationDegree;
        [SerializeField, Range(180, 360)] private float maxLowerRotationDegree;

        [Space(5)]

        [Header("Wheel Stats")]
        public float maxSpead;
        public float accelerationRate;
        public float turnRate;

        [SerializeField]
        private Animator _animator;

        public Rigidbody Rgbd { get; private set; }
        private StateFactory _states;
        public Camera Cam { get; private set; }

        public Vector2 MoveValue { get; private set; }
        private Vector2 _lookValue;

        public GameObject followTransform;
        public GameObject meshObject;

        private void Awake()
        {
            _states = new(this, _animator);
            Cam = Camera.main;
            Rgbd = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            currentState = _states.IdleState();
            currentState.Enter();
        }

        private void Update()
        {
            HandleCameraRotation();
            currentState.Execute();
        }

        private void FixedUpdate()
        {
            currentState.FixedExecute();
        }

        private void HandleCameraRotation()
        {
            followTransform.transform.rotation *= Quaternion.AngleAxis(_lookValue.x * rotationPower, Vector3.up);
            followTransform.transform.rotation *= Quaternion.AngleAxis(_lookValue.y * rotationPower, Vector3.right);

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.transform.localEulerAngles.x;

            if (angle > 180 && angle < maxLowerRotationDegree)
            {
                angles.x = maxLowerRotationDegree;
            }
            else if (angle < 180 && angle > maxUpperRotationDegree)
            {
                angles.x = maxUpperRotationDegree;
            }

            followTransform.transform.localEulerAngles = angles;
        }

        public void StopCharacter()
        {
            Rgbd.linearVelocity = Vector3.zero;
        }

        private void LookUpdate(Vector2 value)
        {
            _lookValue = value;
        }

        private void MoveUpdate(Vector2 value)
        {
            MoveValue = value;
        }

        private void OnEnable()
        {
            InputSignals.Instance.OnInputeLookUpdate += LookUpdate;
            InputSignals.Instance.OnInputMoveUpdate += MoveUpdate;
        }
    }
}