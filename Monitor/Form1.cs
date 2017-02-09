using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Monitor
{
    public partial class Form1 : Form
    {
        List<Process> ProcessList = new List<Process>();
        Process HonorBuddyClient;
        const string PROC_NAME = @"Honorbuddy";
        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            InitializeLog();
            GetHonorBuddyClient();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void InitializeLog()
        {
            Logger.Log.Clear();
            lbLog.DataSource = Logger.Log;
        }

        private void GetHonorBuddyClient()
        {
            ProcessList = Process.GetProcesses().ToList();

            HonorBuddyClient = ProcessList.FirstOrDefault(x => x.ProcessName.Contains(PROC_NAME));

            if (HonorBuddyClient == null)
            {
                Logger.WriteMessage(Logger.State.BAD, "Cant find Honorbuddy client.");
                return;
            }
            else
            {
                Logger.WriteProcessDetails(HonorBuddyClient);
            }

        }
    }
}
