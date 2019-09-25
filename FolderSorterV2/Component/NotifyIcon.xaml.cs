using System;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace FolderSorterV2.Component
{
    /// <summary>
    /// Interaction logic for NotifyIcon.xaml
    /// </summary>
    public partial class NotifyIcon : UserControl
    {
        public System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();

        public NotifyIcon()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            notifyIcon.Text = "FolderWatcher";
            notifyIcon.Icon = appIcon;
            notifyIcon.Visible = false;

            var menu = new System.Windows.Forms.ContextMenu();

            var RunItem = new System.Windows.Forms.MenuItem()
            {
                Text = "Run"
            };
            var ExitItem = new System.Windows.Forms.MenuItem()
            {
                Text = "Exit"
            };

            ExitItem.Click += ExitItem_Click;

            menu.MenuItems.Add(RunItem);
            menu.MenuItems.Add(ExitItem);
            notifyIcon.ContextMenu = menu;
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
