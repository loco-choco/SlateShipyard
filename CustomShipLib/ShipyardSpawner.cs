using SlateShipyard.ShipSpawner;
using SlateShipyard.ShipSpawner.RampUI;
using SlateShipyard.ShipSpawner.SelectionUI;

using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard
{
    public static class ShipyardSpawner 
    {
        public static GameObject SpawnShipyard(Transform transform, Vector3 position, Quaternion rotation)
        {
            Transform t = GameObject.Instantiate(SlateShipyard.defaultShipSpawnerPrefab, position, rotation, transform).transform;

            //SpawnPosition
            LaunchPadSpawn spawner = t.GetChild(0).gameObject.AddComponent<LaunchPadSpawn>();
            ////UI Stuff-----
            Transform screen = t.GetChild(2).GetChild(0);
            //Buttons
            GameObject selectButton = screen.GetChild(0).gameObject;
            InteractReceiver select = selectButton.AddComponent<InteractReceiver>();
            select.ChangePrompt("select");

            GameObject nextButton = screen.GetChild(1).gameObject;
            InteractReceiver next = nextButton.AddComponent<InteractReceiver>();
            next.ChangePrompt("next");

            GameObject previousButton = screen.GetChild(2).gameObject;
            InteractReceiver previous = previousButton.AddComponent<InteractReceiver>();
            previous.ChangePrompt("previous");

            //Ship Data
            Text shipName = screen.GetChild(3).GetChild(0).gameObject.GetComponent<Text>();

            ShipVisualizationUI visual = screen.GetChild(4).gameObject.AddComponent<ShipVisualizationUI>();
            visual.shipName = shipName;

            //Warning
            Text warning = screen.GetChild(4).gameObject.GetComponent<Text>();

            ShipSelectionUI selection = screen.gameObject.AddComponent<ShipSelectionUI>();
            selection.spawner = spawner;
            selection.shipVisualization = visual;

            selection.middleDisplayText = warning;
            selection.nextShipButton = next;
            selection.previousShipButton = previous;
            selection.spawnShipButton = select;
            ////---------

            //Ramp
            Transform ramp = t.GetChild(4);
            Transform rampUIScreen = t.GetChild(5).GetChild(0);

            RampAngleUI rampAnlgleUI = ramp.gameObject.AddComponent<RampAngleUI>();

            //Buttons
            GameObject decreaseButton = rampUIScreen.GetChild(0).gameObject;
            InteractReceiver decrease = decreaseButton.AddComponent<InteractReceiver>();
            decrease.ChangePrompt("decrease");

            Text angleValue = rampUIScreen.GetChild(1).gameObject.GetComponent<Text>();

            GameObject increaseButton = rampUIScreen.GetChild(2).gameObject;
            InteractReceiver increase = increaseButton.AddComponent<InteractReceiver>();
            increase.ChangePrompt("increase");

            rampAnlgleUI.increaseAngle = increase;
            rampAnlgleUI.decreaseAngle = decrease;
            rampAnlgleUI.angleDisplayText = angleValue;

            return t.gameObject;
        }

    }
}
