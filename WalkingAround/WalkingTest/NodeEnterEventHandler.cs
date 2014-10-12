using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WalkingTest
{
    public delegate void NodeEventHandler(Node sender, NodeEventType e);



    public enum NodeEventType
    {
        Enter,
        Exit,
        Click

    }
}
