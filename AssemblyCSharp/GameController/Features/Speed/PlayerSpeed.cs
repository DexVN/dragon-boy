using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Speed
{
    internal class PlayerSpeed : ICommand
    {
        public void Execute(GameControllerCommand gcObj)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                try
                {
                    Char.myCharz().cspeed = (int) gcObj.value;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to set player speed: {ex.Message}");
                }
            });
        }
    }
}
