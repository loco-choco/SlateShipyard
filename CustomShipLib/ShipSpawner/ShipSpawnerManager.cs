using UnityEngine;
using System.Collections.Generic;
using System;

namespace SlateShipyard.ShipSpawner
{
    //! Where all the ships from addons is stored.
    /*! This is the class you want to call if you want to access a ship or the ships added by addons.*/
    public static class ShipSpawnerManager
    {
        private static readonly Dictionary<string, ShipData> ShipDictionary = new Dictionary<string, ShipData>();
        private static readonly List<ShipData> ShipList = new List<ShipData>();

        //! Adds the addon ship to the table of accessable addon ships.
        /*! Returns false if there is already a ship with the same name, and true if it succeeded*/
        public static bool AddShip(GameObject prefab, string name) 
        {
            if (ShipDictionary.ContainsKey(name)){
                return false;
            }
            ShipData data = new ShipData() { name = name, prefab = prefab };
            ShipDictionary.Add(name, data);
            ShipList.Add(data);
            return true;
        }
        //! Removes the addon ship from the table of accessable addon ships.
        public static bool RemoveShip(string name)
        {
            if (ShipDictionary.TryGetValue(name, out ShipData data))
            {
                ShipDictionary.Remove(name);
                ShipList.Remove(data);
                return true;
            }
            return false;
        }
        //! The amount of ships in the table of accessable addon ships.
        public static int ShipAmount()
        {
            return ShipList.Count;
        }
        //! Returns the ship in the table of accessable addon ships by passing its index.
        public static ShipData GetShipData(int index)
        {
            if(index < ShipList.Count){
                return ShipList[index];
            }
            return default;
        }
        //! Returns the ship in the table of accessable addon ships by passing its name.
        public static ShipData GetShipData(string name)
        {
            if (ShipDictionary.TryGetValue(name, out ShipData data))
            {
                return data;
            }
            return default;
        }
        //! Tries to return the ship in the table of accessable addon ships by passing its name.
        public static bool TryGetShipData(string name, out ShipData data)
        {
            return ShipDictionary.TryGetValue(name, out data);
        }
    }
    //! The data stored about the ship addons in ShipSpawnerManager.
    public struct ShipData 
    {
        public GameObject prefab;//!< A function returning the ship addon.
        public string name;//!< The name shown on the ship selector UI (must be unique!).
    }
}
