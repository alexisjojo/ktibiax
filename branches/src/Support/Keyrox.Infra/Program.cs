using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTibiaX.Infra {
    static class Program {

        [STAThread]
        static void Main() {

            var pi = new System.Diagnostics.ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + Properties.Settings.Default.CMDFileName);
            pi.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
            pi.UseShellExecute = false;
            pi.RedirectStandardInput = true;
            pi.CreateNoWindow = false;
            pi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            var Console = System.Diagnostics.Process.Start(pi);
            System.Threading.Thread.Sleep(1000);

            var trace = new Transaction.TraceListner(KTibiaX.Infra.Transaction.TraceListenerType.Console);
            trace.Console = Console;

            System.Diagnostics.Trace.Listeners.Add(trace);
            

            
            
        }

    }
}
