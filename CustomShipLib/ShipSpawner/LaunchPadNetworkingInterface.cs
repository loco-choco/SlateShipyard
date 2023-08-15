using SlateShipyard.NetworkingInterface;
using SlateShipyard.ShipSpawner.DestroyShipsUI;
using SlateShipyard.ShipSpawner.RampUI;
using SlateShipyard.ShipSpawner.SelectionUI;
using UnityEngine;

namespace SlateShipyard.ShipSpawner
{
    public class LaunchPadNetworkingInterface : SimpleNetworkingInterface
    {
        public ShipSelectionUI ShipSelectionUI;
        public RampAngleUI RampAngleUI;
        public DestroyAndResetShipsUI DestroyAndResetShipsUI;

        [SyncableProperty]
        public int SelectedShip
        {
            get => ShipSelectionUI.currentSelectedShip;
            set
            {
                ShipSelectionUI.currentSelectedShip = value;
                if (IsPuppet)
                    ShipSelectionUI.UpdateSelection();
            }
        }

        [NetworkableMethod]
        public void SelectNextShip(bool previous)
        {
            if (previous)
                ShipSelectionUI.OnPreviousPageInteract();
            else
                ShipSelectionUI.OnNextPageInteract();
        }

        [NetworkableMethod]
        public void SpawnShip() => ShipSelectionUI.OnSelectInteract();

        [SyncableProperty]
        public float RampAngle
        {
            get => RampAngleUI.targetAngle;
            set => RampAngleUI.targetAngle = value;
        }

        [NetworkableMethod]
        public void ChangeRampAngle(bool decrease)
        {
            if (decrease)
                RampAngleUI.OnAngleDecrease();
            else
                RampAngleUI.OnAngleIncrease();
        }

        [NetworkableMethod]
        public void DestroyAllShips() => DestroyAndResetShipsUI.OnDestroyAllShips();

        [NetworkableMethod]
        public void ResetLastShip() => DestroyAndResetShipsUI.OnResetLastShip();
    }
}