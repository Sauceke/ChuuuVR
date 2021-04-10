using BepInEx.Harmony;
using HarmonyLib;
using KKAPI.Utilities;

namespace ChuuuVR
{
    internal static class Hooks
    {
        public static void InstallHooks()
        {
            HarmonyWrapper.PatchAll(typeof(HSceneTriggers));
        }

        internal static class HSceneTriggers
        {
            internal static bool Locked = false;

            [HarmonyPostfix]
            [HarmonyPatch(typeof(HSprite), nameof(HSprite.InitHeroine))]
            public static void InitHeroine(HSprite __instance)
            {
                __instance.GetLeadingHeroine().chaCtrl.GetComponent<KissController>().StartHScene(__instance.flags);
            }

            [HarmonyPostfix]
            [HarmonyPatch(typeof(VRHandCtrl), nameof(VRHandCtrl.IsKissAction))]
            public static void IsKissAction(ref bool __result)
            {
                __result = KissController.IsKissing;
            }
        }
    }
}
