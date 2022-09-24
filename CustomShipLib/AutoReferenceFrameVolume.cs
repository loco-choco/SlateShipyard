namespace SlateShipyard
{
    //! ReferenceFrameVolume which sets the variables automatically (WIP).
    public class AutoReferenceFrameVolume : ReferenceFrameVolume
    {
        public override void Awake()
        {
            _referenceFrame = default;
            base.Awake();
        }
    }
}
