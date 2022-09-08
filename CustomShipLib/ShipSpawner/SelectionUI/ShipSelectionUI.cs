using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard.ShipSpawner.SelectionUI
{
    public class ShipSelectionUI : MonoBehaviour
    {
        int currentSelectedShip = 0;
        bool spawnEvenIfNotPossible = true;
        public LaunchPadSpawn spawner;
        public ShipVisualizationUI shipVisualization;

        public Text middleDisplayText;
        public InteractReceiver nextShipButton;
        public InteractReceiver previousShipButton;
        public InteractReceiver spawnShipButton;

        public void Start() 
        {
            nextShipButton.OnReleaseInteract += OnNextPageInteract;
            previousShipButton.OnReleaseInteract += OnPreviousPageInteract;
            spawnShipButton.OnReleaseInteract += OnSelectInteract;

            WriteTextOnDisplay("Welcome :)");
            if (currentSelectedShip < ShipSpawnerManager.ShipAmount())
            {
                shipVisualization.ChangeShip(ShipSpawnerManager.GetShipData(currentSelectedShip));
            }
        }
        public void OnDestroy() 
        {
            nextShipButton.OnReleaseInteract -= OnNextPageInteract;
            previousShipButton.OnReleaseInteract -= OnPreviousPageInteract;
            spawnShipButton.OnReleaseInteract -= OnSelectInteract;
        }

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
        public void OnSelectInteract()
        {
            spawnShipButton.ResetInteraction();

            ShipData data = ShipSpawnerManager.GetShipData(currentSelectedShip);
            if(data.prefab == null) {
                WriteTextOnDisplay($"No Prefab for {data.name}");
                return;
            }
            else if(spawner.SpawnShip(data.prefab, spawnEvenIfNotPossible)){
                WriteTextOnDisplay($"{data.name} Spawned!");
                return;
            }
            WriteTextOnDisplay($"Couldn't spawn {data.name}");
        }
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
