using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkingAround.DataObjects
{
    [Serializable]
    public class Map : VisualContainer, IEnumerable<MoveCostNode>
    {

        private List<List<int>> _this;
        private List<List<int>> _adjacent;
        private int _vertixCount { get { return _this.Count; } }

        private List<MoveCostNode> _nodes { get; set; }

        private bool debug = false;
        private int _rows = 10;
        private int _cols = 10;

        public int Rows { get { return _rows; } }
        public int Cols { get { return _cols; } }


        public Map()
            : base("Map", "0", null)
        {
            _this = new List<List<int>>();
            _nodes = new List<MoveCostNode>();
            _adjacent = new List<List<int>>();
        }

        public Map(int x, int y)
            : this()
        {
            GenerateMap(x, y);
        }

        public Map(string parsed)
            : this()
        {

            GenerateMap(parsed);
        }

        private void GenerateMap(string parsed)
        {
            var split = parsed.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var splitint = split.Select(x => int.Parse(x));

            GenerateMap(splitint.ElementAt(0), splitint.ElementAt(1), splitint.Skip(2).ToArray());
        }

        private void GenerateMap(int x, int y, int[] nodes = null)
        {
            _rows = y;
            _cols = x;
            Random r = new Random();
            var count = x * y;

            var vals = new List<int>();

            for (int i = 0; i < count; i++)
            {
                var temp = new MoveCostNode(nodes == null ? 3 : nodes[i], this) { Index = i, X = i % x, Y = i / x };
                _nodes.Add(temp);
                _this.Add(GetAdjacentList(i, x, y));
                _adjacent.Add(GetAdjacentList(i, x, y));
            }

            int indx = 0;
            foreach (var item in _this)
            {
                item.RemoveAll(q => _nodes[indx].GetMoveCost(_nodes[q].Key) == 0);
                indx++;

            }
        }

        public string ToFile()
        {
            string maplist = string.Empty;
            maplist += _cols + "," + _rows + ",";
            maplist += string.Join(",", _nodes.Select(x => x.ParsedValue));
            return maplist;
        }

        private List<int> GetAdjacentList(int i, int cols, int rows)
        {
            //  1   2   3
            //  4   i   5
            //  6   7   8

            int x = i % cols;
            int y = i / cols;

            List<int> retVal = new List<int>();

            if (y > 0)
            {
                if (x > 0)
                {
                    //1
                    retVal.Add(i - (cols + 1));
                }
                //2
                retVal.Add(i - (cols));

                if (x < cols - 1)
                {
                    //3
                    retVal.Add(i - (cols - 1));
                }
            }

            //4
            if (x > 0)
            {
                retVal.Add(i - 1);
            }
            //ignore self (retVal.Add(i))
            //5
            if (x < cols - 1)
            {
                retVal.Add(i + 1);
            }

            if (y < rows - 1)
            {
                //6
                if (x > 0)
                {
                    retVal.Add(i + (cols - 1));
                }
                //7
                retVal.Add(i + (cols));
                //8
                if (x < cols - 1)
                {
                    retVal.Add(i + (cols + 1));
                }
            }


            return retVal;
        }

        //tests whether there is an edge from node x to node y.
        public bool IsAdjacent(MoveCostNode x, MoveCostNode y)
        {

            return _this[x.Index].Contains(y.Index);
        }

        //lists all nodes y such that there is an edge from x to y.
        public List<MoveCostNode> GetNeighbors(MoveCostNode x)
        {
            int indx_x = x.Index;
            var neighbors = _this[indx_x];

            List<MoveCostNode> retVal = new List<MoveCostNode>();

            foreach (var item in neighbors)
            {
                if (item < 0 || item >= _vertixCount)
                {
                    continue;
                }
                retVal.Add(_nodes[item]);
            }
            return retVal;
        }

        public List<MoveCostNode> GetAdjacents(MoveCostNode x)
        {
            int indx_x = x.Index;
            var neighbors = _adjacent[indx_x];

            List<MoveCostNode> retVal = new List<MoveCostNode>();

            foreach (var item in neighbors)
            {
                if (item < 0 || item >= _vertixCount)
                {
                    continue;
                }
                retVal.Add(_nodes[item]);
            }
            return retVal;
        }


        //returns the value associated to the edge (x,y).
        public int GetEdgeValue(int x, int y)
        {
            if (_this.Count < x - 1)
            {
                return 0;
            }
            else if (_nodes[y].VisibleStatus == 3)
            {
                return 10;
            }
            else if (!_this[x].Contains(y))
            {
                return 0;
            }

            else
            {
                return _nodes[x].GetMoveCost(_nodes[y].Key);
            }
        }

        public List<MoveCostNode> GetShortPath(MoveCostNode start, MoveCostNode end)
        {
            return GetAStarShortPath(start, end);
        }


        private List<MoveCostNode> GetAStarShortPath(MoveCostNode source, MoveCostNode target)
        {
            if (debug)
            {
                Console.WriteLine("Start Short Path: " + DateTime.Now.Ticks.ToString());
            }
            bool found = false;
            bool unsorted = false;
            List<MoveCostNode> neighbors = null;
            List<MoveCostNode> retVal = new List<MoveCostNode>();
            List<AStarNode> open = new List<AStarNode>();
            List<AStarNode> closed = new List<AStarNode>();

            open.Add(new AStarNode() { Index = source.Index, G = 0, H = GetDistToTarget(target.Index, source.Index), Node = source, ParentIndex = -1 });
            AStarNode current = open[0];

            while (open.Count > 0)
            {
                current = open[0];
                closed.Add(current);
                open.Remove(current);
                if (current.Index == target.Index)
                {
                    break;
                }

                neighbors = GetAdjacents(current.Node);
                if (neighbors.Contains(target) && !target.GetCanMoveTo(target.Key))
                {
                    neighbors.RemoveAll(m => m != target);
                }
                else
                {
                    neighbors.RemoveAll(m => m.VisibleStatus < 3 && !_this[current.Index].Contains(m.Index));

                }

                foreach (var neighbor in neighbors)
                {
                    if (closed.Exists(n => n.Index == neighbor.Index))
                    {
                        if (debug)
                        {
                            Console.Write(current.ToString() + ", " + neighbor.ToString() + ": ");
                            closed.ForEach(c => Console.Write(c.ToString() + " ~ "));
                            Console.WriteLine();
                        }
                        continue;
                    }
                    else if (open.Exists(n => n.Index == neighbor.Index))
                    {
                        var curNeighbor = open.Single(o => o.Index == neighbor.Index);
                        int newG = current.G + GetEdgeValue(current.Index, neighbor.Index);

                        if (newG < curNeighbor.G)
                        {
                            curNeighbor.ParentIndex = current.Index;
                            curNeighbor.G = GetEdgeValue(curNeighbor.ParentIndex, curNeighbor.Index);
                            open.Sort();
                        }
                    }
                    else
                    {
                        open.Add(new AStarNode()
                        {
                            Index = neighbor.Index,
                            G = GetEdgeValue(current.Index, neighbor.Index),
                            H = GetDistToTarget(neighbor.Index, target.Index),
                            ParentIndex = current.Index,
                            Node = neighbor
                        });
                        unsorted = true;
                    }
                    if (debug)
                    {
                        Console.Write(current.ToString() + ", " + neighbor.ToString() + ": ");
                        closed.ForEach(c => Console.Write(c.ToString() + " ~ "));
                        Console.WriteLine();
                    }
                }
                if (unsorted)
                {
                    open.Sort();
                    unsorted = false;
                }


            }


            retVal.Add(target);
            while (current.ParentIndex >= 0)
            {
                retVal.Add(_nodes[current.ParentIndex]);
                current = closed.Single(c => c.Index == current.ParentIndex);
            }
            retVal.Reverse();


            return retVal;
        }

        private int GetDistToTarget(int start, int target)
        {
            return Math.Abs(_nodes[start].X - _nodes[target].X) + Math.Abs(_nodes[start].Y - _nodes[target].Y);
        }

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._nodes.GetEnumerator();
        }

        IEnumerator<MoveCostNode> IEnumerable<MoveCostNode>.GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        #endregion

        internal class AStarNode : IComparable<AStarNode>
        {
            internal int Index { get; set; }
            internal int F { get { return G + H; } }
            internal int G { get; set; }
            internal int H { get; set; }
            internal int ParentIndex { get; set; }
            internal MoveCostNode Node { get; set; }

            internal AStarNode()
            { }


            public int CompareTo(AStarNode other)
            {
                return this.F.CompareTo(other.F);
            }

            public override string ToString()
            {
                return Node.Index + String.Format("({2} + {3} = {4},{0}=>{1})", new object[] { ParentIndex, Index, G, H, F });
            }
        }


    }





}