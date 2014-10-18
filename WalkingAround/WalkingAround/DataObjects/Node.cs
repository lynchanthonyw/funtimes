namespace WalkingAround.DataObjects
{
    /// <summary>
    /// TYPES:
    /// 0: water
    /// 1: Wall
    /// 2: Mud
    /// 3: Grass
    /// 4: Ice
    /// 5: Unknown
    /// </summary>
    public abstract class Node : VisualContainer
    {
        private int _status;

        public int Index { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public Node()
            : base("Node", "")
        {
        }

        public Node(int key, VisualContainer parent = null)
            : base("Node", key.ToString(), parent)
        {
        }

        public Node(string key, VisualContainer parent = null)
            : base("Node", key.ToString(), parent)
        {
        }
    }
}