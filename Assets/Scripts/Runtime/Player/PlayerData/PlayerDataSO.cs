using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RunTime
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Stats")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Movement data")]
        public float walkingSpeed;
        public float sprintSpeed;
        public float aimingSpeed;
        [InspectorLabel("Character facing directino turn speed")]
        public float turnSpeed;

        [Header("Dash data")]
        public float dashPower;
        public float dashCountdown;

        [Space(20)]

        [Header("Jump data")]
        public float verticalJumpPower;
        public float forwardJumpPower;
        public float airControl;
        public float gravityPower;
        public float groundedGravity;
        public float gravityMultiplier;
    }
}