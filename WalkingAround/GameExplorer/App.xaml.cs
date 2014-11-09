using System;
using System.Windows;
using WalkingAround.Game;

namespace GameExplorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Clock.Start();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Clock.Stop();
        }
    }
}