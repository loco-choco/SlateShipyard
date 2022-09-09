using System;
using UnityEngine;

namespace SlateShipyard.ShipSpawner
{
    //Giving the prefab, this will spawn on the best place a ship
    public class LaunchPadSpawn : MonoBehaviour
    {
        OWRigidbody rigidbody;
        public void Start() 
        {
            rigidbody = gameObject.GetAttachedOWRigidbody();
        }
        public bool SpawnShip(Func<GameObject> shipPrefab, bool spawnEvenIfNotAllowed) 
        {
            GameObject g = shipPrefab.Invoke();

            Bounds shipBounds = GetCombinedBoundingBoxOfChildren(g.transform);
            //TODO Melhorar esse algoritimo aqui de verificar se é seguro spawnar
            if (Physics.CheckBox(shipBounds.center, shipBounds.size / 2f, Quaternion.identity, OWLayerMask.physicalMask) 
                && !spawnEvenIfNotAllowed)
            {
                Destroy(g);
                return false;
            }
            OWRigidbody r = g.GetAttachedOWRigidbody();
            r.WarpToPositionRotation(transform.position, transform.rotation);
            r.SetVelocity(rigidbody.GetPointVelocity(transform.position));
            r.SetAngularVelocity(rigidbody.GetAngularVelocity());
            return true;
        }

        //From https://forum.unity.com/threads/how-do-i-get-the-bounds-of-a-rigidbodys-compound-collider.166691/
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
