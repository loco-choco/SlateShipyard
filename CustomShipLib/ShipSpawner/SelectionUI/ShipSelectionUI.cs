using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard.ShipSpawner.SelectionUI
{
    //! UI element to select and spawn ships from ShipSpawnerManager.
    public class ShipSelectionUI : MonoBehaviour
    {
        int currentSelectedShip = 0;
        bool spawnEvenIfNotPossible = true;
        public LaunchPadSpawn spawner; //!< The LaunchPadSpawn being used.
        public ShipVisualizationUI shipVisualization;//!< The ShipVisualizationUI to display the selected ship data.

        public Text middleDisplayText; //!< Text UI element to display messages and warnings.
        public InteractReceiver nextShipButton; //!< The button to select the next ship on the list.
        public InteractReceiver previousShipButton; //!< The button to select the previous ship on the list.
        public InteractReceiver spawnShipButton; //!< The button to spawn the current selected ship.

        //! The Start method.
        public void Start()
        {
            nextShipButton.ChangePrompt("next");
            previousShipButton.ChangePrompt("previous");
            spawnShipButton.ChangePrompt("select");

            nextShipButton.OnReleaseInteract += OnNextPageInteract;
            previousShipButton.OnReleaseInteract += OnPreviousPageInteract;
            spawnShipButton.OnReleaseInteract += OnSelectInteract;

            WriteTextOnDisplay("Welcome :)");
            if (currentSelectedShip < ShipSpawnerManager.ShipAmount())
            {
                shipVisualization.ChangeShip(ShipSpawnerManager.GetShipData(currentSelectedShip));
            }
        }
        //! The OnDestroy method.
        public void OnDestroy() 
        {
            nextShipButton.OnReleaseInteract -= OnNextPageInteract;
            previousShipButton.OnReleaseInteract -= OnPreviousPageInteract;
            spawnShipButton.OnReleaseInteract -= OnSelectInteract;
        }

        //! The method called to select the next ship on the list.
        public void OnNextPageInteract() 
        {
            if (ShipSpawnerManager.ShipAmount() > 0)
            {
                currentSelectedShip = (currentSelectedShip + 1) % ShipSpawnerManager.ShipAmount();
                if (currentSelectedShip < ShipSpawnerManager.ShipAmount())
                {
                    shipVisualization.ChangeShip(ShipSpawnerManager.GetShipData(currentSelectedShip));
                }
            }

            nextShipButton.ResetInteraction();
        }
        //! The method called to select the previous ship on the list.
        public void OnPreviousPageInteract()
        {
            if (ShipSpawnerManager.ShipAmount() > 0)
            {
                currentSelectedShip--;
                if (currentSelectedShip < 0)
                {
                    currentSelectedShip = ShipSpawnerManager.ShipAmount() - 1;
                }

                if (currentSelectedShip < ShipSpawnerManager.ShipAmount())
                {
                    shipVisualization.ChangeShip(ShipSpawnerManager.GetShipData(currentSelectedShip));
                }
            }
            previousShipButton.ResetInteraction();
        }
        //! The method called to spawn the currently selected ship.
        public void OnSelectInteract()
        {
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
