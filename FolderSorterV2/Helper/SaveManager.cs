using FolderSorterV2.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace FolderSorterV2.Helper
{
    public static class SaveManager
    {
        private static string curretPath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.xml";

        public static ObservableCollection<Input> Load()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(SaveMe));
                System.IO.StreamReader file = new System.IO.StreamReader(curretPath);
                SaveMe overview = (SaveMe)reader.Deserialize(file);
                file.Close();
                return overview.Inputs;
            } catch
            {
                Save();
                return new SaveMe().Inputs;
            }
        }

        public static void Save()
        {
            try
            {
                var s = new SaveMe() { Inputs = MainWindow.vm.Inputs };
                var writer = new System.Xml.Serialization.XmlSerializer(typeof(SaveMe));
                var wfile = new System.IO.StreamWriter(curretPath);
                writer.Serialize(wfile, s);
                wfile.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public class SaveMe
        {
            public ObservableCollection<Input> Inputs { get; set; } = new ObservableCollection<Input>();
        }
    }
}
