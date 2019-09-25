using System;
using System.Deployment.Application;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace FolderSorterV2
{
    public partial class MainWindow : Window
    {
        public static ViewModels.InputPathViewModel vm;
        

        public MainWindow()
        {
            InitializeComponent();
            this.Title += " " + getRunningVersion();

#if DEBUG
            this.Title += " | DEBUG";
#endif
            vm = new ViewModels.InputPathViewModel();

            this.DataContext = vm;
            notify.notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            this.StateChanged += MainWindow_StateChanged;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            notify.notifyIcon.Visible = false;
            this.ShowInTaskbar = true;
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                notify.notifyIcon.Visible = true;
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private Version getRunningVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (Exception)
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
    }

}
