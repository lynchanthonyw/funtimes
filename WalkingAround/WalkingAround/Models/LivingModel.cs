using System;
using System.Windows.Threading;
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
            Clock.ChangedSchedule += PopScheduleItem;
        }

        private void PopScheduleItem(object sender, ScheduleEventArgs args)
        {
            PopSchedule(args.CurrentItem);
            //Dispatcher.Invoke(() => , DispatcherPriority.Normal);

        }

        protected virtual object PopSchedule(int schedItem)
        {
            var item = GetScheduleItem(schedItem);
            if (item != null)
            {
                return item.Task.Perform();
            }
            else
            {
                return null;
            }
        }

        public bool AddTask(Task task)
        {
            return Tasks.AddTask(task);
        }

        public bool PerformNextTask()
        {
            bool retVal = false;
            Task task = Tasks.NextTask();
            if (task.IsEmpty)
            {
                return false;
            }
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

        public bool AddScheduleItem(ScheduleItem item)
        {
            return this.Schedule.Add(item);
        }


        public ScheduleItem GetScheduleItem(int itemID = -1)
        {
            if (itemID < 0)
            {
                return Schedule.Get(Clock.CurrentDT.ToString("HHmm"));
            }
            else
            {
                return Schedule.Get(itemID);
            }
        }

        public ScheduleItem GetNextScheduleItem()
        {
            throw new NotImplementedException();
        }
    }
}