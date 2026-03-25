using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Speed
{
    internal class PlayerSpeed : ICommand
    {
        private static PlayerSpeed _instance;
        public static PlayerSpeed gI() => _instance ?? (_instance = new PlayerSpeed());

        private const int MinPlayerSpeed = 1;

        public void Execute(GameControllerCommand gcObj)
        {   
                try
                {
                    if (Char.myCharz() == null)
                    {
                        Debug.LogWarning("[PlayerSpeed] Cannot set speed: Player character is not loaded yet.");
                        return;
                    }

                    int requestedSpeed = (int)gcObj.value;
                    int newSpeed = Mathf.Max(MinPlayerSpeed, requestedSpeed);

                    Char.myCharz().cspeed = (int) gcObj.value;

                    Debug.Log($"[PlayerSpeed] Player speed successfully set to: {newSpeed}");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"[PlayerSpeed] Failed to set player speed: {ex.Message}");
                }
        }
    }
}
