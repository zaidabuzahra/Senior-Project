using Cinemachine;
using System.Text;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace RunTime.Player
{
    public class PlayerStateManager : MonoBehaviour
    {
        //public, seriliazed, private... in that order
        public PlayerDataSO playerData;
        public PlayerBaseState currentState; // make private
        public LayerMask layer; //fix and adjust
        [Header("Object References")]
        [SerializeField]
        private Animator _animator; //organize 

        public GameObject sphere;                 //align and fix
        public GameObject followObject;             //align and fix
        public GameObject meshObject;
        public LineRenderer lineRenderer;

        [Space(10)]

        [Header("Movement Stats")]
        public float speed; //organize

        [Space(10)]

        public Rigidbody Rgbd; //make public get and organize
        private StateFactory _states; //organize

        public Vector2 MoveValue { get; private set; } // input related 
        
        public bool canSwitchUtility = true;   // readjust and align
        public bool isAiming;                  // readjust and align
        public bool jumpPressed;
        public bool isSprinting;
        public bool isFalling;
        public float gravityMultiplier = 1f;
        public LayerMask layerMask;            // readjust and align
        public RaycastHit Hit;                 // readjust and align
        public Transform legTransform;  // organize

        public GameObject uiInstruction;       // UI related
        public GameObject aimedAtObject = null;// Aim related
        private void Awake()
        {
            _states = new(this, _animator);
        }

        private void Start()
        {
            currentState = _states.GroundedState();
            currentState.Enter();
        }

        private void Update()
        { 
            currentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateStates();
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
                    currentState.OnChangeState(_states.GroundedState());
                    break;
                default:
                    break;
            }
        }
        public void CalculatePlayerRotation(Camera cam, out Vector3 moveDir)
        {
            var camForward = cam.transform.forward;
            var camRight = cam.transform.right;
            camForward.y = camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            var forwardRelative = camForward * MoveValue.y;
            var rightRelative = camRight * MoveValue.x;
            moveDir = forwardRelative + rightRelative;
        }

        public bool Grounded()
        {
            bool grounded = Physics.CheckSphere(legTransform.position, legTransform.localScale.x, layer);
            Debug.DrawLine(transform.position, legTransform.position, Color.red);
            return grounded;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(legTransform.position, legTransform.localScale.x);
        }
        #region input functions
        private void SwitchToMagnet()
        {
            SwitchUtilities(UtilityType.Magnet);
        }

        private void SwitchToFullBody()
        {
            SwitchUtilities(UtilityType.FullBody);
        }

        private void PressAim()
        {
            isAiming = true;
        }

        private void ReleaseAim()
        {
            isAiming = false;
        }

        private void SwitchInstructionUI() // temporary for submissions only
        {
            bool state = uiInstruction.activeInHierarchy ? false : true;
            uiInstruction.SetActive(state);
        }

        private void PressJump()
        {
            jumpPressed = true;
        }

        private void ReleaseJump()
        {
            jumpPressed = false;
        }

        private void PressSprint()
        {
            isSprinting = true;
        }

        private void ReleaseSprint()
        {
            isSprinting = false;
        }

        private void OnEnable()
        {
            InputSignals.Instance.OnInputMoveUpdate += MoveUpdate;
            InputSignals.Instance.OnInputUtilityWheelOpen += SwitchToMagnet;
            InputSignals.Instance.OnInputUtilityWheelClose += SwitchToFullBody;
            InputSignals.Instance.OnInputAimPressed += PressAim;
            InputSignals.Instance.OnInputAimReleased += ReleaseAim;
            InputSignals.Instance.OnInputJumpPressed += PressJump;
            InputSignals.Instance.OnInputJumpReleased += ReleaseJump;
            InputSignals.Instance.OnInputSprintPressed += PressSprint;
            InputSignals.Instance.OnInputSprintReleased += ReleaseSprint;
        }
        #endregion
    }
}