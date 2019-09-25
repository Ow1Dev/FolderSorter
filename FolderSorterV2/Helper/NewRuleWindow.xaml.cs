using FolderSorterV2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FolderSorterV2.Helper
{
    public partial class NewRuleWindow : Window
    {
        private bool isOk = false;

        public Rule CreateRule()
        {
            InitializeComponent();
            this.Title = "Create Rule";

            return GetRule();
        }

        public Rule EditRule(Rule rule)
        {
            InitializeComponent();
            this.Title = "Edit Rule";

            this.Browse.InputpathTxt.Text = rule.OutputFolder;
            this.RegexTxt.Text = rule.Regex;
            this.PriorityUpDown.Value = rule.Priority;

            return GetRule();
        }

        private Rule GetRule()
        {
            this.ShowDialog();

            if (isOk)
            {
                return new Rule()
                {
                    OutputFolder = Browse.InputpathTxt.Text,
                    Regex = RegexTxt.Text,
                    Priority = (ushort)PriorityUpDown.Value
                };
            }
            return null;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Browse.InputpathTxt.Text) || String.IsNullOrWhiteSpace(RegexTxt.Text) || !PriorityUpDown.Value.HasValue)
            {
                MessageBox.Show("path, regex or Priority is empty");
                return;
            }

            if (Regex.IsMatch(Browse.InputpathTxt.Text, @"^[A-Z]:[\\]([A-z0-9-_+]+[\\])*([A-z0-9]+)$"))
            {
                //TODO: Check if there is a backslach or not
                if (Directory.Exists(Browse.InputpathTxt.Text))
                {
                    try
                    {
                        Regex _regex = new Regex(RegexTxt.Text);
                        isOk = true;
                        this.Close();
                    } catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Regex is invalid", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    return;
                }
                MessageBox.Show("This Path does not exist", "Path does not exist", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show("This Folder Path is not valid", "Invalid Path", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
