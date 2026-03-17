using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.GameController.Command
{
    internal class GameControllerCommand
    {
        public string action;
        public float value;

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(action);
        }
    }
}
