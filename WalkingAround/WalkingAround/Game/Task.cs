using System;

namespace WalkingAround.Game
{
    public class Task
    {
        public Guid ID { get; private set; }

        public object Source { get; set; }

        public object Target { get; set; }

        public TaskAction Action { get; set; }

        internal bool Perform()
        {
            return false;
        }
    }
}