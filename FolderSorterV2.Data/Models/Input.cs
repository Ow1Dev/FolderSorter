using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSorterV2.Data.Models
{
    public class Input : INotifyPropertyChanged
    {
        private string inputPath;
        private ObservableCollection<Rule> rules = new ObservableCollection<Rule>();

        /*Gettes and settes*/
        public string InputPath
        {
            get { return inputPath; }
            set
            {
                if(!string.IsNullOrWhiteSpace(value))
                {
                    inputPath = value;
                    RaisePropertyChanged("InputPath");
                }
            }
        }

        public ObservableCollection<Rule> Rules
        {
            get { return rules; }
            set
            {
                rules = value;
                RaisePropertyChanged("Rules");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
