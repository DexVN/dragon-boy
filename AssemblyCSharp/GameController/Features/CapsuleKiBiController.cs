

using System;
using System.IO;

namespace AssemblyCSharp.GameController.Features
{
    public class CapsuleKiBiController
    {
        private static CapsuleKiBiController _instance;
        public static CapsuleKiBiController gI() => _instance ?? (_instance = new CapsuleKiBiController());

        private int CAPSULE_KI_BI_ID = 2758;
        public static bool isAuto = false;
        public bool isLoaded = false;
        private int countTime = -1;

        private long _lastTimeCheck = 0;

        public void Update()
        {
            if (!isLoaded)
            {
                LoadSavedState();
                isLoaded = true;
            }

            if (!isAuto) { return; }

            long now = mSystem.currentTimeMillis();
            if (now - _lastTimeCheck < 1000) return;

            _lastTimeCheck = now;
            countTime = -1;

            for (int i = 0; i < Char.vItemTime.size(); i++)
            {
                ItemTime item = (ItemTime)Char.vItemTime.elementAt(i);
                if (item.idIcon != CAPSULE_KI_BI_ID) continue;
                countTime = item.coutTime;
            }

            Char me = Char.myCharz();
            if (me == null || me.arrItemBag == null) return;
            Logger.Info($"CountTime: {countTime}");
            if (countTime < 5)
            {
                
                for (int i = 0; i < me.arrItemBag.Length; i++)
                {
                    Logger.Info($"me.arrItemBag[i].template.id: {me.arrItemBag[i].template.id} - me.arrItemBag[i].template.name: {me.arrItemBag[i].template.name}");
                    if (me.arrItemBag[i] != null && me.arrItemBag[i].template.id == ItemID.MAY_DO_CAPSULE_KI_BI)
                    {
                        Service.gI().useItem(0, 1, (sbyte)i, -1);
                        break;
                    }
                }
            }

            for (int i = 0; i < me.arrItemBag.Length; i++)
            {
                if (me.arrItemBag[i] != null && me.arrItemBag[i].template.id == ItemID.VIEN_CAPSULE_KI_BI)
                {
                    Service.gI().useItem(0, 1, (sbyte)i, -1);
                    break;
                }
            }
        }

        public void LoadSavedState()
        {
            try
            {
                string exeName = Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                string capsulekibiKey = exeName + "_capsulekibi";
                string state = Rms.loadRMSString(capsulekibiKey) ?? "0";
                isAuto = (state == "1");
            }
            catch { }
        }
    }
}
