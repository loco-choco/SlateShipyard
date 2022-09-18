using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using OWML.ModHelper;
using OWML.Common;

using HarmonyLib;

using SlateShipyard.VanishObjects;
using SlateShipyard.PlayerAttaching;

namespace SlateShipyard
{
    public class SlateShipyard : ModBehaviour
    {
        public static IModHelper modHelper;
        public static GameObject defaultShipSpawnerPrefab;

        private static bool spawnDefaultShipYard = true;

        private struct DefaultShipyardSpawnPositions 
        {
            public Vector3 localPosition;
            public Vector3 localRotation;
            public AstroObject.Name astro;
        }

        private DefaultShipyardSpawnPositions[] defaultSpawnLocations =
        {
            new DefaultShipyardSpawnPositions()
            {
                localPosition = new Vector3(-16.9654f, -71.9921f, 219.14f),
                localRotation = new Vector3(3.2138f, 88.3628f, 105.9461f),
                astro = AstroObject.Name.TimberHearth
            },
            new DefaultShipyardSpawnPositions()
            {
                localPosition = new Vector3(0.9f, -121.4f, 224.5f),
                localRotation = new Vector3(59.5181f, 202.7799f,197.9099f),
                astro = AstroObject.Name.TimberHearth
            },
            new DefaultShipyardSpawnPositions()
            {
                localPosition = new Vector3(-47.1f, -309.7f, 30.7f),
                localRotation = new Vector3(8.5875f, 163.4293f, 188.3791f),
                astro = AstroObject.Name.BrittleHollow
            },
            new DefaultShipyardSpawnPositions()
            {
                localPosition = new Vector3(-5.0f, -151.0f, 68.6f),
                localRotation = new Vector3(20.6541f, 206.9409f, 193.8155f),
                astro = AstroObject.Name.CaveTwin
            },
        };

        public override object GetApi() => new SlateShipyardAPI();

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
            harmonyInstance.PatchAll(typeof(VanishVolumesPatches));
            harmonyInstance.PatchAll(typeof(PlayerCameraControllerPatches));
            SceneManager.sceneLoaded += SceneLoading_OnSceneLoad;
        }
        private void SceneLoading_OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine("SpawnShipyardDelay");            
        }

        private IEnumerator SpawnShipyardDelay() 
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            if (spawnDefaultShipYard) {
                for (int i = 0; i < defaultSpawnLocations.Length; i++){                    
                    AstroObject astro = Locator.GetAstroObject(defaultSpawnLocations[i].astro);
                    if (astro != null && spawnDefaultShipYard){
                        SpawnShipyard(astro.transform, defaultSpawnLocations[i].localPosition, defaultSpawnLocations[i].localRotation);
                    }
                } 
            }
        }

        //For Finding places to spawn the shipyards
        //public void Update()
        //{
        //    if (Event.current != null)
        //    {
        //        if (Event.current.Equals(Event.KeyboardEvent("f8")))
        //        {
        //            var t = Locator.GetPlayerCamera().transform;
        //            if (Physics.Raycast(t.position, t.forward, out var info, 100f))
        //            {
        //                ModHelper.Console.WriteLine($"Local Pos: {info.rigidbody.transform.InverseTransformPoint(info.point)}" +
        //                    $" Local Rot: {info.rigidbody.transform.InverseTransformRotation(Quaternion.LookRotation(Locator.GetPlayerTransform().forward, info.normal))}");
        //            }
        //        }
        //    }
        //}
        private void Start()
        {
            modHelper = ModHelper;
            AssetBundle bundle = ModHelper.Assets.LoadBundle("AssetBundles/shipspawner");
            defaultShipSpawnerPrefab = bundle.LoadAsset<GameObject>("DefaultShipSpawner.prefab");
        }
    }
}
