using System.Collections.Generic;

namespace WalkingTest.Animation
{
    public class GameLoop
    {
        public Queue<IAnimationEvent> EventQueue { get; private set; }
    }
}