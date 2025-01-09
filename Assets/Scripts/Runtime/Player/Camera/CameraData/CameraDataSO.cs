using UnityEngine;

namespace RunTime.Cam
{
    [CreateAssetMenu(fileName = "CameraDataSO", menuName = "Scriptable Objects/CameraDataSO")]
    public class CameraDataSO : ScriptableObject
    {
        [Header("Input senstivity")]
        public float horizontalSensetivity;
        public float verticalSensetivity;
        [Header("Input aim sensitivity")]
        public float aimHorizontalSensetivity;
        public float aimVerticalSensitivty;

        [Header("Camera rotation limits")]
        [Range(0, 180)]
        public float downLimit;
        [Range(180, 360)]
        public float upLimit;
        [Range(0, 180)]
        public float aimDownLimit;
        [Range(180, 360)]
        public float aimUpLimit;

    }
}
