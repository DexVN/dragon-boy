using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Speed
{
    internal class GameSpeed : ICommand
    {
        private static GameSpeed _instance;
        public static GameSpeed gI() => _instance ?? (_instance = new GameSpeed());
        private const float MinTimeScale = 0f;

        public void Execute(GameControllerCommand gcObj)
        {
            try
            {
                float newSpeed = Mathf.Max(MinTimeScale, gcObj.value);
                Time.timeScale = newSpeed;
                Debug.Log($"[GameSpeed] Game speed successfully set to: {newSpeed}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[GameSpeed] Failed to set game speed: {ex.Message}");
            }
        }
    }
}
