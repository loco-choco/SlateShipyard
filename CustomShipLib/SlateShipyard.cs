using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using OWML.ModHelper;
using OWML.Common;

using HarmonyLib;

using SlateShipyard.VanishObjects;
using SlateShipyard.PlayerAttaching;
using SlateShipyard.NetworkingInterface;

namespace SlateShipyard
{
    //! Main class of Slate's Shipyard.
    public class SlateShipyard : ModBehaviour
    {
        //! Returns the modHelper from OWML.
        public static IModHelper modHelper;
        //! Returns prefab of the default shipyards.
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
                localPosition = new Vector3(-19.2676f, -72.6158f, 218.9678f),
                localRotation = new Vector3(3.2138f, 87.9346f, 105.9461f),
                astro = AstroObject.Name.TimberHearth
            },
            new DefaultShipyardSpawnPositions()
            {
                localPosition = new Vector3(-9.1549f, -119.2572f, 224.7264f),
                localRotation = new Vector3(58.8809f, 147.8917f, 151.7245f),
                astro = AstroObject.Name.TimberHearth
            },
            new DefaultShipyardSpawnPositions()
            {
                localPosition = new Vector3(-47.0739f, -309.5077f, 30.6901f),
                localRotation = new Vector3(7.843f, 130.0688f, 182.5558f),
                astro = AstroObject.Name.BrittleHollow
            },
            new DefaultShipyardSpawnPositions()
            {
                localPosition = new Vector3(-4.9911f, -150.7467f, 68.4886f),
                localRotation = new Vector3(19.7716f, 206.9409f, 190.7944f),
                astro = AstroObject.Name.CaveTwin
            },
        };
        //! Returns the API to OWML.
        public override object GetApi() => new SlateShipyardAPI();
        //! Dissables the spawn of the default shipyards.
        public static void DontSpawnDefaultShipyard() 
        {
            spawnDefaultShipYard = false;
        }
        //! Spawns a default shipyard giving the local position, rotation and parent transform.
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


        //! Access to the current NetworkingInterface.
        public static NetworkingInterface.NetworkingInterface NetworkingInterface = new EmptyNetworkingInterface();
        //! Adds networking interface from multiplayer addons.
        public static void SetNetworkingInterface(NetworkingInterface.NetworkingInterface networkingInterface)
        {
            NetworkingInterface = networkingInterface;
        }
    }
}
