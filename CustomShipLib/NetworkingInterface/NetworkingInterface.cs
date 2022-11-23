using SlateShipyard.ShipSpawner;
using UnityEngine;

namespace SlateShipyard.NetworkingInterface
{
    //TODO add docs to NetworkingInterface
    public abstract class NetworkingInterface
    {
        public abstract void InvokeMethod(ObjectNetworkingInterface sender, string methodName, params object[] parameters);
        public abstract void SpawnRemoteShip(ShipData shipData, GameObject shipObject);
    }

    public class EmptyNetworkingInterface : NetworkingInterface
    {
        public override void InvokeMethod(ObjectNetworkingInterface sender, string methodName, params object[] parameters) 
        {
        }
        public override void SpawnRemoteShip(ShipData shipData, GameObject shipObject) 
        {
        }
    }
}
