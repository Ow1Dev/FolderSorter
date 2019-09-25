using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FolderSorterV2.Component
{
    /// <summary>
    /// Interaction logic for InputBrowse.xaml
    /// </summary>
    public partial class InputBrowse : UserControl
    {
        public InputBrowse()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            using (CommonOpenFileDialog openFileDialog = new CommonOpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users";
                openFileDialog.IsFolderPicker = true;
                openFileDialog.EnsurePathExists = true;

                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                if (openFileDialog.ShowDialog(window) == CommonFileDialogResult.Ok)
                {
                    InputpathTxt.Text = openFileDialog.FileName + @"\";
                    this.Focus();
                }
            }
        }
    }
}
