using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RunTime
{
    public class WheelManager : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Buttons
        };

        public enum Axel
        {
            Front,
            Rear
        }

        [Serializable]
        public struct Wheel
        {
            public GameObject wheelModel;
            public WheelCollider wheelCollider;
            public Axel axel;
        }

        public Vector2 MoveValue;
        public ControlMode control;

        public float speed = 600f;
        public float maxAcceleration = 30.0f;
        public float brakeAcceleration = 50.0f;

        public float turnSensitivity = 1.0f;
        public float maxSteerAngle = 30.0f;

        public Vector3 _centerOfMass;

        public List<Wheel> wheels;

        float moveInput;
        float steerInput;

        private Rigidbody carRb;

        void Start()
        {
            carRb = GetComponent<Rigidbody>();
            carRb.centerOfMass = _centerOfMass;
        }

        void Update()
        {
            GetInputs();
            AnimateWheels();
        }

        void LateUpdate()
        {
            Move();
            Steer();
        }

        public void MoveInput(float input)
        {
            moveInput = input;
        }

        public void SteerInput(float input)
        {
            steerInput = input;
        }

        void GetInputs()
        {
            if (control == ControlMode.Keyboard)
            {
                moveInput = MoveValue.y;
                steerInput = MoveValue.x;
            }
        }

        void Move()
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = moveInput * speed * maxAcceleration * Time.fixedDeltaTime;
                if (moveInput == 0)
                {
                    wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration;
                }
                else
                {
                    wheel.wheelCollider.brakeTorque = 0;
                }
            }
        }

        void Steer()
        {
            foreach (var wheel in wheels)
            {
                if (wheel.axel == Axel.Front)
                {
                    var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                    wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
                }
            }
        }

        void AnimateWheels()
        {
            foreach (var wheel in wheels)
            {
                Quaternion rot;
                Vector3 pos;
                wheel.wheelCollider.GetWorldPose(out pos, out rot);
                wheel.wheelModel.transform.position = pos;
                wheel.wheelModel.transform.rotation = rot;
            }
        }

        private void MoveUpdate(Vector2 value)
        {
            MoveValue = value;
        }

        private void OnEnable()
        {
            InputSignals.Instance.OnInputMoveUpdate += MoveUpdate;
        }
    }
}