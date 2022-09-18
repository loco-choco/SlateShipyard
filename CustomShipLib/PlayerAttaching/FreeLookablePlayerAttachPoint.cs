using System;

namespace SlateShipyard.PlayerAttaching
{
    public class FreeLookablePlayerAttachPoint : PlayerAttachPoint
    {
        public Func<bool> AllowFreeLook = () => false;
    }
}
