using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.GameController.Features.Mission
{
    public interface IMission
    {
        string MissionName { get; }

        bool IsCompleted { get; }

        void OnReceiveMessage(string message);

        void Execute();
    }
}
