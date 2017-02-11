using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Monitor
{
    public static class Logger
    {
        public static BindingList<string> Log = new BindingList<string>();
        private const string _LOG_FORMAT = @"[{0,4}] {1, 5}";

        public enum State
        {
            OK,
            INFO,
            BAD
        }

        public static void WriteMessage(State state, string message)
        {
            string logEntry = string.Format(_LOG_FORMAT, state, message);
            Log.Add(logEntry);
        }

        public static void WriteProcessDetails(Process proc)
        {
            WriteMessage(State.INFO, "----------");

            string attachedWowPid = proc.MainWindowTitle.Substring(proc.MainWindowTitle.Length - 4, 4);
            string hbPid = string.Format("Honorbuddy PID:{0}", proc.Id);
            string wowPid = string.Format("Attached to Wow client:{0}", attachedWowPid);

            WriteMessage(State.INFO, hbPid);
            WriteMessage(State.INFO, wowPid);


            TimeSpan aliveTime = (DateTime.Now - proc.StartTime);
            string sTime = string.Format("Running for: {0} hour(s) {1} minute(s)", aliveTime.Hours, aliveTime.Minutes);

            WriteMessage(State.INFO, sTime);
            WriteMessage(State.INFO, "----------");
        }

        public static void WriteAliveTime(Process[] procs, TimeSpan[] aliveTime)
        {
            for(int i = 0; i < procs.Length; i++)
            {
                string format = string.Format("{0, 10} running for {1} hour(s) {1} minute(s)", procs[i].ProcessName, aliveTime[i].Hours, aliveTime[i].Minutes);
                WriteMessage(State.INFO, format);
            }

        }
    }
}
