using UnityEngine;
using System.Collections.Generic;
using System;

namespace SlateShipyard.ShipSpawner
{
    public static class ShipSpawnerManager
    {
        private static Dictionary<string, ShipData> ShipDictionary = new Dictionary<string, ShipData>();
        private static List<ShipData> ShipList = new List<ShipData>();
        
        public static bool AddShip(Func<GameObject> prefab, string name) 
        {
            if (ShipDictionary.ContainsKey(name)){
                return false;
            }
            ShipData data = new ShipData() { name = name, prefab = prefab };
            ShipDictionary.Add(name, data);
            ShipList.Add(data);
            return true;
        }
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
        public static int ShipAmount()
        {
            return ShipList.Count;
        }
        public static ShipData GetShipData(int index)
        {
            if(index < ShipList.Count){
                return ShipList[index];
            }
            return default;
        }
        public static ShipData GetShipData(string name)
        {
            if (ShipDictionary.TryGetValue(name, out ShipData data))
            {
                return data;
            }
            return default;
        }
    }
    public struct ShipData 
    {
        //public GameObject prefab;
        public Func<GameObject> prefab;
        public string name;
    }
}
