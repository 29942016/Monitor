using System;
using System.Diagnostics;


namespace Monitor
{
    public static class ApplicationManager
    {
        private static string _DirWarcraft = @"D:\World of Warcraft\Wow.exe",
                              _DirHonorBuddy = @"C:\Users\Lombardi\Desktop\HB\Honorbuddy.exe";

        public enum Application
        {
            Warcraft,
            HonorBuddy
        }

        public static void StartProcess(Application app)
        {
            Process newProc;

            try
            {
                switch (app)
                {
                    case Application.Warcraft:
                        newProc = Process.Start(new ProcessStartInfo(_DirWarcraft));
                        break;
                    case Application.HonorBuddy:
                        newProc = Process.Start(new ProcessStartInfo(_DirHonorBuddy));
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.WriteMessage(Logger.State.BAD, e.InnerException.ToString());
            }
        }

    }
}
