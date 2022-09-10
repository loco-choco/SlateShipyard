using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace SlateShipyard.VanishObjects
{
    public static class VanishVolumesPatches
    {
        public static void DoPatches(Harmony harmonyInstance)
        {         
            #region VanishVolume
            MethodInfo VanishVolumeFixedUpdate = AccessTools.Method(typeof(VanishVolume), nameof(VanishVolume.FixedUpdate));
            MethodInfo VanishVolumeOnTriggerEnter = AccessTools.Method(typeof(VanishVolume), nameof(VanishVolume.OnTriggerEnter));

            HarmonyMethod fixedUpdatePrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.FixedUpdatePrefix));
            HarmonyMethod onTriggerEnterPrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.OnTriggerEnterPrefix));

            harmonyInstance.Patch(VanishVolumeFixedUpdate, prefix: fixedUpdatePrefix);
            harmonyInstance.Patch(VanishVolumeOnTriggerEnter, prefix: onTriggerEnterPrefix);
            #endregion

            #region DestructionVolume
            MethodInfo DestructionVolumeVanish = AccessTools.Method(typeof(DestructionVolume), nameof(DestructionVolume.Vanish));
            HarmonyMethod destructionVanishPrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.DestructionVanishPrefix));
            harmonyInstance.Patch(DestructionVolumeVanish, prefix: destructionVanishPrefix);
            #endregion

            #region SupernovaDestructionVolume
            MethodInfo SupernovaDestructionVolumeVanish = AccessTools.Method(typeof(SupernovaDestructionVolume), nameof(SupernovaDestructionVolume.Vanish));
            HarmonyMethod supernovadestructionVanishPrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.SupernovaDestructionVanishPrefix));
            harmonyInstance.Patch(SupernovaDestructionVolumeVanish, prefix: supernovadestructionVanishPrefix);
            #endregion

            #region BlackHoleVolume
            MethodInfo BlackHoleVolumeVanish = AccessTools.Method(typeof(BlackHoleVolume), nameof(BlackHoleVolume.Vanish));
            MethodInfo BlackHoleVolumeVanishPlayer = AccessTools.Method(typeof(BlackHoleVolume), nameof(BlackHoleVolume.VanishPlayer));

            HarmonyMethod blackHoleVanishPrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.BlackHoleVanishPrefix));
            HarmonyMethod vanishPlayerPrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.VanishPlayerPrefix));

            harmonyInstance.Patch(BlackHoleVolumeVanish, prefix: blackHoleVanishPrefix);
            harmonyInstance.Patch(BlackHoleVolumeVanishPlayer, prefix: vanishPlayerPrefix);
            #endregion

            #region WhiteHoleVolume
            MethodInfo WhiteHoleVolumeReceiveWarpedBody = AccessTools.Method(typeof(WhiteHoleVolume), nameof(WhiteHoleVolume.ReceiveWarpedBody));
            MethodInfo WhiteHoleVolumeAddToGrowQueue = AccessTools.Method(typeof(WhiteHoleVolume), nameof(WhiteHoleVolume.AddToGrowQueue));

            HarmonyMethod receiveWarpedBodyPrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.ReceiveWarpedBodyPrefix));
            HarmonyMethod addToGrowQueuePrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.AddToGrowQueuePrefix));

            harmonyInstance.Patch(WhiteHoleVolumeReceiveWarpedBody, prefix: receiveWarpedBodyPrefix);
            harmonyInstance.Patch(WhiteHoleVolumeAddToGrowQueue, prefix: addToGrowQueuePrefix);
            #endregion

            #region TimeLoopBlackHoleVolume
            MethodInfo TimeLoopBlackHoleVolumeVanish = AccessTools.Method(typeof(TimeLoopBlackHoleVolume), nameof(TimeLoopBlackHoleVolume.Vanish));

            HarmonyMethod timeLoopVanishPrefix = new HarmonyMethod(typeof(VanishVolumesPatches), nameof(VanishVolumesPatches.TimeLoopBlackHoleVanishPrefix));

            harmonyInstance.Patch(TimeLoopBlackHoleVolumeVanish, prefix: timeLoopVanishPrefix);
            #endregion
        }

        static bool DestructionVanishPrefix(OWRigidbody bodyToVanish, RelativeLocationData entryLocation, DestructionVolume __instance)
        {
            var vanishableObjectComponent = bodyToVanish.GetComponentInChildren<ControlledVanishObject>();
            if (vanishableObjectComponent != null)
            {
                return vanishableObjectComponent.OnDestructionVanish(__instance);
            }

            return true;
        }
        static bool SupernovaDestructionVanishPrefix(OWRigidbody bodyToVanish, RelativeLocationData entryLocation, SupernovaDestructionVolume __instance)
        {
            var vanishableObjectComponent = bodyToVanish.GetComponentInChildren<ControlledVanishObject>();
            if (vanishableObjectComponent != null)
            {
                return vanishableObjectComponent.OnSupernovaDestructionVanish(__instance);
            }

            return true;
        }
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
        static bool AddToGrowQueuePrefix(OWRigidbody bodyToGrow)
        {
            var vanishableObjectComponent = bodyToGrow.GetComponent<ControlledVanishObject>();
            if (vanishableObjectComponent == null)
                vanishableObjectComponent = bodyToGrow.GetComponentInChildren<ControlledVanishObject>();

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
