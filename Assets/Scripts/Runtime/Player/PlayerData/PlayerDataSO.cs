using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RunTime
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Stats")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Speed related data")]
        public float speed;
        public float sprintSpeed;
        public float aimingSpeed;
        [InspectorLabel("Character facing directino turn speed")]
        public float turnSpeed;

        [Header("Step up stairs")]
        public float stepHeight = 0.3f;
        public float stepCheckDistance = 0.5f;
        public float stepSmooth = 5f;

        [Space(20)]

        [Header("Jump related data")]
        public float verticalJumpPower;
        public float forwardJumpPower;
        public float airControl;
        public float gravityPower;
        public float gravityMultiplier;
    }
}