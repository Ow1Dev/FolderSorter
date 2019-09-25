using FolderSorterV2.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FolderSorterV2.Lib
{
    public static class FolderManager
    {
        private static bool _IsRunning = false;
        private static ICollection<FolderWatcher> folderWatchers = new List<FolderWatcher>();

        public static void Run(Input[] inputs)
        {
            Debug.Log("Staring..");
            RunOnce(inputs.ToList());
            for (int i = 0; i < inputs.Length; i++)
            {
                folderWatchers.Add(new FolderWatcher(foo, inputs[i], true));
            }
            _IsRunning = true;
        }

        public static void Stop()
        {
            Debug.Log("Stop");
            foreach (FolderWatcher folderWatcher in folderWatchers)
            {
                folderWatcher.Stop();
            }
            _IsRunning = false;
            folderWatchers.Clear();
        }

        public static async Task<bool> RunOnce(ICollection<Input> inputs)
        {
            await Task.Run(() =>
            {
                foreach (Input input in inputs)
                {
                    foo(input);
                }
            });

            return true;
        }

        private static void foo(Input input)
        {
            foreach (string FilePath in Directory.GetFiles(input.InputPath))
            {
                string filename = FilePath.Substring(FilePath.LastIndexOf("\\") + 1, FilePath.Length - (FilePath.LastIndexOf("\\") + 1));

                List<Rule> p = input.Rules.OrderBy(x => x.Priority).ToList();
                p.Reverse();

                for (int i = 0; i < p.Count; i++)
                {
                    if(Regex.IsMatch(filename, p[i].Regex))
                    {
                        MoveFile(FilePath, p[i].OutputFolder, filename);
                        break;
                    }
                }
            }
        }

        private static void MoveFile(string filepath, string toPath, string filename)
        {
            if (!File.Exists(toPath + "\\" + filename))
            {
                Debug.Log($"Move {filepath} to {toPath}");
                File.Move(filepath, toPath + "\\" + filename);
            } else
            {
                Debug.Log($"{toPath + "\\" + filename} is already exist");
                File.Move(filepath, toPath + "\\" + "Copy - " + filename);
            }
        }

        public static bool IsRunning()
        {
            return _IsRunning;
        }
    }

}
