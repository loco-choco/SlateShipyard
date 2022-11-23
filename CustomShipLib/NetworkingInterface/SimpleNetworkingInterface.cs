using UnityEngine;

namespace SlateShipyard.NetworkingInterface
{
    public class SimpleNetworkingInterface : ObjectNetworkingInterface
    {
        protected bool isPuppet = false;
        public override bool IsPuppet 
        { 
            get => isPuppet;
            set => OnIsPuppetChange(value);
        }
        public override string UniqueScriptID 
        { 
            get => transform.name; 
        }

        public virtual void OnIsPuppetChange(bool isPuppet) 
        {
            if(isPuppet != this.isPuppet) 
            {
                for (int i = 0; i < scriptsToDisableWhenPuppet.Length; i++)
                {
                    scriptsToDisableWhenPuppet[i].enabled = !isPuppet;
                }
                for (int i = 0; i < gameObjectsToDisableWhenPuppet.Length; i++)
                {
                    gameObjectsToDisableWhenPuppet[i].SetActive(!isPuppet);
                }
                Rigidbody r = GetComponent<Rigidbody>();
                if (r != null && RigidbodyToKinematicWhenPuppet)
                {
                    r.isKinematic = isPuppet;
                }
            }

            this.isPuppet = isPuppet;
        }

        public GameObject[] gameObjectsToDisableWhenPuppet;
        public MonoBehaviour[] scriptsToDisableWhenPuppet;
        public bool RigidbodyToKinematicWhenPuppet = true;
    }
}
