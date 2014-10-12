using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WalkingAround;
using WalkingAround.DataObjects;
using WalkingAround.Models;

namespace WalkingTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MapView : Window
    {
        private Map _this;
        private ModelView _character;
        private MoveCostNode _start;
        private MoveCostNode _end;
        private int _rows = 10;
        private int _cols = 10;

        internal bool Debug = true;
        internal bool Build = false;

        private Node _currentNode;
        private List<Node> _nodes;

        public MapView()
        {
            InitializeComponent();
            this.Height = 56 + (24 * _rows);
            this.Width = 15 + (24 * _cols);
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            _nodes = new List<Node>();
        }

        public void CreateMap(object state)
        {

            _this = new Map(_cols, _rows);
            (state as AutoResetEvent).Set();
        }

        public void LoadMap(object state)
        {
            var parsed = (string)(state as object[])[0];
            _this = new Map(File.ReadAllText(parsed));
            ((state as object[])[1] as AutoResetEvent).Set();
        }

        public void ShowMap()
        {

            grMain.Children.Clear();
            _nodes.Clear();
            grMain.RowDefinitions.Clear();
            grMain.ColumnDefinitions.Clear();

            for (int i = 0; i < _rows; i++)
            {
                grMain.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _cols; i++)
            {
                grMain.ColumnDefinitions.Add(new ColumnDefinition());
            }

            foreach (MoveCostNode item in _this)
            {
                var cur = new Node(item);
                cur.NodeEvent += NodeEvent;
                Grid.SetRow(cur, item.Y);
                Grid.SetColumn(cur, item.X);
                grMain.Children.Add(cur);
                _nodes.Add(cur);

            }
            _start = ((Node)grMain.Children[0]).Context;
            _currentNode = ((Node)grMain.Children[0]);
            _currentNode.SetAsCurrent();
            createCharacter();
            _currentNode.AddModel();

        }

        private void createCharacter()
        {
            _character = new Model(0);
            _character.PropertyChanged +=Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        void NodeEvent(Node sender, NodeEventType e)
        {
            switch (e)
            {
                case NodeEventType.Enter:
                    NodeEnter(sender);
                    break;
                case NodeEventType.Exit:
                    NodeExit(sender);
                    break;
                case NodeEventType.Click:
                    NodeClick(sender);
                    break;
                default:
                    break;
            }
        }
        void NodeClick(Node sender)
        {
            List<MoveCostNode> path;
            try
            {
                if (Debug)
                {
                    Console.WriteLine("Start Short Path: " + DateTime.Now.Ticks.ToString());
                }
                path = _this.GetShortPath(_start, ((Node)sender).Context);
                if (Debug)
                {
                    Console.WriteLine("End Short Path: " + DateTime.Now.Ticks.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
            _end = ((Node)sender).Context;

            //_start
            if (_start == null || _end == null)
            {
                MessageBox.Show("Pick 2 Nodes");
                return;
            }

            if (Debug)
            {
                int moveCost = 0;
                MoveCostNode temp = null;
                foreach (var item in path)
                {
                    Console.Write(item.Index + " => ");
                    MarkStatus(item.X, item.Y, 2);
                    if (temp != null)
                    {
                        moveCost += temp.GetMoveCost(item.Key);
                    }
                    temp = item;
                }
                Console.WriteLine(" Move Cost: " + moveCost);
            }

            WalkPath(sender, path);

        }

        private void WalkPath(Node sender, List<MoveCostNode> path)
        {
            if (!Build)
            {

                _currentNode.UnsetAsCurrent();
                _start = MoveCostNode.SetAsVisited(path);
                if (_start != sender.Context)
                {
                    if (_start.GetCanMoveTo(sender.Context.Key))
                    {
                        NodeClick(sender);
                    }
                    else
                    {
                        _currentNode = _nodes.FirstOrDefault(m => m.Index == _start.Index);
                        if (_currentNode != null)
                            _currentNode.SetAsCurrent();
                    }
                }
                else
                {
                    _currentNode = sender;
                    _currentNode.SetAsCurrent();
                    _currentNode.AddModel(_character.context);
                }
            }
        }

        void NodeEnter(Node sender)
        {

        }

        void NodeExit(Node sender)
        {

        }


        private void Export_Click(object sender, RoutedEventArgs e)
        {
            string filename = string.Empty;
            var dlg = new SaveFileDialog();
            dlg.FileName = "untitled"; // Default file name
            dlg.DefaultExt = ".mp"; // Default file extension
            dlg.Filter = "Map Files (.mp)|*.mp"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return;
            }

            System.IO.File.WriteAllText(filename, _this.ToFile());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grCreation.Visibility = System.Windows.Visibility.Visible;
            grMain.Visibility = System.Windows.Visibility.Collapsed;
            this.UpdateLayout();
            List<WaitHandle> waitHandles = new List<WaitHandle>();
            waitHandles.Add(new AutoResetEvent(false));

            ThreadPool.QueueUserWorkItem(new WaitCallback(CreateMap), waitHandles.Last());
            WaitHandle.WaitAll(waitHandles.ToArray());
            ShowMap();
            grCreation.Visibility = System.Windows.Visibility.Collapsed;
            grMain.Visibility = System.Windows.Visibility.Visible;
            this.UpdateLayout();
        }

        private void MarkStatus(int col, int row, int status)
        {
            var element = grMain.Children
              .Cast<Node>()
              .First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);

            element.SetVisited();

        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            if (_this == null)
            {
                throw new Exception();
            }
            if (Build)
            {
                _this.ToList().ForEach(x => x.VisibleStatus = (int)NodeVisibility.Hidden);
            }
            else
            {
                _this.ToList().ForEach(x => x.VisibleStatus = (int)NodeVisibility.Visible);
            }
            Build = !Build;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            string filename = string.Empty;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".mp"; // Default file extension
            dlg.Filter = "Map Files (.mp)|*.mp"; // Filter files by extension 
            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return;
            }

            grCreation.Visibility = System.Windows.Visibility.Visible;
            grMain.Visibility = System.Windows.Visibility.Collapsed;
            this.UpdateLayout();
            List<WaitHandle> waitHandles = new List<WaitHandle>();
            waitHandles.Add(new AutoResetEvent(false));

            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadMap), new object[] { filename, waitHandles.Last() });
            WaitHandle.WaitAll(waitHandles.ToArray());
            ShowMap();
            grCreation.Visibility = System.Windows.Visibility.Collapsed;
            grMain.Visibility = System.Windows.Visibility.Visible;
            this.UpdateLayout();
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}

