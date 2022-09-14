using UnityEngine;

namespace SlateShipyard.Modules.Wheels
{
	//God tutorial that made this possible: https://www.youtube.com/watch?v=x0LUiE0dxP0
	public class OWSimpleRaycastWheel : MonoBehaviour
	{
		public float wheelRadius;
		public float restLenght;
		public float springTravel;
		public float springStiffness;
		public float damperStiffness;

		public float frictionCoeficient = 0.3f;
		public float steeringFrictionCoeficient = 2f;

		private float maxLenght;
		private float minLenght;
		private float lastLenght;
		private float springLenght;
		private float springForce;
		private float springVelocity;
		private float damperForce;

		private float wheelAngle;
		public float steerAngle;
		public float steerTime = 2f;

		private Vector3 suspensionForce;
		private Vector3 wheelVelocityLocal;
		private float frictionForce;
		private float steeringForce;

		public Rigidbody rb;
		public LayerMask collisionMask;

		void Start()
		{
			minLenght = restLenght - springTravel;
			maxLenght = restLenght + springTravel;
		}

		void FixedUpdate()
		{
			GetGround();
		}

		void Update()
		{
			wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
			transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);
		}

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
