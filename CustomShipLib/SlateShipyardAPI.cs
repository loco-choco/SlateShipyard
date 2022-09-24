using UnityEngine;

namespace SlateShipyard
{
    //! The mod API you can call with OWML.
    public class SlateShipyardAPI 
    {
        //! Spawns a default shipyard giving the local position, rotation and parent transform.
        public GameObject SpawnShipyard(Transform t, Vector3 localPosition, Vector3 localRotation) => SlateShipyard.SpawnShipyard(t, localPosition, localRotation);
        //! Dissables the spawn of the default shipyards.
        public void DontSpawnDefaultShipyard() => SlateShipyard.DontSpawnDefaultShipyard();
    }
}
