using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard.ShipSpawner.SelectionUI
{
    public class ShipVisualizationUI : MonoBehaviour
    {
        public Text shipName;

        public void ChangeShip(ShipData data) 
        {
            shipName.text = data.name;
        }
    }
}
