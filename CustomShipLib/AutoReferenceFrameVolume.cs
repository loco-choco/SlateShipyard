namespace SlateShipyard
{
    internal class AutoReferenceFrameVolume : ReferenceFrameVolume
    {
        public override void Awake()
        {
            _referenceFrame = default;
            base.Awake();
        }
    }
}
