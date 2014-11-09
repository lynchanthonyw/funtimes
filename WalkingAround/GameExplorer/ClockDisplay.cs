using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingAround.Game;

namespace GameExplorer
{
    public class ClockDisplay : INotifyPropertyChanged
    {

        public ClockDisplay()
        {
            Clock.ChangedProperty += Clock_ChangedProperty;
            Clock.ChangedSchedule += Clock_ChangedSchedule;

        }

        void Clock_ChangedSchedule(object sender, ScheduleEventArgs e)
        {
            if (ScheduleChanged != null)
            {
                ScheduleChanged(this, e);
            }
        }

        void Clock_ChangedProperty(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event ScheduleEventHandler ScheduleChanged;


    }
}
