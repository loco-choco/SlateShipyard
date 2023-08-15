using UnityEngine;

namespace SlateShipyard.Modules.Wheels
{
	//! This is a script which allows you to have simple wheel simulations with suspension, friction and other stuff.
	/*! This simulation uses a single raycast to represent the wheel and its suspension, 
	 * if you want to have a more in depth look on how it works [watch these series of videos](https://www.youtube.com/watch?v=x0LUiE0dxP0).*/
	public class OWSimpleRaycastWheel : MonoBehaviour
	{
		public float wheelRadius; //!< The radius of the wheel.
		public float restLenght; //!< The lenght which the suspension considers to be at rest.
		public float springTravel; //!< The distance which the suspension can travel.
		/*!< The max and min lenght it can have are calculated from this equation: minLenght = restLenght - springTravel; maxLenght = restLenght + springTravel.*/
		public float springStiffness; //!< The spring constant of the suspension.
		public float damperStiffness; //!< The damping constant of the suspension.
		/*!< Usually make it smaller then the springStiffness to have better results.*/

		public float frictionCoeficient = 0.3f; //!< The coeficient for the friction the wheel will experience for its foward direction velocity when touching a ground.
		/*!< Usually make it a value between 1.0 and 0.0, 0.0 being like driving in ice and 1.0 like in glue. */
		public float steeringFrictionCoeficient = 2f; //!< The coeficient for the friction the wheel will experience for velocities that aren't on the wheel direction.
		/*!< This means that if the wheel is sliding, this friction force will try to reduce this motion.*/

		private float maxLenght;
		private float minLenght;
		private float lastLenght;
		private float springLenght;
		private float springForce;
		private float springVelocity;
		private float damperForce;

		private float wheelAngle;
		public float steerAngle; //!< The target angle (in degrees) which you want the wheel to be at.
		public float steerTime = 2f; //!< The 'time' that the wheel will take to reach the target angle in steerAngle.
		/*!< Bigger values make it reach the end value faster.*/

		private Vector3 suspensionForce;
		private Vector3 wheelVelocityLocal;
		private float frictionForce;
		private float steeringForce;

		public Rigidbody rb; //!< The Rigidbody the wheel will use to apply the forces it calculates.
		public LayerMask collisionMask; //!< The LayerMask it will use on the raycast to be considered as valid ground.

		public bool enablePhysics = true;
		void Start()
		{
			minLenght = restLenght - springTravel;
			maxLenght = restLenght + springTravel;
		}

		void FixedUpdate()
		{
			if (!enablePhysics)
				return;

			GetGround();
		}

		void Update()
		{
			wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
			transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);
		}

		//! Returns true if the wheel is hitting a valid ground. False if not.
		public bool IsOnGround() => isOnGround;
		private bool isOnGround;
		void GetGround()
		{
			isOnGround = false;
			if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLenght + wheelRadius, collisionMask))
			{
				isOnGround = true;
				//SUSpension
				lastLenght = springLenght;
				springLenght = hit.distance - wheelRadius;
				springLenght = Mathf.Clamp(springLenght, minLenght, maxLenght);
				springVelocity = (lastLenght - springLenght) / Time.fixedDeltaTime;
				springForce = springStiffness * (restLenght - springLenght);
				damperForce = damperStiffness * springVelocity;

				suspensionForce = (springForce + damperForce) * transform.up;
				//---------------
				wheelVelocityLocal = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point) - hit.rigidbody.GetPointVelocity(hit.point));

				//Friction and steer forces

				frictionForce = (springForce + damperForce) * Mathf.Sign(-wheelVelocityLocal.z) * frictionCoeficient;

				steeringForce = wheelVelocityLocal.x * steeringFrictionCoeficient;
				//----------

				rb.AddForceAtPosition(suspensionForce + (frictionForce * transform.forward) + (steeringForce * -transform.right), hit.point);
			}
		}
	}
}
