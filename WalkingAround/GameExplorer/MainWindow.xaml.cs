using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace GameExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Map _map;
        public MainWindow()
        {
            _map = new Map(File.ReadAllText(@"C:\Users\Anthony\Desktop\untitled.mp"));
            InitializeComponent();
            eMain.Header = _map.Type;
            eMain.Content = new ListView();
            int index = 0;
            Grid cd = null;
            foreach (var item in _map)
            {
                if (index % _map.Cols == 0)
                {
                    if (cd != null)
                    {
                        (eMain.Content as ListView).Items.Add(cd);
                    }
                    cd = new Grid();
                    }
                cd.ColumnDefinitions.Add(new ColumnDefinition());
                AddDisplayItems(item, cd);
                    index++;
            }
            if (cd != null)
            {
                (eMain.Content as ListView).Items.Add(cd);
            }

        }

        private void AddDisplayItems(MoveCostNode item, Grid gv)
        {
            var val = new Expander() { Header = new Image() { Source = new BitmapImage(new Uri(StaticResources.Get("node", int.Parse(item.Key)))) } };
            val.Content = new ListView();
                
            (val.Content as ListView).Items.Add("X: " + item.X);
            (val.Content as ListView).Items.Add("Y: " + item.Y);
            (val.Content as ListView).Items.Add("I: " + item.Index);
            (val.Content as ListView).Items.Add("S: " + item.Status);
            (val.Content as ListView).Items.Add("V: " + Enum.GetName(typeof(NodeVisibility), item.VisibleStatus));



            gv.Children.Add(val);
            Grid.SetColumn(val, gv.ColumnDefinitions.Count - 1);
        }


    }
}
