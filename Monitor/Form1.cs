using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Monitor;

namespace Monitor
{
    public partial class Form1 : Form
    {
        List<Process> ProcessList = new List<Process>();
        Process HonorBuddyClient;
        const string PROC_NAME = @"Honorbuddy";

        WatchDog Watcher;

        private enum State
        {
            Running,
            Idle,
            Error
        }

        public Form1()
        {
            InitializeComponent();
        }

        #region private methods
        private void InitializeLog()
        {
            Logger.Log.Clear();
            lbLog.DataSource = Logger.Log;
        }

        private bool GetHonorBuddyClient()
        {
            ProcessList = Process.GetProcesses().ToList();

            HonorBuddyClient = ProcessList.FirstOrDefault(x => x.ProcessName.Contains(PROC_NAME));
            lblState.Text = HonorBuddyClient == null ? State.Error.ToString() : State.Running.ToString();

            if (HonorBuddyClient == null)
            {
                Logger.WriteMessage(Logger.State.BAD, "Cant find Honorbuddy client.");
                return false;
            }
            else
            {
                Logger.WriteProcessDetails(HonorBuddyClient);
                return true;
            }
        }
        #endregion

        #region event handlers
        private void btnStart_Click(object sender, EventArgs e)
        {
            InitializeLog();
            if (!GetHonorBuddyClient())
                return;

            Watcher = new WatchDog(HonorBuddyClient, this);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Watcher.IsRunning = false;
        }
        #endregion

        #region CrossThread handling logging
        public delegate void SetLogEntry(Logger.State state, string text);
        public void LogEntry(Logger.State state, string text)
        {
            if (lbLog.InvokeRequired)
            {
                SetLogEntry del = new SetLogEntry(LogEntry);
                this.Invoke(del, new object[] { state, text });
            }
            else
            {
                Logger.WriteMessage(state, text);
            }

        }
        #endregion

    }
}
