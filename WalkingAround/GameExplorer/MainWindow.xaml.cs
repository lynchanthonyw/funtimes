using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WalkingAround;
using WalkingAround.DataObjects;
using WalkingAround.Game;
using WalkingAround.Game.Actions;
using WalkingAround.Models;

namespace GameExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Map _map;
        Animal lm;
        Animal lm2;
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

            lm = new Animal(1);
            lm2 = new Animal(2);
            SetupSchedule();
            

        }

        private void SetupSchedule()
        {
            var item = new ScheduleItem();
            item.StartSlot = 2;
            item.Duration = 2;
            item.Task = new ScheduledTask(new DialogAction("LM1 1", lm, lm, 1000, 1000));
            lm.AddScheduleItem(item);
            
            var item2 = new ScheduleItem();
            item2.StartSlot = 8;
            item2.Duration = 2;
            item2.Task = new ScheduledTask(new DialogAction("LM1 2", lm, lm, 1000, 1000));
            lm.AddScheduleItem(item2);
            
            var item3 = new ScheduleItem();
            item3.StartSlot = 16;
            item3.Duration = 2;
            item3.Task = new ScheduledTask(new DialogAction("LM1 3", lm, lm, 1000, 1000));
            lm.AddScheduleItem(item3);

            var item4 = new ScheduleItem();
            item4.StartSlot = 0;
            item4.Duration = 2;
            item4.Task = new ScheduledTask(new DialogAction("LM2 1", lm, lm, 1000, 1000));
            lm.AddScheduleItem(item4);

            var item5 = new ScheduleItem();
            item5.StartSlot = 4;
            item5.Duration = 2;
            item5.Task = new ScheduledTask(new DialogAction("LM2 2", lm, lm, 1000, 1000));
            lm.AddScheduleItem(item5);

            var item6 = new ScheduleItem();
            item6.StartSlot = 32;
            item6.Duration = 2;
            item6.Task = new ScheduledTask(new DialogAction("LM2 3", lm, lm, 1000, 1000));
            lm.AddScheduleItem(item6);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Schedule sched = new Schedule();
            sched.Add(new ScheduleItem() { Display = "Item 1", StartSlot = 8, Duration = 7, Task = new ScheduledTask(new DialogAction("Walking to work", source: new Model(1), duration: 1000, cooldown: 2000), new DialogAction("Working", cooldown: 10000, source: new Model(1)), new DialogAction("Walking to home", duration: 1000, cooldown: 2000, source: new Model(1))) });
            Console.WriteLine(sched.ToString());
            sched[0].Task.Perform();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void ClockControl_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PrintDialog a = new PrintDialog();
            a.ShowDialog();
        }
    }
}