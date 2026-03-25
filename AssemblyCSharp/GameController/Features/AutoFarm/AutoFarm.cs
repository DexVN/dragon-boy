using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoFarm
{
    internal class AutoFarm : ICommand
    {
        private static AutoFarm _instance;
        public static AutoFarm gI() => _instance ?? (_instance = new AutoFarm());
        public static bool IsAutoFarming { get; private set; } = false;
        public AutoFarm() { }

        public static void Update()
        {
            GameScr.isAutoPlay = IsAutoFarming;
            GameScr.canAutoPlay = IsAutoFarming;
            GameScr.gI().autoPlay();
        }

        public void Execute(GameControllerCommand cmdObj)
        {
            try
            {
                IsAutoFarming = cmdObj.value != 0f;
                Debug.Log($"[AutoFarm] Status changed to: {IsAutoFarming}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[AutoFarm] Failed to toggle auto farm: {ex.Message}");
            }
        }
    }
}
