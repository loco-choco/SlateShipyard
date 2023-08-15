using UnityEngine;

namespace SlateShipyard.VanishObjects
{
    //! A class to handle when a object interacts with VanishVolumes.
    /*! Because of how VanishVolumes are coded, you can only add custom interactions with the different VanishVolumes 
     * by changing the object tag or by editing the game code. So to make handling this situation easier, when an object enters the
     * VanishVolumes trigger volume and has the ControlledVanishObject script, instead of calling the default function it will call
     * the following specialized methods.*/
    abstract public class ControlledVanishObject : MonoBehaviour
    {
        //! Handles when the object is supposed to Vanish in a DestructionVolume.
        /*! Return false if you want the original code to run, and true if you don't want to*/
        public virtual bool OnDestructionVanish(DestructionVolume destructionVolume) { return true; }
        //! Handles when the object is supposed to Vanish in a SupernovaDestructionVolume.
        /*! Return false if you want the original code to run, and true if you don't want to*/
        public virtual bool OnSupernovaDestructionVanish(SupernovaDestructionVolume supernovaDestructionVolume) { return true; }
        //! Handles when the object is supposed to Vanish in a BlackHoleVolume.
        /*! Return false if you want the original code to run, and true if you don't want to*/
        public virtual bool OnBlackHoleVanish(BlackHoleVolume blackHoleVolume, RelativeLocationData entryLocation) { return true; }
        //! Handles when the object is supposed to get Warped in a WhiteHoleVolume.
        /*! Return false if you want the original code to run, and true if you don't want to*/
        public virtual bool OnWhiteHoleReceiveWarped(WhiteHoleVolume whiteHoleVolume, RelativeLocationData entryData) { return true; }
        //! Handles when the object is supposed to SpawnImmediately in a WhiteHoleVolume.
        /*! Return false if you want the original code to run, and true if you don't want to*/
        public virtual void OnWhiteHoleSpawnImmediately(WhiteHoleVolume whiteHoleVolume, RelativeLocationData entryData, out bool playerPassedThroughWarp) { playerPassedThroughWarp = false; }
        //! Handles when the object is supposed to Vanish in a TimeLoopBlackHoleVolume.
        /*! Set playerPassedThroughWarp to true if the player is passing through the blackhole attached to the object, and false if it isn't*/
        public virtual bool OnTimeLoopBlackHoleVanish(TimeLoopBlackHoleVolume timeloopBlackHoleVolume) { return true; }
        //! Handles if the object components should be destroyed after passing on Vanish.
        /*! Set it to true if you want for it to get destroyed, and false if you don't want to.*/
        public bool DestroyComponentsOnGrow { get; protected set; } = true;
    }
}
