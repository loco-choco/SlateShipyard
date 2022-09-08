using UnityEngine;

namespace SlateShipyard.VanishObjects
{
    abstract public class ControlledVanishObject : MonoBehaviour
    {
        public virtual bool OnDestructionVanish(DestructionVolume destructionVolume) { return true; }
        public virtual bool OnSupernovaDestructionVanish(SupernovaDestructionVolume supernovaDestructionVolume) { return true; }
        public virtual bool OnBlackHoleVanish(BlackHoleVolume blackHoleVolume, RelativeLocationData entryLocation) { return true; }
        public virtual bool OnWhiteHoleReceiveWarped(WhiteHoleVolume whiteHoleVolume, RelativeLocationData entryData) { return true; }
        public virtual bool OnTimeLoopBlackHoleVanish(TimeLoopBlackHoleVolume timeloopBlackHoleVolume) { return true; }

        public bool DestroyComponentsOnGrow { get; protected set; } = true;
    }
}
