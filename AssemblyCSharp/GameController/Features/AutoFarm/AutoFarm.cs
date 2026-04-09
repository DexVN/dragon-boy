using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoFarm
{
    internal class AutoFarm : ICommand
    {
        private static AutoFarm _instance;
        public static AutoFarm gI() => _instance ?? (_instance = new AutoFarm());

        public static bool IsAutoFarming { get; private set; } = false;

        private long _lastTimeChangeZone;
        private int _delayChangeZone = 5000;

        public AutoFarm() { }

        public static void Update()
        {
            if (!IsAutoFarming) return;
            GameScr.isAutoPlay = true;
            GameScr.canAutoPlay = true;
            GameScr.gI().autoPlay();
            gI().HandleAutoChangeZone();
        }

        public void Execute(GameControllerCommand cmdObj)
        {
            try
            {
                IsAutoFarming = cmdObj.value != 0f;
                Logger.Info($"Status changed to: {IsAutoFarming}");
                if (!IsAutoFarming)
                {
                    GameScr.isAutoPlay = false;
                    GameScr.canAutoPlay = false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Info($"Failed to toggle auto farm: {ex.Message}");
            }
        }

        private void HandleAutoChangeZone()
        {
            if (!IsAutoFarming) return;
            if (mSystem.currentTimeMillis() - _lastTimeChangeZone < _delayChangeZone) return;
            if (IsStrangerPresent())
            {
                int nextZone = Res.random(0, 20);
                if (nextZone == TileMap.zoneID) nextZone = (nextZone + 1) % 20;
                Logger.Info($"Phát hiện người lạ! Đang chuồn sang khu: {nextZone}");
                Service.gI().requestChangeZone(nextZone, -1);
                _lastTimeChangeZone = mSystem.currentTimeMillis();
            }
        }

        private bool IsStrangerPresent()
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char c = (Char)GameScr.vCharInMap.elementAt(i);
                if (c != null &&
                    !c.me &&
                    c.charID > 0 &&
                    !IsMyPet(c.charID))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsMyPet(int charId)
        {
            Char me = Char.myCharz();
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char c = (Char)GameScr.vCharInMap.elementAt(i);
                if (c != null && c.charID == charId)
                {
                    return c.cName == me.cName + " đệ tử";
                }
            }
            return false;
        }
    }
}