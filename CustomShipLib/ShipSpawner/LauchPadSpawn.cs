using System;
using UnityEngine;

namespace SlateShipyard.ShipSpawner
{
    //! Spawner of the ship addons added on ShipSpawnerManager.
    /*! Giving the prefab function, this will spawn on the best place a ship (WIP).*/
    public class LaunchPadSpawn : MonoBehaviour
    {
        OWRigidbody rigidbody;
        //! The Start function of a MonoBehaviour.
        public void Start() 
        {
            rigidbody = gameObject.GetAttachedOWRigidbody();
        }
        //! Spawns a ship giving the shipPrefab function.
        /*! If spawnEvenIfNotAllowed is set to false, it will first check to see if the ship will spawn inside something and
         * will not spawn it if that is the case (WIP). If it is set to true it will ignore that check. The check feature is still WIP so
         * set spawnEvenIfNotAllowed to true if you want to be able to use the function.*/
        public bool SpawnShip(ShipData shipData, bool spawnEvenIfNotAllowed) 
        {
            var shipPrefab = shipData.prefab;
            GameObject g = Instantiate(shipPrefab);

            Bounds shipBounds = GetCombinedBoundingBoxOfChildren(g.transform);
            //TODO Melhorar esse algoritimo aqui de verificar se é seguro spawnar
            if (Physics.CheckBox(shipBounds.center, shipBounds.size / 2f, Quaternion.identity, OWLayerMask.physicalMask) 
                && !spawnEvenIfNotAllowed)
            {
                Destroy(g);
                return false;
            }
            g.SetActive(true);

            OWRigidbody r = g.GetAttachedOWRigidbody();
            r.WarpToPositionRotation(transform.position, transform.rotation);
            r.SetVelocity(rigidbody.GetPointVelocity(transform.position));
            r.SetAngularVelocity(rigidbody.GetAngularVelocity());

            SlateShipyard.NetworkingInterface.SpawnRemoteShip(shipData, g);
            return true;
        }

        //! Gets the Bounds of a object combined collider.
        //From this thread https://forum.unity.com/threads/how-do-i-get-the-bounds-of-a-rigidbodys-compound-collider.166691
        public static Bounds GetCombinedBoundingBoxOfChildren(Transform root)
        {
            if (root == null)
            {
                throw new ArgumentException("The supplied transform was null");
            }

            var colliders = root.GetComponentsInChildren<Collider>();
            if (colliders.Length == 0)
            {
                throw new ArgumentException("The supplied transform " + root?.name + " does not have any children with colliders");
            }

            Bounds totalBBox = colliders[0].bounds;
            foreach (var collider in colliders)
            {
                totalBBox.Encapsulate(collider.bounds);
            }
            return totalBBox;
        }
    }
}
