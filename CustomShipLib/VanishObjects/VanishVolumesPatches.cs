using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;

namespace SlateShipyard.VanishObjects
{
    public struct ControlledVanishObjectData
    {
        public ControlledVanishObject ControlledVanishObject;
        public RelativeLocationData RelativeLocationData;
    }
    public class VanishVolumesExtraData : MonoBehaviour 
    {
        public List<ControlledVanishObjectData> VanishObjectData = new List<ControlledVanishObjectData>();
    }
    public static class VanishVolumesPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(DestructionVolume), nameof(DestructionVolume.Vanish))]
        static bool DestructionVanishPrefix(OWRigidbody bodyToVanish, RelativeLocationData entryLocation, DestructionVolume __instance)
        {
            var vanishableObjectComponent = bodyToVanish.GetComponentInChildren<ControlledVanishObject>();
            if (vanishableObjectComponent != null)
            {
                return vanishableObjectComponent.OnDestructionVanish(__instance);
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SupernovaDestructionVolume), nameof(SupernovaDestructionVolume.Vanish))]
        static bool SupernovaDestructionVanishPrefix(OWRigidbody bodyToVanish, RelativeLocationData entryLocation, SupernovaDestructionVolume __instance)
        {
            var vanishableObjectComponent = bodyToVanish.GetComponentInChildren<ControlledVanishObject>();
            if (vanishableObjectComponent != null)
            {
                return vanishableObjectComponent.OnSupernovaDestructionVanish(__instance);
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BlackHoleVolume), nameof(BlackHoleVolume.Vanish))]
        static bool BlackHoleVanishPrefix(OWRigidbody bodyToVanish, RelativeLocationData entryLocation, BlackHoleVolume __instance)
        {
            var vanishableObjectComponent = bodyToVanish.GetComponent<ControlledVanishObject>();
            if (vanishableObjectComponent == null)
                vanishableObjectComponent = bodyToVanish.GetComponentInChildren<ControlledVanishObject>();

            if (vanishableObjectComponent != null)
            {
                return vanishableObjectComponent.OnBlackHoleVanish(__instance, entryLocation);
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(TimeLoopBlackHoleVolume), nameof(TimeLoopBlackHoleVolume.Vanish))]
        static bool TimeLoopBlackHoleVanishPrefix(OWRigidbody bodyToVanish, RelativeLocationData entryLocation, TimeLoopBlackHoleVolume __instance)
        {
            var vanishableObjectComponent = bodyToVanish.GetComponent<ControlledVanishObject>();
            if (vanishableObjectComponent == null)
            {
                vanishableObjectComponent = bodyToVanish.GetComponentInChildren<ControlledVanishObject>();
            }

            if (vanishableObjectComponent != null)
            {
                return vanishableObjectComponent.OnTimeLoopBlackHoleVanish(__instance);
            }

            return true;
        }
        public delegate bool ConditionsForPlayerToWarp();
        public static event ConditionsForPlayerToWarp OnConditionsForPlayerToWarp;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BlackHoleVolume), nameof(BlackHoleVolume.VanishPlayer))]
        static bool VanishPlayerPrefix()
        {
            bool condition = true;
            if (OnConditionsForPlayerToWarp != null)
            {
                foreach (ConditionsForPlayerToWarp d in OnConditionsForPlayerToWarp.GetInvocationList())
                {
                    if (d != null)
                    {
                        condition &= d.Invoke();
                    }
                }
            }
            return condition;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(WhiteHoleVolume), nameof(WhiteHoleVolume.ReceiveWarpedBody))]
        static bool ReceiveWarpedBodyPrefix(OWRigidbody warpedBody, RelativeLocationData entryData, WhiteHoleVolume __instance)
        {
            var vanishableObjectComponent = warpedBody.GetComponent<ControlledVanishObject>();
            if (vanishableObjectComponent == null)
                vanishableObjectComponent = warpedBody.GetComponentInChildren<ControlledVanishObject>();

            if (vanishableObjectComponent != null)
            {
                return vanishableObjectComponent.OnWhiteHoleReceiveWarped(__instance, entryData);
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(WhiteHoleVolume), nameof(WhiteHoleVolume.AddToGrowQueue))]
        static bool AddToGrowQueuePrefix(OWRigidbody bodyToGrow)
        {
            var vanishableObjectComponent = bodyToGrow.GetComponent<ControlledVanishObject>();
            if (vanishableObjectComponent == null)
                vanishableObjectComponent = bodyToGrow.GetComponentInChildren<ControlledVanishObject>();

            if (vanishableObjectComponent != null)
                return vanishableObjectComponent.DestroyComponentsOnGrow;

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(WhiteHoleVolume), nameof(WhiteHoleVolume.SpawnImmediately))]
        static bool SpawnImmediatelyPrefix(OWRigidbody overrideBody, RelativeLocationData entryData, WhiteHoleVolume __instance)
        {
            var vanishableObjectComponent = overrideBody.GetComponent<ControlledVanishObject>();
            if (vanishableObjectComponent == null)
                vanishableObjectComponent = overrideBody.GetComponentInChildren<ControlledVanishObject>();

            if (vanishableObjectComponent != null)
            {
                __instance.MoveBodyToOverrideExitLocation(overrideBody, entryData);

                vanishableObjectComponent.OnWhiteHoleSpawnImmediately(__instance, entryData, out bool playerPassedThroughWarp);
                if (playerPassedThroughWarp)
                {
                    Locator.GetPlayerAudioController().PlayPlayerSingularityTransit();
                    for (int i = 0; i < __instance._airlocksToOpen.Length; i++)
                    {
                        __instance._airlocksToOpen[i].ResetToOpenState();
                    }
                }
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(VanishVolume), nameof(VanishVolume.OnTriggerEnter))]
        static bool OnTriggerEnterPrefix(Collider hitCollider, VanishVolume __instance)
        {
            if (hitCollider.attachedRigidbody != null)
            {
                var vanishableObjectComponent = hitCollider.attachedRigidbody.GetComponent<ControlledVanishObject>();
                if (vanishableObjectComponent == null)
                {
                    vanishableObjectComponent = hitCollider.attachedRigidbody.GetComponentInChildren<ControlledVanishObject>();
                }
                if (vanishableObjectComponent != null)
                {
                    ControlledVanishObjectData data = new ControlledVanishObjectData()
                    {
                        ControlledVanishObject = vanishableObjectComponent,
                        RelativeLocationData = new RelativeLocationData(hitCollider.GetAttachedOWRigidbody(), __instance.transform)
                    };

                    VanishVolumesExtraData extraData = __instance.gameObject.GetAddComponent<VanishVolumesExtraData>();
                    extraData.VanishObjectData.Add(data);

                    return false;
                }
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(VanishVolume), nameof(VanishVolume.FixedUpdate))]
        static void FixedUpdatePrefix(VanishVolume __instance)
        {
            VanishVolumesExtraData extraData = __instance.GetComponent<VanishVolumesExtraData>();
            if (extraData != null)
            {
                foreach (var vanishObjectData in extraData.VanishObjectData)
                {
                    __instance.Vanish(vanishObjectData.ControlledVanishObject.GetAttachedOWRigidbody(), vanishObjectData.RelativeLocationData);
                }
                extraData.VanishObjectData.Clear();
            }
        }
    }
}
