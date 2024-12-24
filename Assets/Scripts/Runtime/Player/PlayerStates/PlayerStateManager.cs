using Cinemachine;
using UnityEngine;

namespace RunTime.Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        public PlayerBaseState currentState;

        [Header("Object References")]
        [SerializeField]
        private Animator _animator;

        public GameObject followTransform, sphere;
        public GameObject meshObject;

        [Space(10)]

        [Header("Movement Stats")]
        [HideInInspector] public float speed;
        public float aimingSpeed;
        public float normalSpeed;
        [Range(0f, 10f)] 
        public float turnSpeed;

        [Space(10)]

        [Header("Camera Controller")]
        public float rotationPowerX = 3f;
        public float rotationPowerY = 3f;
        public float aimRotationPowerX = 3f;
        public float aimRotationPowerY = 3f;
        public float normalRotationPowerX = 3f;
        public float normalRotationPowerY = 3f;
        [SerializeField, Range(0, 180)] private float maxUpperRotationDegree;
        [SerializeField, Range(180, 360)] private float maxLowerRotationDegree;

        public CinemachineVirtualCamera aimVirtaulCamera;

        [Space(10)]

        [Header("Wheel Stats")]
        public float maxSpead;
        public float accelerationRate;
        public float turnRate;

        public Rigidbody Rgbd { get; private set; }
        private StateFactory _states;
        public Camera Cam { get; private set; }

        public Vector2 MoveValue { get; private set; }
        private Vector2 _lookValue;

        public bool canSwitchUtility = true;
        public bool isAiming;
        public LayerMask layerMask;
        public RaycastHit Hit;
        public GameObject aimedAtObject = null  ;

        private void Awake()
        {
            _states = new(this, _animator);
            Cam = Camera.main;
            Rgbd = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            currentState = _states.FullBodyState();
            currentState.Enter();
        }

        private void Update()
        {
            HandleCameraRotation();
            currentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateStates();
        }

        private void HandleCameraRotation()
        {
            followTransform.transform.rotation *= Quaternion.AngleAxis(_lookValue.x * rotationPowerX, Vector3.up);
            followTransform.transform.rotation *= Quaternion.AngleAxis(_lookValue.y * rotationPowerY, Vector3.right);

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

        private void LookUpdate(Vector2 value)
        {
            _lookValue = value;
        }

        private void MoveUpdate(Vector2 value)
        {
            MoveValue = value;
        }

        private void SwitchUtilities(UtilityType utility)
        {
            if (!canSwitchUtility) return;
            switch (utility)
            {
                case UtilityType.Magnet:
                    currentState.OnChangeState(_states.MagnetState());
                    break;
                case UtilityType.FullBody:
                    currentState.OnChangeState(_states.FullBodyState());
                    break;
            }
        }

        private void SwitchToMagnet()
        {
            SwitchUtilities(UtilityType.Magnet);
        }

        private void SwitchToFullBody()
        {
            SwitchUtilities(UtilityType.FullBody);
        }

        private void Aim()
        {
            isAiming = true;
        }

        private void UnAim()
        {
            isAiming = false;
        }

        private void OnEnable()
        {
            InputSignals.Instance.OnInputeLookUpdate += LookUpdate;
            InputSignals.Instance.OnInputMoveUpdate += MoveUpdate;
            InputSignals.Instance.OnInputUtilityWheelOpen += SwitchToMagnet;
            InputSignals.Instance.OnInputUtilityWheelClose += SwitchToFullBody;
            InputSignals.Instance.OnInputAimPressed += Aim;
            InputSignals.Instance.OnInputAimReleased += UnAim;
        }
    }
}