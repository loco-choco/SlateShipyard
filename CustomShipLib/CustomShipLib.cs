using UnityEngine;
using UnityEngine.SceneManagement;
using OWML.ModHelper;
using OWML.Common;
using HarmonyLib;
using SlateShipyard.VanishObjects;
using System;
using System.Collections;

namespace SlateShipyard
{
    public class SlateShipyard : ModBehaviour
    {
        public static IModHelper modHelper;
        public static GameObject defaultShipSpawnerPrefab;

        private static bool spawnDefaultShipYard = true;
        private Vector3 defaultShipYardLocalPosition = new Vector3(-13.4418f, -75.4162f, 218.6133f);
        private Vector3 defaultShipYardLocalRotation = new Vector3(358.9226f, 86.607f, 106.1305f);
        public static void DontSpawnDefaultShipyard() 
        {
            spawnDefaultShipYard = false;
        }
        public static GameObject SpawnShipyard(Transform t, Vector3 localPosition, Vector3 localRotation)
        {
            var go = ShipyardSpawner.SpawnShipyard(t.GetAttachedOWRigidbody().transform, Vector3.zero, Quaternion.identity);
            go.transform.localEulerAngles = localRotation;
            go.transform.localPosition = localPosition;
            return go;
        }
        private void Awake()
        {
            Harmony harmonyInstance = new Harmony("com.locochoco.plugin.customshiplib");

            SceneManager.sceneLoaded += SceneLoading_OnSceneLoad;
            harmonyInstance.PatchAll(typeof(VanishVolumesPatches));
        }
        private void SceneLoading_OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            VanishVolumesPatches.CheckControlledVanishObjectVolumeList();

            StartCoroutine("SpawnShipyardDelay");            
        }

        private IEnumerator SpawnShipyardDelay() 
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            AstroObject th = Locator.GetAstroObject(AstroObject.Name.TimberHearth);
            if (th != null && spawnDefaultShipYard)
            {
                SpawnShipyard(th.transform, defaultShipYardLocalPosition, defaultShipYardLocalRotation);
            }
        }

        public void Update()
        {
            if (Event.current != null)
            {
                if (Event.current.Equals(Event.KeyboardEvent("f8")))
                {
                    var t = Locator.GetPlayerCamera().transform;
                    if (Physics.Raycast(t.position, t.forward, out var info, 100f))
                    {
                        ModHelper.Console.WriteLine($"Local Pos: {info.rigidbody.transform.InverseTransformPoint(info.point)}" +
                            $" Local Rot: {info.rigidbody.transform.InverseTransformRotation(Quaternion.LookRotation(Locator.GetPlayerTransform().forward, info.normal))}");
                    }
                }
            }
        }
        private void Start()
        {
            AssetBundle bundle = ModHelper.Assets.LoadBundle("AssetBundles/shipspawner");
            defaultShipSpawnerPrefab = bundle.LoadAsset<GameObject>("DefaultShipSpawner.prefab");
        }
    }
}
