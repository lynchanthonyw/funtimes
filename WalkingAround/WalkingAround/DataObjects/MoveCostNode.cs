using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WalkingAround.DataObjects
{

    public class MoveCostNode : Node
    {
        private int _vStatus;

        protected int NodeType { get { return int.Parse(Key); } }


        private readonly int[][] COSTTABLE = new int[][] 
        { 
            new int[] { 1,0,3,3,2,0}, 
            new int[] { 0,0,0,0,0,0}, 
            new int[] { 2,0,2,1,1,0}, 
            new int[] { 2,0,3,2,1,0}, 
            new int[] { 2,0,2,1,1,0}, 
            new int[] { 0,0,0,0,0,0} 
        };

        public int VisibleStatus
        {
            get { return _vStatus; }
            set
            {
                _vStatus = value;
                OnPropertyChanged("VisibleStatus");
            }
        }

        public int ParsedValue
        {
            get { return ToParsedValue(); }
            set { FromParsedValue(value); }
        }

        public List<MoveCostNode> Neighbors
        {
            get { return (Parent as Map).GetNeighbors(this); }
        }

        public List<MoveCostNode> Adjacents
        {
            get { return (Parent as Map).GetAdjacents(this); }
        }


        private int ToParsedValue()
        {
            return NodeType;
        }

        private void FromParsedValue(int value)
        {

        }

        public MoveCostNode(int type, Map parent)
            : base(type, parent)
        {
        }

        [Obsolete]
        public MoveCostNode(string nodeinfo, Map parent)
        {
            Type = nodeinfo.Substring(nodeinfo.IndexOf("(") + 1).TrimEnd(')');
            Index = int.Parse(nodeinfo.Substring(0, nodeinfo.IndexOf("(")));
            Parent = parent;
        }

        public void SetAsCurrent()
        {
            SetVisiblity(Adjacents, NodeVisibility.Visible);
            VisibleStatus = (int)NodeVisibility.Visible;
            Status = 2;
        }

        public void UnsetAsCurrent()
        {
            VisibleStatus = (int)NodeVisibility.Fog;
            Status = 1;
            SetVisiblity(Adjacents, NodeVisibility.Fog);
        }

        public static void SetVisiblity(List<MoveCostNode> neighbors, NodeVisibility state)
        {
            foreach (var item in neighbors)
            {
                item.VisibleStatus = (int)state;
            }
        }

        public static MoveCostNode SetAsVisited(List<MoveCostNode> path)
        {
            MoveCostNode endnode = null;
            foreach (var item in path)
            {
                if (item.GetCanMoveTo(item.NodeType))
                {
                    endnode = item;
                    item.VisibleStatus = (int)NodeVisibility.Fog;
                    item.Status = 1;
                    SetVisiblity(item.Adjacents, NodeVisibility.Fog);
                }
                else
                {
                    break;
                }

            }
            return endnode;
        }


        public int GetMoveCost(string movetotype)
        {
            return COSTTABLE[NodeType][int.Parse(movetotype)];
        }

        public int GetMoveCost(int movetotype)
        {

            return COSTTABLE[NodeType][movetotype];
        }

        public bool GetCanMoveTo(int movetotype)
        {
            return COSTTABLE[NodeType][movetotype] > 0;
        }

        public bool GetCanMoveTo(string movetotype)
        {
            return COSTTABLE[NodeType][int.Parse(movetotype)] > 0;
        }

        public override string ToString()
        {
            return Index + "(" + Key + ")";
        }





    }
}
