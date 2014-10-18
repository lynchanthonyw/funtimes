using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WalkingAround;
using WalkingAround.DataObjects;

namespace WalkingTest
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        private const string resourcetype = "node";
        private MoveCostNode _this;

        private bool DebugMode
        {
            get
            {
                return (Window.GetWindow(this) as MapView) != null && (Window.GetWindow(this) as MapView).Debug;
            }
        }

        private bool BuildMode
        {
            get
            {
                return (Window.GetWindow(this) as MapView) != null && (Window.GetWindow(this) as MapView).Build;
            }
        }

        public string Coordinates { get { return string.Format("({0},{1})", _this.X, _this.Y); } }

        public int Index { get { return _this.Index; } }

        public int Type { get { return int.Parse(_this.Key); } }

        public bool Visited { get { return _this.Status > 0; } }

        public MoveCostNode Context { get { return _this; } }

        private bool _mousedown = false;

        public Node()
        {
            InitializeComponent();
        }

        public Node(MoveCostNode node)
        {
            _this = node;
            _this.VisibleStatus = 3;
            _this.Status = 0;
            _this.PropertyChanged += _this_PropertyChanged;
            InitializeComponent();

            ((ImageBrush)this.polyBackground.Fill).ImageSource = new BitmapImage(new Uri(StaticResources.Get(resourcetype, this.Type)));
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (NodeEvent != null && _mousedown)
            {
                NodeEvent(this, NodeEventType.Click);
            }
            _mousedown = false;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mousedown = true;
        }

        public event NodeEventHandler NodeEvent;

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DebugMode)
            {
                grMain.Visibility = System.Windows.Visibility.Visible;
            }
            polyMouseOver.Visibility = System.Windows.Visibility.Visible;
            if (NodeEvent != null)
            {
                NodeEvent(this, NodeEventType.Enter);
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            _mousedown = false;
            if (DebugMode)
            {
                grMain.Visibility = System.Windows.Visibility.Collapsed;
            }

            polyMouseOver.Visibility = System.Windows.Visibility.Hidden;

            if (NodeEvent != null)
            {
                NodeEvent(this, NodeEventType.Exit);
            }
        }

        internal void SetAsCurrent()
        {
            polyCurrent.Visibility = System.Windows.Visibility.Visible;
            _this.SetAsCurrent();
        }

        internal void UnsetAsCurrent()
        {
            polyCurrent.Visibility = System.Windows.Visibility.Hidden;
            _this.UnsetAsCurrent();
        }

        internal void SetVisited()
        {
        }

        internal void SetVisiblity(NodeVisibility value)
        {
            switch (value)
            {
                case NodeVisibility.Visible:
                    polyVisible.Opacity = 0;
                    break;

                case NodeVisibility.Fog:
                    polyVisible.Opacity = .7;
                    break;

                case NodeVisibility.Hidden:
                    polyVisible.Opacity = 1;
                    break;

                default:
                    break;
            }
        }

        private void _this_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "VisibleStatus":
                    SetVisiblity(_this.VisibleStatus);
                    break;

                case "Status":
                    StatusChanged();
                    break;

                case "Type":
                    TypeChanged();
                    break;

                case "Children":
                    ChildrenChanged();
                    break;

                default:
                    break;
            }
        }

        private void ChildrenChanged()
        {
            //        foreach (var item in _this.Children)
            //{
            //     //if(this.cMain.Children.Contains(item)
            //}
            // this.cMain.Children.Contains
        }

        private void TypeChanged()
        {
            ((ImageBrush)this.polyBackground.Fill).ImageSource = new BitmapImage(new Uri(StaticResources.Get(resourcetype, this.Type)));
        }

        private void StatusChanged()
        {
            if (_this.Status == 0)
            {
                SetVisiblity(NodeVisibility.Hidden);
            }
            else if (_this.Status == 2)
            {
                SetVisiblity(NodeVisibility.Visible);
            }
            else if (_this.Status == 1)
            {
                SetVisiblity(NodeVisibility.Fog);
            }
        }

        private void SetVisiblity(int val)
        {
            SetVisiblity((NodeVisibility)Enum.Parse(typeof(NodeVisibility), val.ToString()));
        }

        private void UserControl_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((Window.GetWindow(this) as MapView) != null && (Window.GetWindow(this) as MapView).Build)
            {
                var newkey = int.Parse(_this.Key) + 1;
                _this.SetKey(newkey);
                if (newkey > StaticResources.Count(resourcetype) - 1)
                {
                    _this.SetKey(0);
                }
            }
        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        internal void AddModel(ModelView model)
        {
            this.cMain.Children.Add(model);
            Canvas.SetZIndex(model, 500);
            this.UpdateLayout();
        }

        internal void RemoveModel(ModelView model)
        {
            this.cMain.Children.Remove(model);
            this.UpdateLayout();
        }
    }
}