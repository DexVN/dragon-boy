using AssemblyCSharp.GameController.Command;

namespace AssemblyCSharp.GameController.Features.Speed
{
    internal class PlayerSpeedCommand : ICommand
    {
        public void Execute(int value)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                Char.myCharz().cspeed = value;
            });
        }

        public void Execute(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}
