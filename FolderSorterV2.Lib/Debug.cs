using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSorterV2.Lib
{
    public static class Debug
    {
        public static void Log(string msg)
        {
#if DEBUG
            Console.WriteLine("[LOG] " + msg);
#endif
        }
    }
}
