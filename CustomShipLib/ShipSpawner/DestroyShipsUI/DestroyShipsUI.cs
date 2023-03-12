﻿using UnityEngine;
using UnityEngine.UI;

namespace SlateShipyard.ShipSpawner.RampUI
{
    //! UI element to destroy all spawned ships by the shipyard.
    public class DestroyAndResetShipsUI : MonoBehaviour
    {
        public InteractReceiver destroyAllShips; //!< The button to destroy all ships spawned by this shipyard.
        public InteractReceiver resetLastShip; //!< The button to reset last spawned ship.
        public LaunchPadSpawn launchPadSpawn; //!< The lauchpad this button is from.
        
        public LaunchPadNetworkingInterface launchPadNetworkingInterface;
        
        //! The Start method.
        public void Start()
        {
            destroyAllShips.ChangePrompt("destroy all ships");
            resetLastShip.ChangePrompt("reset last ship");

            destroyAllShips.OnReleaseInteract += OnDestroyAllShips;
            resetLastShip.OnReleaseInteract += OnResetLastShip;
        }

        //! The OnDestroy method.
        public void OnDestroy()
        {
            destroyAllShips.OnReleaseInteract -= OnDestroyAllShips;
            resetLastShip.OnReleaseInteract -= OnResetLastShip;
        }

        //! Method called the destroy spawned ships by the shipyard. 
        public void OnDestroyAllShips()
        {
            if (launchPadNetworkingInterface.IsPuppet)
            {
                SlateShipyard.NetworkingInterface.InvokeMethod(
                    launchPadNetworkingInterface,
                    nameof(LaunchPadNetworkingInterface.DestroyAllShips));
                return;
            }
            
            launchPadSpawn.DestroyAllSpawnedShip();
            destroyAllShips.ResetInteraction();
        }

        //! Method called reset last spawned ship. 
        public void OnResetLastShip()
        {
            if (launchPadNetworkingInterface.IsPuppet)
            {
                SlateShipyard.NetworkingInterface.InvokeMethod(
                    launchPadNetworkingInterface,
                    nameof(LaunchPadNetworkingInterface.ResetLastShip));
                return;
            }
            
            launchPadSpawn.ResetLatestSpawnedShip();
            resetLastShip.ResetInteraction();
        }
    }
}
