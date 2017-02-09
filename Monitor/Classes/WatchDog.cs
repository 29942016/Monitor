using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Monitor
{
    /// <summary>
    /// Looks after the running WoW/Honorbuddy processes, reporting information
    /// back to the logger.
    /// </summary>
    public class WatchDog
    {
        private Thread tMain;
        private Form1 mainForm;
        private Process hbProc;

        public bool IsRunning;

        public WatchDog(Process proc, Form1 @ref)
        {
            if (proc == null)
                return;

            hbProc = proc;
            IsRunning = true;
            mainForm = @ref;
            tMain = new Thread(StartWatching);
            tMain.Start();
        }

        private void StartWatching()
        {
            while (IsRunning)
            {
                Thread.Sleep(1000);
                mainForm.LogEntry(Logger.State.OK, "adsfasf");
            }

            string message = string.Format("Watchdog for {0}({1}) disposed.", hbProc.MainWindowTitle , hbProc.Id);
            mainForm.LogEntry(Logger.State.INFO, message);
        }

    }
}
