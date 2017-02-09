using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        BindingList<string> Log = new BindingList<string>();

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
            Log.Clear();
            lbLog.DataSource = Log;
        }

        private void GetHonorBuddyClient()
        {
            ProcessList = Process.GetProcesses().ToList();

            HonorBuddyClient = ProcessList.FirstOrDefault(x => x.ProcessName.Contains(PROC_NAME));

            if (HonorBuddyClient == null)
            {
                Log.Add("Honorbuddy client not running.");
            }
            else
            {
                Log.Add("Honorbuddy client found.");
            }

        }
    }
}
