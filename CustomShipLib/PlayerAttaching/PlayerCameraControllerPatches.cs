using HarmonyLib;
using UnityEngine;

namespace SlateShipyard.PlayerAttaching
{
    public static class PlayerCameraControllerPatches
    {
        public static class VanishVolumesPatches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(PlayerCameraController), nameof(PlayerCameraController.UpdateRotation))]
            static bool UpdateRotationPrefix(PlayerCameraController __instance)
            {
				__instance._degreesX %= 360f;
				__instance._degreesY %= 360f;

				if (!__instance._isSnapping)
				{
					if (AllowFreeLook(__instance) && OWInput.IsPressed(InputLibrary.freeLook, 0f))
					{
						__instance._degreesX = Mathf.Clamp(__instance._degreesX, -60f, 60f);
						__instance._degreesY = Mathf.Clamp(__instance._degreesY, -35f, 80f);
					}
					else
					{
						__instance._degreesX = 0f;
						__instance._degreesY = Mathf.Clamp(__instance._degreesY, -80f, 80f);
					}
				}

				__instance._rotationX = Quaternion.AngleAxis(__instance._degreesX, Vector3.up);
				__instance._rotationY = Quaternion.AngleAxis(__instance._degreesY, -Vector3.right);
				Quaternion localRotation = __instance._rotationX * __instance._rotationY * Quaternion.identity;
				__instance._playerCamera.transform.localRotation = localRotation;


				return false;
            }

			private static bool AllowFreeLook(PlayerCameraController cameraController) 
			{
                if (cameraController._shipController != null && cameraController._shipController.AllowFreeLook()) 
				{
					return true;
				}
				if(cameraController.transform.parent != null) //Means we are attached to something (I hope)
                {
					FreeLookablePlayerAttachPoint freeLook = cameraController.transform.parent.GetComponent<FreeLookablePlayerAttachPoint>();
					if (freeLook != null)
					{
						return  freeLook.AllowFreeLook.Invoke();
					}
				}
				return false;
			}
        }
    }
}
