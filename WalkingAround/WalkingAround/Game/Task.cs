using System;

namespace WalkingAround.Game
{
    public class Task:ITask
    {
        public Guid ID { get; private set; }

        public VisualObject Source { get; set; }

        public VisualObject Target { get; set; }

        public TaskAction Action { get; set; }

        public bool IsEmpty { get { return Source == null && Target == null && Action == null; } }

        public Task() { }


        public bool Perform()
        {
            try
            {
                return Action.Perform(Source, Target);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task(VisualObject source, VisualObject target = null, TaskAction action = null)
        {
            ID = Guid.NewGuid();
            Source = source;
            Target = target;
            Action = action;
        }

    }
}