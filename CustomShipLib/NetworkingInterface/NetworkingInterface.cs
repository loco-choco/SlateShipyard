using SlateShipyard.ShipSpawner;
using UnityEngine;

namespace SlateShipyard.NetworkingInterface
{
    //TODO add docs to NetworkingInterface
    public interface NetworkingInterface
    {
        void InvokeMethod(ObjectNetworkingInterface sender, string methodName, params object[] parameters);
        GameObject SpawnShip(ShipData shipData);
    }

    public class NonNetworkingInterface : NetworkingInterface
    {
        public void InvokeMethod(ObjectNetworkingInterface sender, string methodName, params object[] parameters) 
        {
        }
        public GameObject SpawnShip(ShipData shipData)
        {
            GameObject g = GameObject.Instantiate(shipData.prefab);
            g.SetActive(true);
            return g;
        }
    }
}
