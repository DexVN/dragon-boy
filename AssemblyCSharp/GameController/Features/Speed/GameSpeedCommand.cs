using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.Speed
{
    internal class GameSpeedCommand : ICommand
    {
        public void Execute(float value)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                Time.timeScale = value;
            });
        }

        public void Execute(int value)
        {
            throw new System.NotImplementedException();
        }
    }
}
