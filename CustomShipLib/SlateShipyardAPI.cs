using UnityEngine;

namespace SlateShipyard
{
    public class SlateShipyardAPI 
    {
        public GameObject SpawnShipyard(Transform t, Vector3 localPosition, Vector3 localRotation) => SlateShipyard.SpawnShipyard(t, localPosition, localRotation);
        public void DontSpawnDefaultShipyard() => SlateShipyard.DontSpawnDefaultShipyard();
    }
}
