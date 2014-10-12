﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalkingAround
{
    public abstract class VisualContainer : VisualObject
    {
        protected List<VisualObject> _children;

        protected List<VisualObject> Children
        {
            get { return _children.ToList(); }

        }

        protected VisualContainer(string type, string key, VisualContainer parent = null)
            : base(type, key, parent)
        {
            _children = new List<VisualObject>();
        }

        protected void AddChild(VisualObject obj)
        {
            _children.Add(obj);
            OnPropertyChanged("Children");
        }

        protected void RemoveChild(VisualObject obj)
        {
            _children.Remove(obj);
            OnPropertyChanged("Children");
        }
    }
}
