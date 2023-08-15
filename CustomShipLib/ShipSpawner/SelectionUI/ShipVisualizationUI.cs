using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard.ShipSpawner.SelectionUI
{
    //! UI element to display the current selected ship.
    public class ShipVisualizationUI : MonoBehaviour
    {
        public Text shipName;//!< The text UI element which displays the name of the selected ship.

        //! Changes the selected ship data being displayed.
        public void ChangeShip(ShipData data) 
        {
            shipName.text = data.name;
        }
    }
}
