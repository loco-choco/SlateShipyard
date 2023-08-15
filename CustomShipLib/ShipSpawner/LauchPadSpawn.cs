using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlateShipyard.ShipSpawner
{
    //! Spawner of the ship addons added on ShipSpawnerManager.
    /*! Giving the prefab function, this will spawn on the best place a ship (WIP).*/
    public class LaunchPadSpawn : MonoBehaviour
    {
        OWRigidbody rigidbody;
        private Stack<GameObject> spawnedShips = new Stack<GameObject>();
        private ShipData lastShipTypeSpawned;
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
            GameObject g = SlateShipyard.NetworkingInterface.SpawnShip(shipData);

            //TODO Melhorar esse algoritimo aqui de verificar se é seguro spawnar
            //if (Physics.CheckBox(shipBounds.center, shipBounds.size / 2f, Quaternion.identity, OWLayerMask.physicalMask) 
            //    && !spawnEvenIfNotAllowed)
            //{
            //    Destroy(g);
            //    return false;
            //}
            if(g == null)
                return false;

            OWRigidbody r = g.GetAttachedOWRigidbody();

            Bounds shipBounds = GetCombinedBoundingBoxOfChildren(g.transform.root);
            float heightToAdd = (g.transform.position -shipBounds.min).y;
            Vector3 spawnPosition = transform.position + transform.up * heightToAdd;

            r.WarpToPositionRotation(spawnPosition, transform.rotation);

            r.SetVelocity(rigidbody.GetPointVelocity(spawnPosition));
            r.SetAngularVelocity(rigidbody.GetAngularVelocity());

            spawnedShips.Push(g);
            lastShipTypeSpawned = shipData;
            return true;
        }

        //! Destroys the ship, detaching/retrieving probe and/or killing the player if attached.
        public void DestroyShip(GameObject ship)
        {
            //Return probes
            SurveyorProbe[] attachedProbes = ship.GetComponentsInChildren<SurveyorProbe>();
            for (int i = 0; i < attachedProbes.Length; i++)
            {
                attachedProbes[i].Unanchor();
                attachedProbes[i].ExternalRetrieve();
            }
            //Detach and kill players
            PlayerAttachPoint attachPoint = ship.GetComponentInChildren<PlayerAttachPoint>();
            if (attachPoint.enabled)//Only if the player is attached the attach point is enabled
            {
                attachPoint.DetachPlayer();
                Locator.GetDeathManager().KillPlayer(DeathType.BlackHole);
            }
            Destroy(ship);
        }

        //! Resets last spawned ship by this launch pad.
        public void ResetLatestSpawnedShip() 
        {
            if (spawnedShips.Count <= 0)
                return;

            GameObject latestSpawnedShip = spawnedShips.Pop();
            if (latestSpawnedShip != null)
                DestroyShip(latestSpawnedShip);

            SpawnShip(lastShipTypeSpawned, true);
        }
        //! Destroys last spawned ship by this launch pad.
        public void DestroyLatestSpawnedShip()
        {
            if (spawnedShips.Count <= 0)
                return;

            GameObject latestSpawnedShip = spawnedShips.Pop();
            if (latestSpawnedShip != null)
                DestroyShip(latestSpawnedShip);
        }
        //! Destroys all ships spawned by this launch pad.
        public void DestroyAllSpawnedShip()
        {
            int i = 0;
            foreach(var ship in spawnedShips) 
            {
                if (ship != null)
                {
                    DestroyShip(ship);
                    i++;
                }
            }
            SlateShipyard.modHelper.Console.WriteLine($"Destroyed {i} ships");
            spawnedShips.Clear();
        }

        //! Gets the Bounds of a object combined collider.
        //From this thread https://forum.unity.com/threads/how-do-i-get-the-bounds-of-a-rigidbodys-compound-collider.166691
        public static Bounds GetCombinedBoundingBoxOfChildren(Transform t)
        {
            if (t == null)
            {
                throw new ArgumentException("The supplied transform was null");
            }
            List<Collider> colliders = new List<Collider>();
            t.GetComponentsInChildren<Collider>(colliders);

            colliders.RemoveAll(x => (OWLayerMask.physicalMask & (1 << x.gameObject.layer)) == 0 || x.isTrigger);

            if (colliders.Count > 0)
            {
                Bounds totalBBox = colliders[0].bounds;
                for (int i = 1; i < colliders.Count; i++)
                {
                    if (colliders[i] != null)
                        totalBBox.Encapsulate(colliders[i].bounds);;
                }
                return totalBBox;
            }
            return new Bounds();
        }
    }
}
