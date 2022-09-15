# Slate's Shipyard

 ![Slate's Shipyard](pictures/slatesShipyard.png)

This is a mod which allows the player to spawn custom ships on "shipyards" scattered on the solar system (by default). It doesn't come with default ships, but allows for the making of them and an easier set up.

## API Usage
To use the API add this as a file in your project:
```Csharp
using UnityEngine;
public class ISlateShipyardAPI 
{
  public GameObject SpawnShipyard(Transform t, Vector3 localPosition, Vector3 localRotation);
  public void DontSpawnDefaultShipyard();
}
```
And do `ModHelper.Interaction.TryGetModApi<IGizmosAPI>("Locochoco.SlateShipyard");` to be able to access the api calls. 

With `SpawnShipyard` you can spawn the default shipyard anywhere you want (call this when the transform that is the parent is already loaded), this is specially usefull for New Horizons addons that delete Timber Hearth but want for players to spawn custom ships. 

And with `DontSpawnDefaultShipyard` you can make the mod not spawn the default spawner on Timber Hearth, you only need to call this once, and it will no longer attempt to spawn on the default location.
