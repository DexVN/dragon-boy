using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.GameController.Features.Mission
{
    public interface IMission
    {
        string Name { get; }

        bool IsStart { get; }

        void OnReceiveMessage(sbyte cmd, string message);

        void Execute();

        void CheckMission();
    }
}
