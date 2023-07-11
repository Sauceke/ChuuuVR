using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using IllusionUtility.GetUtility;
using KKAPI.MainGame;
using UnityEngine;

namespace ChuuuVR
{
    internal class KissController : GameCustomFunctionController
    {
        private readonly float thresholdSq = Mathf.Pow(0.03f, 2);
        private Transform camera;
        
        protected override void OnStartH(MonoBehaviour proc, HFlag hFlag, bool vr)
        {
            var renderType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(TryGetTypes)
                .FirstOrDefault(type => type.Name == "SteamVR_Render");
            if (renderType == null)
            {
                ChuuuVRPlugin.Logger.LogError("SteamVR_Render not found.");
                enabled = false;
                return;
            }
            camera = Traverse.Create(renderType)
                .Method("Top")
                .Property<Transform>("head")
                .Value;
            for (int i = 0; i < hFlag.lstHeroine.Count; i++)
            {
                StartCoroutine(Run(hFlag, hFlag.lstHeroine[i].chaCtrl, i));
            }
        }

        protected override void OnEndH(MonoBehaviour proc, HFlag hFlag, bool vr) =>
            StopAllCoroutines();

        private IEnumerator Run(HFlag hFlag, ChaControl female, int femaleIndex)
        {
            var mouth = female.objHeadBone.transform.FindLoop("cf_J_MouthLow").transform;
            var wait = new WaitForSeconds(0.2f);
            while (true)
            {
                while (!IsKissing(mouth))
                {
                    yield return wait;
                }
                hFlag.click = HFlag.ClickKind.mouth;
                hFlag.voice.playVoices[femaleIndex] = 101;
                hFlag.AddKiss();
                hFlag.DragStart();
                float startTime = Time.time;
                while (IsKissing(mouth) && Time.time - startTime < 5f)
                {
                    yield return wait;
                }
                hFlag.FinishDrag();
            }
        }

        private bool IsKissing(Transform mouth) => Vector2
            .SqrMagnitude(mouth.position - camera.position - camera.rotation * Vector3.down * 0.05f)
            < thresholdSq;
        
        private static Type[] TryGetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch
            {
                return new Type[] { };
            }
        }
    }
}
