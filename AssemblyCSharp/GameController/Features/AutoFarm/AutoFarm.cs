using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoFarm
{
    internal class AutoFarm : ICommand
    {
        public static bool IsAutoFarming { get; private set; } = false;
        static bool hasTeleported = false;
        static Mob currentTarget = null;

static int lastHp = -1;
static float lastHpChangeTime = 0f;

const float STUCK_TIME = 1f; // 3 giây
        public AutoFarm() { }

        public static void Update()
        {
            if (!IsAutoFarming)
            {
                GameScr.gI().auto = 0;
                return;
            }
            GameScr.gI().auto = 1;
            Debug.Log("Auto farming..." + IsAutoFarming);
            MainThreadDispatcher.Enqueue(() => Attack());
        }

        public void Execute(GameControllerCommand cmdObj)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                try
                {
                    IsAutoFarming = cmdObj.value != 0f;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to toggle auto farm: {ex.Message}");
                }
            });
        }

        public static void Attack()
        {
            Debug.Log("Executing auto farm attack...");
            var me = Char.myCharz();

            // teleport to mob
            if (me == null) return;

            Mob mob = FindMobGlobal();

            if (mob.hp < lastHp)
            {
                lastHp = mob.hp;
                lastHpChangeTime = Time.time;
            }

            // 👇 nếu 3s không giảm HP → reset teleport
            if (Time.time - lastHpChangeTime > STUCK_TIME)
            {
                Debug.Log("Stuck → reset teleport");

                hasTeleported = false; // 🔥 QUAN TRỌNG

                lastHp = mob.hp;
                lastHpChangeTime = Time.time;
            }

            if (me.mobFocus != mob || mob.hp <= 0)
            {
                hasTeleported = false;
                me.mobFocus = mob;
            }

            if (!hasTeleported)
            {
                me.cx = mob.x;
                me.cy = mob.y;

                hasTeleported = true;
            }

            if (me.mobFocus == null) return;

            me.setAttack();
        }

        public static Mob FindMobGlobal()
        {
            Mob best = null;
            float minDist = float.MaxValue;

            var me = Char.myCharz();

            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                if (mob == null) continue;
                if (mob.hp <= 0) continue;
                if (mob.isInvisible()) continue;

                float dx = me.cx - mob.x;
                float dy = me.cy - mob.y;
                float dist = dx * dx + dy * dy;

                if (dist < minDist)
                {
                    minDist = dist;
                    best = mob;
                }
            }

            return best;
        }

    }
}
