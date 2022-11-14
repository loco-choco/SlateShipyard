using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard.ShipSpawner.RampUI
{
    //! UI element to display the current angle of the ramp and to handle its angle.
    public class RampAngleUI : MonoBehaviour
    {
        float maxAngle = 90f;
        float minAngle = 0f;

        float targetAngle = 0f;
        float angleStep = 5f;
        float angleChangeSpeed = 180f;

        public Text angleDisplayText; //!< Text UI element to display the current angle in degrees.
        public InteractReceiver increaseAngle; //!< The button to increase the angle value.
        public InteractReceiver decreaseAngle; //!< The button to decrease the angle value.
        
        //! The Start method.
        public void Start()
        {
            increaseAngle.ChangePrompt("increase");
            decreaseAngle.ChangePrompt("decrease");

            increaseAngle.OnReleaseInteract += OnAngleIncrease;
            decreaseAngle.OnReleaseInteract += OnAngleDecrease;
        }

        //! The OnDestroy method.
        public void OnDestroy()
        {
            increaseAngle.OnReleaseInteract -= OnAngleIncrease;
            decreaseAngle.OnReleaseInteract -= OnAngleDecrease;
        }

        //! Method called to increase the angle of the ramp. 
        public void OnAngleIncrease()
        {
            targetAngle += angleStep;
            targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);
            angleDisplayText.text = $" {(int)(targetAngle)}°";
            increaseAngle.ResetInteraction();
        }
        //! Method called to decrease the angle of the ramp. 
        public void OnAngleDecrease()
        {
            targetAngle -= angleStep;
            targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);
            angleDisplayText.text = $" {(int)(targetAngle)}°";
            decreaseAngle.ResetInteraction();
        }
        //! The Update method.
        public void Update() 
        {
            Vector3 currentAngle = transform.localEulerAngles;
            float difference = targetAngle - transform.localEulerAngles.z;
            float step = Mathf.Clamp(Mathf.Abs(difference), 0f, angleChangeSpeed) * Time.deltaTime * Mathf.Sign(difference);
            currentAngle.z += step;
            transform.localEulerAngles = currentAngle;

        }
    }
}
