using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Speed
{
    internal class GameSpeed : ICommand
    {
        public void Execute(GameControllerCommand gcObj)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                try
                {
                    Time.timeScale = gcObj.value;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to set game speed: {ex.Message}");
                }
            });
        }
    }
}
