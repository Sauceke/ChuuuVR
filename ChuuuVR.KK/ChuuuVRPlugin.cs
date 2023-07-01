using BepInEx;
using KKAPI;
using KKAPI.MainGame;
using UnityEngine.SceneManagement;

namespace ChuuuVR
{
    [BepInPlugin(GUID, "ChuuuVR.KK", Version)]
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]
    internal class ChuuuVRPlugin : BaseUnityPlugin
    {
        private const string GUID = "Sauceke.ChuuuVR";
        private const string Version = "1.0.0";

        private void Start()
        {
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (scene.name == "VRHScene")
                {
                    GameAPI.RegisterExtraBehaviour<KissController>(GUID);
                }
            };
        }
    }
}
