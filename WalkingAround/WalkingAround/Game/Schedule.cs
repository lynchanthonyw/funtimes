using System;
using System.Collections.Generic;
using System.Linq;

namespace WalkingAround.Game
{
    public delegate void ScheduleEventHandler(object sender, ScheduleEventArgs e);

    public class ScheduleEventArgs : EventArgs
    {
        public int CurrentItem { get; set; }

        public ScheduleEventArgs(int item) { CurrentItem = item; }
    }

    [Serializable]
    public class Schedule
    {
        private List<ScheduleItem> _items;
        private Dictionary<int, int> _this;
        public ScheduleItem this[int x]
        {
            get
            {
                return _items[x];
            }
        }
        public Schedule()
        {
            _this = new Dictionary<int, int>();
            //96 is the # of 15 blocks in a 24 hour span.
            for (int i = 0; i < 96; i++)
            {
                _this.Add(i, -1);
            }

            _items = new List<ScheduleItem>();
        }

        public bool Add(ScheduleItem item)
        {
            bool retVal = true;
            _items.Add(item);
            int index = _items.IndexOf(item);
            for (int i = item.StartSlot; i < (item.StartSlot + item.Duration); i++)
            {
                if (_this[i] != -1)
                {
                    _items.Remove(item);
                    retVal = false;
                    ResetItems(index, item.StartSlot, item.StartSlot + item.Duration);

                    break;
                }
                else
                {
                    _this[i] = index;
                }
            }

            return retVal;
        }

        public bool Remove(ScheduleItem item)
        {
            return Remove(_items.IndexOf(item));
        }

        public bool Remove(int index)
        {
            if (index < _items.Count)
                return false;

            ResetItems(index, _items[index].StartSlot, _items[index].StartSlot + _items[index].Duration);
            _items.RemoveAt(index);

            return true;
        }
        public ScheduleItem Get(int index)
        {
            var idx = _this[index];
            if (idx >= 0)
                return _items[idx];
            else
                return null;

        }

        public ScheduleItem Get(string index)
        {
            return Get(Schedule.FromTime(index));
        }


        private void ResetItems(int index, int start = 0, int end = 96)
        {
            var endslot = (start + end) > _this.Count ? (start + end) : _this.Count;
            for (int i = start; i < endslot; i++)
            {
                if (_this[i] == index)
                {
                    _this[i] = -1;
                }
            }
        }

        public override string ToString()
        {
            string retVal = string.Empty;
            foreach (var item in _this)
            {
                retVal += ToTime(item.Key) + "\t";
                if (item.Value >= 0)
                {
                    retVal += _items[item.Value].ToString();
                }
                retVal += Environment.NewLine;
            }
            return retVal;
        }

        public static string ToTime(int slotnumber)
        {
            return (((slotnumber % 4) * 15) + ((slotnumber / 4) * 100)).ToString("0000");
        }

        public static int FromTime(string time)
        {
            var hr = int.Parse(time.Substring(0, 2));
            var min = int.Parse(time.Substring(2, 2));

            return (hr * 4) + (min / 15);
        }
    }
}