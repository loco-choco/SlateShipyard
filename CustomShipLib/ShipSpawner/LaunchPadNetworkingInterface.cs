using SlateShipyard.NetworkingInterface;
using SlateShipyard.ShipSpawner.RampUI;
using SlateShipyard.ShipSpawner.SelectionUI;

namespace SlateShipyard.ShipSpawner;

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
            ShipSelectionUI.UpdateSelection();
        }
    }

    [NetworkableMethod]
    public void SelectNextShip(bool previous = false)
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
    public void DestroyAllShips() => DestroyAndResetShipsUI.OnDestroyAllShips();
    
    [NetworkableMethod]
    public void ResetLastShip() => DestroyAndResetShipsUI.OnResetLastShip();
}