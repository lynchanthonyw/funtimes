using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public bool AddTask(Task task) { throw new NotImplementedException(); }
        public bool PerformTask(Task task) { throw new NotImplementedException(); }
        public bool RemoveTask(Task task) { throw new NotImplementedException(); }
        public ScheduleItem GetCurrentScheduleItem() { throw new NotImplementedException(); }
        public ScheduleItem GetNextScheduleItem() { throw new NotImplementedException(); }


    }
}
