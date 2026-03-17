using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.GameController.Command
{
    internal interface ICommand
    {
        void Execute(float value);
        void Execute(int value);
    }
}
