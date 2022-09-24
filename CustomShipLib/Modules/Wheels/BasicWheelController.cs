using UnityEngine;

namespace SlateShipyard.Modules.Wheels
{
    //! A basic controller to control vehicles with wheels.
    /*! This basic controller is to be used by vehicles with the two front wheels being the ones generating the force.
     * It was made to be able to exapanded on with the virtual methods.*/
    public class BasicWheelController : MonoBehaviour
    {

        public OWSimpleRaycastWheel frontRWheel; //!< The front right wheel.
        public OWSimpleRaycastWheel frontLWheel; //!< The front left wheel.
        public OWRigidbody body; //!< The vehicle body.

        public float maxSteerAngle = 30f; //!< The max angle the wheels can go.

        public float maxAccelerationForce; //!< The max force the "motor" can reach.

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

        //!When the motor force it got from the inputs is added.
        public virtual void FixedUpdate()
        {
            WheelMotorInput(frontRWheel, normalRFrictionCoeficient);
            WheelMotorInput(frontLWheel, normalLFrictionCoeficient);
        }

        //!When the wheels steer angle it got from the inputs is changed.
        public virtual void Update()
        {
            float steerInput = OWInput.GetValue(InputLibrary.thrustX, InputMode.All);
            float targetAngle = maxSteerAngle * steerInput;

            frontRWheel.steerAngle = targetAngle;
            frontLWheel.steerAngle = targetAngle;
        }
    }
}

