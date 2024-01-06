using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Learn.Classes;

namespace Learn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void CenterWindow(Window window, Window parent)
        {
            Point centerControl = new Point(window.Width / 2, window.Height / 2);
            Point centerParent = new Point(parent.Width / 2, parent.Height / 2);

            Point newLocation = new Point((centerParent.X - centerControl.X) + parent.Left, (centerParent.Y - centerControl.Y) + parent.Top);

            window.Left = newLocation.X;
            window.Top = newLocation.Y;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            System.Windows.Forms.Application.SetHighDpiMode(System.Windows.Forms.HighDpiMode.SystemAware);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Settings.LoadSettings();
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            base.OnStartup(e);
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Settings.SaveSettings();
        }

    }
}
