using FolderSorterV2.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FolderSorterV2.Lib
{
    public class FolderWatcher
    {
        public delegate void OnFolderChange(Input input);

        public UInt16 _Delay = 1;

        private string[] _LastFiles;
        private Input _Input;
        private bool _isRunning = false;
        private Thread thread;

        private OnFolderChange folderChange;

        public FolderWatcher(OnFolderChange folderChange, Input input, bool StartOnCreated)
        {
            setInput(input);
            init(folderChange);

            if (StartOnCreated)
            {
                Start();
            }
        }

        public FolderWatcher(OnFolderChange folderChange, Input input)
        {
            //calls the Initialize method
            setInput(input);
            init(folderChange);
        }

        public FolderWatcher(OnFolderChange folderChange, Input input, bool StartOnCreated, UInt16 Delay)
        {
            //Sets the Delay as the Path
            this._Delay = Delay;
            setInput(input);
            init(folderChange);
            if (StartOnCreated)
            {
                Start();
            }
        }

        public FolderWatcher(OnFolderChange folderChange, Input input, UInt16 Delay)
        {
            //Sets the Delay as the Path
            this._Delay = Delay;
            setInput(input);
            init(folderChange);
        }

        public void setInput(Input Path)
        {
            //Sets the InputFolder as the Path
            this._Input = Path;
        }

        private void init(OnFolderChange folderChange)
        {
            //Initialize the folderChange and set a function to it
            this.folderChange = new OnFolderChange(folderChange);

            //initialize a new thread pointing on Run
            
        }

        private async void Run()
        {
            //While the thread is running
            while(_isRunning)
            {
                //Checks if there are a path and a method
                if (_Input != null && this.folderChange != null)
                {
                    //Scans the folder and gets all filespaths
                    string[] currentFiles = ScanFolder();

                    //if there are no fils just copy the curret files to last files 
                    if (_LastFiles == null || currentFiles == null)
                    {
                        _LastFiles = currentFiles;
                    } else
                    {
                        //If the dont match
                        if(!currentFiles.SequenceEqual(_LastFiles))
                        {
                            //Call the function and sets the current files to the last files
                            folderChange.Invoke(input: _Input);
                            _LastFiles = currentFiles;
                        }
                    }
                }
                //Wait for secounds
                await Task.Delay(_Delay * 1000);
            }
        }

        public void Start()
        {
            //If the thread is not runing
            if(!_isRunning)
            {
                //Run it
                _isRunning = true;
                new Thread(Run).Start();
            }
        }

        public void Stop()
        {
            if(_isRunning)
            {
                _isRunning = false;
            }
        }

        //Gets alle the files in a path
        private string[] ScanFolder()
        {
            if(_Input != null)
            {
                return Directory.GetFiles(_Input.InputPath);
            }
            return null;
        }

        /*Getters*/
        public string[] GetLastFiles
        {
            get { return _LastFiles; }
        }

        public Input InputFolderPath
        {
            get { return _Input; }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }
    }
}