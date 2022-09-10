using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace SlateShipyard.VanishObjects
{
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
            foreach (var d in OnConditionsForPlayerToWarp.GetInvocationList())
            {
                if (d != null)
                {
                    condition &= ((ConditionsForPlayerToWarp)d).Invoke();
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
        static bool SpawnImmediatelyPrefix(OWRigidbody overrideBody, RelativeLocationData entryData)
        {
            var vanishableObjectComponent = overrideBody.GetComponent<ControlledVanishObject>();
            if (vanishableObjectComponent == null)
                vanishableObjectComponent = overrideBody.GetComponentInChildren<ControlledVanishObject>();

            if (vanishableObjectComponent != null)
                return vanishableObjectComponent.DestroyComponentsOnGrow;

            return true;
        }

        private static Dictionary<VanishVolume, List<ControlledVanishObjectData>> controlledVanishObjectVolumeList = new Dictionary<VanishVolume, List<ControlledVanishObjectData>>();
        private struct ControlledVanishObjectData 
        {
            public ControlledVanishObject ControlledVanishObject;
            public RelativeLocationData RelativeLocationData;
        }
        public static void CheckControlledVanishObjectVolumeList()
        {
            var keys = controlledVanishObjectVolumeList.Keys;
            List<VanishVolume> volumesToRemove = new List<VanishVolume>();
            foreach (var key in keys){
                if (key == null) {
                    volumesToRemove.Add(key);
                }
            }
            foreach(var vol in volumesToRemove){
                controlledVanishObjectVolumeList.Remove(vol);
            }
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
                    if (controlledVanishObjectVolumeList.TryGetValue(__instance, out var list))
                    {                        
                        list.Add(data);
                    }
                    else
                    {
                        controlledVanishObjectVolumeList.Add(__instance, new List<ControlledVanishObjectData> { data });
                    }
                    return false;
                }
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(VanishVolume), nameof(VanishVolume.FixedUpdate))]
        static void FixedUpdatePrefix(VanishVolume __instance)
        {
            if (controlledVanishObjectVolumeList.TryGetValue(__instance, out var list))
            {
                foreach (var vanishObjectData in list)
                {
                    __instance.Vanish(vanishObjectData.ControlledVanishObject.GetAttachedOWRigidbody(), vanishObjectData.RelativeLocationData);
                }
                list.Clear();
            }
        }
    }
}
