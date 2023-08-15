using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard.ShipSpawner.SelectionUI
{
    //! UI element to select and spawn ships from ShipSpawnerManager.
    public class ShipSelectionUI : MonoBehaviour
    {
        public int currentSelectedShip = 0;
        bool spawnEvenIfNotPossible = true;
        public LaunchPadSpawn spawner; //!< The LaunchPadSpawn being used.
        public ShipVisualizationUI shipVisualization;//!< The ShipVisualizationUI to display the selected ship data.

        public Text middleDisplayText; //!< Text UI element to display messages and warnings.
        public InteractReceiver nextShipButton; //!< The button to select the next ship on the list.
        public InteractReceiver previousShipButton; //!< The button to select the previous ship on the list.
        public InteractReceiver spawnShipButton; //!< The button to spawn the current selected ship.

        public LaunchPadNetworkingInterface launchPadNetworkingInterface;

        //! The Start method.
        public void Start()
        {
            launchPadNetworkingInterface.ShipSelectionUI = this;
            nextShipButton.ChangePrompt("next");
            previousShipButton.ChangePrompt("previous");
            spawnShipButton.ChangePrompt("select");

            nextShipButton.OnReleaseInteract += OnNextPageInteract;
            previousShipButton.OnReleaseInteract += OnPreviousPageInteract;
            spawnShipButton.OnReleaseInteract += OnSelectInteract;

            WriteTextOnDisplay("Welcome :)");
            UpdateSelection();
        }
        //! The OnDestroy method.
        public void OnDestroy() 
        {
            nextShipButton.OnReleaseInteract -= OnNextPageInteract;
            previousShipButton.OnReleaseInteract -= OnPreviousPageInteract;
            spawnShipButton.OnReleaseInteract -= OnSelectInteract;
        }
        //! Updates the UI for the selected ship
        public void UpdateSelection()
        {
            launchPadNetworkingInterface.SelectedShip = currentSelectedShip;
            if (currentSelectedShip < ShipSpawnerManager.ShipAmount() && currentSelectedShip >= 0)
            {
                shipVisualization.ChangeShip(ShipSpawnerManager.GetShipData(currentSelectedShip));
            }
        }

        //! The method called to select the next ship on the list.
        public void OnNextPageInteract() 
        {
            if (launchPadNetworkingInterface.IsPuppet)
            {
                SlateShipyard.NetworkingInterface.InvokeMethod(
                    launchPadNetworkingInterface,
                    nameof(LaunchPadNetworkingInterface.SelectNextShip),false);
                return;
            }
            
            if (ShipSpawnerManager.ShipAmount() > 0)
            {
                currentSelectedShip = (currentSelectedShip + 1) % ShipSpawnerManager.ShipAmount();
                UpdateSelection();
            }

            nextShipButton.ResetInteraction();
        }
        //! The method called to select the previous ship on the list.
        public void OnPreviousPageInteract()
        {
            if (launchPadNetworkingInterface.IsPuppet)
            {
                SlateShipyard.NetworkingInterface.InvokeMethod(
                    launchPadNetworkingInterface,
                    nameof(LaunchPadNetworkingInterface.SelectNextShip),true);
                return;
            }
            
            if (ShipSpawnerManager.ShipAmount() > 0)
            {
                currentSelectedShip--;
                if (currentSelectedShip < 0)
                {
                    currentSelectedShip = ShipSpawnerManager.ShipAmount() - 1;
                }

                UpdateSelection();
            }
            previousShipButton.ResetInteraction();
        }
        //! The method called to spawn the currently selected ship.
        public void OnSelectInteract()
        {
            if (launchPadNetworkingInterface.IsPuppet)
            {
                SlateShipyard.NetworkingInterface.InvokeMethod(
                    launchPadNetworkingInterface,
                    nameof(LaunchPadNetworkingInterface.SpawnShip));
                return;
            }
            
            spawnShipButton.ResetInteraction();

            ShipData data = ShipSpawnerManager.GetShipData(currentSelectedShip);
            if(data.prefab == null) {
                WriteTextOnDisplay($"No Prefab for {data.name}");
                return;
            }
            else if(spawner.SpawnShip(data, spawnEvenIfNotPossible)){
                WriteTextOnDisplay($"{data.name} Spawned!");
                return;
            }
            WriteTextOnDisplay($"Couldn't spawn {data.name}");
        }
        //! Writes text on the middle text ui and removes it after 2 seconds.
        public void WriteTextOnDisplay(string text) 
        {
            middleDisplayText.text = text;
            StartCoroutine(WaitToRemoveText(2f));
        }
        IEnumerator WaitToRemoveText(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            middleDisplayText.text = "";
        }
    }
}
