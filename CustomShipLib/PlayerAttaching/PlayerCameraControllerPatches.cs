using HarmonyLib;
using UnityEngine;

namespace SlateShipyard.PlayerAttaching
{
    public static class PlayerCameraControllerPatches
    {
        private static bool AllowFreeLook(PlayerCameraController cameraController)
        {
            PlayerCharacterController characterController = cameraController._characterController;
            if (characterController != null && characterController.transform != null && characterController.transform.parent != null) //Means we are attached to something (I hope), if it is the ship then it won't have FreeLookablePlayerAttachPoint ;)
            {
                FreeLookablePlayerAttachPoint freeLook = characterController.transform.parent.GetComponent<FreeLookablePlayerAttachPoint>();
                if (freeLook != null)
                {
                    return freeLook.AllowFreeLook.Invoke();
                }
            }
            if (cameraController._shipController != null && cameraController._shipController.AllowFreeLook())
            {
                return true;
            }
            return false;
        }

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

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerCameraController), nameof(PlayerCameraController.Update))]
        static bool UpdatePrefix(PlayerCameraController __instance)
        {
            if (AllowFreeLook(__instance) && OWInput.IsNewlyReleased(InputLibrary.freeLook, InputMode.All))
            {
                __instance.CenterCameraOverSeconds(0.33f, true);
            }
            if (OWTime.IsPaused(OWTime.PauseType.Reading))
            {
                __instance.UpdateCamera(Time.unscaledDeltaTime);
            }
            return false;
        }
        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerCameraController), nameof(PlayerCameraController.UpdateInput))]
        static bool UpdateInputPrefix(float deltaTime, PlayerCameraController __instance)
        {
            bool flag = AllowFreeLook(__instance) && OWInput.IsPressed(InputLibrary.freeLook, 0f);
            bool flag2 = OWInput.IsInputMode(InputMode.Character | InputMode.ScopeZoom | InputMode.NomaiRemoteCam | InputMode.PatchingSuit);
            if (__instance._isSnapping || __instance._isLockedOn || (PlayerState.InZeroG() && PlayerState.IsWearingSuit()) || (!flag2 && !flag))
            {
                return false;
            }
            bool flag3 = Locator.GetAlarmSequenceController() != null && Locator.GetAlarmSequenceController().IsAlarmWakingPlayer();
            Vector2 vector = Vector2.one;
            vector *= ((__instance._zoomed || flag3) ? PlayerCameraController.ZOOM_SCALAR : 1f);
            vector *= __instance._playerCamera.fieldOfView / __instance._initFOV;
            if (Time.timeScale > 1f)
            {
                vector /= Time.timeScale;
            }
            if (flag)
            {
                Vector2 axisValue = OWInput.GetAxisValue(InputLibrary.look, InputMode.All);
                __instance._degreesX += axisValue.x * 180f * vector.x * deltaTime;
                __instance._degreesY += axisValue.y * 180f * vector.y * deltaTime;
                return false;
            }
            float num = OWInput.UsingGamepad() ? PlayerCameraController.GAMEPAD_LOOK_RATE_Y : PlayerCameraController.LOOK_RATE;
            __instance._degreesY += OWInput.GetAxisValue(InputLibrary.look, InputMode.All).y * num * vector.y * deltaTime;

            return false;
        }
    }
}
