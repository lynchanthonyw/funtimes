using System;
using WalkingAround.Utils;
namespace WalkingAround.Game
{
    public class ScheduleItem : IComparable<ScheduleItem>, ICloneable<ScheduleItem>
    {
        public string Display { get; set; }
        public int Duration { get; set; }

        public int StartSlot { get; set; }

        public ScheduledTask Task { get; set; }

        public int CompareTo(ScheduleItem other)
        {
            if (this.StartSlot == other.StartSlot)
            {
                return this.Duration.CompareTo(other.Duration);
            }
            else
            {
                return this.StartSlot.CompareTo(other.StartSlot);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}-{2})", Display, Schedule.ToTime(StartSlot), Schedule.ToTime(StartSlot + Duration));
        }

       
        ScheduleItem ICloneable<ScheduleItem>.Clone()
        {
            ScheduleItem item = new ScheduleItem();
            item.Display = this.Display;
            item.Duration = this.Duration;
            item.StartSlot = this.StartSlot;
            item.Task = this.Task.Clone();

            return item;
        }
    }
}