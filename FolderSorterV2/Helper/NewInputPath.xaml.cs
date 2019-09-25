using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FolderSorterV2.Helper
{
    /// <summary>
    /// Interaction logic for NewInputPath.xaml
    /// </summary>
    public partial class NewInputPath : Window
    {
        public string GetInputPath()
        {
            InitializeComponent();
            this.ShowDialog();
            if(!String.IsNullOrEmpty(Browse.InputpathTxt.Text))
            {
                return Browse.InputpathTxt.Text;
            }
            return null;
        }

        #region Events
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Browse.InputpathTxt.Text))
            {
                MessageBox.Show("You have not selected a path");
                return;
            }

            else if(Regex.IsMatch(Browse.InputpathTxt.Text, @"^[A-Z]:[\\]([A-z0-9-_+]+[\\])*([A-z0-9]+)$"))
            {
                //TODO: Check if there is a backslach or not
                if (Directory.Exists(Browse.InputpathTxt.Text))
                {
                    this.Close();
                    return;
                }
                MessageBox.Show("This Path does not exist");
            }
            MessageBox.Show("This Folder Path is not valid");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Browse.InputpathTxt.Text = "";
            this.Close();
        }
        #endregion

    }
}
