using System;
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
        [Header("Character facing directino turn speed")]
        public float turnSpeed;

        [Space(20)]

        [Header("Jump related data")]
        public float jumpPower;
        public float airControl;
        public float gravityPower;
        public float gravityMultiplier;
    }
}