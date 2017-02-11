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

        public Form1()
        {
            InitializeComponent();
        }

        #region private methods
        private void InitializeLog()
        {
            Logger.Log.Clear();
            lbLog.DataSource = Logger.Log;

            lbLog.SelectedValueChanged += LbLog_SelectedValueChanged;
        }

        #endregion

        #region event handlers
        private void LbLog_SelectedValueChanged(object sender, EventArgs e)
        {
            lbLog.SelectedIndex = lbLog.Items.Count - 1;
        }

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
    }
}
