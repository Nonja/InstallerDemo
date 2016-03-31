using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    interface ISetupView
    {
        string destinationDirectory { get; set; }
        string numberOfFiles { get; set; }
        string progressOfFiles { get; set; }

        void Message(string msg);
        void InstallDone();
    }
}
