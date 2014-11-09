using System;
using System.Collections.Generic;

namespace WalkingAround.Game
{
    internal class TaskList
    {
        private List<Task> _this;

        public TaskList()
        {
            _this = new List<Task>();
        }

        public bool AddTask(Task task)
        {
            try
            {
                _this.Add(task);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Task NextTask()
        {
            try
            {
                Task nextTask = _this[0];
                _this.Remove(nextTask);
                return nextTask;
            }
            catch (Exception e)
            {
                return new Task(null);
            }
        }

        public bool SkipToTask(Task task)
        {
            try
            {
                int indx = _this.IndexOf(task);
                _this.RemoveRange(0, indx);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SkipToTask(int indx)
        {
            return SkipToTask(_this[indx]);
        }

        public bool RemoveTask(Task task)
        {
            try
            {
                _this.Add(task);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}