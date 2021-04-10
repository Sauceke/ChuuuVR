using KKAPI;
using KKAPI.Chara;
using System.Diagnostics;
using System.Timers;
using UnityEngine;

namespace ChuuuVR
{
    public class KissController : CharaCustomFunctionController
    {
        private const long POLL_TIME_MILLIS = 200L;
        private const float THRESHOLD_DISTANCE = 0.09f;

        private static Stopwatch lastCollision;
        private Timer timer = new Timer(POLL_TIME_MILLIS);

        internal static bool IsKissing
        {
            get
            {
                return lastCollision == null ? false : lastCollision.ElapsedMilliseconds <= 1000;
            }
        }

        internal void StartHScene(HFlag flags)
        {
            lastCollision = Stopwatch.StartNew();
            timer.Elapsed += (obj, args) => Poll(flags);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void Poll(HFlag flags) { 
            var camera = SteamVR_Render.Top().head;
            float distance = Vector2.Distance(camera.position, ChaControl.objHead.transform.position);
            if (distance < THRESHOLD_DISTANCE)
            {
                if (!IsKissing && flags.mode != HFlag.EMode.aibu)
                {
                    // i've no clue where the normal kissing voice is but this sounds nice too
                    flags.voice.playVoices[0] = 101;
                }
                lastCollision.Reset();
                lastCollision.Start();
                flags.click = HFlag.ClickKind.mouth;
            }
        }

        protected override void OnDestroy()
        {
            timer.Dispose();
            base.OnDestroy();
        }

        protected override void OnCardBeingSaved(GameMode currentGameMode)
        {
        }
    }
}
