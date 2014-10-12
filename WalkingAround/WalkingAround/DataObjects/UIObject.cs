using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WalkingAround
{
    public abstract class VisualObject: INotifyPropertyChanged
    {

        internal VisualContainer _parent;
        internal string _key;

        public string Type { get; internal set; }
        public string Key 
        {
            get { return _key; }
            internal set
            {
                _key = value;
                OnPropertyChanged("Key");
            }
        }

        protected VisualContainer Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged("Parent");
            }
        }

        protected VisualObject(string type, string key, VisualContainer parent = null)
        {
            Type = type;
            Key = key;
            Parent = parent;
        }


        public void SetKey(int key)
        {
            SetKey(key.ToString());
        }

        public void SetKey(string key)
        {
            Key = key;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}
