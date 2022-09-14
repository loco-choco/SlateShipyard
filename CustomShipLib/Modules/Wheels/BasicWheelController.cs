using UnityEngine;

namespace SlateShipyard.Modules.Wheels
{
    public class BasicWheelController : MonoBehaviour
    {

        public OWSimpleRaycastWheel frontRWheel;
        public OWSimpleRaycastWheel frontLWheel;
        public OWRigidbody body;

        public float maxSteerAngle = 30f;

        public float maxAccelerationForce;

        private float normalRFrictionCoeficient;
        private float normalLFrictionCoeficient;

        private void Start()
        {
            normalRFrictionCoeficient = frontRWheel.frictionCoeficient;
            normalLFrictionCoeficient = frontLWheel.frictionCoeficient;
        }
        private void WheelMotorInput(OWSimpleRaycastWheel obj, float frictionCoeficient)
        {
            if (!obj.IsOnGround())
                return;

            float input = OWInput.GetValue(InputLibrary.thrustZ, InputMode.All);

            float force = maxAccelerationForce * input;

            body.AddForce(obj.transform.forward * force, obj.transform.position);
        }

        public virtual void FixedUpdate()
        {
            WheelMotorInput(frontRWheel, normalRFrictionCoeficient);
            WheelMotorInput(frontLWheel, normalLFrictionCoeficient);
        }

        public virtual void Update()
        {
            float steerInput = OWInput.GetValue(InputLibrary.thrustX, InputMode.All);
            float targetAngle = maxSteerAngle * steerInput;

            frontRWheel.steerAngle = targetAngle;
            frontLWheel.steerAngle = targetAngle;
        }
    }
}

