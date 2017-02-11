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
        private Thread tWatchDog;
        private Form1 mainForm;

        private Process WoWProc;
        private TimeSpan WoWLifeSpan;

        private Process hbProc;
        private TimeSpan HBLifeSpan;

        public bool IsRunning;

        public WatchDog(Form1 @ref)
        {
            mainForm = @ref;
            if (!Initialize())
                throw new AccessViolationException();

            tWatchDog = new Thread(StartWatching);
            tWatchDog.Start();
        }

        private bool Initialize()
        {
            List<Process> ProcessList = Process.GetProcesses().ToList();
            const string PROC_NAME_HB = "Honorbuddy",
                         PROC_NAME_WOW = "Wow";

            hbProc = ProcessList.FirstOrDefault(x => x.ProcessName.Contains(PROC_NAME_HB));
            WoWProc = ProcessList.FirstOrDefault(x => x.ProcessName.Contains(PROC_NAME_WOW));

            if (hbProc == null || WoWProc == null)
            {
                Logger.WriteMessage(Logger.State.BAD, "Cant find client.");
                return false;
            }
            else
            {
                Logger.WriteProcessDetails(hbProc);
                return true;
            }
        }

        private void StartWatching()
        {
            IsRunning = true;

            while (IsRunning)
            {
                Thread.Sleep(1000);

                if(!WoWProc.HasExited)
                    WoWLifeSpan = DateTime.Now - WoWProc.StartTime;

                if(!hbProc.HasExited)
                    HBLifeSpan = DateTime.Now - hbProc.StartTime;

                LogAliveTime(new[] { WoWProc, hbProc }, new[] { WoWLifeSpan, HBLifeSpan });
            }

            string message = string.Format("Disposed Watchdog for {0}({1}).", hbProc.MainWindowTitle , hbProc.Id);
            Log(Logger.State.INFO, message);
        }
        #region wrappers
        private void Log(Logger.State state, string message)
        {
            if (mainForm == null)
                return;

            mainForm.Invoke((MethodInvoker)(() => Logger.WriteMessage(state, message)));
        }

        private void LogAliveTime(Process[] procs, TimeSpan[] lengths)
        {
            if (mainForm == null)
                return;

            mainForm.Invoke((MethodInvoker)(() => Logger.WriteAliveTime(procs, lengths)));
        }
        #endregion
    }
}
