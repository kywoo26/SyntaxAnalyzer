using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_SyntaxAnalyzer
{
    public enum TransitionType { SHIFT, GOTO, REDUCE }

    class Transition
    {
        public readonly TransitionType TransitionType;
        public readonly int Destination;

        public Transition(TransitionType transitionType, int destination)
        {
            TransitionType = transitionType;
            Destination = destination;
        }
    }
}
