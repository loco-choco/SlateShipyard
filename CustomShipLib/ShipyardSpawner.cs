using SlateShipyard.ShipSpawner;
using SlateShipyard.ShipSpawner.RampUI;
using SlateShipyard.ShipSpawner.SelectionUI;

using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard
{
    //! Class that holds SpawnShipyard.
    public static class ShipyardSpawner
    {
        //! Spawns a default shipyard.
        public static GameObject SpawnShipyard(Transform transform, Vector3 position, Quaternion rotation)
        {
            Transform t = GameObject.Instantiate(SlateShipyard.defaultShipSpawnerPrefab, position, rotation, transform).transform;
            return t.gameObject;
        }
    }
}
