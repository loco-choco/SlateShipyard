using System;

namespace SlateShipyard.PlayerAttaching
{

    //! A PlayerAttachPoint you can use to enable free look.
    /*! Because of how the free look code was made, it is only active when sitted on the ship.
     * To be able to use it on custom ships/custom PlayerAttachPoints you can use FreeLookablePlayerAttachPoint.
     When the game detects that the player is attached to a FreeLookablePlayerAttachPoint it will call AllowFreeLook
    to know if the player should be able to use free look.*/
    public class FreeLookablePlayerAttachPoint : PlayerAttachPoint
    {
        public Func<bool> AllowFreeLook = () => false; //!< The function called to know if the player attached to it should be able to free look.
        /*< Return true if you want the player to be able to free look, and false if you don't want.*/
    }
}
