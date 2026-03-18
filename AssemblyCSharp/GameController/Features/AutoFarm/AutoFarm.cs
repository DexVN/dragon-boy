using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoFarm
{
    internal class AutoFarm : ICommand
    {
        public static bool IsAutoFarming { get; private set; } = false;

        public AutoFarm() { }

        public static void Update()
        {
            GameScr.isAutoPlay = IsAutoFarming;
            GameScr.canAutoPlay = IsAutoFarming;
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
    }
}
