using FolderSorterV2.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using FolderSorterV2.Helper;

namespace FolderSorterV2.ViewModels
{
    public class InputPathViewModel
    {
        public MyICommand AddInputCommand { get; set; }
        public MyICommand DeleteInputCommand { get; set; }

        public MyICommand AddRuleCommand { get; set; }
        public MyICommand EditRuleCommand { get; set; }
        public MyICommand DeleteRuleCommand { get; set; }

        public InputPathViewModel()
        {
            DeleteInputCommand = new MyICommand(OnDeleteInput, IsInputSelected);
            AddInputCommand = new MyICommand(OnAddInput);

            AddRuleCommand = new MyICommand(OnAddRule, IsInputSelected);
            EditRuleCommand = new MyICommand(OnEditRule, IsRuleSelected);
            DeleteRuleCommand = new MyICommand(OnDeleteRule, IsRuleSelected);

            Inputs = new ObservableCollection<Input>();
            LoadInputs();

            //Selectected the first InputPath
            if(Inputs.Count > 0)
            {
                SelectedInput = Inputs[0];
            }
        }

        public ObservableCollection<Input> Inputs
        {
            get;
            set;
        }

        public void LoadInputs()
        {
            Inputs = SaveManager.Load();
        }

        #region input

        private Input _selectedInput;
        public Input SelectedInput
        {
            get
            {
                return _selectedInput;
            }

            set
            {
                _selectedInput = value;
                AddRuleCommand.RaiseCanExecuteChanged();
                DeleteInputCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsInputSelected()
        {
            return SelectedInput != null;
        }

        #region Commands

        private void OnDeleteInput()
        {
            Inputs.Remove(SelectedInput);
            SaveManager.Save();
        }

        //TODO make this more mvvm
        public void OnAddInput()
        {
            string input = new Helper.NewInputPath().GetInputPath();
            if(input != null)
            {
                //TODO: Seleced last
                Inputs.Add(new Input { InputPath = input });
                SaveManager.Save();
            }
        }
        #endregion
        #endregion

        #region Rule
        private Rule _selectedRule;
        public Rule SelectedRule
        {
            get
            {
                return _selectedRule;
            }

            set
            {
                _selectedRule = value;
                DeleteRuleCommand.RaiseCanExecuteChanged();
                EditRuleCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsRuleSelected()
        {
            if(IsInputSelected())
            {
                return SelectedRule != null;
            }
            return false;
        }

        #region Commands


        //TODO make this more mvvm
        public void OnAddRule()
        {
            Rule input = new Helper.NewRuleWindow().CreateRule();
            if (input != null && IsInputSelected())
            {
                SelectedInput.Rules.Add(input);
                SaveManager.Save();
            }
        }

        private void OnEditRule()
        {
            Rule input = new Helper.NewRuleWindow().EditRule(SelectedRule);
            if (input != null && IsInputSelected())
            {
                SelectedInput.Rules[SelectedInput.Rules.IndexOf(SelectedRule)] = input;
                SaveManager.Save();
            }
        }

        private void OnDeleteRule()
        {
            if(IsRuleSelected())
            {
                SelectedInput.Rules.Remove(_selectedRule);
                SaveManager.Save();
            }
        }

        #endregion
        #endregion
    }
}
