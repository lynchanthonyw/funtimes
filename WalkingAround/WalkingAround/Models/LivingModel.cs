using System;
using WalkingAround.Game;

namespace WalkingAround.Models
{
    public abstract class LivingModel : Model
    {
        internal Schedule Schedule { get; set; }

        internal TaskList Tasks { get; set; }

        public byte Stamina { get; private set; }

        public byte Hunger { get; private set; }

        public byte Thirst { get; private set; }

        public string Name { get; private set; }

        public LivingModel(int key, VisualContainer parent = null)
            : base(key, parent)
        {
            Schedule = new Schedule();
            Tasks = new TaskList();
        }

        public bool AddTask(Task task)
        {
            return Tasks.AddTask(task);
        }

        public bool PerformNextTask()
        {
            bool retVal = false;
            Task task = Tasks.NextTask();
            try
            {
                retVal = task.Perform();
            }
            catch (Exception e)
            {
                retVal = false;
            }
            return retVal;
        }

        public bool DropToTask(int taskid)
        {
            bool retVal = false;
            try
            {
                retVal = Tasks.SkipToTask(taskid);
            }
            catch (Exception)
            {
                retVal = false;
            }
            return retVal;
        }

        public ScheduleItem GetCurrentScheduleItem()
        {
            throw new NotImplementedException();
        }

        public ScheduleItem GetNextScheduleItem()
        {
            throw new NotImplementedException();
        }
    }
}