using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard.ShipSpawner.RampUI
{
    public class RampAngleUI : MonoBehaviour
    {
        float maxAngle = 90f;
        float minAngle = 0f;

        float targetAngle = 0f;
        float angleStep = 5f;
        float angleChangeSpeed = 180f;

        public Text angleDisplayText;
        public InteractReceiver increaseAngle;
        public InteractReceiver decreaseAngle;

        public void Start()
        {
            increaseAngle.OnReleaseInteract += OnAngleIncrease;
            decreaseAngle.OnReleaseInteract += OnAngleDecrease;
        }
        public void OnDestroy()
        {
            increaseAngle.OnReleaseInteract -= OnAngleIncrease;
            decreaseAngle.OnReleaseInteract -= OnAngleDecrease;
        }

        public void OnAngleIncrease()
        {
            targetAngle += angleStep;
            targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);
            angleDisplayText.text = $" {(int)(targetAngle)}°";
            increaseAngle.ResetInteraction();
        }
        public void OnAngleDecrease()
        {
            targetAngle -= angleStep;
            targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);
            angleDisplayText.text = $" {(int)(targetAngle)}°";
            decreaseAngle.ResetInteraction();
        }
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
