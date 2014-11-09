using System;
using System.Threading;
using WalkingAround.Utils;
namespace WalkingAround.Game
{
    public abstract class TaskAction: ICloneable<TaskAction>
    {
        private VisualObject _source;
        private VisualObject _target;


        public long Duration { get; private set; }
        public long CoolDown { get; private set; }
        public TaskAction() { }
        public TaskAction(VisualObject source = null, VisualObject target = null, long duration = 0, long cooldown = 0)
        {
            _source = source;
            _target = target; 
            Duration = duration;
            CoolDown = cooldown;
        }

        public bool Perform(VisualObject source = null, VisualObject target = null)
        {
            bool retVal = false;
            if (_source == null)
            {
                _source = source;
            }
            if (_target == null)
            {
                _target = target;
            }

            if (_source == null && _target == null)
            {
                retVal =  false;
            }
            else
            {
                PreAction();
                retVal = Action();
                PostAction();
            }
            return retVal;
        }

        private void PreAction()
        {
            
        }

        private void PostAction()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(CoolDown));
        }

        internal abstract bool Action();

        public abstract TaskAction Clone();


       
    }
}