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

        #endregion

        #region event handlers
        private void btnStart_Click(object sender, EventArgs e)
        {
            InitializeLog();
            Watcher = new WatchDog(this);
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
