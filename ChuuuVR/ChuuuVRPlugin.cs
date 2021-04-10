using BepInEx;
using BepInEx.Logging;
using KKAPI;
using KKAPI.Chara;
using UnityEngine.SceneManagement;

namespace ChuuuVR
{
    [BepInPlugin(GUID, "ChuuuVR", Version)]
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]
    internal class ChuuuVRPlugin : BaseUnityPlugin
    {
        public const string GUID = "Sauceke.ChuuuVR";
        public const string Version = "1.0.0";

        private void Start()
        {
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (scene.name == "VRHScene")
                {
                    CharacterApi.RegisterExtraBehaviour<KissController>(GUID);
                    Hooks.InstallHooks();
                }
            };
        }
    }
}
