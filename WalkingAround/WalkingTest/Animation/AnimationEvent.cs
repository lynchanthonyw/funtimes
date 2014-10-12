using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkingTest.Animation
{
    public interface IAnimationEvent
    {
        void PreEvent();
        void EventStart();
        void EventEnd();
        void PostEvent();

    }
}
