namespace SlateShipyard.NetworkingInterface
{
    //TODO add docs to NetworkingInterface
    public abstract class NetworkingInterface
    {
        public abstract void InvokeMethod(ObjectNetworkingInterface sender, string methodName, params object[] parameters);
    }
}
