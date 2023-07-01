using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using KKAPI.MainGame;
using UnityEngine;

namespace ChuuuVR
{
    internal class KissController : GameCustomFunctionController
    {
        private readonly float thresholdSq = Mathf.Pow(0.05f, 2);
        private Transform camera;
        
        protected override void OnStartH(MonoBehaviour proc, HFlag hFlag, bool vr)
        {
            var renderType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(TryGetTypes)
                .FirstOrDefault(type => type.Name == "SteamVR_Render");
            if (renderType == null)
            {
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
            var femaleHead = female.objHead.transform;
            var waitForEndOfFrame = new WaitForEndOfFrame();
            var wait = new WaitForSeconds(0.2f);
            while (true)
            {
                while (!IsKiss(femaleHead))
                {
                    yield return wait;
                }
                hFlag.voice.playVoices[femaleIndex] = 101;
                hFlag.click = HFlag.ClickKind.mouth;
                hFlag.AddKiss();
                while (IsKiss(femaleHead))
                {
                    yield return waitForEndOfFrame;
                    hFlag.click = HFlag.ClickKind.mouth;
                }
                hFlag.click = HFlag.ClickKind.none;
            }
        }

        private bool IsKiss(Transform femaleHead) =>
            Vector2.SqrMagnitude(femaleHead.position - camera.position) < thresholdSq;
        
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
