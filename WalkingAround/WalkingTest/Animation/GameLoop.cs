using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkingTest.Animation
{
    public class GameLoop
    {
        public Queue<IAnimationEvent> EventQueue { get; private set; }

    }
}
